using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UserStats;
using UserHit;
using UserAnimate;
using UserTargeting;

namespace UserMelee
{
    [RequireComponent(typeof(Stats), typeof(Targeting))]
    public class Melee : MonoBehaviour
    {
        [SerializeField]
        protected float offset = 2;
        [SerializeField]
        protected float radius = 4;

        Stats stats;
        Targeting targeting;

        public void Reset()
        {
            if (GetComponent<Hit>() == null)
                gameObject.AddComponent<Hit>();
        }

        void Start()
        {
            stats = GetComponent<Stats>();
            targeting = GetComponent<Targeting>();
        }

        void DamageEnemy(Collider enemy)
        {
            Hit enemyHit = enemy.GetComponent<Hit>();

            if (enemyHit != null)
                enemyHit.TakeMeleeDamage(this.stats.getStrength);
        }

        void MeleeContactEvent()
        {
            targeting.ToNearbyEnemies(offset, radius, DamageEnemy);
        }
    }
}