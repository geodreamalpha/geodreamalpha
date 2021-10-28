using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SplashInterface : MonoBehaviour
{
	public Text MessageBox;
	public InputField EmailText;
	public InputField PasswordText;
	protected EventSystem system; 

	// Start is called before the first frame update
	void Start() {
		MessageBox = GameObject.Find("Panel/MessageBox").GetComponent<Text>();
		EmailText = GameObject.Find("Panel/EmailForm").GetComponent<InputField>();
		PasswordText = GameObject.Find("Panel/PasswordField").GetComponent<InputField>();
		system = EventSystem.current;
	}

	// Update is called once per frame
	void Update() {

		// This is necessary to allow users to tab between inputs.
		// Source: https://forum.unity.com/threads/tab-between-input-fields.263779/

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

			if (next != null)
			{

				InputField inputfield = next.GetComponent<InputField>();
				if (inputfield != null)
					inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

				system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
			}
			//else Debug.Log("next nagivation element not found");

		}
}

public string Hello() {
        return "Hello from Component SplashScreen";
    }
        
    public void Login () {
		Debug.Log("Implementing Login. ");
		bool loginToken = fireBaseSendLogin(); 

		if (EmailText.text == "" || PasswordText.text == "")
        {
			MessageBox.text = "Please enter an email and password to log in.";
        }
		else
		{
			// If the login succeeds, we change the scene. 
			if (loginToken == true)
			{
				Debug.Log("Login succeeded.");
				MessageBox.text = "Login Success"; 
				SceneManager.LoadScene("Game");
			} 
			
			// In the event of a login failure, we display a message and prompt the user to try again. 
			else
			{
				Debug.Log("Login failed");
				MessageBox.text = "Login Failed"; 
				return; 
			}
		}

		return;	
	}
		
	public void Register () {
		Debug.Log("Implementing registration. ");
		bool registrationToken = fireBaseSendRegister();

		if (EmailText.text == "" || PasswordText.text == "")
		{
			MessageBox.text = "Sorry, but you must enter an email and a password to register.";
		}
		else {
			if (registrationToken == false) {
				MessageBox.text = "Registration failed - email already registered. Please try again.";
			}
			else {
				MessageBox.text = "Registration succeeded";
				// SceneManager.LoadScene("Game");
				FadeStartMenu();
			}
		}
		return;
	}
		
	public void ForgotPassword () {
		// return "Hello from Forgotpassword"; 
		Debug.Log("Implementing forgotPassword. ");
		bool passwordToken = fireBaseSendPassword();

		if (passwordToken == false) {
			MessageBox.text = "Could not reset password. Email does not exist.";
		}
		else {
			MessageBox.text = "An email has been sent to reset your password.";
		}
	return;
	}

	// Rest API calls, for firebase connector. 

	protected bool fireBaseSendLogin() {
		Debug.Log("API call to send login through firebaseSendLogin()");
		return true; 
		// Rest API calls. 
	}

	protected bool fireBaseSendRegister() {
		Debug.Log("API call to send login through firebaseSendRegister()");
			
		return true; 
	}

	protected bool fireBaseSendPassword() {
		Debug.Log("API call to send login through firebaseSendPassword()");
		return true; 

	}


	// Allows a clean fade from the login screen to a loading splash page for registration, etc. 
	public void FadeStartMenu ()
    {
		StartCoroutine(DoFade());
    }

	IEnumerator DoFade()
    {
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha > 0 ) {
			canvasGroup.alpha -= Time.deltaTime / 2;
			yield return null;
		}
		canvasGroup.interactable = false;
		yield return null;
    }
}