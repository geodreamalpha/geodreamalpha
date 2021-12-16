using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//serves as the basic AI framework for all AI decisions
namespace UserDecision
{
    [System.Serializable]
    class Command : MonoBehaviour
    {
        public new string name = "peaceful";
        [Range(0, 100)]
        public int proximity = 40;
        [Range(0.1f, 3)]
        public float seconds = 0.5f;
        [Range(1, 10)]
        public int repeat = 4;
        Action run;
        int counter = 0;
        float elapsed = 0f;

        public string getName
        {
            get { return name; }
        }

        public void Initialize()
        {
            elapsed = 0;
            counter = 0;
        }

        public void Set(Action action)
        {
            run = action;
        }

        public void Run(params Action[] resetEvents)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= seconds)
            {
                elapsed = 0;
                foreach (Action resetEvent in resetEvents)
                    resetEvent();
                run();
                counter++;        
            }
        }

        public bool IsDoneOrHasNoDecisionFor(float distance)
        {
            return counter >= repeat || distance > proximity;
        }
    }
}
