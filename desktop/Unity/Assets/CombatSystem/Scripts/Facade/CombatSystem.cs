using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent {

    //Jake Aldridge 
    public class CombatSystem : MonoBehaviour
    {
        //Note to Dr. Layman 
        //Combat system will only provide the stats class as well as any particle effects and may not need.
        //It does not involve any other classes, we have determined that the stats class object will be declared 
        //For both the player and enemy classes instead of being managed by the combat system. 

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Returns a string that introduces the component. 
        /// </summary>
        /// <returns>String introducing the component.</returns>
        public string Hello()
        {
            return "Hello from Component CombatSystem"; 
        }
    }
}  
