using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SplashInterfaceTests
{
    // A Test behaves as an ordinary method
    [Test]

    
    public void SplashInterfaceTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        SplashInterface splash = new SplashInterface();
        Assert.AreEqual("SplashInterface", splash.objectName); 
    }
   
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SplashInterfaceTestsWithEnumeratorPasses()
    {
        SplashInterface splash = new SplashInterface();
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
