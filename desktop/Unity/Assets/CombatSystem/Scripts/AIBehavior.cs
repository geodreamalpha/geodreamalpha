using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

            actionPool = new Action[] { PeacefulAI, () => Move(faceTarget, grabWalking), () => Move(faceTarget, grabRunning), Attack, Projectile};        

            foreach (CommandGroup commandGroup in commandGroups)
            {
                commandGroupQueue.Enqueue(commandGroup);

                foreach (CommandGroup.Command command in commandGroup.commands)
                {
                    command.run = actionPool[(int)command.state];
                }
            }
        }

        //Update
        void Update()
        {
            UpdateGravityAndVelocity();
            UpdateRotation();
            MakeDecision();
            CheckIfCharacterIsGrounded();
        }

        //Update Helpers
        protected void MakeDecision()
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
        protected float TargetDistance()
        {
            return Vector3.Distance(controller.transform.position, target.position); ;
        }

        //Action AI
        protected void PeacefulAI()
        {
            Move(Vector3.forward, grabWalking);
        }
    }
}

