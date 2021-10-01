using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovementComponent
{
    public class Player
    {
        Stats playerStats = new Stats();

        /// <summary>
        /// Get current player stats. 
        /// </summary>
        /// <returns>Current player stats object.</returns>
        public Stats GetPlayerStats()
        {
            return playerStats;
        }
    }
}
