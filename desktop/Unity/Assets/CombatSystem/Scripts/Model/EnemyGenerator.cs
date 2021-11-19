using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class EnemyGenerator
    {
        public const int MAX_ENEMIES = 100;
        int currentEnemies = 0;
        List<GameObject> enemies = new List<GameObject> { };
        Vector3[] potentialPositions = new Vector3[] {  };

        List<int> enemiesToBeRemoved = new List<int>();

        public void Generate(CombatSystemAssets assets, Transform player)
        {
            while (currentEnemies < MAX_ENEMIES && TerrainGeneratorComponent.Chunk.enemies.Count > 0)
            {
                currentEnemies += 1;
                enemies.Add(GameObject.Instantiate(assets.enemies[TerrainGeneratorComponent.Chunk.enemies[0].index],
                    TerrainGeneratorComponent.Chunk.enemies[0].position, Quaternion.identity));
                TerrainGeneratorComponent.Chunk.enemies.RemoveAt(0);

                CharacterController controller = enemies[enemies.Count - 1].GetComponent<CharacterController>();
                controller.Move(new Vector3(0f, 400f, 0f));
                TerrainGeneratorComponent.SnapQueue.Add(controller);
                //TerrainGeneratorComponent.Chunk.Snap(enemies[enemies.Count - 1].GetComponent<CharacterController>());

                HelperBase aIBase = enemies[enemies.Count - 1].GetComponent<HelperBase>();
                aIBase.SetAssets(assets);
                aIBase.Target(player);//maybe you do not need this
            }

            int removeIndex = 0;
            enemiesToBeRemoved.Clear();
            foreach(GameObject enemy in enemies)
            {
                if (enemy.transform.position.y < -10 || enemy.GetComponent<CharacterBase>().health <= 0)
                {
                    
                    enemiesToBeRemoved.Add(removeIndex);
                    currentEnemies--;
                }
                removeIndex++;
            }

            foreach(int index in enemiesToBeRemoved)
            {
                //need to have a unified way of destroying enemy gameobjects.
                CombatSystem.ProperDestroy(enemies[index]);
                enemies.RemoveAt(index);
            }
        }
    }
}
