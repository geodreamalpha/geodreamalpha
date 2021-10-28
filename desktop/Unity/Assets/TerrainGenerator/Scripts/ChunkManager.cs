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
    public class ChunkManager
    {
        int indexX = 0;
        int indexY = 0;

        const int maxChunksFromPlayer = 3;
        const int maxViewDistanceFromPlayer = 512;

        Dictionary<(int, int), Chunk> chunks = new Dictionary<(int, int), Chunk>();

        public void Refresh(CharacterController playerController, MapAssets assets, Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, Vector3> Generate)
        {
            #region Get Terrain Indices That Player Is On
            //these two values represent the Key used to identify active terrain chunk (chunk that player is on) and nearby terrain chunks relative to the player
            indexX = Mathf.RoundToInt(playerController.transform.position.x / Chunk.faceLength);
            indexY = Mathf.RoundToInt(playerController.transform.position.z / Chunk.faceLength);
            #endregion

            //chunks within range of the player along the x-axis
            for (int x = indexX - maxChunksFromPlayer; x <= indexX + maxChunksFromPlayer; x++)
            {
                //chunks within range of the player along the y-axis
                for (int y = indexY - maxChunksFromPlayer; y <= indexY + maxChunksFromPlayer; y++)
                {
                    //if chunk does not exist in-game but is within view distance of the player, then load chunk
                    if (!chunks.ContainsKey((x, y)) && isWithinPlayerRange((x, y), playerController.transform.position))
                    {
                        #region Load Chunk
                        Chunk chunk = new Chunk();
                        chunk.LoadAsync(x, y, assets, Generate);
                        chunks.Add((x, y), chunk);
                        #endregion
                    }
                    //if chunk DOES exist in-game but is outside view distance of the player, then delete chunk
                    else if (chunks.ContainsKey((x, y)) && !isWithinPlayerRange((x, y), playerController.transform.position))
                    {
                        #region Delete Chunk
                        chunks[(x, y)].Destroy();
                        chunks.Remove((x, y));
                        #endregion
                    }
                }
            }
        }

        public bool isWithinPlayerRange((int, int) key, Vector3 pos)
        {
            //returns the distance between two positions (chunk position and player position)
            return Vector2.Distance(new Vector2(key.Item1 * Chunk.faceLength, key.Item2 * Chunk.faceLength), new Vector2(pos.x, pos.z)) < maxViewDistanceFromPlayer;
        }
    }
}
