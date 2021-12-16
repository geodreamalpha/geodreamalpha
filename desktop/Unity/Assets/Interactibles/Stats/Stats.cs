using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserStats
{
    public class Stats : MonoBehaviour
    {
        Level levelStats = new Level();
        [SerializeField]
        Base baseStats = new Base();

        public float health = 0;
        public float stamina = 0;

        public float getStrength
        {
            get { return levelStats.strength * baseStats.strength * 10f; }
        }

        public float getEnergy
        {
            get { return levelStats.stamina * baseStats.energy * 10f; }
        }

        void Start()
        {
            health = levelStats.health * baseStats.health * 100;
            stamina = levelStats.stamina * baseStats.stamina * 100;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}