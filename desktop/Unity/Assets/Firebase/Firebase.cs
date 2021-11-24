using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using Newtonsoft.Json;
public class Firebase
{

    private static Firebase UniqueInstance;
    static string FSApiURL = "https://firestore.googleapis.com/v1/";
    static string ProjectID = "foot-leprechauns";
    string FSAuthURL = "https://identitytoolkit.googleapis.com/v1/accounts";
    string FSBaseURL = $"{FSApiURL}projects/{ProjectID}/databases/(default)/documents/users/";
    string CurrUserId;
    string API_KEY = "AIzaSyD3S-N_VOPv_zGVWOUgv2fPT0SgkSUzPaY";

    private Firebase() {}

    public static Firebase GetInstance()
    {
        if(UniqueInstance == null)
        {
            UniqueInstance = new Firebase();
        }
        return UniqueInstance;
    }

    public bool IsAuthenticated()
    {
        return this.CurrUserId != null;
    }

    public delegate void GetDocCallback(Document doc);
    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="docPath"> Path of the document to get </param>
    /// <param name="callback"> What to do after the document is retrieved </param>
    public void GetDoc(string docPath, GetDocCallback callback)
    {
        if(IsAuthenticated())
        {
            RestClient.Get($"{this.FSBaseURL}{this.CurrUserId}/{docPath}").Then(res =>
            {
                Document doc = JsonConvert.DeserializeObject<Document>(res.Text);
                callback(doc);
            }).Catch(err=>
            {
                Document doc = new Document();
                callback(doc);
            });
        }
    }

    public delegate void UpdateIntFieldCallback(UpdateDocRes res);
    /// <summary>
    /// Patches a document's given integer field
    /// </summary>
    /// <param name="docPath"> Path of the document to patch </param>
    /// <param name="fieldName"> Name of the integer field </param>
    /// <param name="value"> The integer value to update the field with </param>
    /// <param name="callback"> What to do after the document is patched </param>
    public void UpdateIntField(string docPath, string fieldName, int value, UpdateIntFieldCallback callback)
    {
        if(IsAuthenticated())
        {
            string body = "{\r\n  \"writes\": [\r\n    {\r\n    \t\"updateMask\": {\r\n            \"fieldPaths\": [\r\n                \""+fieldName+"\"\r\n            ]\r\n  \t\t},\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+this.CurrUserId+docPath+"\",\r\n\t  \t\t\"fields\": {\r\n              \t\""+fieldName+"\": {\r\n                  \"integerValue\": \""+value.ToString()+"\"\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    }\r\n  ]\r\n}";
            RestClient.Post($"{FSApiURL}projects/{ProjectID}/databases/(default)/documents:commit/?key={this.API_KEY}", body).Then(res=>{
                // Debug.Log(res.Text);
            }).Catch(err=>{
                // var error = err as RequestException;
                // Debug.Log(error.Response);
            });
            // Debug.Log(body);
        }
    }

    public delegate void UpdateStrFieldCallback(UpdateDocRes res);
    /// <summary>
    /// Patches a document's given string field
    /// </summary>
    /// <param name="docPath"> Path of the document to patch </param>
    /// <param name="fieldName"> Name of the string field </param>
    /// <param name="value"> The string value to update the field with </param>
    /// <param name="callback"> What to do after the document is patched </param>
    public void UpdateStrField(string docPath, string fieldName, string value, UpdateStrFieldCallback callback)
    {
        if(IsAuthenticated())
        {
            string body = "{\r\n  \"writes\": [\r\n    {\r\n    \t\"updateMask\": {\r\n            \"fieldPaths\": [\r\n                \""+fieldName+"\"\r\n            ]\r\n  \t\t},\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+this.CurrUserId+docPath+"\",\r\n\t  \t\t\"fields\": {\r\n              \t\""+fieldName+"\": {\r\n                  \"stringValue\": \""+value.ToString()+"\"\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    }\r\n  ]\r\n}";
            RestClient.Post($"{FSApiURL}projects/{ProjectID}/databases/(default)/documents:commit/?key={this.API_KEY}", body).Then(res=>{
                // Debug.Log(res.Text);
            }).Catch(err=>{
                // var error = err as RequestException;
                // Debug.Log(error.Response);
            });
            // Debug.Log(body);
        }
    }

    public delegate void GetSignInResCallback(SignInRes signInRes);
    /// <summary>
    /// Authenticates a firebase user account with email and password
    /// </summary>
    /// <param name="userEmail"> Email of the user that we are signing into </param>
    /// <param name="userPassword"> Password of the user that we are signing into </param>
    /// <param name="callback"> What to do after we attempt to authenticate </param>
    public void SignIn(string userEmail, string userPassword, GetSignInResCallback callback)
    {
            //Create request body
            SignInReq req = new SignInReq();
            req.email = userEmail;
            req.password = userPassword;

            //Make HTTP Request
            RestClient.Post($"{this.FSAuthURL}:signInWithPassword?key={this.API_KEY}", req).Then(res =>
            {
                SignInSuccessRes response = JsonConvert.DeserializeObject<SignInSuccessRes>(res.Text);
                SignInRes fullResponse = new SignInRes();
                fullResponse.Success = true;
                fullResponse.Uid = response.localId;
                fullResponse.Email = response.email;
                fullResponse.Token = response.idToken;
                fullResponse.RefreshToken = response.refreshToken;
                fullResponse.ExpiresIn = response.expiresIn;

                //Sets current user
                this.CurrUserId = response.localId;

                callback(fullResponse);
            }).Catch(err=>{
                SignInRes fullResponse = new SignInRes();
                fullResponse.Success = false;
                callback(fullResponse);
            });
    }

