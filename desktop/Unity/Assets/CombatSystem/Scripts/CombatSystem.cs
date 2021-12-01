using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent {

    //Jake Aldridge
    //Tyler Anderson
    //Facade class for Combat System Component
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField]
        Transform player;
        [SerializeField]
        CharacterBase playerBase;
        [SerializeField]
        CharacterBase companionBase;
        [SerializeField]
        CombatSystemAssets assets;
        [SerializeField]
        MultiplierStats multiplierStats;
        EnemyGenerator enemyGenerator = new EnemyGenerator();
        static bool isStartUp = true;

        void Awake()
        {
            if (isStartUp)
            {
                isStartUp = false;
                CharacterBase.InitializeMultiplierStats(multiplierStats);
            }           
        }

        void Start()
        {
            player.gameObject.transform.root.GetComponent<PlayerBehavior>().SetAssets(assets);
            GameObject.Find("Companion").GetComponent<CompanionBehavior>().SetAssets(assets);
            Cam.Initialize();
        }

        void Update()
        {
            enemyGenerator.CreateNewEnemies(assets, player);  
        }

        void LateUpdate()
        {
            enemyGenerator.RemoveOldEnemies(player);
            Cam.Update();
        }

        /// <summary>
        /// Returns a string that introduces the component. 
        /// </summary>
        /// <returns>String introducing the component.</returns>
        public string Hello()
        {
            return "Hello from Component CombatSystem"; 
        }

        //used for unit testing
        public float GetAReductionDamageValue(float offenseValue, float reducerValue)
        {
            return DerivedStats.GetReductionDamage(offenseValue, reducerValue);
        }

        public static Vector3 GetGravity()
        {
            return CharacterBase.gravity;
        }

        public float GetPlayerHealth()
        {
            return (playerBase.health / playerBase.gameStats.healthPoints) * 100f;
        }

        public float GetPlayerStamina()
        {
            return (playerBase.stamina / playerBase.gameStats.staminaPoints) * 100f;
        }

        public string PrintPlayerStats()
        {
            return playerBase.levelStats.ToString();
        }

        public string PrintCompanionStats()
        {
            return companionBase.levelStats.ToString();
        }
        
        public static void ProperDestroy(GameObject obj, float time = 0)
        {
            Destroy(obj, time);
            obj = null;
        }
    }
}  
