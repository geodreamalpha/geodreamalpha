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

        /// <summary>
        /// the global speed that the character will move
        /// </summary>
        protected float speed = 5;
        /// <summary>
        /// the speed at which the character rotates from one position
        /// </summary>
        protected float rotationLerp = 0.25f;
        /// <summary>
        /// the force of gravity per second that pulls down the character.
        /// to pull the character down, specify a negative value for the y parameter
        /// </summary>
        protected Vector3 gravity;

        #region Declare Inpsector Fields
        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected CharacterController controller;
        [SerializeField]
        protected Transform target;
        #endregion

        /// <summary>
        /// returns the Vector3 direction from the character to the target
        /// </summary>
        protected Vector3 getDirectionToTarget
        {
            get { return target.position - controller.transform.position; }
        }

        protected virtual void Start()
        {
            #region Initialize Gravity
            //will pull character down at a rate of gravity meters/ second.
            //(0, -9.8, 0) is the rate of gravity on Earth
            gravity = new Vector3(0, -9.8f, 0);
            #endregion

            #region Initialize Character Events
            //these events control almost everything needed: movement, rotation, direction, attacks, jumps, etc.
            //the trigger (first parameter) stores a function that takes no parameters and returns a bool.  It describes what triggers the event.
            //the action (second parameter) stores a method that takes no parameters.  It describes what happens during the event.
            forwardEvent = (DefaultTrigger, DefaultAction);
            backEvent = (DefaultTrigger, DefaultAction);
            leftEvent = (DefaultTrigger, DefaultAction);
            rightEvent = (DefaultTrigger, DefaultAction);
            sprintEvent = (DefaultTrigger, DefaultAction);
            jumpEvent = (DefaultTrigger, DefaultAction);
            attackEvents = new List<(Func<bool>, Action)> { };
            #endregion
        }

        protected virtual void Update() 
        {
            if (!TerrainGeneratorComponent.TerrainGenerator.isPaused)
            {
                ListenForEvents();
                //pulls gameObject down at a rate of gravity meters/ second.
                controller.Move(gravity * Time.deltaTime);
            }
        }

        /// <summary>
        /// call this in Unity's update method.
        /// checks if an event trigger has returned true.  If true, it will call the event action
        /// </summary>
        protected void ListenForEvents()
        {
            #region Reset Boolean Animation Parameters to False
            animator.SetBool("isWalking", false);
            #endregion

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

        /// <summary>
        /// returns a boolean value indicating whether the target's distance from this gameObject is within the specified range (minDistance, maxDistance).
        /// </summary>
        /// <param name="minDistnce">the minimum distance of the target from this gameObject (exclusive)</param>
        /// <param name="maxDistance">the maximum distance of the target from this gameObject (exclusive)</param>
        /// <returns></returns>
        public bool IsTargetWithinRange(float minDistnce, float maxDistance = float.PositiveInfinity)
        {
            //this function calculates the euclidian distance between character and target (the length of distance between the two)
            //it returns a boolean value indicating whether that distance is within the specified range (minDistance, maxDistance).
            float distance = Vector3.Distance(controller.transform.position, target.position);
            return distance > minDistnce && distance < maxDistance;
        }

        /// <summary>
        /// executes a variety of rotation, movement, and animation behaviors for the character.
        /// </summary>
        /// <param name="yAxisFacingDirection">the rotation the character will orient to.  only the y-axis rotation is set</param>
        /// <param name="moveDirection">the direction the character will move to</param>
        /// <param name="animationParameter">the animation parameter name that will trigger animation(s)</param>
        public void SetMove(Vector3 yAxisFacingDirection, Vector3 moveDirection, string animationParameter = "")
        {
            #region Update Character's Rotation to yAxisFacingDirection
            //unity stores rotations as quaternions because it causes less artifacts (quaternions are complicated)
            //but you can use their conversion methods to convert quaternion rotations into ino euler angles (360 degrees) or radians (2PI)
            //Vector3 types can either be used as points, rotations, OR directions in the gameworld.
            //basically, this updates the character's rotation to yAxisFacingDirection (the character faces that direction).
            //it only updates the y-axis rotation, because other rotations would look weird (we don't need them).
            //the Lerp() function is used to smoothly rotate character from previous rotation to new rotation (it happens over multiple frames)
            //the speed of this rotation transition is determined by rotationLerp.
            Vector3 rotation = controller.transform.rotation.eulerAngles;
            Vector3 newRotation = Quaternion.LookRotation(yAxisFacingDirection).eulerAngles;
            rotation.y = newRotation.y;
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, Quaternion.Euler(rotation), rotationLerp);
            #endregion

            #region Move Character in moveDirection At A Rate of Speed/Second.
            controller.Move(moveDirection * speed * Time.deltaTime);
            #endregion

            #region if animationParameter is not an empty string, Update Animation parameter to trigger the specified animation
            //it plays an animation.  The animation is optional by entering "" for the animationParameter or leaving the animationParameter out of the function call.
            if (animationParameter != "")
                animator.SetBool(animationParameter, true);
            #endregion
        }

        //DefaultTrigger() is assigned to events as a default state, thus preventing null reference exceptions.
        //
        bool DefaultTrigger() { return false; }

        //DefaultAction() is assigned to events as a default state, thus preventing null reference exceptions.
        //
        void DefaultAction() { }
    }
}