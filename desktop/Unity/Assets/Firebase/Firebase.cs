using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class Firebase : MonoBehaviour
{

    string baseURL = "https://firestore.googleapis.com/v1/";
    string project_id = "foot-leprechauns";
    string user = "uXkIuHksjSUBvP5Yp3xw";
    string API_KEY = System.IO.File.ReadAllText("Assets/Firebase/apikey.0");
    public HealthBar hb;

    public void GetValue(string field)
    {
        string data = "";
        string look;
        int intIdx;
        int length = 0;
        string requestURL = $"{baseURL}projects/{project_id}/databases/(default)/documents/users/{this.user}/?key={API_KEY}";
        RestClient.Get(requestURL).Then(res =>
        {
            intIdx = res.Text.ToString().IndexOf("\"integerValue\": \"") +17;
            look = res.Text.ToString().Substring(intIdx);
            for(int i = 0; i<look.Length-1; i++)
            {
                length++;
                if(look[i] == '"'){
                    break;
                }
            }
            data = res.Text.ToString().Substring(intIdx,length-1);
            hb.SetHealth(int.Parse(data));
        });
    }
}
