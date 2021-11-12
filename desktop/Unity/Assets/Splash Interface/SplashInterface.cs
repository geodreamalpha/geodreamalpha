using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// using Assets.Firebase.Firebase; 

public class SplashInterface : MonoBehaviour
{

	private static SplashInterface UniqueInstance;
	public Text MessageBox;
	public InputField EmailText;
	public InputField PasswordText;


	protected EventSystem system;
	public bool signedIn = false;
	public string uid;
	public string email;
	public Firebase fb = Firebase.GetInstance();
	protected int failedLoginsCount = 0;
	protected int maxLoginAttempts = 10;
	protected bool response_received = false;
	protected bool reg_received = false;
	public string reg_email;
	public string reg_password;
	public bool pwd_received = false;
	public bool pwd_success = false; 
	public bool reg_success; 
	public bool pwd_submit_received = false;
	public bool pwd_submit_success = false;
	public string objectName = "SplashInterface";

	// Overlay
	public InputField NewPassword;
	public InputField oobCode;
	public InputField NewPasswordConfirm;
	public GameObject ResetPasswordPanel;
	public Text MessageBoxPWD;

	// Deprecated. Firebase already has a web API to handle this. 
	public bool enablePasswordOverlay = false;

	// Constructor (required for unit testing)
	public static SplashInterface GetInstance()
	{
		if (UniqueInstance == null)
		{
			UniqueInstance = new SplashInterface();
		}
		return UniqueInstance;
	}

