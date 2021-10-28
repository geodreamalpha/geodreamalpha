using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagerComponent
{
    //Nick Preston
    public static class DataManager
    {

        static FBHook fbh = new FBHook();

        /// <summary>
        /// Gets the "Hello from" string of this component
        /// </summary>
        /// <returns> A string that introduces this component </returns>
        public static string Hello()
        {
            return "Hello from Component DataManager";
        }

        /// <summary>
        /// Gets a world seed from Firebase with given index. Index of 0 means latest seed. 
        /// Higher values indicate previous seeds respectively.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Seed in string form</returns>
        public static string GetSeed(int index)
        {
            return "";
        }

        /// <summary>
        /// Gets the player's last known health from Firebase
        /// </summary>
        /// <returns>Player's health as an integer</returns>
        public static int GetHealth()
        {
            return 0;
        }

        /// <summary>
        /// Get's the player's last known strength value from Firebase
        /// </summary>
        /// <returns>Player's strength value as an integer</returns>
        public static int GetStrength()
        {
            return 0;
        }

        /// <summary>
        /// Get's the player's last known speed value from Firebase
        /// </summary>
        /// <returns>Player's speed value as an integer</returns>
        public static int GetSpeed()
        {
            return 0;
        }

        /// <summary>
        /// Get's the player's last saved XP value from Firebase
        /// </summary>
        /// <returns>Player's XP value as an integer</returns>
        public static int GetXP()
        {
            return 0;
        }
    }//
}//
