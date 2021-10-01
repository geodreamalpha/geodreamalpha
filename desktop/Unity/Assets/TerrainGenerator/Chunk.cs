using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneratorComponent
{
	public class Chunk
	{
		//stores the height for each y,x vertex of the terrain
		float[,] heightmap;
		//stores the texture maps of the terrain
		float[,,] alphamapLayers;
		//stores the grass/ flower/ small plants/ small rocks detail of the terrain
		int[][,] detailLayers;
		//stores the trees, big plants, and big rocks of the terrain
		List<TreeInstance> trees;

		//the gameobjec the Terrain Component is attached to
		GameObject gameObject;
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


		public Chunk(GameObject gameObject, Vector3 pos)
		{


		}//

		bool trueOnFirstFrame = true;

		//when play ventures too far from the map, the maps data will be recalculated and will re-align the map's center with players position
		//the maps data will be loaded asynchronously.
		public void RefreshChunk(Vector3 currentPlayerPosition)
		{

		}
	}
}

