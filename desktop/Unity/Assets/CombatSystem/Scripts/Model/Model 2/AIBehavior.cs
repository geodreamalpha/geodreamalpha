using System;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    [System.Serializable]
    public class AIBehavior : HelperBase
    {
        int peacefulDirection = 0;

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

            OnDecision = OnPeacefulDecision;

            target = GetDefaultTarget();

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
            CheckIfCharacterIsGrounded();
        }

        //Make Decision Events
        protected override void OnPeacefulDecision()
        {
            timer.Update();
            if (timer.isAtMax)
            {
                timer.Reset();
                ResetDecisionValues();
                peacefulDirection = UnityEngine.Random.Range(0, 4);
            }
            
            Move(directions[peacefulDirection], grabWalking);
        }
        protected override void OnCombatDecision()
        {
            timer.Update();
            if (timer.isAtMax)
            {
                timer.Reset();
                ResetDecisionValues();
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

