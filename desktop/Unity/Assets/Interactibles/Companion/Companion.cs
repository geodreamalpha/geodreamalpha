using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UserMover;
using UserAnimate;
using UserMelee;
using UserTargeting;

namespace UserCompanion
{
    [RequireComponent(typeof(Mover), typeof(Animate), typeof(Melee))]
    [RequireComponent(typeof(Targeting))]
    public class Companion : MonoBehaviour
    {
        Mover mover;
        Animate animate;
        Melee melee;
        Targeting targeting;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            animate = GetComponent<Animate>();
            melee = GetComponent<Melee>();
            targeting = GetComponent<Targeting>();
        }

        // Update is called once per frame
        void Update()
        {
            mover.ResetVelocity();
            animate.ResetWalkAndSprint();

            Vector3 faceTarget = targeting.GetDirectionToTarget();

            float distance = targeting.GetTargetDistance();
            if (distance > 3)
                mover.Accelerate(faceTarget, targeting.GetTargetDistance(), animate.Sprint);
            else if (distance > 2)
                mover.Accelerate(faceTarget, targeting.GetTargetDistance(), animate.Walk);
            mover.Move();
        }
    }
}