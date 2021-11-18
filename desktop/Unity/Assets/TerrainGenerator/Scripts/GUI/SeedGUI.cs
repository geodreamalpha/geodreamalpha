using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

namespace TerrainGeneratorComponent
{
    //
    class SeedGUI : MonoBehaviour
    {
        #region Load Screen Components
        GameObject loadScreen;
        Slider loadingBar;
        TMP_Text progressPercent;
        #endregion

        #region Seed Input Screen Components
        TMP_Text nameDisplay;
        TMP_InputField seedInput;
        Button generateButton;
        TMP_Dropdown seedDropdown;
        TMP_Text helpMessage;
        #endregion

        //hold the current seed value for the terrain generator
        public static string currentSeed { get; private set; }

        //used to determin what input from the user is considered valid
        Regex Validator = new Regex(@"^[0123456789]+[_]{1}[0123456789]+$");

        //returns a boolean value indicating if user input is valid or not
        bool seedInputIsInCorrectFormat
        {            
            get { return Validator.IsMatch(seedInput.text); }
        }

        void Start()
        {
            #region Initializes Load Screen Components
            loadScreen = GameObject.Find("Load Screen");
            loadingBar = loadScreen.GetComponentInChildren<Slider>();
            progressPercent = GameObject.Find("Progress Percentage").GetComponent<TMP_Text>();
            loadScreen.SetActive(false);
            #endregion

            #region Initialize Seed Input Screen Components
            nameDisplay = GetComponentInChildren<TMP_Text>();
            seedInput = GetComponentInChildren<TMP_InputField>();
            generateButton = GetComponentInChildren<Button>();
            seedDropdown = GetComponentInChildren<TMP_Dropdown>();

            seedDropdown.options.Add(new TMP_Dropdown.OptionData("0_0")); //this is default case for a user that has no previous seed values
            seedDropdown.options.Add(new TMP_Dropdown.OptionData("12345_0"));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData("0_12345"));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData("5000_5000"));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData("10000_10000"));
            seedDropdown.captionText.text = seedDropdown.options[0].text;
            seedInput.text = seedDropdown.captionText.text;
            #endregion

            Application.backgroundLoadingPriority = ThreadPriority.High;
        }

        public void OnSeedInputChanged()
        {

        }

        public void OnGenerateButtonClick()
        {
            string[] integers = seedInput.text.Split('_');

            //ensures seed input is in correct format and is within valid game map range
            if (seedInputIsInCorrectFormat && Mathf.Abs(int.Parse(integers[0])) < 50001 && Mathf.Abs(int.Parse(integers[1])) < 50001)
            {
                currentSeed = seedInput.text;               
                StartCoroutine(LoadGameLevelAsync());

            }
            else
                Debug.LogError("Invalid Format: Seed input must contain two intergers separated by an underscore AND each integer must be between the values -50001 and 50001.  Example: 123_-3670");         
        }

        public void OnSeedDropdownClick()
        {
            seedInput.text = seedDropdown.captionText.text;
        }

        IEnumerator LoadGameLevelAsync()
        {
            loadScreen.SetActive(true);
            
            Resources.UnloadUnusedAssets();
            AsyncOperation gameScene = SceneManager.LoadSceneAsync("TerrainGenerator/Scene/GameScene", LoadSceneMode.Single);

            while (!gameScene.isDone)
            {
                loadingBar.value = gameScene.progress;
                progressPercent.text = (gameScene.progress * 100f).ToString() + " %";
                yield return null;
            }          
        }
    }
}