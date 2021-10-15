using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneratorComponent
{
    //Tyler Anderson
    public class TerrainGenerator : MonoBehaviour
    {
        //@Dr Layman: I put my fields in this class instead of a separate data model class because that makes the most sense to me.
        //I hope that is not an issue.  If so, please let me know and I will change this.
        [SerializeField]
        Transform player;

        Generator generator;

        Chunk chunk;

        bool initialGenerate;

        public static bool isPaused { get; set; } = false;

        GameObject exitMenu;

        // Start is called before the first frame update
        void Start()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(60f / 255f, 87f / 255f, 113f / 255f);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.fogDensity = 0.0035f;

            chunk = new Chunk(gameObject);

            exitMenu = GameObject.Find("Exit Menu");
            exitMenu.SetActive(false);

            isPaused = false;

            generator = new Generator();
            SetPlayerPositionFromString();
            initialGenerate = true;
            //chunk.ListenForRefresh(generator.Generate, player.transform, true, false);
        }

        // Update is called once per frame
        void Update()
        {
            chunk.ListenForRefresh(generator.Generate, player.transform, initialGenerate, false);
            initialGenerate = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitMenu.SetActive(true);
                isPaused = true;
            }
            
        }

        public void SetPlayerPositionFromString()
        {
            //going to need to check to see if position string is a valid position and correct format
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
            //@Dr Layman: this is the height data and tree locations that were referenced in one of the component cards.  This data is needed because
            //gameobjects need to spawn at the correct height of the terrain's ground and also do not need to spawn in a tree or rock, etc.
            //This method solves the same problem, but instead of returning height data and tree locations, it just returns the nearest available position that the
            //gameobject can safely be placed at.
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

