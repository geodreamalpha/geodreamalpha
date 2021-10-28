public class FBHook
{
    //Properties
    Firebase fb = Firebase.GetInstance();

    public delegate void GetIntCallback(int value);

    /// <summary>
    /// Blanket method to retrieve player stats as integers
    /// </summary>
    /// <param name="stat">The name of the stat</param>
    /// <param name="callback">Callback function</param>
    public void GetStat(string stat, GetIntCallback callback)
    {
        fb.GetDoc("playerStats/0", doc=>
        {
            string val = doc.fields[stat]["integerValue"];
            int value = int.Parse(val);
            callback(value);
        });
    }
}