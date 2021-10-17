using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace TerrainGeneratorComponent
{
	public class Chunk
	{
		public const int layer = 3;

		public const float seaLevel = 30f;
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
		public static int lineLength = 513;
		//the world size of the terrain
		public static Vector3 size = new Vector3(32, 1200, 32);
		//the face count of the terrain
		public static int faceLength = 512;
		//the world size of the terrain divided by 2.
		public static int halfFaceLength = faceLength / 2;

		public async void LoadAsync(int indexX, int indexY, MapAssets assets, Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, Vector3> Generate)
		{
			#region Initialize Water Object
			//waterObject = GameObject.Find("Water");
			//waterObject.transform.localPosition = new Vector3(0f, seaLevel, 0f);
			#endregion

			data = new TerrainData();
			data.terrainLayers = assets.GetLayers();
			data.detailPrototypes = assets.GetDetailTextures();
			data.treePrototypes = assets.GetTrees();
			data.size = size;
			data.heightmapResolution = lineLength;
			data.alphamapResolution = faceLength;
			data.SetDetailResolution(faceLength, 8);
			data.wavingGrassAmount = 0.2f;
			data.wavingGrassStrength = 0.5f;
			
			detailLayers = new int[data.detailPrototypes.Length][,];
			trees = new List<TreeInstance> { };
			heightmap = new float[lineLength, lineLength];
			alphamapLayers = new float[faceLength, faceLength, data.terrainLayers.Length];
			for (int i = 0; i < detailLayers.Length; i++)
				detailLayers[i] = new int[faceLength, faceLength];
			trees.Clear();
			//async
			var task2 = await Task.Run(() =>
			{
				Generate(indexX * faceLength, indexY * faceLength, heightmap, alphamapLayers, detailLayers, trees, size);
				return 0;
			});

			//update terrain
			#region Update Terrain Data
			data.SetHeights(0, 0, heightmap);
			data.SetAlphamaps(0, 0, alphamapLayers);
			for (int i = 0; i < detailLayers.Length; i++)
            {
				data.SetDetailLayer(0, 0, i, detailLayers[i]);
			}
			data.SetTreeInstances(trees.ToArray(), true);			
			#endregion
			
			terrainObject = Terrain.CreateTerrainGameObject(data);
			SetCenterPosition(new Vector3(indexX * faceLength, 0f, indexY * faceLength));
			terrainObject.layer = layer;
			terrainObject.isStatic = true;

			terrain = terrainObject.GetComponent<Terrain>();
			terrain.detailObjectDistance = 180f;
			terrain.treeBillboardDistance = 100f;
			terrain.treeCrossFadeLength = 50f;
			
			data.RefreshPrototypes();
		}//

		public void Destroy()
        {
			GameObject.Destroy(terrainObject);
        }

		public Vector3 GetCenterPosition()
        {
			Vector3 pos = terrainObject.transform.position;
			return new Vector3(pos.x + halfFaceLength, pos.y, pos.z + halfFaceLength);
        }//

		public void SetCenterPosition(Vector3 pos)
		{
			terrainObject.transform.position = new Vector3(pos.x - halfFaceLength, pos.y, pos.z - halfFaceLength);
		}//

		public static void Snap(CharacterController controller)
		{
			RaycastHit hit;
			controller.Move(Vector3.up * 1300);
			Vector3 newPosition = controller.transform.position;
			Physics.Raycast(newPosition, Vector3.down, out hit, float.PositiveInfinity, layer);
			newPosition.y = hit.point.y;
			controller.Move(-controller.transform.position + newPosition);
		}//
	}
}
