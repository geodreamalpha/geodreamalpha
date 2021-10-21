using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SplashInterface : MonoBehaviour
{
	public Text MessageBox;
	public InputField EmailText;
	public InputField PasswordText;

	// Start is called before the first frame update
	void Start() {
		MessageBox = GameObject.Find("Panel/MessageBox").GetComponent<Text>();
		EmailText = GameObject.Find("Panel/EmailForm").GetComponent<InputField>();
		PasswordText = GameObject.Find("Panel/PasswordField").GetComponent<InputField>();
	}

	// Update is called once per frame
	void Update() {
		// Debug.Log("Testing from update in Splashinterface.");
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

		if (registrationToken == false) {
			MessageBox.text = "Registration failed.";
		}
		else {
			MessageBox.text = "Registration succeeded";
		}
		return;
	}
		
	public void ForgotPassword () {
		// return "Hello from Forgotpassword"; 
		Debug.Log("Implementing forgotPassword. ");
		bool passwordToken = fireBaseSendPassword();

		if (passwordToken == false) {
			MessageBox.text = "Could not reset password.";
		}
		else {
			MessageBox.text = "Password reset successfully.";
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
		
	protected void failedLogin() {
		Debug.Log("Login failed, cleaing fields. ");
		return; 			
	}
}