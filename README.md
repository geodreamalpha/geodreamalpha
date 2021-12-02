# CSC 450 - Team Project
## Team Members
Project Manager - Aaron Schwartz
Lead Developer - Tyler Anderson
Quality Assurance - Nick Preston
Developer - Jake Aldridge
Developer - Blake Ottinger

## Running end-to-end tests
First, you will need to install AltUnityTester if you haven't already. Pulling from main is not enough for it to be installed on your machine. Go here and make sure to download and import the package: https://assetstore.unity.com/packages/tools/utilities/altunity-tester-ui-test-automation-112101  

Next, open the project in Unity. Ensure that in the program's menubar is "AltUnity Tools". Click on this, and select "AltUnity Tester Editor"  
In this window, here are the settings you will need to run one particular test:
![image](https://user-images.githubusercontent.com/10481914/141410198-74c2d55f-effa-466f-b11f-50eb321aab60.png)

You can select any single test, or multiple and they will all run.
But it is advised that you do one at a time so you can see how the game responds.


## Running Android Espresso tests
(This section written by Aaron Schwartz)

To run the Android espresso tests, a new test configuration must be created. From the main Android Studio workplace, go to Run > Edit Configurations.

From here, hit the plus button in the top left corner and create a new Android Instrumented Test. Give it whatever name you want (I called mine "Caster Tests" as per a YouTube tutorial). From here, select the app as the Module, and click apply. Now you should be able to run "Caster Tests" in Android Studio to run all Android espresso tests one-by-one. 

Note: In order for the tests to work, you must have logged into the app with a fresh account first, and then closed the app without logging in. This is because the app pages that are tested are only accessible with a firebase account, and the logic to create/log into a firebase account cannot be accomplished with espresso instrumented tests.

The YouTube tutorial I originally used to learn this process can be found here (go to ~9:30): https://www.youtube.com/watch?v=TGU0B4qRlHY&t=736s