using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace CombatSystemComponent {

    //Jake Aldridge 
    public class CombatSystem : MonoBehaviour
    {
        static readonly Stats companionStats = new Stats();
        [SerializeField]
        Transform player;
        [SerializeField]
        CombatSystemAssets assets;
        EnemyGenerator enemyGenerator = new EnemyGenerator();
        static bool isStartUp = true;

        CinemachineBrain brain;
        CinemachineOrbitalTransposer orbital;
        CinemachineComposer aim;

        //Note to Dr. Layman 
        //Combat system will only provide the stats class as well as any particle effects and may not need.
        //It does not involve any other classes, we have determined that the stats class object will be declared 
        //For both the player and enemy classes instead of being managed by the combat system. 

        void Awake()
        {
            if (isStartUp)
            {
                isStartUp = false;
                CharacterBase.InitializeMultiplierStats();
            }           
        }

        void Start()
        {
            player.gameObject.transform.root.GetComponent<PlayerBehavior>().SetAssets(assets);
            GameObject.Find("Companion").GetComponent<CompanionBehavior>().SetAssets(assets);

            brain = Camera.main.GetComponent<CinemachineBrain>();
            CinemachineVirtualCamera cineCam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            orbital = cineCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
            aim = cineCam.GetCinemachineComponent<CinemachineComposer>();
        }

        void Update()
        {
            enemyGenerator.CreateNewEnemies(assets, player);
            companionStats.Update();   
        }

        void LateUpdate()
        {
            enemyGenerator.RemoveOldEnemies(player);

            if (Time.timeScale > 0.5)
            {
                brain.enabled = true;
                Vector3 follow = orbital.m_FollowOffset;
                follow.z = Mathf.Clamp(follow.z - Input.mouseScrollDelta.y * 10f * Time.deltaTime, 6, 14);
                follow.y = Mathf.Clamp(follow.y - Input.GetAxis("Mouse Y") * 1f * Time.deltaTime, 0, 5);
                orbital.m_FollowOffset = follow;
            }
            else
                brain.enabled = false;
        }

        /// <summary>
        /// Returns a string that introduces the component. 
        /// </summary>
        /// <returns>String introducing the component.</returns>
        public string Hello()
        {
            return "Hello from Component CombatSystem"; 
        }

        public float GetPlayerHealth()
        {
            return player.root.gameObject.GetComponent<CharacterBase>().health;
        }

        public static void ProperDestroy(GameObject obj, float time = 0)
        {
            Destroy(obj, time);
            obj = null;
        }
    }
}  
