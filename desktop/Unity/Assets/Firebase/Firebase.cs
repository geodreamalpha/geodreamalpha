using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;

public class Firebase
{

    static string apiURL = "https://firestore.googleapis.com/v1/";
    static string project_id = "foot-leprechauns";
    static string API_KEY = System.IO.File.ReadAllText("Assets/Firebase/apikey.0");

    public delegate void GetUserCallback(User user);
    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetUser(string userId)
    {
        RestClient.Get("https://firestore.googleapis.com/v1/projects/foot-leprechauns/databases/(default)/documents/users/uXkIuHksjSUBvP5Yp3xw/?key=AIzaSyD3S-N_VOPv_zGVWOUgv2fPT0SgkSUzPaY").Then(res =>
        {
            Debug.Log(res.Text);
        });
    }
}
