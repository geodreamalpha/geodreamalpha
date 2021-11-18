using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class EnemyGenerator
    {
        public const int MAX_ENEMIES = 10;
        int currentEnemies = 0;
        List<GameObject> enemies = new List<GameObject> { };
        Vector3[] potentialPositions = new Vector3[] {  };

        public void Generate(CombatSystemAssets assets, Transform player)
        {
            while (currentEnemies < MAX_ENEMIES)
            {
                currentEnemies += 1;
                enemies.Add(GameObject.Instantiate(assets.enemies[UnityEngine.Random.Range(0,33)], player.position + new Vector3(1, 3f, 1) * UnityEngine.Random.Range(40f, 60f), Quaternion.identity));
                //TerrainGeneratorComponent.Chunk.Snap(enemies[enemies.Count - 1].GetComponent<CharacterController>());
                HelperBase aIBase = enemies[enemies.Count - 1].GetComponent<HelperBase>();
                aIBase.SetAssets(assets);
                aIBase.Target(player);
            }
        }

        public static void Tester()
        {
            int thiss = MAX_ENEMIES;
        }
    }
}
