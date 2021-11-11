using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// using Assets.Firebase.Firebase; 

public class SplashInterface : MonoBehaviour
{
	public Text MessageBox;
	public InputField EmailText;
	public InputField PasswordText;
	protected EventSystem system;
	public bool signedIn = false;
	public string uid;
	public string email;
	public Firebase fb;
	protected int failedLoginsCount = 0;
	protected int maxLoginAttempts = 10;
	public string objectName = "SplashInterface";
	protected bool response_received = false;

	// Start is called before the first frame update
	void Start()
	{
		MessageBox = GameObject.Find("Panel/MessageBox").GetComponent<Text>();
		EmailText = GameObject.Find("Panel/EmailForm").GetComponent<InputField>();
		PasswordText = GameObject.Find("Panel/PasswordField").GetComponent<InputField>();
		system = EventSystem.current;

		fb = Firebase.GetInstance();
	}

	// Update is called once per frame
	void Update()
	{

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

		if (response_received == true)
		{

			// If the login succeeds, we change the scene. 
			if (signedIn == true)
			{
				Debug.Log("Login succeeded.");
				MessageBox.text = "Login Success";
				response_received = false;
				SceneManager.LoadScene("TerrainGenerator/Scene/MenuScene");
			}

			// In the event of a login failure, we display a message and prompt the user to try again. 
			else
			{
				// We must keep track of how many times they've logged in. 
				failedLoginsCount = failedLoginsCount + 1;
				response_received = false;
				if (failedLoginsCount > maxLoginAttempts)
				{
					Debug.Log("Sorry, but we could not log you in because you failed too many times. Please restart the application and try again.");
					MessageBox.text = "Sorry, but you have failed too many login attempts. Please restart game and try again";
				}
				else
				{
					Debug.Log("Login failed. Please check your email or password. ");
					MessageBox.text = "Login Failed. Please check your email or password.";
				}
			}
		}

	}

	public string Hello()
	{
		return "Hello from Component SplashScreen";
	}

	public void Login()
	{
		Debug.Log("Implementing Login. ");

		// We need to make sure not to allow any more attempts if they've exceeded the max allowed.
		// If we reach this, it's because the user received a message already but still tried to login. 
		if (failedLoginsCount > maxLoginAttempts)
		{
			Debug.Log("Sorry, but we could not log you in because you failed too many times. Please restart the application and try again.");
			MessageBox.text = "Sorry, but you have failed too many login attempts. Please restart game and try again";
		}

		if (EmailText.text == "" || PasswordText.text == "")
		{
			MessageBox.text = "Please enter an email and password to log in.";
		}

		else
		{
			// This sends the actual login and attempts to authenticate the user. 
			fireBaseSendLogin(EmailText.text, PasswordText.text);
			MessageBox.text = "Loading...";
		}
		return;
	}

	public void Register()
	{
		Debug.Log("Implementing registration. ");
		bool registrationToken = fireBaseSendRegister();

		if (EmailText.text == "" || PasswordText.text == "")
		{
			MessageBox.text = "Sorry, but you must enter an email and a password to register.";
		}
		else
		{
			if (registrationToken == false)
			{
				MessageBox.text = "Registration failed - email already registered. Please try again.";
			}
			else
			{
				MessageBox.text = "Registration succeeded";
				// SceneManager.LoadScene("Game");
				FadeStartMenu();
			}
		}
		return;
	}

	public void ForgotPassword()
	{
		// return "Hello from Forgotpassword"; 
		Debug.Log("Implementing forgotPassword. ");
		bool passwordToken = fireBaseSendPassword();

		if (passwordToken == false)
		{
			MessageBox.text = "Could not reset password. Email does not exist.";
		}
		else
		{
			MessageBox.text = "An email has been sent to reset your password.";
		}
		return;
	}

	// Rest API calls, for firebase connector. 

	protected void fireBaseSendLogin(string email, string password)
	{
		Debug.Log("API call to send login through firebaseSendLogin()");

		// This code goes your login method or code block
		fb.SignIn(email, password, res =>
		{
			signedIn = res.Success; // # true if login worked
			uid = res.Uid; // # unique user id
			email = res.Email; // # user's email
			response_received = true;
		});

		return;
		// return true; 
		// Rest API calls. 
	}

	protected bool fireBaseSendRegister()
	{
		Debug.Log("API call to send login through firebaseSendRegister()");

		return true;
	}

	protected bool fireBaseSendPassword()
	{
		Debug.Log("API call to send login through firebaseSendPassword()");
		return true;

	}


	// Allows a clean fade from the login screen to a loading splash page for registration, etc. 
	public void FadeStartMenu()
	{
		StartCoroutine(DoFade());
	}

	IEnumerator DoFade()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime / 2;
			yield return null;
		}
		canvasGroup.interactable = false;
		yield return null;
	}
}