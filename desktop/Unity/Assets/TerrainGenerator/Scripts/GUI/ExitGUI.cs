using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TerrainGeneratorComponent;

namespace TerrainGeneratorComponent
{
    //Manages the in-game exit menu
    public class ExitGUI : MonoBehaviour
    {
        [SerializeField]
        GameObject damageMenu;

        public void OnResumeclick()
        {
            TerrainGenerator.SetMouseBehavior(false);          
            damageMenu.SetActive(true);
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void OnNewLocation()
        {
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene("TerrainGenerator/Scene/MenuScene");
        }

        public void OnReturnToLoginClick()
        {
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene("Main Menu");
        }

        public void OnQuitClick()
        {
            Application.Quit();
        }
    }
}
