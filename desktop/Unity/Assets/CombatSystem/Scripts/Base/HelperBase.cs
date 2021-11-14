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
        public void Move(Vector3 yAxisFacingDirection, string animationParameter = "")
        {
            Rotate(yAxisFacingDirection);

            //set direction
            Vector3 direction = new Vector3(yAxisFacingDirection.x, 0, yAxisFacingDirection.z);
            direction = direction.normalized;

            //set velocity
            if (controller.isGrounded && animationParameter != "")
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
        public void Rotate(Vector3 yAxisFacingDirection)
        {
            //set rotation
            rotation = controller.transform.rotation.eulerAngles;
            Vector3 newRotation = Quaternion.LookRotation(yAxisFacingDirection).eulerAngles;
            rotation.y = newRotation.y;
        }
        public void Jump()
        {
            if (controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(gameStats.jumpHeight * 2f * -gravity.y);
                animator.SetTrigger(grabJump);
            }
        }
        public void Sprint()
        {
            if (controller.isGrounded)
            {
                animator.SetTrigger(grabSprinting); 
            }
        }
        public void Target(Transform target)
        {
            this.target = target;
        }
        public void Melee()
        {
            animator.SetTrigger(grabMelee);
        }
        public void Projectile(string name)
        {
            Rotate(faceTarget); //might need to do something different with yAxisFacingDirection
            GameObject projectile = Instantiate(assets.getProjectileByName(name), controller.transform.position + (faceTarget.normalized + Vector3.up) * 5f, Quaternion.identity);
            ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
            projectileBehavior.target = target;
            projectileBehavior.sender = gameObject;
            TagGroup thisTags = GetComponent<TagGroup>();
            TagGroup projectileTags = projectile.GetComponent<TagGroup>();
            if (projectileTags.identifiers.Contains("redProjectile"))
                projectileTags.identifiers.AddRange(thisTags.identifiers);
        }
        public void Retarget(float distance)
        {
            //might need to make thisTags component more easily accessible.
            TagGroup thisTags = this.transform.root.gameObject.GetComponent<TagGroup>();

            int bitLayer = 1 << 10;
            Collider[] colliders = Physics.OverlapSphere(controller.transform.position, distance, bitLayer);
            List<Transform> validTargets = new List<Transform>();

            foreach (Collider collider in colliders)
            {  
                TagGroup otherTags = collider.transform.root.gameObject.GetComponent<TagGroup>();
                if (thisTags.IsHarmedBy(otherTags.identifiers))
                    validTargets.Add(collider.transform.root);
            }
            
            if (validTargets.Count != 0)
            {
                target = validTargets.OrderBy(c => (c.transform.position - controller.transform.position).sqrMagnitude).First();
                OnDecision = OnCombatDecision;
            }                
            else
            {
                target = GetDefaultTarget();
                OnDecision = OnPeacefulDecision;
            }
                
        }

        //Update Helpers
        protected void ResetBooleanAnimationParameters()
        {

            animator.SetBool(grabWalking, false);
            animator.SetBool(grabSprinting, false);
        }
        protected void UpdateGravityAndVelocity()
        {
            velocity.y = Mathf.Clamp(velocity.y + gravity.y * Time.deltaTime, gravity.y, float.MaxValue);
            controller.Move(velocity * Time.deltaTime);
        }
        protected void UpdateRotation()
        {
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, Quaternion.Euler(rotation), 0.15f);
        }
        protected void ResetMoveVelocity()
        {
            velocity.x = 0f;
            velocity.z = 0f;
        }
        protected void CheckIfCharacterIsGrounded()
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
                animator.SetBool(grabGrounded, true);
            else
                animator.SetBool(grabGrounded, false);
        }

        //Make Decision Events
        protected Action OnPeacefulDecision = () => { };
        protected virtual void OnCombatDecision()
        {        
        }

        //Misc Helpers
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

        //Collision Damage Logic
        void OnTriggerEnter(Collider other)
        {
            //other.transform.root.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Melee")
            TagGroup thisTags = this.transform.root.gameObject.GetComponent<TagGroup>();
            TagGroup otherTags = other.transform.root.gameObject.GetComponent<TagGroup>();

            if (health > 0 && thisTags.IsHarmedBy(otherTags.identifiers))
            {
                //damage calculation
                int damage = 0;
                if (otherTags.identifiers.Contains("redProjectile"))
                {
                    CharacterBase otherStats = other.GetComponent<ProjectileBehavior>().sender.GetComponent<CharacterBase>();
                    damage = (int)(otherStats.gameStats.energy * 2 - otherStats.gameStats.aura);
                }
                else
                {
                    CharacterBase otherStats = other.transform.root.GetComponent<CharacterBase>();
                    damage = (int)(otherStats.gameStats.strength * 2 - otherStats.gameStats.defense);
                }
                damage += UnityEngine.Random.Range(-1, 2);
                health -= damage;

                //check if object is dead
                if (health <= 0)
                {
                    animator.SetTrigger(grabDead);
                    gameObject.AddComponent<ProjectileBehavior>();
                    ProjectileBehavior behavior = gameObject.GetComponent<ProjectileBehavior>();
                    behavior.lifetime = new Timer(2f);
                }
                else
                    animator.SetTrigger(grabHit);

                //show damage text
                GameObject display = GameObject.Find("DamageMenu");
                GameObject textObject = Instantiate(assets.getDamageText(), display.transform, false);

                TMPro.TMP_Text text = textObject.GetComponent<TMPro.TMP_Text>();
                text.text = damage.ToString();
                text.color = damageTextColor;

                Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-150, 150), UnityEngine.Random.Range(-150, 151), 0);
                textObject.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(controller.transform.position) + randomOffset;
            }    
        }

        //Asset Setter
        public void SetAssets(CombatSystemAssets assets)
        {
            this.assets = assets;
        }
    }
}