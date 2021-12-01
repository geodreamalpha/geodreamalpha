using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystemComponent
{
    //Accumulates time based on the time that elapses since the last frame.  Can be reset when max value is reached
    [System.Serializable]
    public struct Timer
    {
        [SerializeField]
        float maxTime;
        float accumulator;

        public bool isAtMax
        {
            get { return accumulator >= maxTime; }
        }

        public float getMaxTime
        {
            get { return maxTime; }
        }

        public float getAccumulator
        {
            get { return accumulator; }
        }

        public Timer(float maxTime, float startTime = 0)
        {
            this.maxTime = maxTime;
            accumulator = startTime;
        }

        public void Update()
        {
            accumulator += Time.deltaTime;
        }

        public void Reset(float value = 0)
        {
            accumulator = value;
        }
    }
}