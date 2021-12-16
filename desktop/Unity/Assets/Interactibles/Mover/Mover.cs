using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UserMover
{
    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        CharacterController controller;

        public readonly static Vector3 gravity = new Vector3(0, -9.8f, 0);
        protected Vector3 velocity = Vector3.zero;
        protected Vector3 rotation = Vector3.forward;

        public void Reset()
        {
            if (GetComponent<CharacterController>() == null)
                gameObject.AddComponent<CharacterController>();
        }

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        public void Accelerate(Vector3 direction, float speed, params Action[] moveEvents)
        {
            Rotate(direction);

            Vector3 newDirection = new Vector3(direction.x, 0, direction.z);
            newDirection = newDirection.normalized;

            velocity.x = newDirection.x * speed;
            velocity.z = newDirection.z * speed;

            foreach (Action moveEvent in moveEvents)
                moveEvent();
        }

        public void Jump(bool isGrounded, params Action[] moveEvents)
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(10f * 2f * -gravity.y);
                foreach (Action moveEvent in moveEvents)
                    moveEvent();
            }
        }

        public void Rotate(Vector3 yAxisFacingDirection)
        {
            rotation = controller.transform.rotation.eulerAngles;
            Vector3 newRotation = Quaternion.LookRotation(yAxisFacingDirection).eulerAngles;
            rotation.y = newRotation.y;
        }

        public void ResetVelocity()
        {
            velocity.x = 0;
            velocity.z = 0;
        }

        public void Move()
        {
            velocity.y = Mathf.Clamp(velocity.y + gravity.y * Time.deltaTime, gravity.y, float.MaxValue);
            controller.Move(velocity * Time.deltaTime);
            controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, Quaternion.Euler(rotation), 0.15f * 60f * Time.deltaTime);
        }
    }
}