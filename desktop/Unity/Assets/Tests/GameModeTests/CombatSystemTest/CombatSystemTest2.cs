using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CombatSystemComponent; 

public class CombatSystemTest2
{ 
    [UnityTest]
    public IEnumerator CombatSystemTest2WithEnumeratorPasses()
    {
        CombatSystem combatSystem = new CombatSystem(); 

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
        Assert.AreEqual(CharacterBase.gravity, new Vector3(0, -9.8f, 0)); 
    }
}
