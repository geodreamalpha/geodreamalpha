using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    /// <summary>
    /// this class will handle the Stats object as well as additional stats, enemy/ player collision detection (as a result of combat),
    /// damage calculations, and any other functions dealing with the manipulation or access of stats. 
    /// </summary>
    public class StatsBase : ActionBase
    {
        Stats stats;
        protected int thisLayer = 7;
        protected int otherLayer = 9;
        
        private void OnTriggerEnter(Collider other)
        {
            //
            if (gameObject.layer == thisLayer && other.gameObject.layer == otherLayer)
            {
                animator.SetTrigger(grabHit);
            }
        }
    }//
}
