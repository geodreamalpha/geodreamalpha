using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//serves as the basic AI framework for all AI decisions
namespace CombatSystemComponent
{
    [System.Serializable]
    class CommandGroup
    {
        public List<Command> commands;
        int commandCounter = 0;
        int maxCommands = 4;

        public void ChooseCommand(float distance)
        {
            foreach (Command command in commands)
                if (HasComandFor(command, distance))
                {
                    command.run(); //--
                    commandCounter++;
                    break;
                }
        }

        static bool HasComandFor(Command command, float distance)
        {
            return command.DoesProximityContain(distance) && command.run != Command.None;
        }

        public bool IsDoneOrHasNoCommandFor(float distance)
        {
            return commandCounter >= maxCommands || !HasComandFor(commands[commands.Count - 1], distance);
        }

        public void Reset()
        {
            commandCounter = 0;
        }

        [System.Serializable]
        public class Command
        {
            [Range(4, 50)]
            public int proximity;
            public string name;
            public Action run;

            public bool DoesProximityContain(float distance)
            {
                return distance <= proximity;
            }

            public static void None() { }
        }
    }
}