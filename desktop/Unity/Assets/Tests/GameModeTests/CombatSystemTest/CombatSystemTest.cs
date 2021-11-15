using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CombatSystemComponent;

//Tyler Anderson; Unit Test
public class CombatSystemTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CombatSystemTestWithEnumeratorPasses()
    {
        CombatSystem combatSystem = new CombatSystem();
        
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        Assert.AreEqual("Hello from Component CombatSystem", combatSystem.Hello());
    }
}
