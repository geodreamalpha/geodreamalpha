using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserMover;

namespace UserAnimate
{
    [RequireComponent(typeof(Animator), typeof(CharacterController))]
    public class Animate : MonoBehaviour
    {
        Animator animator;
        CharacterController controller;

        public const string idle = "idle";
        public const string grabWalking = "isWalking";
        public const string grabSprinting = "isSprinting";
        public const string grabGrounded = "isGrounded";
        public const string grabJump = "isJump";
        public const string grabMelee = "isMelee";
        public const string grabProjectile = "isProjectile";
        public const string grabBlock = "isBlock";
        public const string grabHit = "isHit";
        public const string grabDead = "isDead";

        public bool isGrounded
        {
            get { return controller.isGrounded; }
        }

        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
        }

        public void Update()
        {
            if (controller.isGrounded)
                animator.SetBool(grabGrounded, true);
            else
                animator.SetBool(grabGrounded, false);
        }

        public void ResetWalkAndSprint()
        {
            animator.SetBool(grabWalking, false);
            animator.SetBool(grabSprinting, false);
        }

        public void Walk()
        {
            animator.SetBool(grabWalking, true);
        }

        public void Sprint()
        {
            animator.SetBool(grabSprinting, true);
        }

        public void Jump()
        {
            animator.SetTrigger(grabJump);
        }

        public void UseMelee()
        {
            animator.SetTrigger(grabMelee);
        }

        public void UseProjectile()
        {
            animator.SetTrigger(grabProjectile);
        }

        public void Block()
        {
            animator.SetTrigger(grabBlock);
        }

        public void Hit()
        {
            animator.SetTrigger(grabHit);
        }

        public void Die()
        {
            animator.SetTrigger(grabDead);
        }
    }
}