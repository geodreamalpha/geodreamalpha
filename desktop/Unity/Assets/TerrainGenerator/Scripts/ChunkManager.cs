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

        int distanceFromPlayer = 1;

        Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();
        List<(int, int)> oldKeys = new List<(int, int)>();
        List<(int, int)> newKeys = new List<(int, int)>();


        public void Refresh(Vector3 playerPosition, MapAssets assets, Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, Vector3> Generate)
        {
            indexX = (int)(playerPosition.x / Chunk.faceLength);
            indexY = (int)(playerPosition.z / Chunk.faceLength);

            oldKeys = newKeys;
            newKeys = new List<(int, int)>();

            for (int x = indexX - distanceFromPlayer; x <= indexX + distanceFromPlayer; x++)
            {
                for (int y = indexY - distanceFromPlayer; y <= indexY + distanceFromPlayer; y++)
                {
                    if (!chunks.ContainsKey((x, y)))
                    {
                        Chunk chunk = new Chunk();
                        chunk.Generate(x, y, assets, Generate);
                        chunks.Add((x, y), chunk);
                        
                    }
                    newKeys.Add((x, y));
                }
            }

            foreach ((int, int) key in oldKeys)
            {
                if (!newKeys.Contains(key))
                {
                    chunks[key].Destroy();
                    chunks.Remove(key);
                }
            }
        }//
    }
}
