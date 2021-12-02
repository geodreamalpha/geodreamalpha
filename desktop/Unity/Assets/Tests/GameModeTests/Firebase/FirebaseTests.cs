using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// Author: Nick Preston

public static class TestData
{
    public static bool success;
    public static string error;
}

public class FirebaseTests
{
    Firebase fb = Firebase.GetInstance();

    [UnityTest]
    public IEnumerator SuccessfulLoginTest()
    {
        fb.SignIn("nick@geodream.alpha", "1337h4x0r", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(true, TestData.success);
    }

    [UnityTest]
    public IEnumerator InvalidCredLoginTest()
    {
        fb.SignIn("wrong@email.com", "wrongpassword", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(false, TestData.success);
    }

    [UnityTest]
    public IEnumerator SuccessfulSignUpTest()
    {
        fb.SignUp("unit-testing-user@example.com", "unit-testing-password", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(true, TestData.success);
    }

    // This one is unsuccessful because of malformed credentials
    [UnityTest]
    public IEnumerator UnsuccessfulSignUpTest()
    {
        fb.SignUp("unit-test-1", "unittest1", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(false, TestData.success);
    }

    [UnityTest]
    public IEnumerator SuccessfulPasswordResetTest()
    {
        fb.PasswordReset("nick@geodream.alpha", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(true, TestData.success);
    }

    // This one is unsuccessful because of an unregistered email
    [UnityTest]
    public IEnumerator UnsuccessfulPasswordResetTest()
    {
        fb.PasswordReset("gaben@valvesoftware.com", res=>{
            TestData.success = res.Success;
        });

        yield return new WaitForSeconds(2);

        Assert.AreEqual(false, TestData.success);
    }

    // Skipping password reset submit method, its not being used in our code
}
