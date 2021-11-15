using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TerrainGeneratorComponent;

namespace TerrainGeneratorComponent
{
    //this class manages the in-game exit menu
    public class ExitGUI : MonoBehaviour
    {
        GameObject damageMenu;

        void Start()
        {
            damageMenu = GameObject.Find("DamageMenu");
        }

        public void OnResumeclick()
        {
            TerrainGenerator.ActiveMouse(false);          
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
            //returns the user to the login screen
        }

        public void OnQuitClick()
        {
            Application.Quit();
        }
    }
}
