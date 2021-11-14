using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CommandGroup
{
    public Timer timer = new Timer(2f);
    public List<Command> commands;

    public void ChooseCommand(float distance)
    {
        timer.Update();

        if (timer.isAtMax)
        {
            timer.Reset();
            foreach (Command command in commands)
                if (command.DoesProximityContain(distance))
                {
                    command.run();
                    break;
                }
        }
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
