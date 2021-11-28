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

        yield return null;

        float strength = 1;
        float defense = 1000;

        //with faulty damage calculations, a high defense and low strength could cause damage to be zero or less (which isn't good)
        Assert.IsTrue(combatSystem.GetAReductionDamageValue(strength, defense) > 0f,
            "damage calculation test is less than or equal to zero");

        Assert.AreEqual("Hello from Component CombatSystem", combatSystem.Hello());
    }
}
