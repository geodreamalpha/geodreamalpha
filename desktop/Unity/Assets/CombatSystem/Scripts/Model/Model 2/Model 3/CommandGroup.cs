using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CommandGroup
{
    public List<Command> commands;
    [SerializeField]

    int commandCounter = 0;
    int maxCommands = 4;

    public bool hasCommand { get; private set; } = false;
    public bool isAtMax { get { return commandCounter >= maxCommands; } }

    public void ChooseCommand(float distance)
    {
        hasCommand = false;
        foreach (Command command in commands)
            if (command.DoesProximityContain(distance))
            {
                command.run();
                commandCounter++;
                hasCommand = true;
                break;
            }
    }

    public void ResetCounter()
    {
        commandCounter = 0;
    }

    [System.Serializable]
    public class Command
    {
        public float proximity;
        public string name;
        public Action run;

        public bool DoesProximityContain(float distance)
        {
            return distance <= proximity;
        }
    }
}