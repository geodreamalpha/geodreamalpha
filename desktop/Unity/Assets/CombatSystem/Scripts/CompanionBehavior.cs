using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class CompanionBehavior : ActionBase
    {
        protected override void Start()
        {
            base.Start();
            speed = 5f;
            events.Add((() => Vector3.Distance(controller.transform.position, target.position) > 2f,
                            () => SetMove(getDirectionToTarget, transform.forward, grabWalking)));
        }

        protected override void Update()
        {
            base.Update();

            if (Vector3.Distance(controller.transform.position, target.position) > 20f)
                controller.Move(-controller.transform.position + target.position + new Vector3(2f, 0f, 0f));
        }
    }
}