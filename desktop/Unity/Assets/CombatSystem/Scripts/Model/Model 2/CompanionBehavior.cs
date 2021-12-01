using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatSystemComponent
{
    //Manages companion behavior in the game
    class CompanionBehavior : AIBehavior
    {
        [SerializeField]
        protected Transform subject;
        //create timer and set to over max value
        [SerializeField]
        Image peacefulIcon;
        [SerializeField]
        Image combatIcon;

        Timer pullTimer = new Timer(5f, 6f);

        void Update()
        {
            PullFirebaseStats();
            AdjustInGameStats();
            CheckForNullTarget();
            UpdateCharacterController();     
            PlayerCommands();
            OnDecision();
            CheckIfCharacterIsGrounded();
        }

        //Initializers
        protected override void InitializeDefaultCommand() { }

        //Update Helpers
        void PullFirebaseStats()
        {
            pullTimer.Update();
            if (pullTimer.isAtMax)
            {
                pullTimer.Reset();
                levelStats.PullCompanion();
            }
        }
        void CheckForNullTarget()
        {
            if (target == GetDefaultTarget())
                SetPeacefulBehavior();
        }
        void PlayerCommands()
        {
            //peaceful
            if (Input.mouseScrollDelta.y < 0)
                SetPeacefulBehavior();
            //combat
            if (Input.mouseScrollDelta.y > 0)
                SetCombatBehavior();               
        }

        //Make Decision Events
        protected override void OnPeacefulDecision()
        {
            ResetDecisionValues();
            Vector3 faceSubject = subject.position - transform.position;

            float distance = faceSubject.magnitude;
            if (distance > 50)
                controller.Move(-controller.transform.position + subject.position + new Vector3(0, 1, 10));
            else if (distance > 3)
                Move(faceSubject, grabSprinting);
            else if (distance > 2)
                Move(faceSubject, grabWalking);
        }

        //Misc Helpers
        protected override Transform GetDefaultTarget()
        {
            return GameObject.Find("Player").transform;
        }
        protected void SetPeacefulBehavior()
        {
            OnDecision = OnPeacefulDecision;
            peacefulIcon.color = new Color(1f, 1f, 1f);
            combatIcon.color = new Color(0.4f, 0.4f, 0.4f);
        }
        protected void SetCombatBehavior()
        {
            OnNearbyEnemies(0, 40, Retarget);
            combatIcon.color = new Color(1f, 1f, 1f);
            peacefulIcon.color = new Color(0.4f, 0.4f, 0.4f);
        }
        protected override float TargetDistance()
        {
            return Vector3.Distance(controller.transform.position, target.position); ;
        }
    }
}

