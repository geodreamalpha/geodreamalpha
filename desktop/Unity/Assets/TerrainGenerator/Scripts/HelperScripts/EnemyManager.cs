using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class EnemyManager : CalculationManager
    {
        protected override void Start()
        {
            base.Start();
            speed = 4f;

            //normally, the enemy would spawn somewhere, but this is for testing purposes
            controller.Move(-controller.transform.position + new Vector3(500f, 80f, 20f)); //---XXX

            forwardEvent =   (() => IsTargetWithinRange(4f, 20f),
                              () => SetMove(transform.forward, getDirectionToTarget, "isWalking"));

            attackEvents.Add((() => IsTargetWithinRange(3.9f, 4.001f),
                              () => animator.SetTrigger("isFirstAttack")));
        }

        
    }
}
