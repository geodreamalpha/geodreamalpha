using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace TerrainGeneratorComponent
{
    //this class loads and deletes map chunk instances as they enter and leave the view distance of the player
    //
    class ChunkManager
    {
        int indexX = 0;
        int indexY = 0;

        const int maxChunksFromPlayer = 3;
        public const int maxViewDistanceFromPlayer = 768;
        
        Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();
        List<(int, int)> keys = new List<(int, int)> { };
        List<(int, int)> keysToRemove = new List<(int, int)> { };

        public void Refresh(Vector3 playerPosition, MapAssets assets, Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, List<(int, Vector3)>, Vector3> Generate)
        {
            #region Get Terrain Indices That Player Is On
            //these two values represent the Key used to identify active terrain chunk (chunk that player is on) and nearby terrain chunks relative to the player
            indexX = Mathf.RoundToInt(playerPosition.x / Chunk.faceLength);
            indexY = Mathf.RoundToInt(playerPosition.z / Chunk.faceLength);
            #endregion

            //chunks within range of the player along the x-axis
            for (int x = indexX - maxChunksFromPlayer; x <= indexX + maxChunksFromPlayer; x++)
            {
                //chunks within range of the player along the y-axis
                for (int y = indexY - maxChunksFromPlayer; y <= indexY + maxChunksFromPlayer; y++)
                {
                    //if chunk does not exist in-game but is within view distance of the player, then load chunk
                    if (!chunks.ContainsKey((x, y)) && isWithinPlayerRange((x, y), playerPosition, 0f))
                    {
                        #region Load Chunk
                        keys.Add((x, y));
                        chunks.Add((x, y), new Chunk());
                        chunks[(x, y)].LoadAsync(x, y, assets, Generate);
                        #endregion
                    }
                }
            }

            #region Delete Chunks Outside of Player View Distance
            //deleting chunks from the active keys ensures that chunks outside the bounds of the previous two loops will eventially be deleted
            //before deletion, chunk must be instantiated after completing an asyncronous load.  This ensures all, known lose ends are accounted for before deletion
            keysToRemove.Clear();
            foreach ((int, int) key in keys)
            {
                //if chunk DOES exist in-game but is outside view distance of the player, then delete chunk
                if (chunks[key].isInstantiated && !isWithinPlayerRange(key, playerPosition, 64f))
                {
                    #region Delete Chunk
                    chunks[key].Destroy();
                    chunks.Remove(key);
                    keysToRemove.Add(key);
                    #endregion
                }
            }
            //iterates through keys that need to be removed and removes them from "keys" list
            foreach ((int, int) keyToRemove in keysToRemove)
                keys.Remove(keyToRemove);
            #endregion
        }

        public bool isWithinPlayerRange((int, int) key, Vector3 pos, float offsetDistance)
        {
            //returns the distance between two positions (chunk position and player position)
            return Vector2.Distance(new Vector2(key.Item1 * Chunk.faceLength, key.Item2 * Chunk.faceLength), new Vector2(pos.x, pos.z)) < maxViewDistanceFromPlayer + offsetDistance;
        }
    }
}
