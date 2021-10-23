using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class CalculationManager : ActionsManager
    {
        /// <summary>
        /// the layer 
        /// </summary>
        protected int thisLayer = 7;
        protected int otherLayer = 9;

        private void OnTriggerEnter(Collider other)
        {
            //
            if (gameObject.layer == thisLayer && other.gameObject.layer == otherLayer)
            {
                animator.SetTrigger("isHit");
            }
        }
    }//
}
