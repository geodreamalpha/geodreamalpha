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

        protected new void Start()
        {
            base.Start();
            commandPool["peaceful"] = SetPeacefulBehavior;
        }

        void Update()
        {
            PullFirebaseStats();
            AdjustInGameStats();
            PlayerCommands();
            CheckForNullTarget();
            OnDecision();
            UpdateCharacterController();          
            CheckIfCharacterIsGrounded();
        }

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
            if (target == null)
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
            target = GetDefaultTarget();
            OnDecision = OnPeacefulDecision;
            peacefulIcon.color = new Color(1f, 1f, 1f);
            combatIcon.color = new Color(0.4f, 0.4f, 0.4f);
        }
        protected void SetCombatBehavior()
        {
            if (OnNearbyEnemies(0, 50, Retarget))
            {
                combatIcon.color = new Color(1f, 1f, 1f);
                peacefulIcon.color = new Color(0.4f, 0.4f, 0.4f);
            }          
        }
        protected override float TargetDistance()
        {
            return Vector3.Distance(controller.transform.position, target.position); ;
        }
        public override void TakeDamage(float damageAmount) { }
        protected override void ApplyMeleeDamageToEvent(Collider collider)
        {
            if (collider.gameObject.GetComponent<CharacterBase>().health <= 0)
            {
                CharacterBase playerBase = GetDefaultTarget().root.gameObject.GetComponent<CharacterBase>();
                playerBase.levelStats.AddHealthExp(5);
                playerBase.levelStats.AddSpeedExp(5);
                playerBase.levelStats.AddHealthExp(5);
                playerBase.levelStats.AddHealthExp(5);
            }
        }
    }
}

