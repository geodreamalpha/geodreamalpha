using System;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    [System.Serializable]
    public class AIBehavior : HelperBase
    {
        int peacefulDirection = 0;

        Dictionary<string, Action> combatPool;

        Timer commandTimer = new Timer(2f);
        Timer defaultTimer = new Timer(3f);

        [SerializeField]
        List<CommandGroup> commandGroups;
        Queue<CommandGroup> commandGroupQueue = new Queue<CommandGroup>();

        //Start
        void Start()
        {
            SetInGameStats();

            commandTimer = new Timer(2f);

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
            combatPool.Add("fireball", () => Projectile("Fireball"));
            #endregion

            foreach (CommandGroup commandGroup in commandGroups)
            {
                commandGroupQueue.Enqueue(commandGroup);

                foreach (CommandGroup.Command command in commandGroup.commands)
                    command.run = combatPool[command.name];
            }         
        }

        //Update
        void Update()
        {
            UpdateCharacterController();
            OnDecision();
            CheckIfCharacterIsGrounded();
        }

        protected override void OnCombatDecision()
        {
            commandTimer.Update();
            if (commandTimer.isAtMax)
            {
                commandTimer.Reset();
                ResetDecisionValues();              

                if (commandGroupQueue.Peek().isAtMax)
                {
                    commandGroupQueue.Peek().ResetCounter();
                    commandGroupQueue.Enqueue(commandGroupQueue.Dequeue());
                }

                commandGroupQueue.Peek().ChooseCommand(TargetDistance());

                if (!commandGroupQueue.Peek().hasCommand)
                {
                    Retarget(new Collider[] { });
                    peacefulDirection = UnityEngine.Random.Range(0, 4);
                    Move(directions[peacefulDirection], grabWalking);
                }
                    
            }  
        }

        //Make Decision Events
        protected override void OnPeacefulDecision()
        {
            defaultTimer.Update();
            if (defaultTimer.isAtMax)
            {
                defaultTimer.Reset();
                ResetDecisionValues();
                OnNearbyEnemies(0, 40, Retarget);

                if (target == null)
                {
                    peacefulDirection = UnityEngine.Random.Range(0, 4);
                    Move(directions[peacefulDirection], grabWalking);
                }
                else
                    commandGroupQueue.Peek().ChooseCommand(TargetDistance());
            }           
        }
        
        //Misc Helpers
        protected float TargetDistance()
        {
            return Vector3.Distance(controller.transform.position, target.position); ;
        }
    }
}

