using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class PlayerBehavior : StatsBase
    {  
        protected override void Start()
        {
            base.Start();

            events.Add((() => Input.GetKey(KeyCode.W),
                              () => SetMove(Camera.main.transform.forward, transform.forward, grabWalking)));

            events.Add((() => Input.GetKey(KeyCode.S),
                              () => SetMove(-Camera.main.transform.forward, transform.forward, grabWalking)));

            events.Add((() => Input.GetKey(KeyCode.A),
                              () => SetMove(-Camera.main.transform.right, transform.forward, grabWalking)));

            events.Add((() => Input.GetKey(KeyCode.D),
                              () => SetMove(Camera.main.transform.right, transform.forward, grabWalking)));

            events.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger(grabMelee)));

            events.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger(grabMelee)));

            events.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger(grabMelee)));

            events.Add((() => Input.GetKeyDown(KeyCode.Space),
                              () => SetJump()));
        }//
    }
}
