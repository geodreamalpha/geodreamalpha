public class FBHook
{
    //Properties
    Firebase fb = Firebase.GetInstance();

    public delegate void GetIntCallback(int value);
    public delegate void GetStringCallback(string value);

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

    /// <summary>
    /// Blanket method to write player stats as integers
    /// </summary>
    /// <param name="stat">The name of the stat</param>
    /// <param name="callback">Callback function</param>
    public void SetStat(string stat, int value, GetIntCallback callback)
    {
        fb.UpdateIntField("/playerStats/0", stat, value, doc=>{});
    }

    /// <summary>
    /// Method to get the seed at the given index in the user's worlds collection
    /// </summary>
    /// <param name="index">Index of the seed</param>
    /// <param name="callback">Callback function</param>
    public void GetSeed(int index, GetStringCallback callback)
    {
        fb.GetDoc($"worlds/{index.ToString()}", doc=>
        {
            if (doc.fields == null) {
                callback(null);
            }
            else
            {
                string val = doc.fields["seed"]["stringValue"];
                callback(val);
            }
            
        });
    }
    /// <summary>
    /// Method to set the seed at the given index in the user's worlds collection
    /// </summary>
    /// <param name="index">Index of the seed</param>
    public void SetSeed(string seed, int index)
    {
        fb.UpdateStrField($"/worlds/{index.ToString()}", "seed", seed, res=>{});
    }

    /// <summary>
    /// Blanket method to retrieve companion stats as integers
    /// </summary>
    /// <param name="stat">The name of the stat</param>
    /// <param name="callback">Callback function</param>
    public void GetCompStat(string stat, GetIntCallback callback)
    {
        fb.GetDoc("compStats/0", doc=>
        {
            string val = doc.fields[stat]["integerValue"];
            int value = int.Parse(val);
            callback(value);
        });
    }
}