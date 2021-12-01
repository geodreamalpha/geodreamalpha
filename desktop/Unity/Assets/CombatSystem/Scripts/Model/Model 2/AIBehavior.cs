using System;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    //Manages the AI Decisions of characters in the game (including the companion)
    class AIBehavior : HelperBase
    {
        int arbitraryDirection = 0;

        Dictionary<string, Action> commandPool;

        Timer commandTimer = new Timer(2f);
        Timer defaultTimer = new Timer(3f);

        [SerializeField]
        List<CommandGroup> commandGroups;
        Queue<CommandGroup> commandGroupQueue = new Queue<CommandGroup>();

        //Start
        protected void Start()
        {
            InitializeSoundFX();
            InitializeInGameStats();

            commandTimer = new Timer(2 / (multiplier.speed.curve.Evaluate(levelStats.GetSpeed()) * 2));

            OnDecision = OnPeacefulDecision;

            target = GetDefaultTarget();

            #region Initialize Command Pool Dictionary
            commandPool = new Dictionary<string, Action>();
            commandPool.Add("peaceful", () => Move(GetArbitraryDirection(), grabWalking));
            commandPool.Add("none", () => CommandGroup.Command.None());
            commandPool.Add("walkTo", () => Move(faceTarget, grabWalking));
            commandPool.Add("walkFrom", () => Move(-faceTarget, grabWalking));
            commandPool.Add("walkLeft", () => Move(-faceRightOfTarget, grabWalking));
            commandPool.Add("walkRight", () => Move(faceRightOfTarget, grabWalking));
            commandPool.Add("sprintTo", () => Move(faceTarget, grabSprinting));
            commandPool.Add("sprintFrom", () => Move(-faceTarget, grabSprinting));
            commandPool.Add("sprintLeft", () => Move(-faceRightOfTarget, grabSprinting));
            commandPool.Add("sprintRight", () => Move(faceRightOfTarget, grabSprinting));
            commandPool.Add("retarget", () => OnNearbyEnemies(0, 50, Retarget));
            commandPool.Add("melee", Melee);
            commandPool.Add("fireball", () => Projectile("Fireball"));
            #endregion

            InitializeDefaultCommand();

            foreach (CommandGroup commandGroup in commandGroups)
            {
                commandGroupQueue.Enqueue(commandGroup);

                foreach (CommandGroup.Command command in commandGroup.commands)
                    command.run = commandPool[command.name];
            }
        }

        //Update
        void Update()
        {
            UpdateCharacterController();
            OnCombatDecision();
            CheckIfCharacterIsGrounded();
        }

        //Initializers
        protected virtual void InitializeDefaultCommand()
        {
            //default command for AI is peaceful command
            CommandGroup peacefulGroup = new CommandGroup();
            peacefulGroup.commands = new List<CommandGroup.Command>();
            CommandGroup.Command noCommand = new CommandGroup.Command();
            CommandGroup.Command peaceCommand = new CommandGroup.Command();

            noCommand.run = commandPool["none"];
            noCommand.proximity = 50;
            peacefulGroup.commands.Add(noCommand);
            peaceCommand.run = commandPool["peaceful"];
            peaceCommand.proximity = 250;
            peacefulGroup.commands.Add(peaceCommand);
            commandGroupQueue.Enqueue(peacefulGroup);
        }

        //Action Helpers
        protected override void Melee()
        {
            Rotate(faceTarget);
            base.Melee();
        }

        //Make Decision Events
        protected override void OnCombatDecision()
        {
            commandTimer.Update();
            if (commandTimer.isAtMax)
            {
                commandTimer.Reset();
                ResetDecisionValues();
                commandGroupQueue.Peek().ChooseCommand(TargetDistance());
            }
            if (commandGroupQueue.Peek().IsDoneOrHasNoCommandFor(TargetDistance()))
            {
                commandGroupQueue.Peek().Reset();
                commandGroupQueue.Enqueue(commandGroupQueue.Dequeue());
                commandTimer.Reset(float.MaxValue);
            }
        }
      
        //Misc Helpers
        protected virtual float TargetDistance()
        {
            return Vector3.Distance(controller.transform.position, GetDefaultTarget().position); ;
        }
        protected Vector3 GetArbitraryDirection()
        {
            arbitraryDirection = UnityEngine.Random.Range(0, 4);
            return directions[arbitraryDirection];
        }
    }
}

