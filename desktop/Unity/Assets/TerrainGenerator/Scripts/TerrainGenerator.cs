using UnityEngine;
using System.Collections.Generic;

namespace TerrainGeneratorComponent
{
    //Tyler Anderson
    //the facade class that other components will interact with
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField]
        CharacterController playerController;   

        [SerializeField]
        MapAssets assets;
        Generator generator = new Generator();
        ChunkManager chunkManager;

        [SerializeField]
        Music music;

        public GameObject exitMenu;
        public GameObject damageMenu;

        // Start is called before the first frame update
        void Start()
        {
            #region Initialize Fog Settings
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(102f / 255f, 157f / 255f, 195f / 255f) * 0.7f;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.fogDensity = 0.0035f;
            #endregion

            generator = new Generator();
            chunkManager = new ChunkManager();

            exitMenu = GameObject.Find("Exit Menu");
            damageMenu = GameObject.Find("DamageMenu");
            exitMenu.GetComponent<ExitGUI>().OnResumeclick();
            
            SetPlayerPositionFromString();
            SnapList.Add(playerController);

            music.ChooseStartMusic();
        }

        // Update is called once per frame
        void Update()
        {
            chunkManager.Refresh(playerController.transform.position, assets, generator.Generate);

            #region Display Exit Menu if Escape Key is Pressed
            if (Input.GetKeyDown(KeyCode.Escape))
                DisplayExitMenu();
            #endregion

            SnapList.UpdateSnaps();
        }

        public void RunAUnitTest(float worldX, float worldY, float[,] heightmap, float[,,] alphamap, int[][,] detailLayer, List<TreeInstance> instances, List<(int index, Vector3 position)> enemies)
        {           
            generator.Generate(worldX, worldY, heightmap, alphamap, detailLayer, instances, enemies, Chunk.correctSize);
        }

        public void SetPlayerPositionFromString()
        {
            //takes map seed and converts it into the initial position of the player
            string[] s = SeedGUI.currentSeed.Split('_');
            int x = int.Parse(s[0]);
            int z = int.Parse(s[1]);
            playerController.Move(-playerController.transform.position);
            playerController.Move(new Vector3(x, 1203f, z));
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

        public static void SetMouseBehavior(bool isNormal)
        {
            Cursor.visible = isNormal;
            Cursor.lockState = isNormal? CursorLockMode.None: CursorLockMode.Confined;
        }

        public void DisplayExitMenu()
        {
            Time.timeScale = 0f;
            exitMenu.SetActive(true);
            damageMenu.SetActive(false);
            SetMouseBehavior(true);
        }

        public void DisplayGameOver()
        {
            DisplayExitMenu();
            exitMenu.transform.Find("Resume Button").gameObject.SetActive(false);
            exitMenu.transform.Find("Game Over").gameObject.GetComponent<TMPro.TMP_Text>().text = "Game Over";
        }
    }
}

