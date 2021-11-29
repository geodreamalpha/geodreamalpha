using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatSystemComponent
{
    public class CompanionBehavior : AIBehavior
    {
        [SerializeField]
        protected Transform subject;
        //create timer and set to over max value
        [SerializeField]
        Image peacefulIcon;
        [SerializeField]
        Image combatIcon;

        void Update()
        {
            //-----------------------------
            //pull from firebase
            AdjustInGameStats();
            //-----------------------------
            CheckForNullTarget();
            UpdateCharacterController();     
            PlayerCommands();
            OnDecision();
            CheckIfCharacterIsGrounded();
        }

        void CheckForNullTarget()
        {
            if (target == null)
                OnDecision = OnPeacefulDecision;
        }

        void PlayerCommands()
        {
            //peaceful
            if (Input.mouseScrollDelta.y < 0)
            {
                OnDecision = OnPeacefulDecision;
                peacefulIcon.color = new Color(1f, 1f, 1f);
                combatIcon.color = new Color(0.4f, 0.4f, 0.4f);
            }
            //combat
            if (Input.mouseScrollDelta.y > 0)
            {
                OnNearbyEnemies(0, 40, Retarget);
                combatIcon.color = new Color(1f, 1f, 1f);
                peacefulIcon.color = new Color(0.4f, 0.4f, 0.4f);               
            }               
        }

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
    }
}

