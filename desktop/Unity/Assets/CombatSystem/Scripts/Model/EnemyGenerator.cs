using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TerrainGeneratorComponent;

namespace CombatSystemComponent
{
    public class EnemyGenerator
    {
        public const int MAX_ENEMIES = 100;
        public const int DESTROY_DISTANCE = ChunkManager.maxViewDistanceFromPlayer;
        int currentEnemies = 0;
        List<GameObject> enemies = new List<GameObject> { };
        Vector3 yDistance = new Vector3(0, 600, 0);

        public void CreateNewEnemies(CombatSystemAssets assets, Transform player)
        {
            while (currentEnemies < MAX_ENEMIES && Chunk.enemies.Count > 0)
            {
                currentEnemies++;
                enemies.Add(GameObject.Instantiate(assets.enemies[Chunk.enemies[0].index], Chunk.enemies[0].position + yDistance, Quaternion.identity));
                Chunk.enemies.RemoveAt(0);

                SnapList.Add(enemies[enemies.Count - 1].GetComponent<CharacterController>());
                enemies[enemies.Count - 1].GetComponent<HelperBase>().SetAssets(assets);

                //either pull level from player or companion level stats
                enemies[enemies.Count - 1].GetComponent<CharacterBase>().levelStats = player.root.gameObject.GetComponent<PlayerBehavior>().levelStats;
                //call initialize stats maybe?
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
