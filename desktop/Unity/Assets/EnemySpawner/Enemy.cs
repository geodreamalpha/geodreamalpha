using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystemComponent;

public class Enemy : MonoBehaviour
{
    Stats stats;
    List<string> abilities;
    GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns the stats of the enemy
    public Stats GetStats()
    {
        return stats;
    }

    //returns the position of the enemy
    public Vector3 GetPosition()
    {
        return Vector3.zero;
    }

    //gets the current ability that the enemy is using
    public string GetCurrentAbility()
    {
        return "ability"; 
    }
}//
