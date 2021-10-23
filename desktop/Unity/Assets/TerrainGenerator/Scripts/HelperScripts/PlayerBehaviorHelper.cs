using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class PlayerBehaviorHelper : CalculationManager
    {  
        protected override void Start()
        {
            base.Start();

            forwardEvent =   (() => Input.GetKey(KeyCode.W),
                              () => SetMove(Camera.main.transform.forward, transform.forward, "isWalking"));

            backEvent =      (() => Input.GetKey(KeyCode.S),
                              () => SetMove(-Camera.main.transform.forward, transform.forward, "isWalking"));

            leftEvent =      (() => Input.GetKey(KeyCode.A),
                              () => SetMove(-Camera.main.transform.right, transform.forward, "isWalking"));

            rightEvent =     (() => Input.GetKey(KeyCode.D),
                              () => SetMove(Camera.main.transform.right, transform.forward, "isWalking"));

            attackEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger("isFirstAttack")));

            attackEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger("isSecondAttack")));

            attackEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger("isThirdAttack")));
        }//
    }
}
