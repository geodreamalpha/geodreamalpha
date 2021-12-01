using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TerrainGeneratorComponent;

namespace CombatSystemComponent
{
    //manages the procedural generation of enemies in the game.
    class EnemyGenerator
    {
        public const int MAX_ENEMIES = 100; //100
        public const int DESTROY_DISTANCE = 768;
        int currentEnemies = 0;
        List<GameObject> enemies = new List<GameObject> { };
        Vector3 yDistance = new Vector3(0, 600, 0);

        public void CreateNewEnemies(CombatSystemAssets assets, Transform player)
        {
            List<(int index, Vector3 position)> enemyInfo = TerrainGenerator.GetEnemySpawnInfo();

            while (currentEnemies < MAX_ENEMIES && enemyInfo.Count > 0)
            {
                currentEnemies++;
                enemies.Add(GameObject.Instantiate(assets.enemies[enemyInfo[0].index], enemyInfo[0].position + yDistance, Quaternion.identity));
                enemyInfo.RemoveAt(0);

                TerrainGenerator.SnapToGround(enemies[enemies.Count - 1].GetComponent<CharacterController>());
                enemies[enemies.Count - 1].GetComponent<HelperBase>().SetAssets(assets);
                enemies[enemies.Count - 1].GetComponent<CharacterBase>().levelStats = player.root.gameObject.GetComponent<PlayerBehavior>().levelStats;
            }
        }

        public void RemoveOldEnemies(Transform player)
        {
            for (int i = 0; i < enemies.Count; i++)
                if (Vector3.Distance(enemies[i].transform.position, player.position) > DESTROY_DISTANCE || enemies[i].GetComponent<CharacterBase>().health <= 0)
                {
                    CombatSystem.ProperDestroy(enemies[i]);
                    enemies.RemoveAt(i);
                    currentEnemies--;
                    i--;                   
                }
        }
    }
}
