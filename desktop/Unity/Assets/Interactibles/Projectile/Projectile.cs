using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UserTargeting;
using UserHit;
using UserStats;
using UserMover;

namespace UserProjectile
{
    [RequireComponent(typeof(Targeting), typeof(Stats), typeof(Mover))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        GameObject impact;

        [SerializeField]
        float lifeTime = 5f;
        [SerializeField]
        float checkRate = 0.5f;

        Targeting targeting;
        Stats stats;
        Mover mover;

        void Start()
        {
            Destroy(gameObject, lifeTime);
            targeting = GetComponent<Targeting>();
            stats = GetComponent<Stats>();
            mover = GetComponent<Mover>();
        }

        void DamageEnemy(Collider enemy)
        {
            Hit enemyHit = enemy.GetComponent<Hit>();

            if (enemyHit != null)
                enemyHit.TakeProjectileDamage(this.stats.getEnergy);
        }

        void Update()
        {
            if (targeting.GetTarget() != null)
            {
                checkRate += Time.deltaTime;
                if (checkRate == 0.5f)
                {
                    mover.ResetVelocity();
                    mover.Accelerate(targeting.GetDirectionToTarget(), 10);
                    checkRate = 0f;
                }
            }
            mover.Move();

            targeting.ToNearbyEnemies(0, 2, DamageEnemy);
            targeting.OnOverlap(0, 2, () => Instantiate(impact), () => Destroy(gameObject));
        }
    }
}