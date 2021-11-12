using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// Author: Nick Preston

public static class TestData
{
    public static bool success;
}

public class FirebaseTests
{
    Firebase fb = Firebase.GetInstance();

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SuccessfulLoginTest()
    {
        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(true, TestData.success);
    }
}
