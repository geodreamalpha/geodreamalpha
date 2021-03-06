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

        public delegate void GetSeedCallback(string seed);
        /// <summary>
        /// Gets a world seed from Firebase with given index.
        /// The callback will return null if the seed does not exist or the index is out of range
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Seed in string form</returns>
        public static void GetSeed(int index, GetSeedCallback callback)
        {
            if (index >= 0 && index < 6) {
                fbh.GetSeed(index, val=>{callback(val);});
            }
            else {
                callback(null);
            }
        }
        /// <summary>
        /// Saves a world seed to Firebase with given index.
        /// If the index is out of the range of 0 to 5 then it will not write the value.
        /// </summary>
        /// <param name="seed">Value of the seed</param>
        /// <param name="index">Index of the seed</param>
        /// <returns>Seed in string form</returns>
        public static void SetSeed(string seed, int index)
        {
            if (index >= 0 && index < 6) {
                fbh.SetSeed(seed, index);
            }
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
        /// Sets the player's current health in Firebase
        /// </summary>
        public static void SetCurrHP(int value)
        {
            fbh.SetStat("currHP", value, res=>{});
        }

        /// <summary>
        /// Gets the player's last known max health from Firebase
        /// </summary>
        public static void GetMaxHP(GetStatCallback callback)
        {
            fbh.GetStat("maxHP", val=>{callback(val);});
        }
        /// <summary>
        /// Sets the player's max health in Firebase
        /// </summary>
        public static void SetMaxHP(int value)
        {
            fbh.SetStat("maxHP", value, res=>{});
        }

        /// <summary>
        /// Gets the player's last known current stamina from Firebase
        /// </summary>
        public static void GetCurrSTM(GetStatCallback callback)
        {
            fbh.GetStat("currSTM", val=>{callback(val);});
        }
        /// <summary>
        /// Sets the player's current stamina in Firebase
        /// </summary>
        public static void SetCurrSTM(int value)
        {
            fbh.SetStat("currSTM", value, res=>{});
        }

        /// <summary>
        /// Gets the player's last known max stamina from Firebase
        /// </summary>
        public static void GetMaxSTM(GetStatCallback callback)
        {
            fbh.GetStat("maxSTM", val=>{callback(val);});
        }
        /// <summary>
        /// Sets the player's max stamina in Firebase
        /// </summary>
        public static void SetMaxSTM(int value)
        {
            fbh.SetStat("maxSTM", value, res=>{});
        }

        /// <summary>
        /// Get's the player's last known strength value from Firebase
        /// </summary>
        public static void GetStrength(GetStatCallback callback)
        {
            fbh.GetStat("strength", val=>{callback(val);});
        }
        /// <summary>
        /// Sets the player's strength in Firebase
        /// </summary>
        public static void SetStrength(int value)
        {
            fbh.SetStat("strength", value, res=>{});
        }

        /// <summary>
        /// Get's the player's last known speed value from Firebase
        /// </summary>
        public static void GetSpeed(GetStatCallback callback)
        {
            fbh.GetStat("speed", val=>{callback(val);});
        }
        /// <summary>
        /// Sets the player's speed in Firebase
        /// </summary>
        public static void SetSpeed(int value)
        {
            fbh.SetStat("speed", value, res=>{});
        }

        /// <summary>
        /// Get's the player's last saved XP value from Firebase
        /// </summary>
        public static void GetXP(GetStatCallback callback)
        {
            fbh.GetStat("xp", val=>{callback(val);});
        }
        /// <summary>
        /// Sets the player's xp in Firebase
        /// </summary>
        public static void SetXP(int value)
        {
            fbh.SetStat("xp", value, res=>{});
        }

        /// <summary>
        /// Get's the companion's last saved level from Firebase
        /// </summary>
        public static void GetCompLevel(GetStatCallback callback)
        {
            fbh.GetCompStat("level", val=>{callback(val);});
        }

        /// <summary>
        /// Get's the companion's last saved speed value from Firebase
        /// </summary>
        public static void GetCompSpeed(GetStatCallback callback)
        {
            fbh.GetCompStat("speed", val=>{callback(val);});
        }

        /// <summary>
        /// Get's the companion's last saved strength value from Firebase
        /// </summary>
        public static void GetCompStrength(GetStatCallback callback)
        {
            fbh.GetCompStat("strength", val=>{callback(val);});
        }
    }//
}//
