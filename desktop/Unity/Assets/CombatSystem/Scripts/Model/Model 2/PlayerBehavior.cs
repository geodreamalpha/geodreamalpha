using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombatSystemComponent
{
    public class PlayerBehavior : HelperBase
    {
        protected List<(Func<bool>, Action)> moveEvents = new List<(Func<bool>, Action)> { };

        void Start()
        {
            SetInGameStats();
        }

        void Update()
        {
            UpdateCharacterController();
            ResetBooleanAnimationParameters();
            ResetMoveVelocity();
            UpdateUserControls();
            CheckIfCharacterIsGrounded();
        }

        private void LateUpdate()
        {
            GameOverIfDead();
        }

        public void UpdateUserControls()
        {
            Transform t = Camera.main.transform;
            string animation = grabWalking;

            if (Input.GetKey(KeyCode.LeftShift))
                animation = grabSprinting;

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                Move(t.forward + t.right, animation);
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                Move(t.right + -t.up, animation);
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                Move(-t.up + -t.right, animation);
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                Move(-t.right + t.forward, animation);

            else if (Input.GetKey(KeyCode.W))
                Move(t.forward, animation);
            else if (Input.GetKey(KeyCode.S))
                Move(-t.forward, animation);
            else if (Input.GetKey(KeyCode.A))
                Move(-t.right, animation);
            else if (Input.GetKey(KeyCode.D))
                Move(t.right, animation);


            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
            if (Input.GetMouseButtonDown(0))
                Melee();
        }

        protected virtual void GameOverIfDead()
        {
            if (health <= 0)
            {
                animator.SetTrigger(grabDead);
                GameObject.Find("TerrainGenerator").GetComponent<TerrainGeneratorComponent.TerrainGenerator>().DisplayGameOver();
            }
        }
    }
}
