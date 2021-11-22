using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombatSystemComponent
{
    public class PlayerBehavior : HelperBase
    {
        void Start()
        {
            SetInGameStats();
        }

        void Update()
        {
            CancelTargetIfOutOfRange(50);
            SlowlyRegainHealthAndStamina();
            UpdateCharacterController();
            ResetDecisionValues();
            UpdateUserControls();
            CheckIfCharacterIsGrounded();
        }

        private void LateUpdate()
        {
            GameOverIfDead();
        }

        //Action Helpers
        protected override void Projectile(string name)
        {
            if (stamina > 10)
            {
                base.Projectile(name);
                DecreaseStaminaBy(10);
            }
        }
        protected void SetSprint(ref string animation)
        {
            if (stamina > 0)
            {
                animation = grabSprinting;
                DecreaseStaminaBy(10 * Time.deltaTime);
            }
        }

        //Update Helpers
        protected void SlowlyRegainHealthAndStamina()
        {
            health = Mathf.Clamp(health + (gameStats.healthPoints * 0.01f * Time.deltaTime), 0, gameStats.healthPoints);

            if (!Input.GetKey(KeyCode.LeftShift))
                stamina = Mathf.Clamp(stamina + (gameStats.staminaPoints * 0.02f * Time.deltaTime), 0, gameStats.staminaPoints);
        }
        protected void UpdateUserControls()
        {
            Transform c = Camera.main.transform; //target;
            string animation = grabWalking;

            if (Input.GetKey(KeyCode.LeftShift))
                SetSprint(ref animation);

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                Move(c.forward + c.right, animation);
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                Move(c.right + -c.forward, animation);
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                Move(-c.forward + -c.right, animation);
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                Move(-c.right + c.forward, animation);

            else if (Input.GetKey(KeyCode.W))
                Move(c.forward, animation);
            else if (Input.GetKey(KeyCode.S))
                Move(-c.forward, animation);
            else if (Input.GetKey(KeyCode.A))
                Move(-c.right, animation);
            else if (Input.GetKey(KeyCode.D))
                Move(c.right, animation);

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (Input.GetMouseButtonDown(0))
                Melee();
            if (Input.GetMouseButtonDown(1))
                SetTarget();
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Projectile("Fireball");
        }
        protected virtual void GameOverIfDead()
        {
            if (health <= 0)
            {
                animator.SetTrigger(grabDead);
                GameObject.Find("TerrainGenerator").GetComponent<TerrainGeneratorComponent.TerrainGenerator>().DisplayGameOver();
            }
        }

        //Misc Helpers
        protected override void MeleeContactEvent()
        {
            base.MeleeContactEvent();
            levelStats.AddStrengthExp();
        }
        public override void TakeDamage(float damageAmount)
        {
            base.TakeDamage(damageAmount);
            levelStats.AddHealthExp();
        }
        public void DecreaseStaminaBy(float amount)
        {
            stamina = Mathf.Clamp(stamina - amount, 0, gameStats.staminaPoints);
        }
        public void CancelTargetIfOutOfRange(float distance)
        {
            if (target != null && faceTarget.magnitude > distance)
                CancelTarget();
        }
        public void CancelTarget()
        {
            target = Camera.main.transform;
        }
        public void SetTarget()
        {
            OnNearbyEnemies(0, 50, Retarget);
        }
    }
}
