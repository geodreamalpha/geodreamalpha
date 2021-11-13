using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CombatSystemComponent
{
    [System.Serializable]
    public class AIBehavior : HelperBase
    {
        Action[] actionPool;

        Timer timer = new Timer(5f);

        [SerializeField]
        List<CommandGroup> commandGroups;
        Queue<CommandGroup> commandGroupQueue = new Queue<CommandGroup>();        

        //Start
        void Start()
        {
            SetInGameStats();

            timer = new Timer(5f);

            actionPool = new Action[] { OnPeacefulDecision, () => Move(faceTarget, grabWalking), () => Move(faceTarget, grabSprint), () => Retarget(50), Attack, 
                () => Projectile("FireballProjectile") };

            foreach (CommandGroup commandGroup in commandGroups)
            {
                commandGroupQueue.Enqueue(commandGroup);

                foreach (CommandGroup.Command command in commandGroup.commands)
                {
                    command.run = actionPool[(int)command.state];
                }
            }

            OnDecision = OnPeacefulDecision;
        }

        //Update
        void Update()
        {
            UpdateGravityAndVelocity();
            UpdateRotation();
            Retarget(40);
            OnDecision();
            CheckIfCharacterIsGrounded();
        }

        //Make Decision Events
        protected override void OnPeacefulDecision()
        {
            Move(Vector3.forward, grabWalking);
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

