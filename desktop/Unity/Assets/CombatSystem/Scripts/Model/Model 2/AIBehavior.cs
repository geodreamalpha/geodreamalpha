using System;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    [System.Serializable]
    public class AIBehavior : HelperBase
    {
        [SerializeField]
        string peacefulName;
        Dictionary<string, Action> peacefulPool;
        Dictionary<string, Action> combatPool;

        Timer timer = new Timer(5f);

        [SerializeField]
        List<CommandGroup> commandGroups;
        Queue<CommandGroup> commandGroupQueue = new Queue<CommandGroup>();

        //Start
        void Start()
        {
            SetInGameStats();

            timer = new Timer(5f);

            #region Peaceful Pool Dictionary
            peacefulPool = new Dictionary<string, Action>();
            peacefulPool.Add("peacefulWalk", PeacefulWalk);
            peacefulPool.Add("peacefulFollow", PeacefulFollow);
            #endregion

            OnPeacefulDecision = peacefulPool[peacefulName];
            OnDecision = OnPeacefulDecision;

            #region Combat Pool Dictionary
            combatPool = new Dictionary<string, Action>();
            combatPool.Add("walkTo", () => Move(faceTarget, grabWalking));
            combatPool.Add("walkFrom", () => Move(-faceTarget, grabWalking));
            combatPool.Add("walkLeft", () => Move(-faceRightOfTarget, grabWalking));
            combatPool.Add("walkRight", () => Move(faceRightOfTarget, grabWalking));
            combatPool.Add("sprintTo", () => Move(faceTarget, grabSprinting));
            combatPool.Add("sprintFrom", () => Move(-faceTarget, grabSprinting));
            combatPool.Add("sprintLeft", () => Move(-faceRightOfTarget, grabSprinting));
            combatPool.Add("sprintRight", () => Move(faceRightOfTarget, grabSprinting));
            combatPool.Add("retarget", () => OnNearbyEnemies(0, 50, Retarget));
            combatPool.Add("melee", Melee);
            combatPool.Add("fireball", () => Projectile("FireballProjectile"));
            #endregion

            foreach (CommandGroup commandGroup in commandGroups)
            {
                commandGroupQueue.Enqueue(commandGroup);

                foreach (CommandGroup.Command command in commandGroup.commands)
                {
                    command.run = combatPool[command.name];
                }
            }         
        }

        //Update
        void Update()
        {
            UpdateCharacterController();
            OnNearbyEnemies(0, 40, Retarget);
            OnDecision();
            //-------
            //OnNearbyEnemies(0, 10, ApplyMeleeDamageTo);
            //------
            CheckIfCharacterIsGrounded();
        }

        private void LateUpdate()
        {
            DestroyIfDead();
        }

        //Make Decision Events
        protected void PeacefulWalk()
        {
            Move(Vector3.forward, grabWalking);
        }
        protected void PeacefulFollow()
        {
            ResetBooleanAnimationParameters();
            ResetMoveVelocity();

            float distance = faceTarget.magnitude;
            if (distance > 20)
                controller.Move(-controller.transform.position + target.position + new Vector3());
            else if (distance > 3)
                Move(faceTarget, grabSprinting);
            else if (distance > 2)
                Move(faceTarget, grabWalking);
        }
        protected override void OnCombatDecision()
        {
            timer.Update();
            if (timer.isAtMax)
            {
                timer.Reset();
                ResetBooleanAnimationParameters();
                ResetMoveVelocity();
                commandGroupQueue.Enqueue(commandGroupQueue.Dequeue());
            }
            commandGroupQueue.Peek().ChooseCommand(TargetDistance());
        }

        protected virtual void DestroyIfDead()
        {
            //check if object is dead
            if (health <= 0)
            {
                animator.SetTrigger(grabDead);
                this.gameObject.layer = 2;
                CombatSystem.ProperDestroy(this.gameObject, 5);
            }
        }

        //Misc Helpers
        protected override Transform GetDefaultTarget()
        {
            return GameObject.Find("Player").transform;
        }
        protected float TargetDistance()
        {
            return Vector3.Distance(controller.transform.position, target.position); ;
        }
    }
}

