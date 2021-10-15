using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneratorComponent
{
    //Tyler Anderson
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField]
        Transform player;

        Generator generator;

        ChunkManager chunkManager;

        [SerializeField]
        MapAssets assets;

        public static bool isPaused { get; set; } = false;

        GameObject exitMenu;

        // Start is called before the first frame update
        void Start()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(60f / 255f, 87f / 255f, 113f / 255f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.fogDensity = 0.0035f;

            chunkManager = new ChunkManager();

            exitMenu = GameObject.Find("Exit Menu");
            exitMenu.SetActive(false);

            isPaused = false;

            generator = new Generator();
            SetPlayerPositionFromString();
        }

        // Update is called once per frame
        void Update()
        {
            chunkManager.Refresh(player.position, assets, generator.Generate);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitMenu.SetActive(true);
                isPaused = true;
            }
            
        }

        public void SetPlayerPositionFromString()
        {
            string[] s = SeedGUI.currentSeed.Split('_');
            int x = int.Parse(s[0]);
            int z = int.Parse(s[1]);
            player.position = new Vector3(x, player.position.y, z);
        }//

        /// <summary>
        /// Checks if gameobject is: 1) spawning at a position that is level with the maps height data and 2) spawning at a position that will not collide with known terrain objects (such as trees)
        /// </summary>
        /// <param name="position"> The requestng position that the gameobject wants to be placed at </param>
        /// <returns> The nearest available position that meets these requirements </returns>
        public float GetNearestPosition(Vector3 position)
        {
            return 0.0f;
        }//

        /// <summary>
        /// Gets the name of the biome the player is currently on.
        /// </summary>
        /// <returns> The name of the biome as a string </returns>
        public string GetBiomeName()
        {
            return "biome name";
        }//

        /// <summary>
        /// Gets the "Hello from" string of this component
        /// </summary>
        /// <returns> A string that introduces this component </returns>
        public string Hello()
        {
            return "Hello from Component TerrainGenerator";
        }
    }
}

