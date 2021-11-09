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
    string API_KEY = System.IO.File.ReadAllText("Assets/Firebase/apikey.0");

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
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public void GetDoc(string docPath, GetDocCallback callback)
    {
        if(IsAuthenticated())
        {
            RestClient.Get($"{this.FSBaseURL}{this.CurrUserId}/{docPath}").Then(res =>
            {
                Document doc = JsonConvert.DeserializeObject<Document>(res.Text);
                callback(doc);
            });
        }
        
    }

    public delegate void GetSignInResCallback(SignInRes signInRes);
    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
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
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
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

    private void CreateUserData(string userEmail, string userId)
    {
        string jsonReq = "{\r\n  \"writes\": [\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"userEmail\": {\r\n\t\t\t      \"stringValue\": \""+userEmail+"\"\r\n\t\t\t\t},\r\n              \t\"userId\": {\r\n                  \"stringValue\": \""+userId+"\"\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    },\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"/playerStats/0\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"currHP\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"currSTM\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"level\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"maxHP\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"maxSTM\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"speed\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"strength\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"xp\": {\r\n                  \"integerValue\": 0\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    },\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"/compStats/0\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"level\": {\r\n\t\t\t      \"integerValue\": 0\r\n\t\t\t\t},\r\n              \t\"speed\": {\r\n                  \"integerValue\": 0\r\n                },\r\n              \t\"strength\": {\r\n                  \"integerValue\": 0\r\n                }\r\n  \t\t\t}\r\n    \t}\r\n    },\r\n    {  \t\r\n    \t\"update\": {\r\n    \t\t\"name\": \"projects/foot-leprechauns/databases/(default)/documents/users/"+userId+"/worlds/0\",\r\n\t  \t\t\"fields\": {\r\n\t\t\t    \"seed\": {\r\n\t\t\t      \"stringValue\": \"\"\r\n\t\t\t\t}\r\n  \t\t\t}\r\n    \t}\r\n    }\r\n  ]\r\n}";
        RestClient.Post($"https://firestore.googleapis.com/v1/projects/{ProjectID}/databases/(default)/documents:commit/?key={this.API_KEY}", jsonReq).Then(res=>{
            // Debug.Log(res.Text);
        });
    }
}
