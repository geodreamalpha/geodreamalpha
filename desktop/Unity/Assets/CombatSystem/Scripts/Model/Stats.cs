using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagerComponent;

namespace CombatSystemComponent
{
    public class Stats
    {
        Timer timer = new Timer(2); 
        public void Update()
        {
            timer.Update();
            if (timer.isAtMax)
            {
                timer.Reset();
                DataManager.GetCurrHP(hp =>
                {
                    health = hp;
                });

                DataManager.GetCurrSTM(stam =>
                {
                    stamina = stam;
                });

                DataManager.GetSpeed(sp =>
                {
                    speed = sp;
                });

                DataManager.GetStrength(st =>
                {
                    strength = st;
                });
                //Debug.Log("Health: " + health + "Stamina: " + stamina); 
            }
        }

        private int health = 100;
        private int stamina = 100;
        private int speed = 100;
        private int strength = 100;

        /// <summary>
        /// Get the current health value.
        /// </summary>
        /// <returns>Current health int</returns>
        public int GetHealth()
        {
            Debug.Log(health);
            return health;
        }

        /// <summary>
        /// Get the current stamina value.
        /// </summary>
        /// <returns>Current stamina int</returns>
        public int GetStamina()
        {
            Debug.Log(stamina);
            return stamina;
        }

        public int GetSpeed()
        {
            Debug.Log(speed);
            return speed;
        }


        /// <summary>
        /// Get the currect attack damage. 
        /// </summary>
        /// <returns>Current attack damage int</returns>
        public int GetStrength()
        {
            Debug.Log(strength);
            return strength;
        }
    }
}

