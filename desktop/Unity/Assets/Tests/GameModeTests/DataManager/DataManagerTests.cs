using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DataManagerComponent;

// Author: Nick Preston

public static class TestDataManager
{
    public static string data;
    public static int value;
}

public class DataManagerTests
{

    Firebase fb = Firebase.GetInstance();

    [UnityTest]
    public IEnumerator SeedTest()
    {
        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.SetSeed("0_test", 2);
            
            new WaitForSeconds(2);

            DataManager.GetSeed(2, val=>
            {
            TestDataManager.data = val;
            });
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual("0_test", TestDataManager.data);
    }
}
