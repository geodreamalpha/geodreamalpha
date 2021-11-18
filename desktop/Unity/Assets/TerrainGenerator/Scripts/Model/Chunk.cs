using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace TerrainGeneratorComponent
{
	//this class manages a specific chunk instance (piece) of the map
	//
	class Chunk
	{
		public const int layer = 6;

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

		//the line length of the terrain
		public const int lineLength = 513;
		//the "corrected" world size of the terrain (its a unity thing)
		public static readonly Vector3 correctSize = new Vector3(32, 1200, 32);
		//the face count of the terrain
		public const int faceLength = 512;
		//the world size of the terrain divided by 2.
		public const int halfFaceLength = faceLength / 2;

		public bool isInstantiated
        {
			get { return terrainObject != null; }
        }

		public async void LoadAsync(int indexX, int indexY, MapAssets assets, Action<float, float, float[,], float[,,], int[][,], List<TreeInstance>, Vector3> Generate)
		{
            #region Initialize Water Object
			//this is the water plane that is only visible at low elevations
            //waterObject = GameObject.Find("Water");
            //waterObject.transform.localPosition = new Vector3(0f, seaLevel, 0f);
            #endregion

            #region Initialize Terrain Data
			//terrainData is a dependancy of terrain.
			//it manages the foilage (map textures, grass, and trees) and foilage settings of the terrain
            data = new TerrainData();
			data.terrainLayers = assets.GetLayers(); //map textures
			data.detailPrototypes = assets.GetDetailTextures(); //grass
			data.treePrototypes = assets.GetTrees(); //trees
			data.size = correctSize;
			data.heightmapResolution = lineLength;
			data.alphamapResolution = faceLength;
			data.SetDetailResolution(faceLength, 8);
			data.wavingGrassAmount = 0.2f;
			data.wavingGrassStrength = 0.5f;
            #endregion

            #region Initialize Procedural Generation Inputs
			//these fields hold the heightmap and foilage data that the procedural generation algorithm will populate
            detailLayers = new int[data.detailPrototypes.Length][,];
			trees = new List<TreeInstance> { };
			heightmap = new float[lineLength, lineLength];
			alphamapLayers = new float[faceLength, faceLength, data.terrainLayers.Length];
			for (int i = 0; i < detailLayers.Length; i++)
				detailLayers[i] = new int[faceLength, faceLength];
			trees.Clear();
            #endregion

            #region Call Procedural Generation Method Asynchronously
			//the inputs that were just initialized will pass by reference to this algorithm, which will
			//populate them with heightmap and foilage data for this specific chunk.
            //async
            var task2 = await Task.Run(() =>
			{
				Generate(indexX * faceLength, indexY * faceLength, heightmap, alphamapLayers, detailLayers, trees, correctSize);
				return 0;
			});
            #endregion
          
            #region Update Terrain Data
			//after procedural generation method is finished, its output will be
			//used to populate the terrain with heightmap and foilage data
            data.SetHeights(0, 0, heightmap);
			data.SetAlphamaps(0, 0, alphamapLayers);
			for (int i = 0; i < detailLayers.Length; i++)
            {
				data.SetDetailLayer(0, 0, i, detailLayers[i]);
			}
			data.SetTreeInstances(trees.ToArray(), true);
            #endregion

            #region Instantiate Terrain GameObject
			//this instantiates the terrain gameobject so that it shows in-game and sets important terrain GameObject data
            terrainObject = Terrain.CreateTerrainGameObject(data);
			SetCenterPosition(new Vector3(indexX * faceLength, 0f, indexY * faceLength));
			terrainObject.layer = layer;
			terrainObject.isStatic = true;
            #endregion

            #region Adjust Terrain Settings
			//adjust terrain settings
            terrain = terrainObject.GetComponent<Terrain>();
			terrain.detailObjectDistance = 250f;
			terrain.treeBillboardDistance = 500f;
			terrain.treeCrossFadeLength = 100f;
			terrain.heightmapPixelError = 5f;
            #endregion

            #region Refresh Terrain Configuration
			//updates changes made to terrain
            terrain.Flush();
			data.RefreshPrototypes();
            #endregion
        }//

        public void Destroy()
        {
			//destroys terrain chunk gameObject
			GameObject.Destroy(terrainObject);
        }

		void SetCenterPosition(Vector3 pos)
		{
			//sets center position of terrain chunk
			terrainObject.transform.position = new Vector3(pos.x - halfFaceLength, pos.y, pos.z - halfFaceLength);
		}//

		/// <summary>
		/// snaps character (such as player or enemy) to the height (y) of the terrain depending on their current (x) and (z) position so that they are properly grounded.
		/// </summary>
		/// <param name="controller">the character controller component of the character</param>
		public static void Snap(CharacterController controller)
		{
			//snaps character (such as player or enemy) to the height (y) of the terrain depending on their current (x) and (z) position so that they are properly grounded.
			RaycastHit hit;
			controller.Move(Vector3.up * 1300);
			Vector3 newPosition = controller.transform.position;
			
			Physics.Raycast(newPosition, Vector3.down, out hit, float.PositiveInfinity, LayerMask.GetMask("ground"), QueryTriggerInteraction.Ignore);
			newPosition.y = hit.point.y;
			controller.Move(-controller.transform.position + newPosition);
		}//
	}
}
