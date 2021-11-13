using UnityEngine;

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
        Generator generator;
        ChunkManager chunkManager;

        GameObject exitMenu;
        GameObject damageMenu;
        bool playerIsInitiallyGrounded = false;

        // Start is called before the first frame update
        void Start()
        {
            #region Initialize Fog Settings
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(78f / 255f, 107f / 255f, 135f / 255f);  //new Color(60f / 255f, 87f / 255f, 113f / 255f)
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.fogDensity = 0.0035f;
            #endregion

            generator = new Generator();
            chunkManager = new ChunkManager();

            exitMenu = GameObject.Find("Exit Menu");
            damageMenu = GameObject.Find("DamageMenu");
            exitMenu.GetComponent<ExitGUI>().OnResumeclick();            
            
            SetPlayerPositionFromString();
            playerIsInitiallyGrounded = false;
        }

        // Update is called once per frame
        void Update()
        {
            chunkManager.Refresh(playerController, assets, generator.Generate);

            #region Display Exit Menu if Escape Key is Pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitMenu.SetActive(true);
                damageMenu.SetActive(false);                      
                ActiveMouse(true);
            }
            #endregion

            #region Attempt to Snap Player to Appropriate Map Height Each Frame Until Player Is Grounded
            //unity's terrain component takes an arbitrary amount of frames to completely instantiate terrain chunks
            //this method appripriates as much time as it needs by delaying gameplay an arbitrary amount of frames until player is properly grounded to the active terrain chunk
            if (Input.GetKeyDown(KeyCode.P) || !playerIsInitiallyGrounded)
            {
                Chunk.Snap(playerController);
                if (playerController.isGrounded)
                {
                    playerIsInitiallyGrounded = true;
                    Debug.Log("grounded");
                }
                    
            }
            #endregion
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

        public static void ActiveMouse(bool isActive)
        {
            Cursor.visible = isActive;
            Cursor.lockState = isActive? CursorLockMode.None: CursorLockMode.Locked;
        }
    }
}

