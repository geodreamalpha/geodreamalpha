using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CombatSystemComponent
{
    [System.Serializable]
    public abstract class HelperBase : CharacterBase
    {
        //Action Helpers
        protected void Move(Vector3 yAxisFacingDirection, string animationParameter = "")
        {
            Rotate(yAxisFacingDirection);

            //set direction
            Vector3 direction = new Vector3(yAxisFacingDirection.x, 0, yAxisFacingDirection.z);
            direction = direction.normalized;

            //set velocity
            animator.SetBool(animationParameter, true);

            if (animationParameter == grabWalking)
            {
                velocity.x = direction.x * gameStats.walkSpeed;
                velocity.z = direction.z * gameStats.walkSpeed;
            }               
            else if (animationParameter == grabSprinting)
            {
                velocity.x = direction.x * gameStats.sprintSpeed;
                velocity.z = direction.z * gameStats.sprintSpeed;
            }
        }
        protected void Rotate(Vector3 yAxisFacingDirection)
        {
            //set rotation
            rotation = controller.transform.rotation.eulerAngles;
            Vector3 newRotation = Quaternion.LookRotation(yAxisFacingDirection).eulerAngles;
            rotation.y = newRotation.y;
        }
        protected void Jump()
        {
            if (IsGrounded())
            {
                velocity.y = Mathf.Sqrt(gameStats.jumpHeight * 2f * -gravity.y);
                animator.SetTrigger(grabJump);
            }         
        }
        protected void Target(Transform target)
        {
            this.target = target;
        }
        protected void Melee()
        {
            animator.SetTrigger(grabMelee);
        }
        protected virtual void Projectile(string name)
        {
            if (target != null)
            {
                Rotate(faceTarget); //might need to do something different with yAxisFacingDirection
                GameObject projectile = Instantiate(assets.GetProjectileByName(name), controller.transform.position + (faceTarget.normalized + Vector3.up) * 5f, Quaternion.identity);
                ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
                projectileBehavior.speed = gameStats.projectileSpeed;
                projectileBehavior.target = target;
                projectileBehavior.sender = gameObject;
            }
        }
        protected void Retarget(Collider[] enemies)
        {         
            if (enemies.Length != 0)
            {
                target = enemies.OrderBy(c => (c.transform.position - controller.transform.position).sqrMagnitude).First().transform;
                OnDecision = OnCombatDecision;
            }
            else
            {
                target = GetDefaultTarget();
                OnDecision = OnPeacefulDecision;
            } 
        }

        //Update Helpers
        protected void ResetDecisionValues()
        {
            animator.SetBool(grabWalking, false);
            animator.SetBool(grabSprinting, false);
            velocity.x = 0f;
            velocity.z = 0f;
        }
        protected void UpdateCharacterController()
        {
            velocity.y = Mathf.Clamp(velocity.y + gravity.y * Time.deltaTime, gravity.y, float.MaxValue);
            controller.Move(velocity * Time.deltaTime);
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, Quaternion.Euler(rotation), 0.15f * 60f * Time.deltaTime);
        }
        protected void CheckIfCharacterIsGrounded()
        {
            if (controller.isGrounded)
                animator.SetBool(grabGrounded, true);
            else
                animator.SetBool(grabGrounded, false);
        }

        //Make Decision Events
        protected virtual void OnPeacefulDecision() { }
        protected virtual void OnCombatDecision() { }

        //Misc Helpers
        protected bool IsGrounded()
        {
            return animator.GetBool(grabGrounded);
        }
        protected Vector3 faceTarget
        {
            get { return target.position - controller.transform.position; }
        }
        protected Vector3 faceRightOfTarget
        {
            get { return (target.position - controller.transform.position) + Vector3.right; }
        }
        protected virtual Transform GetDefaultTarget()
        {
            return null;
        }
        protected void OnNearbyEnemies(float forwardOffset, float radius, Action<Collider[]> actionToPerform)
        {
            OnNearbyEnemies(transform, gameObject.layer, forwardOffset, radius, actionToPerform);
        }
        public static void OnNearbyEnemies(Transform allieTransform, int allieLayer,  float forwardOffset, float radius, Action<Collider[]> actionToPerform)
        {
            string layerName = allieLayer == 10 ? "Allie" : "Enemy";
            Collider[] colliders = Physics.OverlapSphere(allieTransform.position + (allieTransform.forward * forwardOffset), radius, LayerMask.GetMask(layerName));
            actionToPerform(colliders);
        }
        protected void ApplyMeleeDamageTo(Collider[] enemies)
        {
            foreach (Collider enemy in enemies)
            {
                //damage calculation
                int damageAmount = 0;
                HelperBase enemyStats = enemy.transform.root.GetComponent<HelperBase>();
                damageAmount = (int)DerivedStats.GetReductionDamage(this.gameStats.strength, enemyStats.gameStats.defense);
                damageAmount = (int)(damageAmount * UnityEngine.Random.Range(0.8f, 1.2f));

                Instantiate(assets.GetMeleeHit(), enemy.ClosestPointOnBounds(transform.position), Quaternion.identity);

                enemyStats.TakeDamage(damageAmount);
            }
        }
        public virtual void TakeDamage(float damageAmount)
        {
            health -= (int)damageAmount;

            animator.SetTrigger(grabHit);

            //show damage text
            GameObject.Find("DamageMenu").GetComponent<DamageMenuBehavior>().ShowDamage(transform, damageAmount, damageTextColor, assets);
        }
        protected virtual void MeleeContactEvent()
        {
            OnNearbyEnemies(4, 5, ApplyMeleeDamageTo);
        }

        //Asset Setter
        public void SetAssets(CombatSystemAssets assets)
        {
            this.assets = assets;
        }
    }
}