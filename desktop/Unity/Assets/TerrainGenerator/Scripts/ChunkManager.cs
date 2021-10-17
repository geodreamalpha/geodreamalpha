using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TerrainGeneratorComponent
{
    public class ChunkManager
    {
        int indexX = 0;
        int indexY = 0;
        int worldX = 0;
        int worldY = 0;

        const int maxChunksFromPlayer = 3;
        const int maxViewDistanceFromPlayer = 512;

        Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();

        public void Refresh(CharacterController playerController, MapAssets assets, Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, Vector3> Generate)
        {
            //may need to use raycast
            indexX = Mathf.RoundToInt(playerController.transform.position.x / Chunk.faceLength);
            indexY = Mathf.RoundToInt(playerController.transform.position.z / Chunk.faceLength);

            for (int x = indexX - maxChunksFromPlayer; x <= indexX + maxChunksFromPlayer; x++)
            {
                for (int y = indexY - maxChunksFromPlayer; y <= indexY + maxChunksFromPlayer; y++)
                {
                    if (!chunks.ContainsKey((x, y)) && isWithinPlayerRange((x, y), playerController.transform.position))
                    {
                        Chunk chunk = new Chunk();
                        chunk.LoadAsync(x, y, assets, Generate);
                        chunks.Add((x, y), chunk);
                    }
                    else if (chunks.ContainsKey((x, y)) && !isWithinPlayerRange((x, y), playerController.transform.position))
                    {
                        //remove unused chunk
                        chunks[(x, y)].Destroy();
                        chunks.Remove((x, y));                   
                    }
                }
            }
        }

        public bool isWithinPlayerRange((int, int) key, Vector3 pos)
        {
            return Vector2.Distance(new Vector2(key.Item1 * Chunk.faceLength, key.Item2 * Chunk.faceLength), new Vector2(pos.x, pos.z)) < maxViewDistanceFromPlayer;
        }
    }
}
