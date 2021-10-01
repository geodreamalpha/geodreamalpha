using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerMovementComponent
{

    // Jake Aldridge 
    public class PlayerMovement : MonoBehaviour
    {
        Player player = new Player(); 
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public string Hello()
        {
            return "Hello from Component Player Movement"; 
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


