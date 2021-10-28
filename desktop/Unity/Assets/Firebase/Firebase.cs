using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public class Firebase
{

    private static Firebase UniqueInstance;
    static string FSApiURL = "https://firestore.googleapis.com/v1/";
    static string ProjectID = "foot-leprechauns";
    string FSBaseURL = $"{FSApiURL}projects/{ProjectID}/databases/(default)/documents/users/";
    string CurrUserId = "xW6rJSevT9VjkBx3XtuTyxCZyJH3";
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

    // public delegate void GetUserCallback(User user);
    // /// <summary>
    // /// Retrieves a user from the Firebase Database, given their id
    // /// </summary>
    // /// <param name="userId"> Id of the user that we are looking for </param>
    // /// <param name="callback"> What to do after the user is downloaded successfully </param>
    // public static void GetUser(string userId)
    // {
    //     RestClient.Get("https://firestore.googleapis.com/v1/projects/foot-leprechauns/databases/(default)/documents/users/uXkIuHksjSUBvP5Yp3xw/?key=AIzaSyD3S-N_VOPv_zGVWOUgv2fPT0SgkSUzPaY").Then(res =>
    //     {
    //         Debug.Log(res.Text);
    //     });
    // }

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
                Debug.Log(res.Text);
            });
        }
        
    }
}
