using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashInterface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// Debug.Log("Testing from update in Splashinterface.");
    }

        /// <summary>
        /// Gets the "Hello from" string of this component
        /// </summary>
        /// <returns> A string that introduces this component </returns>
        public string Hello()
        {
            return "Hello from Component SplashScreen";
        }
        
        public void Login () {
			Debug.Log("Implementing Login. ");
			self.fireBaseSendLogin(); 
			return;
		}
		
		public void Register () {
			Debug.Log("Implementing registration. ");
			self.fireBaseSendRegister(); 
			return;
		}
		
		public void ForgotPassword () {
			// return "Hello from Forgotpassword"; 
			Debug.Log("Implementing forgotPassword. ");
			self.fireBaseSendPassword(); 
			return;
		}

		// Rest API calls, for firebase connector. 

		protected void fireBaseSendLogin() {
			Debug.Log("API call to send login through firebaseSendLogin()");
			return; 
			// Rest API calls. 
		}

		protected void fireBaseSendRegister() {
			Debug.Log("API call to send login through firebaseSendRegister()");
			
			return; 
		}

		protected void fireBaseSendPassword() {
			Debug.Log("API call to send login through firebaseSendPassword()");
			return; 

		}
		
		protected void failedLogin() {
			Debug.Log("Login failed, cleaing fields. ");
			return; 			
		}



}



