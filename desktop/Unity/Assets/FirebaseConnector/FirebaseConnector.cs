using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirebaseConnectorComponent
{
    //Nick Preston
    public class FirebaseConnector : MonoBehaviour
    {
        /// <summary>
        /// Returns a string that introduces the component.
        /// </summary>
        /// <returns>String introducing the component.</returns>
        string Hello() {
            return "Hello from Component FirebaseConnector";
        }

        /// <summary>
        /// Logins into a user account on Firebase
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>true if successful, false otherwise</returns>
        bool Login(string email, string password) {
            return true;
        }

        /// <summary>
        /// Logs out of the currently logged in account
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        bool Logout() {
            return true;
        }
        
        /// <summary>
        /// Sends password reset email to the email entered if the account is registered
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if successful, false otherwise</returns>
        bool ResetPassword(string email) {
            return true;
        }

        /// <summary>
        /// Writes an entire document with all fields to Firestore, to a particular user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="values"></param>
        /// <returns>true if successful, false otherwise</returns>
        bool WriteDoc(string email, Dictionary<string, object> values) {
            return true;
        }

        /// <summary>
        /// Gets an entire document's data of a particular user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The dictionary of key-value pairs from the document</returns>
        Dictionary<string, object> GetDoc(string email) {
            return null;
        }

        /// <summary>
        /// Writes to a field in a particular key of a particular user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true if successful, false otherwise</returns>
        bool WriteField(string email, string key, object value) {
            return true;
        }

        /// <summary>
        /// Gets a fields data of a specific key of a particular user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="key"></param>
        /// <returns>The object containing the data</returns>
        object GetField(string email, string key) {
            return null;
        }
    }
}