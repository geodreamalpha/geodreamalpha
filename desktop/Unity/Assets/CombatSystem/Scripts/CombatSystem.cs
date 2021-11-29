using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent {

    //Jake Aldridge 
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField]
        Transform player;
        [SerializeField]
        CombatSystemAssets assets;
        [SerializeField]
        MultiplierStats multiplierStats;
        EnemyGenerator enemyGenerator = new EnemyGenerator();
        static bool isStartUp = true;

        //main camera objects
        

        //Note to Dr. Layman 
        //Combat system will only provide the stats class as well as any particle effects and may not need.
        //It does not involve any other classes, we have determined that the stats class object will be declared 
        //For both the player and enemy classes instead of being managed by the combat system. 

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

        public float GetAReductionDamageValue(float offenseValue, float reducerValue)
        {
            return DerivedStats.GetReductionDamage(offenseValue, reducerValue);
        }

        public float GetPlayerHealth()
        {
            CharacterBase characterBase = player.root.gameObject.GetComponent<CharacterBase>();
            return (characterBase.health / characterBase.gameStats.healthPoints) * 100f;
        }

        public float GetPlayerStamina()
        {
            CharacterBase characterBase = player.root.gameObject.GetComponent<CharacterBase>();
            return (characterBase.stamina / characterBase.gameStats.staminaPoints) * 100f;
        }
        
        public static void ProperDestroy(GameObject obj, float time = 0)
        {
            Destroy(obj, time);
            obj = null;
        }
    }
}  
