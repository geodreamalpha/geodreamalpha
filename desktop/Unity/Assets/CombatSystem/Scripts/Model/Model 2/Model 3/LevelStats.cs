using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    //Manages the levelling up behavior of player's stats (but can be used for any character)
    class LevelStats : StatsData
    {
        ExpStat healthExp = new ExpStat();
        ExpStat staminaExp = new ExpStat();
        ExpStat strengthLExp = new ExpStat();
        ExpStat speedExp = new ExpStat();

        public void AddHealthExp(int exp = 1)
        {
            healthExp.Add(exp);
            if (healthExp.isLevelIncreaseAvailable)
            {
                healthExp.Reset();
                health = Mathf.Clamp(health + 50, 100, 5000);
            }
        }

        public void AddStaminaExp(int exp = 1)
        {
            staminaExp.Add(exp);
            if (staminaExp.isLevelIncreaseAvailable)
            {
                staminaExp.Reset();
                stamina = Mathf.Clamp(stamina + 1, 1, 100);
            }
        }

        public void AddStrengthExp(int exp = 1)
        {
            strengthLExp.Add(exp);
            if (strengthLExp.isLevelIncreaseAvailable)
            {
                strengthLExp.Reset();
                strength = Mathf.Clamp(strength + 1, 1, 100);
            }
        }

        public void AddSpeedExp(int exp = 1)
        {
            speedExp.Add(exp);
            if (speedExp.isLevelIncreaseAvailable)
            {
                speedExp.Reset();
                speed = Mathf.Clamp(speed + 1, 1, 100);
            }
        }
    }
}


