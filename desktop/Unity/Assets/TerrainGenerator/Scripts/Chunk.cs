using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using System;

namespace TerrainGeneratorComponent
{
	public class Chunk
	{
		public const float seaLevel = 77f;
		GameObject waterObject;

		//stores the height for each y,x vertex of the terrain
		float[,] heightmap;
		//stores the texture maps of the terrain
		float[,,] alphamapLayers;
		//stores the grass/ flower/ small plants/ small rocks detail of the terrain
		int[][,] detailLayers;
		//stores the trees, big plants, and big rocks of the terrain
		List<TreeInstance> trees;

		//the gameobjec the Terrain Component is attached to
		GameObject terrainObject;
		//the terrain component which accesses general terrain settings
		Terrain terrain;
		//the terrain data component which accesses heightmap, texture, detail, and tree data
		TerrainData data;

		//the vertex count of the terrain
		public static int lineLength;
		//the world size of the terrain
		public static Vector3 size;
		//the face count of the terrain
		public static int faceLength;
		//the world size of the terrain divided by four.
		public static int quarterSize;

		public static int moveSize;

		bool running = false;
		Vector3 playerPosition = Vector3.zero;
		bool correctPosition = false;

		public Chunk(GameObject gameObject)
		{
			#region Initialize Water Object
			waterObject = GameObject.Find("Water");
			waterObject.transform.localPosition = new Vector3(0f, seaLevel, 0f);
			#endregion

			#region Initialize Terrain Object And Components
			terrainObject = gameObject;
			terrain = gameObject.GetComponentInChildren<Terrain>();
			data = terrain.terrainData;
			#endregion

			#region Initialize Terrain Data Vars
			detailLayers = new int[data.detailPrototypes.Length][,];
			trees = new List<TreeInstance> { };
			#endregion

			#region Initialize Terrain Data Dimensions
			faceLength = data.detailResolution;
			lineLength = data.heightmapResolution;
			size = data.size;
			quarterSize = (int)size.x / 4;
			moveSize = (int)size.x / 16;
			#endregion

			running = false;
			playerPosition = Vector3.zero;
		}//


		//when play ventures too far from the map, the maps data will be recalculated and will re-align the map's center with players position
		//the maps data will be loaded asynchronously.
		public void ListenForRefresh(Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, Vector3> generate, Transform playerTransform, bool forceGenerate, bool isAsync)
		{
			if (correctPosition)
            {
				#region Snap Player To Proper Terrain Height
				float playerHeight2 = Mathf.Clamp(terrain.SampleHeight(playerTransform.position) + 1f, seaLevel + 1f, data.size.y + 1f);
				playerTransform.position = new Vector3(playerTransform.position.x, playerHeight2, playerTransform.position.z);
				Debug.Log(playerHeight2.ToString() + "   " + playerTransform.position.y.ToString());
				#endregion
				correctPosition = false;
			}

			#region Initialize Positions
			Vector3 playerPos = playerTransform.position;
			Vector3 terrainPos = terrainObject.transform.position;
			#endregion

			#region Check if player is outside of world bounds
			if (Mathf.Abs(playerPos.x) > 50001 || Mathf.Abs(playerPos.z) > 50001)
			{
				//playerPos.x = Mathf.Clamp(playerPos.x, -50000, 50000);
				//playerPos.z = Mathf.Clamp(playerPos.z, -50000, 50000);
				//playerTransform.position = playerPos;
				//Debug.Log("player cannot go outside of world bounds");
			}
			#endregion

			if (!running)
			{
				//if player is nearing the edge of the terrain, update terrain data accordingly and move center of terrain to player position
				if (playerPos.x < terrainPos.x - quarterSize
					|| playerPos.x > terrainPos.x + quarterSize
					|| playerPos.z < terrainPos.z - quarterSize
					|| playerPos.z > terrainPos.z + quarterSize || forceGenerate)
				{
					running = true;

					#region Initialize Procedural Vars
					playerPosition = playerTransform.position;
					playerPosition.x = Mathf.RoundToInt(playerPosition.x / moveSize) * moveSize;
					playerPosition.z = Mathf.RoundToInt(playerPosition.z / moveSize) * moveSize;
					#endregion

					#region Procedurally Generate Biome
					heightmap = new float[lineLength, lineLength];
					alphamapLayers = new float[faceLength, faceLength, data.terrainLayers.Length];
					for (int i = 0; i < detailLayers.Length; i++)
						detailLayers[i] = new int[faceLength, faceLength];
					trees.Clear();

					generate(playerPosition.x, playerPosition.z, heightmap, alphamapLayers, detailLayers, trees, size);
					#endregion

					#region Update Terrain Data
					data.SetHeights(0, 0, heightmap);
					data.SetAlphamaps(0, 0, alphamapLayers);
					for (int i = 0; i < detailLayers.Length; i++)
						data.SetDetailLayer(0, 0, i, detailLayers[i]);
					data.SetTreeInstances(trees.ToArray(), true);
					terrain.Flush();
					#endregion
					
					#region Move Center of Terrain to Player Position
					terrainObject.transform.position = new Vector3(playerPosition.x, 0f, playerPosition.z);
					#endregion

					#region Snap Player To Proper Terrain Height
					float playerHeight = Mathf.Clamp(terrain.SampleHeight(playerTransform.position) + 1f, seaLevel + 1f, data.size.y + 1f);
					playerTransform.position = new Vector3(playerTransform.position.x, playerHeight, playerTransform.position.z);
					Debug.Log(playerHeight.ToString() + "   " + playerTransform.position.y.ToString());
					#endregion

					correctPosition = true;
					running = false;
				}
			}
		}


		IEnumerator Refresh()
        {


			yield return null;
        } 


	}
}

