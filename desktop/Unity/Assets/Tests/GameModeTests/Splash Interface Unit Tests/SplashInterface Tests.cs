using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


// Required to handle asynchronous tests. Stores result of login.
public static class TestData
{
    public static bool success;
}

public class SplashInterfaceTests
{
    SplashInterface splash = SplashInterface.GetInstance();

    // Test the login form. 
    [UnityTest]
    public IEnumerator SuccessfulSplashLoginTest()
    {
        splash.fireBaseSendLogin("unit-testing-user@example.com", "unit-testing-password", signedIn => {
            TestData.success = signedIn;
        });

        yield return new WaitForSeconds(2);
        Assert.AreEqual(true, TestData.success);
    }
}


