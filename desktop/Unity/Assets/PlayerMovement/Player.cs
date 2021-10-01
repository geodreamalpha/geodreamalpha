using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerMovementComponent
{
     public class Player
    {
        Stats playerStats = new Stats(); 

        public Stats GetPlayerStats()
        {
            return playerStats; 
        }
    }
}