using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class CompanionBehavior : ActionsManager
    {
        protected override void Start()
        {
            base.Start();
            speed = 5f;
            forwardEvent = (() => Vector3.Distance(controller.transform.position, subject.position) > 2f,
                            () => SetMove(transform.forward, subject.position - transform.position, "isWalking"));
        }

        protected override void Update()
        {
            base.Update();

            if (Vector3.Distance(controller.transform.position, subject.position) > 20f)
                controller.Move(-controller.transform.position + subject.position + new Vector3(2f, 0f, 0f));
        }
    }
}
