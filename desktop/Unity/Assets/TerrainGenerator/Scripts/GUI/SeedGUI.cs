using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

namespace TerrainGeneratorComponent
{
    //Manages the seed selection and building world menus
    class SeedGUI : MonoBehaviour
    {
        SeedData seedData = new SeedData();

        #region Load Screen Components
        [SerializeField]
        GameObject loadScreen;
        [SerializeField]
        Slider loadingBar;
        [SerializeField]
        TMP_Text progressPercent;
        #endregion

        #region Seed Input Screen Components
        [SerializeField]
        TMP_Text nameDisplay;
        [SerializeField]
        TMP_InputField seedInput;
        [SerializeField]
        Button generateButton;
        [SerializeField]
        TMP_Dropdown seedDropdown;
        [SerializeField]
        TMP_Text helpMessage;
        #endregion

        //hold the current seed value for the terrain generator
        public static string currentSeed { get; private set; }

        //used to determin what input from the user is considered valid
        Regex Validator = new Regex(@"^[0123456789]+[_]{1}[0123456789]+$");

        int firebaseCounter = 0;

        //returns a boolean value indicating if user input is valid or not
        bool seedInputIsInCorrectFormat
        {            
            get { return Validator.IsMatch(seedInput.text); }
        }

        void Start()
        {
            seedData.PullFromFirebase();
            loadScreen.SetActive(false);

            #region Initialize Seed Input Screen Components
            seedDropdown.options.Add(new TMP_Dropdown.OptionData("")); //this is default case for a user that has no previous seed values
            seedDropdown.options.Add(new TMP_Dropdown.OptionData(""));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData(""));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData(""));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData(""));
            seedDropdown.options.Add(new TMP_Dropdown.OptionData(""));
            seedDropdown.captionText.text = seedDropdown.options[0].text;
            seedInput.text = seedDropdown.captionText.text;
            #endregion

            Application.backgroundLoadingPriority = ThreadPriority.High;
        }

        public void Update()
        {
            for (int i = 0; i < 6; i++)
                if (seedDropdown.options[i].text != seedData.GetAt(i))
                {
                    UpdateDropDownValues();
                    break;
                }
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
                seedData.ReplaceWith(currentSeed);
                seedData.PushToFirebase();

                StartCoroutine(LoadGameLevelAsync());
            }
            else
                helpMessage.text = "Invalid Format: Seed input must contain two intergers separated by an underscore AND " +
                                   "each integer must be between the values -50001 and 50001.  Example: 123_-3670";        
        }

        public void OnSeedDropdownClick()
        {
            seedInput.text = seedDropdown.captionText.text;            
        }

        void UpdateDropDownValues()
        {
            seedDropdown.options[0] = (new TMP_Dropdown.OptionData(seedData.GetAt(0))); //this is default case for a user that has no previous seed values
            seedDropdown.options[1] = (new TMP_Dropdown.OptionData(seedData.GetAt(1)));
            seedDropdown.options[2] = (new TMP_Dropdown.OptionData(seedData.GetAt(2)));
            seedDropdown.options[3] = (new TMP_Dropdown.OptionData(seedData.GetAt(3)));
            seedDropdown.options[4] = (new TMP_Dropdown.OptionData(seedData.GetAt(4)));
            seedDropdown.options[5] = (new TMP_Dropdown.OptionData(seedData.GetAt(5)));
            seedDropdown.captionText.text = seedDropdown.options[0].text;
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