using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatSystemAssets
{  
    [SerializeField]
    public List<GameObject> enemies;
    [SerializeField]
    List<GameObject> projectiles;
    [SerializeField]
    GameObject damageText;
    [SerializeField]
    protected AudioSource meleeImpactFX;

    public GameObject getEnemyByName(string name)
    {
        return enemies.Find(e => e.name == name);
    }
    public GameObject getProjectileByName(string name)
    {
        return projectiles.Find(e => e.name == name);
    }
    public GameObject getDamageText()
    {
        return damageText;
    }
}
