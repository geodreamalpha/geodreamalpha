using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class CalculationManager : ActionsManager
    {
        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.layer == 7 && other.gameObject.layer == 9)
            {
                animator.SetTrigger("isHit");
            }
        }
    }//
}
