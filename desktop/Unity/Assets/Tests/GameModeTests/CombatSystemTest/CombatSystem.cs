using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CombatSystemComponent;

//Jake Aldridge; Unit Test
public class CombatSystemTest2
{ 
    [UnityTest]
    public IEnumerator CombatSystemTest2WithEnumeratorPasses()
    {
        CombatSystem combatSystem = new CombatSystem();
        Timer timer = new Timer(1000);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Assert.AreEqual(timer.getMaxTime, 1000); 
        Assert.AreEqual(CharacterBase.gravity, new Vector3(0, -9.8f, 0));

        float strength = 1;
        float defense = 1000;

        //with faulty damage calculations, a high defense and low strength could cause damage to be zero or less (which isn't good)
        Assert.IsTrue(combatSystem.GetAReductionDamageValue(strength, defense) > 0f,
            "damage calculation test is less than or equal to zero");

        Assert.AreEqual("Hello from Component CombatSystem", combatSystem.Hello());

        yield return null;
    }
}