    public delegate void GetSignUpResCallback(SignUpRes signUpRes);
    /// <summary>
    /// Registers a new user in firebase
    /// </summary>
    /// <param name="userEmail"> Email of the user that we want to register </param>
    /// <param name="userPassword"> Chosen password of the user that we want to register </param>
    /// <param name="callback"> What to do after registration is attempted </param>
    public void SignUp(string userEmail, string userPassword, GetSignUpResCallback callback)
    {
            //Create request body
            SignInReq req = new SignInReq();
            req.email = userEmail;
            req.password = userPassword;

            //Make HTTP Request
            RestClient.Post($"{this.FSAuthURL}:signUp?key={this.API_KEY}", req).Then(res =>
            {
                SignUpSuccessRes successResponse = JsonConvert.DeserializeObject<SignUpSuccessRes>(res.Text);
                SignUpRes response = new SignUpRes();
                response.Success = true;
                response.Uid = successResponse.localId;
                response.Email = successResponse.email;
                CreateUserData(response.Email, response.Uid);
                this.CurrUserId = response.Uid;
                callback(response);
            }).Catch(err=>{
                var error = err as RequestException;
                SignUpErrorBody res = JsonConvert.DeserializeObject<SignUpErrorBody>(error.Response);
                SignUpRes response = new SignUpRes();
                response.Success = false;
                response.ErrorMessage = res.error.message;
                callback(response);
            });
    }

    /// <summary>
    /// Creates the necessary user data structure within firestore for a given user
    /// </summary>
    /// <param name="userEmail"></param>
    /// <param name="userId"></param>
    private void CreateUserData(string userEmail, string userId)
    {
        string jsonReq = "{\r\n  \"writes\": [\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"userEmail\": {\r\n\t\t\t      \"stringValue\": \""+userEmail+"\"\r\n\t\t\t\t},\r\n              \t\"userId\": {\r\n                  \"stringValue\": \""+userId+"\"\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    },\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"/playerStats/0\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"currHP\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"currSTM\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"level\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"maxHP\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"maxSTM\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"speed\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"strength\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"xp\": {\r\n                  \"integerValue\": 0\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    },\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"/compStats/0\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"level\": {\r\n\t\t\t      \"integerValue\": 1\r\n\t\t\t\t},\r\n              \t\"speed\": {\r\n                  \"integerValue\": 1\r\n                },\r\n              \t\"strength\": {\r\n                  \"integerValue\": 1\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    },\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"/worlds/0\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"seed\": {\r\n\t\t\t      \"stringValue\": \"\"\r\n\t\t\t\t}\r\n  \t\t\t}\r\n    \t}\r\n    }\r\n  ]\r\n}";
        RestClient.Post($"https://firestore.googleapis.com/v1/projects/{ProjectID}/databases/(default)/documents:commit/?key={this.API_KEY}", jsonReq).Then(res=>{
            // Debug.Log(res.Text);
        });
    }

    public delegate void GetPasswordResetResCallback(PasswordResetRes PasswordResetRes);
    /// <summary>
    /// Requests a password reset code for the user
    /// </summary>
    /// <param name="userEmail"> Email of the user that needs a code </param>
    /// <param name="callback"> What to do after the code is generated successfully </param>
    public void PasswordReset(string userEmail, GetPasswordResetResCallback callback)
    {
        //Create request body
        PasswordResetReq req = new PasswordResetReq();
        req.email = userEmail;
        req.requestType = "PASSWORD_RESET";
 
        //Make HTTP Request
        RestClient.Post($"{this.FSAuthURL}:sendOobCode?key={this.API_KEY}", req).Then(res =>
        {
            PasswordResetSuccessRes response = JsonConvert.DeserializeObject<PasswordResetSuccessRes>(res.Text);
            PasswordResetRes fullResponse = new PasswordResetRes();
            fullResponse.Success = true;
 
            callback(fullResponse);
        }).Catch(err => {
            PasswordResetRes fullResponse = new PasswordResetRes();
            fullResponse.Success = false;
            callback(fullResponse);
        });
    }
 
    // Deprecated: Firebase already handles this via web UI. 
    public delegate void GetPasswordResetSubmitResCallback(PasswordResetSubmitRes PasswordResetSubmitRes);
    /// <summary>
    /// Sets a new password for the user that requests a reset code
    /// </summary>
    /// <param name="oobCode"> The password reset code </param>
    /// <param name="newPassword"> The new password </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public void PasswordResetSubmit(string oobCode, string newPassword, GetPasswordResetSubmitResCallback callback)
    {
        //Create request body
        PasswordResetSubmitReq req = new PasswordResetSubmitReq();
        req.newPassword = newPassword;
        req.oobCode = oobCode;
        // req.requestType = "PASSWORD_RESET";
 
        //Make HTTP Request
        RestClient.Post($"{this.FSAuthURL}:resetPassword?key={this.API_KEY}", req).Then(res =>
        {
            PasswordResetSubmitSuccessRes response = JsonConvert.DeserializeObject<PasswordResetSubmitSuccessRes>(res.Text);
            PasswordResetSubmitRes fullResponse = new PasswordResetSubmitRes();
            fullResponse.Success = true;
 
            callback(fullResponse);
        }).Catch(err => {
            PasswordResetSubmitRes fullResponse = new PasswordResetSubmitRes();
            fullResponse.Success = false;
            callback(fullResponse);
        });
    }
}
