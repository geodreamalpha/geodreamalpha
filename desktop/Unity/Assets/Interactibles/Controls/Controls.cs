using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UserMover;
using UserAnimate;
using UserTargeting;
using UserRanged;

namespace UserControls
{
    [RequireComponent(typeof(Mover), typeof(Animate), typeof(Ranged))]
    public class Controls : MonoBehaviour
    {
        Mover mover;
        Animate animate;
        Targeting targeting;
        Ranged ranged;

        Action moveType;

        public void Reset()
        {
            if (GetComponent<Targeting>() == null)
                gameObject.AddComponent<Targeting>();
        }

        void Start()
        {
            mover = GetComponent<Mover>();
            animate = GetComponent<Animate>();
            targeting = GetComponent<Targeting>();
            ranged = GetComponent<Ranged>();
        }

        void Update()
        {
            mover.ResetVelocity();
            animate.ResetWalkAndSprint();
            Transform c = Camera.main.transform; //target;

            moveType = animate.Walk;
            if (Input.GetKey(KeyCode.LeftShift))
                moveType = animate.Sprint;

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                mover.Accelerate(c.forward + c.right, 0, moveType);
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                mover.Accelerate(c.right + -c.forward, 0, moveType);
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                mover.Accelerate(-c.forward + -c.right, 0, moveType);
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                mover.Accelerate(-c.right + c.forward, 0, moveType);

            else if (Input.GetKey(KeyCode.W))
                mover.Accelerate(c.forward, 0, moveType);
            else if (Input.GetKey(KeyCode.S))
                mover.Accelerate(-c.forward, 0, moveType);
            else if (Input.GetKey(KeyCode.A))
                mover.Accelerate(-c.right, 0, moveType);
            else if (Input.GetKey(KeyCode.D))
                mover.Accelerate(c.right, 0, moveType);

            if (Input.GetKeyDown(KeyCode.Space))
                mover.Jump(animate.isGrounded, animate.Jump);

            if (Input.GetMouseButtonDown(0))
                animate.UseMelee();
            if (Input.GetKey(KeyCode.Alpha2))
                ranged.Fire("N/A");

            mover.Move();
        }
    }
}