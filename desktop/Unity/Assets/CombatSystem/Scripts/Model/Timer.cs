using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Timer(float maxTime)
    {
        this.maxTime = maxTime;
        accumulator = 0;
    }

    public void Update()
    {
        accumulator += Time.deltaTime;
    }

    public void Reset()
    {
        accumulator = 0;
    }
}
