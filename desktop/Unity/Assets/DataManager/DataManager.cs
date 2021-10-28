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


        //Callback for stats
        public delegate void GetStatCallback(int stat);

        /// <summary>
        /// Gets the player's last known current health from Firebase
        /// </summary>
        public static void GetCurrHP(GetStatCallback callback)
        {
            fbh.GetStat("currHP", val=>{callback(val);});
        }

        /// <summary>
        /// Gets the player's last known max health from Firebase
        /// </summary>
        public static void GetMaxHP(GetStatCallback callback)
        {
            fbh.GetStat("maxHP", val=>{callback(val);});
        }

        /// <summary>
        /// Gets the player's last known current stamina from Firebase
        /// </summary>
        public static void GetCurrSTM(GetStatCallback callback)
        {
            fbh.GetStat("currSTM", val=>{callback(val);});
        }

        /// <summary>
        /// Gets the player's last known max stamina from Firebase
        /// </summary>
        public static void GetMaxSTM(GetStatCallback callback)
        {
            fbh.GetStat("maxSTM", val=>{callback(val);});
        }

        /// <summary>
        /// Get's the player's last known strength value from Firebase
        /// </summary>
        public static void GetStrength(GetStatCallback callback)
        {
            fbh.GetStat("strength", val=>{callback(val);});
        }

        /// <summary>
        /// Get's the player's last known speed value from Firebase
        /// </summary>
        public static void GetSpeed(GetStatCallback callback)
        {
            fbh.GetStat("speed", val=>{callback(val);});
        }

        /// <summary>
        /// Get's the player's last saved XP value from Firebase
        /// </summary>
        public static void GetXP(GetStatCallback callback)
        {
            fbh.GetStat("xp", val=>{callback(val);});
        }
    }//
}//
