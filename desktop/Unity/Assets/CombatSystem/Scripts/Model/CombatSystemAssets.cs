using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    //stores all the combat system assets that will be instantiated at runtime
    [System.Serializable]
    class CombatSystemAssets
    {
        [SerializeField]
        public List<GameObject> enemies;
        [SerializeField]
        List<GameObject> projectiles;
        [SerializeField]
        GameObject meleeHit;
        [SerializeField]
        GameObject damageText;
        [SerializeField]
        protected AudioSource meleeImpactFX;

        public GameObject GetEnemyByName(string name)
        {
            return enemies.Find(e => e.name == name);
        }
        public GameObject GetProjectileByName(string name)
        {
            return projectiles.Find(e => e.name == name);
        }
        public GameObject GetMeleeHit()
        {
            return meleeHit;
        }
        public GameObject GetDamageText()
        {
            return damageText;
        }
    }
}