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

            //walk
            moveEvents.Add((() => Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift),
                              () => Move(Camera.main.transform.forward, grabWalking)));

            moveEvents.Add((() => Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift),
                              () => Move(-Camera.main.transform.forward, grabWalking)));

            moveEvents.Add((() => Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift),
                              () => Move(-Camera.main.transform.right, grabWalking)));

            moveEvents.Add((() => Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift),
                              () => Move(Camera.main.transform.right, grabWalking)));


            //run
            moveEvents.Add((() => Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift),
                              () => Move(Camera.main.transform.forward, grabSprinting)));

            moveEvents.Add((() => Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift),
                              () => Move(-Camera.main.transform.forward, grabSprinting)));

            moveEvents.Add((() => Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift),
                              () => Move(-Camera.main.transform.right, grabSprinting)));

            moveEvents.Add((() => Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift),
                              () => Move(Camera.main.transform.right, grabSprinting)));

            //attack
            moveEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger(grabMelee)));

            //jump
            moveEvents.Add((() => Input.GetKeyDown(KeyCode.Space),
                              () => Jump()));

            //events.Add((() => Input.GetKey(KeyCode.LeftShift),
            //() => Sprint()));
        }

        void Update()
        {
            UpdateCharacterController();
            ResetBooleanAnimationParameters();
            ResetMoveVelocity();

            foreach ((Func<bool>, Action) eventElement in moveEvents)
                if (eventElement.Item1())
                    eventElement.Item2();

            CheckIfCharacterIsGrounded();
        }
    }
}
