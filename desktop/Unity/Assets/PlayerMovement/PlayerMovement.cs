using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystemComponent; 

namespace PlayerMovementComponent
{

    // Jake Aldridge 
    public class PlayerMovement : MonoBehaviour
    {
        Player player = new Player();
        // Start is called before the first frame update
        public CharacterController controller;

        public float speed = 12f;
        public float gravity = -30f;
        public float jumpHeight = 5f;

        public Transform groundCheck;
        public float groundDistance = .4f;  //Radius of sphere for ground check 
        public LayerMask groundMask;


        Vector3 velocity;
        bool isGrounded;

        // Update is called once per frame
        void Update()
        {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                //Debug.Log("Grounded: " + velocity.y + "\n");
                velocity.y = -2f;
            }



            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                speed = 24f;
            }

            Vector3 move = (Camera.main.transform.right * x) + (Camera.main.transform.forward * z);
            move[1] = 0;  //Nate added this idk what it does yet 
            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            speed = 12f;
        }

        /// <summary>
        /// Returns a string that introduces the component. 
        /// </summary>
        /// <returns>String introducing the component.</returns>
        public string Hello()
        {
            return "Hello from Component PlayerMovement"; 
        }

        /// <summary>
        /// Gets players current position in-game.
        /// </summary>
        /// <returns>The postion of the player as a Vector3 object</returns>
        public Vector3 GetPlayerPosition()
        {
            return Vector3.zero;  // returns a 0.0.0 vector 
        }

        /// <summary>
        /// Gets current player stats. 
        /// </summary>
        /// <returns>The current stats of the player object being moved.</returns>
        public Stats GetPlayerStats()
        {
            return player.GetPlayerStats(); 
        }
    }
}