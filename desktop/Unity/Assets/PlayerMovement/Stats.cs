using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovementComponent
{
    public class Stats
    {
        private int health = 100;
        private int stamina = 100;
        private int attackDamage = 1;

        /// <summary>
        /// Get the current health value.
        /// </summary>
        /// <returns>Current health int</returns>
        public int GetHealth()
        {
            return health;
        }

        /// <summary>
        /// Get the current stamina value.
        /// </summary>
        /// <returns>Current stamina int</returns>
        public int GetStamina()
        {
            return stamina;
        }


        /// <summary>
        /// Get the currect attack damage. 
        /// </summary>
        /// <returns>Current attack damage int</returns>
        public int GetAttackDamage()
        {
            return attackDamage;
        }
    }
}

