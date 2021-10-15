using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagerComponent
{
    //Nick Preston
    public class DataManager : MonoBehaviour
    {

        //Properties
        Stats PlayerStats = new PlayerStats();

        // Code to sync from Firebase here at startup
        void Start()
        {
            
        }

        // Update HUD every frame
        void Update()
        {
            
        }

        /// <summary>
        /// Gets the "Hello from" string of this component
        /// </summary>
        /// <returns> A string that introduces this component </returns>
        public string Hello()
        {
            return "Hello from Component DataManager";
        }

        /// <summary>
        /// Gets a world seed from Firebase with given index. Index of 0 means latest seed. 
        /// Higher values indicate previous seeds respectively.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Seed in string form</returns>
        public string GetSeed(int index)
        {
            return "";
        }

        /// <summary>
        /// Gets the player's last known health from Firebase
        /// </summary>
        /// <returns>Player's health as an integer</returns>
        public int GetHealth()
        {
            return 0;
        }

        /// <summary>
        /// Get's the player's last known strength value from Firebase
        /// </summary>
        /// <returns>Player's strength value as an integer</returns>
        public int GetStrength()
        {
            return 0;
        }

        /// <summary>
        /// Get's the player's last known speed value from Firebase
        /// </summary>
        /// <returns>Player's speed value as an integer</returns>
        public int GetSpeed()
        {
            return 0;
        }

        /// <summary>
        /// Get's the player's last saved XP value from Firebase
        /// </summary>
        /// <returns>Player's XP value as an integer</returns>
        public int GetXP()
        {
            return 0;
        }
    }//
}//
