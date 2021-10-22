using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TerrainGeneratorComponent
{
    //this class manages the in-game exit menu
    public class ExitGUI : MonoBehaviour
    {
        public void OnResumeclick()
        {
            TerrainGeneratorComponent.TerrainGenerator.isPaused = false;
            this.gameObject.SetActive(false);
        }

        public void OnNewLocation()
        {
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(0);
        }

        public void OnReturnToLoginClick()
        {
            //returns the user to the login screen
        }

        public void OnQuitClick()
        {
            Application.Quit();
        }
    }
}
