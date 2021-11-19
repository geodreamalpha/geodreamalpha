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
                enemies.Add(GameObject.Instantiate(assets.getEnemyByName("Golem"), player.position + new Vector3(1f, 5f, 1f) * UnityEngine.Random.Range(40f, 150f), Quaternion.identity));
                //TerrainGeneratorComponent.Chunk.Snap(enemies[enemies.Count - 1].GetComponent<CharacterController>());
                HelperBase aIBase = enemies[enemies.Count - 1].GetComponent<HelperBase>();
                aIBase.SetAssets(assets);
                aIBase.Target(player);
            }

        }
    }
}