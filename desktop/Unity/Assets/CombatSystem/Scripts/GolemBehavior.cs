using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class GolemBehavior : AIBase
    {
        protected override void Start()
        {
            base.Start();
            speed = 4f;

            //normally, the enemy would spawn somewhere, but this is for testing purposes
            events.Add((() => Vector3.Distance(transform.position, target.position) > 100f,
                              () => controller.Move(-transform.position + target.position + new Vector3(50f, 10f, 50f))));

            events.Add((() => IsTargetWithinRange(4f, 20f),
                              () => SetMove(getDirectionToTarget, transform.forward, "isWalking")));

            events.Add((() => IsTargetWithinRange(3.9f, 4.001f),
                              () => animator.SetTrigger("isMelee")));
        }
    }
}
