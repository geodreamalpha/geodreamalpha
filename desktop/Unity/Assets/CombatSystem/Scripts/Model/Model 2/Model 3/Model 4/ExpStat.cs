using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class ExpStat
    {
        //float expMultiplier = 1.25f;
        int expToNextLevel = 20;
        int exp = 0;

        public bool isLevelIncreaseAvailable
        {
            get { return exp >= expToNextLevel; }
        }

        public void Add(int exp)
        {
            this.exp += exp;
        }

        public void Reset()
        {
            exp = 0;
        }
    }
}

