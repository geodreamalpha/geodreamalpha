using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class CompanionBehavior : AIBehavior
    {
        [SerializeField]
        protected Transform subject;

        void Update()
        {
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
            if (Input.GetKeyDown(KeyCode.T))
                OnNearbyEnemies(0, 40, Retarget);
            else if (Input.GetKeyDown(KeyCode.R))
                OnDecision = OnPeacefulDecision;
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
    }
}

