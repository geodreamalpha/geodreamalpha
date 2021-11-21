using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class LevelStats : Stats
    {
        ExpStat healthExp = new ExpStat();
        ExpStat staminaExp = new ExpStat();
        ExpStat strengthLExp = new ExpStat();
        ExpStat speedExp = new ExpStat();

        public void AddHealthExp()
        {
            healthExp.Add();
            if (healthExp.isLevelIncreaseAvailable)
            {
                healthExp.Reset();
                //increase health
                Debug.Log("health increase");
            }
        }

        public void AddStaminaExp()
        {
            staminaExp.Add();
            if (staminaExp.isLevelIncreaseAvailable)
            {
                staminaExp.Reset();
                //increase stamina
                Debug.Log("stamina increase");
            }
        }

        public void AddStrengthExp()
        {
            strengthLExp.Add();
            if (strengthLExp.isLevelIncreaseAvailable)
            {
                strengthLExp.Reset();
                //increase strength
                Debug.Log("strength increase");
            }
        }

        public void AddSpeedExp()
        {
            speedExp.Add();
            if (speedExp.isLevelIncreaseAvailable)
            {
                speedExp.Reset();
                //increase speed
                Debug.Log("speed increase");
            }
        }
    }
}


