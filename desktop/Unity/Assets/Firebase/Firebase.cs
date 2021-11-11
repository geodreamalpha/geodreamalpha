using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using Newtonsoft.Json;
public class Firebase
{

    private static Firebase UniqueInstance;
    static string FSApiURL = "https://firestore.googleapis.com/v1/";
    static string ProjectID = "foot-leprechauns";
    string FSAuthURL = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
    string FSBaseURL = $"{FSApiURL}projects/{ProjectID}/databases/(default)/documents/users/";
    string CurrUserId;
    string API_KEY = System.IO.File.ReadAllText("Assets/Firebase/apikey.0");

    public static Firebase GetInstance()
    {
        if (UniqueInstance == null)
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
        if (IsAuthenticated())
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
        RestClient.Post($"{this.FSAuthURL}{this.API_KEY}", req).Then(res =>
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
        }).Catch(err => {
            SignInRes fullResponse = new SignInRes();
            fullResponse.Success = false;
            callback(fullResponse);
        });
    }
}
