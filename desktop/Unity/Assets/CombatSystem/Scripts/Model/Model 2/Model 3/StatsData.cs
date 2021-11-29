using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagerComponent;

namespace CombatSystemComponent
{
    public abstract class StatsData
    {
        protected int health = 100;
        protected int stamina = 1;
        protected int speed = 100;
        protected int strength = 1;

        protected int compHealth = 100;
        protected int compStrength = 1; 

        Timer timer = new Timer(2);

        //pull player
        public void PullPlayer()
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

        //pull companion
        //---
        public void PullCompanion()
        {
            timer.Update();
            if (timer.isAtMax)
            {
                timer.Reset();
                DataManager.GetCompStrength(st =>
                {
                    strength = st;
                });

                DataManager.GetCompSpeed(sp =>
                {
                    speed = sp;
                });
            }
        }

        //push player
        //---

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

        public int GetSpeed()
        {
            return speed;
        }


        /// <summary>
        /// Get the currect attack damage. 
        /// </summary>
        /// <returns>Current attack damage int</returns>
        public int GetStrength()
        {
            return strength;
        }
    }
}

