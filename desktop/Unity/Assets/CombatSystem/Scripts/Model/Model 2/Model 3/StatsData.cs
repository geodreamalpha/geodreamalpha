using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagerComponent;

namespace CombatSystemComponent
{
    //basic stats for the game that are pulled from and pushed to firebase.
    //these stats are used to calculate the values of derived stats (stats used in the game)
    abstract class StatsData
    {
        protected int health = 0; //100
        protected int stamina = 0; //1
        protected int speed = 0; //100
        protected int strength = 0; //1

        public bool isAnyStatZero
        {
            get { return health == 0 || stamina == 0 || speed == 0 || strength == 0; }
        }

        public void PullPlayer()
        {
            DataManager.GetMaxHP(hp =>
            {
                health = hp;
            });

            DataManager.GetCurrSTM(stam => //max stamina
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

        public void PullCompanion()
        {
            DataManager.GetCompStrength(st =>
            {
                strength = st;
            });

            DataManager.GetCompSpeed(sp =>
            {
                speed = sp;
            });
        }

        public void PushPlayer()
        {
            DataManager.SetMaxHP(health);
            DataManager.SetCurrSTM(stamina); //max stamina
            DataManager.SetSpeed(speed);
            DataManager.SetStrength(strength);
        }

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
        /// Get the current speed value.
        /// </summary>
        /// <returns>Current speed int</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns> The formated string representation of each stat. </returns>
        public string ToString(string name = "")
        {
            return
                "\n" + name + "\n" +
                "health: " + health.ToString() + "\n" +
                "stamina: " + stamina.ToString() + "\n" +
                "strength: " + strength.ToString() + "\n" +
                "speed: " + speed.ToString() + "\n";
        }
    }
}

