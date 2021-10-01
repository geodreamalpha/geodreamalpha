using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystemComponent;

namespace EnemySpawnerComponent
{
    //Tyler Anderson
    public class EnemySpawner : MonoBehaviour
    {
        //list of enemies that are currently active in the game
        List<GameObject> enemies;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Gets the current position of the enemy
        /// </summary>
        /// <returns> The position as a Vector3 </returns>
        public Vector3 GetEnemyPosition()
        {
            return Vector3.zero;
        }

        /// <summary>
        /// Gets the action currently being performed by the enemy
        /// </summary>
        /// <returns> The name of the action as a string </returns> 
        public string GetCombatAction()
        {
            return "action";
        }

        /// <summary>
        /// Gets the "Hello from" string of this component
        /// </summary>
        /// <returns> A string that introduces this component </returns>
        public string Hello()
        {
            return "Hello from Enemy Spawner Component";
        }
    }
}