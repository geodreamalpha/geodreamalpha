using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombatSystemComponent
{
    /// <summary>
    /// this class will handle all helper functions used in describing the AI behavior of enemies.
    /// </summary>
    public class AIBase : StatsBase
    {
        //demonstration:
        bool enemyDecision = false;

        //NOTES: otherEvaluationsThatMustBeTrue examples could include things like how far away from the player the enemy is,
        //or perhaps if the enemy can see the player, or even how low the players health bar is etc. which will determin what types
        //of decisions the enemy will make and when to make those decisions.
        //demonstration:
        public bool MakeDecision(params Func<bool>[] otherEvaluationsThatMustBeTrue)
        {
            return enemyDecision && Array.TrueForAll(otherEvaluationsThatMustBeTrue, element => element());
        }
    }
}
 
