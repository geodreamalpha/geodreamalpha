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
        TestDataManager.data = null;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetSeed(0, val=>
            {
                TestDataManager.data = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.data!=null);
    }

    [UnityTest]
    public IEnumerator CurrHPTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetCurrHP(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator MaxHPTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetMaxHP(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator CurrSTMTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetCurrSTM(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator MaxSTMTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetMaxSTM(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator StrengthTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetStrength(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator SpeedTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetSpeed(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator XPTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetXP(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    //Skipping comp level because we are not using it in the game's code

    [UnityTest]
    public IEnumerator CompStrengthTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetCompStrength(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }

    [UnityTest]
    public IEnumerator CompSpeedTest()
    {
        TestDataManager.value = -1;

        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>
        {
            DataManager.GetCompSpeed(val=>
            {
                TestDataManager.value = val;
            });
        });

        yield return new WaitForSeconds(1);

        Assert.AreEqual(true, TestDataManager.value!=-1);
    }
}
