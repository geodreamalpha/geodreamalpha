using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombatSystemComponent
{
    //Manages the player behavior
    class PlayerBehavior : HelperBase
    {
        [SerializeField]
        GameObject LockOn;
        bool melee = true;

        Timer speedLevelIncreaseTimer = new Timer(0.5f);
        Timer pushTimer = new Timer(5f, 6f);

        void Start()
        {
            levelStats.PullPlayer();
            InitializeSoundFX();
            InitializeInGameStats();
        }

        void Update()
        {
            PushFirebaseStats();
            AdjustInGameStats();
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
                GameObject projectile = Instantiate(assets.GetProjectileByName(name), Camera.main.transform.position, Quaternion.identity);
                ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
                projectileBehavior.sender = gameObject;
                projectileBehavior.speed = gameStats.projectileSpeed;
                projectileBehavior.direction = Camera.main.ScreenPointToRay(LockOn.GetComponent<RectTransform>().position).direction;
                projectileBehavior.damageHitEvent = () => levelStats.AddStaminaExp();
                DecreaseStaminaBy(10);
            }
        }
        protected void SetSprint(ref string animation)
        {
            if (stamina > 5)
            {
                animation = grabSprinting;
                DecreaseStaminaBy(5 * Time.deltaTime);

                speedLevelIncreaseTimer.Update();
                if (speedLevelIncreaseTimer.isAtMax)
                {
                    speedLevelIncreaseTimer.Reset();
                    levelStats.AddSpeedExp();
                }                   
            }
        }

        //Update Helpers
        void PushFirebaseStats()
        {
            levelStats.ToString();
            pushTimer.Update();
            if (pushTimer.isAtMax && !levelStats.isAnyStatZero)
            {              
                levelStats.ToString();
                pushTimer.Reset();
                levelStats.PushPlayer();
                
            }
        }
        protected void SlowlyRegainHealthAndStamina()
        {
            health = Mathf.Clamp(health + (gameStats.healthPoints * 0.01f * Time.deltaTime), 0, gameStats.healthPoints);

            if (!Input.GetKey(KeyCode.LeftShift))
                stamina = Mathf.Clamp(stamina + (gameStats.staminaPoints * 0.02f * Time.deltaTime), 0, gameStats.staminaPoints);
        }
        protected void UpdateUserControls()
        {
            Vector3 lockOnScreenPosition = LockOn.GetComponent<RectTransform>().position;
            lockOnScreenPosition.y = Mathf.Clamp(lockOnScreenPosition.y + (Input.GetAxis("Mouse Y") * 0.5f), 0, Screen.height); //* Time.deltaTime;
            LockOn.GetComponent<RectTransform>().position = lockOnScreenPosition;

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
                Attack();
            if (Input.GetMouseButtonDown(1))
                ToggleAttackType();
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
        protected void DecreaseStaminaBy(float amount)
        {
            stamina = Mathf.Clamp(stamina - amount, 0, gameStats.staminaPoints);
        }
        protected void Attack()
        {
            if (melee)
                Melee();
            else
                Projectile("Fireball");
        }
        protected void ToggleAttackType()
        {
            melee = !melee;
            LockOn.SetActive(!LockOn.activeSelf);
        }
    }
}
