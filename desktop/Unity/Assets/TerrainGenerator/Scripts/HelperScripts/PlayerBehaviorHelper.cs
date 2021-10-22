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
                              () => SetMove(transform.forward, Camera.main.transform.forward, "isWalking"));

            backEvent =      (() => Input.GetKey(KeyCode.S),
                              () => SetMove(transform.forward, -Camera.main.transform.forward, "isWalking"));

            leftEvent =      (() => Input.GetKey(KeyCode.A),
                              () => SetMove(transform.forward, -Camera.main.transform.right, "isWalking"));

            rightEvent =     (() => Input.GetKey(KeyCode.D),
                              () => SetMove(transform.forward, Camera.main.transform.right, "isWalking"));

            attackEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger("isFirstAttack")));

            attackEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger("isSecondAttack")));

            attackEvents.Add((() => Input.GetMouseButtonDown(0),
                              () => animator.SetTrigger("isThirdAttack")));
        }//
    }
}