	// Start is called before the first frame update
	void Start()
	{
		MessageBox = GameObject.Find("Panel/MessageBox").GetComponent<Text>();
		EmailText = GameObject.Find("Panel/EmailForm").GetComponent<InputField>();
		PasswordText = GameObject.Find("Panel/PasswordField").GetComponent<InputField>();
		system = EventSystem.current;

		Debug.Log ("Launching Splash Interface");
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

		if (reg_received == true)
		{

			// If the login succeeds, we change the scene. 
			if (reg_success == true)
			{
				Debug.Log("Registration succeeded.");
				MessageBox.text = "Registration Succeeded. Please type in your login details to and sign in.";
				reg_received = true;
				SceneManager.LoadScene("TerrainGenerator/Scene/MenuScene");
			}

			else
			{
				Debug.Log("Registration failed. Are you sure this email isn't already registered? ");
				MessageBox.text = "Registration failed. Are you sure this email isn't already registered?";
				reg_received = false;
			}
		}

		// Check to see if the user requested a password reset. 
		if (pwd_received == true)
		{
			// Send a password reset code. 
			Debug.Log("Sent password reset code. Please check your email.");
			MessageBox.text = "Email sent. Please follow the link provided.";
			pwd_received = false;

			// Deprecated: Firebase handles UI stuff via built-in web interface. Link sent in email.
			if (enablePasswordOverlay == true)
			{ 
				OpenPasswordResetPanel();
			}
		}

		if (enablePasswordOverlay == true) {
			// Check to see if the user requested a password reset. 
			if (pwd_submit_received == true)
			{
				if (pwd_submit_success == true) {
					// We've reset the password. Now we close the panel and display success.  
					Debug.Log("Password successfully reset. Please log in.");

					// Ordinarily, we display messages on the overlay, but if there is a success, we do so on the parent panel because the overlay will be closed. 
					MessageBox.text = "Password successfully reset. Please log in.";
					pwd_submit_received = false;

					// Now, we close the overlay and allow them to log in. 
					OpenPasswordResetPanel(false);
				}
				else
				{
					// We use a different messagebox for the overlay. If we aren't closing the overlay, we need to use this instead. 
					setPwdMessage("Failed. Please try again.");
					Debug.Log("Failed to reset password");
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
			fireBaseSendLogin(EmailText.text, PasswordText.text, signedIn => {
				// Check if the login form was submitted. 

				// If the login succeeds, we change the scene. 
				if (signedIn == true)
				{
					Debug.Log("Login succeeded.");
					MessageBox.text = "Login Success";
					response_received = false;
					FadeStartMenu();
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
				
			});
			MessageBox.text = "Loading...";
		}
		return;
	}

	public void Register()
	{
		Debug.Log("Implementing registration. ");

		if (EmailText.text == "" || PasswordText.text == "")
		{
			MessageBox.text = "Sorry, but you must enter an email and a password to register.";
		}
		else
		{
			fireBaseSendRegister(EmailText.text, PasswordText.text);
			MessageBox.text = "Sending registration...";
		}
		return;
	}

	public void ForgotPassword()
	{
		// return "Hello from Forgotpassword"; 
		Debug.Log("Implementing forgotPassword. ");
		fireBaseSendPassword(EmailText.text);

		return; 
	}

	// Sends rest API calls, for firebase connector. 

	public delegate void GetLoginCallback(bool signedIn);

	public bool fireBaseSendLogin(string email, string password, GetLoginCallback callback)
	{
		Debug.Log("API call to send login through firebaseSendLogin()");

		fb.SignIn(email, password, res =>
		{
			signedIn = res.Success; 
			Debug.Log($"Debug Res.success: {res.Success}");
			uid = res.Uid; // # unique user id
			email = res.Email; // # user's email
			response_received = true;
			callback (signedIn);
		});

		return signedIn;
	}

	public bool fireBaseSendRegister(string email, string password)
	{
		Debug.Log("API call to send login through firebaseSendRegister()");

		fb.SignUp(email, password, res =>
		{
			reg_success = res.Success;
			reg_received = true;
			
		});

		return reg_success;
	}

	public bool fireBaseSendPassword(string email)
	{
		Debug.Log("API call to send login through firebaseSendPassword()");

		// First, we need to check and make sure that the user actually submitted an email. 
		if (EmailText.text != "")
        {
			fb.PasswordReset(email, res =>
			{
				pwd_success = res.Success;
				pwd_received = res.Success; 
				if (pwd_success == false) {
					Debug.Log("Failed to get password reset code");
					MessageBox.text = "Failed to get password reset code";
				}
			});
		}
		else
        {
			// If the user did not submit an email, we must prompt them to do so.
			MessageBox.text = "Please enter your email address and try again.";
        }
		return pwd_success; 
	}


	// Deprecated: Firebase already handles this via a built-in web UI. 
	public void ForgotPasswordSubmit()
	{
		Debug.Log("Front end UI for password submit, verifying data.");

		// First, we grab data from our form. 
		oobCode = GameObject.Find("Panel/ResetPasswordPanel/oobCode").GetComponent<InputField>();
		NewPassword = GameObject.Find("Panel/ResetPasswordPanel/NewPassword").GetComponent<InputField>();
		NewPasswordConfirm = GameObject.Find("Panel/ResetPasswordPanel/NewPasswordConfirm").GetComponent<InputField>();

		// Now, we must check and make sure the passwords match, etc. 
		if (NewPassword.text != NewPasswordConfirm.text)
        {
			Debug.Log("Passwords don't match");
			setPwdMessage("Passwords don't match.");
        }
		else if (oobCode.text == "") {
			Debug.Log ("Missing OOBcode");
			setPwdMessage("You must enter a verification code");
        }
		else
        {
			// No errors, so we proceed to submit. 
			processPasswordSubmit(oobCode.text, NewPassword.text);
        }
		return; 
	}

	// Deprecated: Firebase already handles this via built-in web UI. 
	public bool processPasswordSubmit(string oobCode, string newPassword)
	{
		Debug.Log ("Backend password submit, attempting to change password via Firebase API call");
		fb.PasswordResetSubmit(oobCode, newPassword, res =>
		{
			pwd_submit_success = res.Success;
			pwd_submit_received = res.Success;

			// For debug purposes, UI is handled in Update(); 
			if (pwd_submit_success == false)
			{
				Debug.Log("failed to change password.");
			}
			else
			{
				Debug.Log("Successfully changed password");
			}
		});

		return pwd_submit_received;
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

	// Opens the password reset overlay. 
	public void OpenPasswordResetPanel (bool openAction = true)
    {
		if (ResetPasswordPanel != null)
        {
			ResetPasswordPanel.SetActive (openAction); 
        }
    }

	public void setPwdMessage (string message)
    {
		MessageBoxPWD = GameObject.Find("Panel/ResetPasswordPanel/MessageBoxPWD").GetComponent<Text>();
		MessageBoxPWD.text = message;
	}
}