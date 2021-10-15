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
			 
			Debug.Log("Login");
			// firebaseConnector.sendLogin(); 
			// return "Hello from Login";
			return;
		}
		
		public void Register () {
			 
			Debug.Log("Register");
			// Firebaseconnector.sendRegister(); 
			// return "Hello from Register";
			return;
		}
		
		public void ForgotPassword () {
			// return "Hello from Forgotpassword"; 
			Debug.Log("Register");
			return;
		}


		protected void fireBaseSendLogin() {
			return; 
			// Rest API calls. 
		}

		protected void fireBaseSendRegister() {
			return; 
		}

		protected void fireBaseSendPassword() {
			return; 
		}



}



