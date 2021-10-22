using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystemComponent;
using System;

namespace CombatSystemComponent
{
    public abstract class ActionsManager : MonoBehaviour
    {
        protected (Func<bool>, Action) forwardEvent;
        protected (Func<bool>, Action) backEvent;
        protected (Func<bool>, Action) leftEvent;
        protected (Func<bool>, Action) rightEvent;
        protected (Func<bool>, Action) sprintEvent;
        protected (Func<bool>, Action) jumpEvent;
        protected List<(Func<bool>, Action)> attackEvents;
        protected float speed = 5;
        protected float moveLerp = 1f;
        protected float rotationLerp = 0.25f;
        Vector3 gravity;

        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected CharacterController controller;
        [SerializeField]
        protected Transform subject;

        protected Vector3 getDirectionToTarget
        {
            get { return subject.position - controller.transform.position; }
        }

        protected virtual void Start()
        {
            gravity = new Vector3(0, -9.8f, 0);

            forwardEvent = (DefaultTrigger, DefaultAction);
            backEvent = (DefaultTrigger, DefaultAction);
            leftEvent = (DefaultTrigger, DefaultAction);
            rightEvent = (DefaultTrigger, DefaultAction);
            sprintEvent = (DefaultTrigger, DefaultAction);
            jumpEvent = (DefaultTrigger, DefaultAction);
            attackEvents = new List<(Func<bool>, Action)> { };
        }

        protected virtual void Update() 
        {
            if (!TerrainGeneratorComponent.TerrainGenerator.isPaused)
            {
                ListenForEvents();

                controller.Move(gravity * Time.deltaTime);
            }
        }

        protected void ListenForEvents()
        {
            animator.SetBool("isWalking", false);

            if (forwardEvent.Item1())
                forwardEvent.Item2();

            else if (backEvent.Item1())
                backEvent.Item2();

            else if (leftEvent.Item1())
                leftEvent.Item2();    
            
            else if (rightEvent.Item1())
                rightEvent.Item2();

            else if (sprintEvent.Item1())
                sprintEvent.Item2();

            else if (jumpEvent.Item1())
                jumpEvent.Item2();
            else
                foreach ((Func<bool>, Action) attackEvent in attackEvents)
                    if (attackEvent.Item1())
                        attackEvent.Item2();
        }

        public bool IsTargetWithinRange(float minDistnce, float maxDistance = float.PositiveInfinity)
        {
            float distance = Vector3.Distance(controller.transform.position, subject.position);
            return distance > minDistnce && distance < maxDistance;
        }

        public void SetMove(Vector3 moveDirection, Vector3 yAxisFacingDirection, string animationParameter = "")
        {
            Vector3 targetDirection = Vector3.Lerp(Vector3.Normalize(moveDirection), moveDirection, moveLerp);
            controller.Move(moveDirection * speed * Time.deltaTime);

            Vector3 rotation = controller.transform.rotation.eulerAngles;
            Vector3 newRotation = Quaternion.LookRotation(yAxisFacingDirection).eulerAngles;
            rotation.y = newRotation.y;
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, Quaternion.Euler(rotation), rotationLerp);

            if (animationParameter != "")
                animator.SetBool(animationParameter, true);
        }

        bool DefaultTrigger() { return false; }

        void DefaultAction() { }
    }
}
