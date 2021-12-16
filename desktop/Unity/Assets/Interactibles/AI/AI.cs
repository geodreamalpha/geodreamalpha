using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystemComponent;
using System;

using UserDecision;
using UserMover;
using UserAnimate;
using UserTargeting;

namespace UserAI
{
    [RequireComponent(typeof(Mover), typeof(Animate), typeof(Targeting))]
    [RequireComponent(typeof(Command))]
    public class AI : MonoBehaviour
    {
        Dictionary<string, Action> commandPool;
        Queue<Command> commands = new Queue<Command>();

        Mover mover;
        Animate animate;
        Targeting target;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            animate = GetComponent<Animate>();
            target = GetComponent<Targeting>();

            #region Initialize Command Pool Dictionary
            commandPool = new Dictionary<string, Action>();
            commandPool.Add("peaceful", () => mover.Accelerate(Vector3.right, 0, animate.Walk));
            commandPool.Add("walkTo", () => mover.Accelerate(Vector3.left, 0, animate.Walk));
            commandPool.Add("walkFrom", () => mover.Accelerate(-Vector3.left, 0, animate.Walk));
            commandPool.Add("walkLeft", () => mover.Accelerate(-Vector3.left, 0, animate.Walk));
            commandPool.Add("walkRight", () => mover.Accelerate(Vector3.left, 0, animate.Walk));
            commandPool.Add("sprintTo", () => mover.Accelerate(Vector3.left, 0, animate.Sprint));
            commandPool.Add("sprintFrom", () => mover.Accelerate(-Vector3.left, 0, animate.Sprint));
            commandPool.Add("sprintLeft", () => mover.Accelerate(-Vector3.left, 0, animate.Sprint));
            commandPool.Add("sprintRight", () => mover.Accelerate(Vector3.left, 0, animate.Sprint));
            //commandPool.Add("retarget", () => OnNearbyEnemies(0, 50, () => { }));
            commandPool.Add("melee", () => animate.UseMelee());
            //commandPool.Add("fireball", () => Projectile("Fireball"));
            #endregion

            Command[] decisionComponents = GetComponents<Command>();

            foreach (Command decision in decisionComponents)
            {
                decision.Set(commandPool[decision.getName]);
                commands.Enqueue(decision);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float distance = target.GetTargetDistance();

            if (commands.Peek().IsDoneOrHasNoDecisionFor(distance))
            {
                commands.Enqueue(commands.Dequeue());
                commands.Peek().Initialize();
            }
            commands.Peek().Run(mover.ResetVelocity, animate.ResetWalkAndSprint);
            animate.Walk();
            mover.Move();
        }
    }
}