using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UserStats;
using UserAnimate;

namespace UserHit
{
    [RequireComponent(typeof(Stats))]
    public class Hit : MonoBehaviour
    {
        [SerializeField]
        Color damageTextColor = Color.blue;

        Stats stats;
        Animate animate;

        public void Reset()
        {
            if (GetComponent<Animate>() == null)
                gameObject.AddComponent<Animate>();
        }

        void Start()
        {
            stats = GetComponent<Stats>();
            animate = GetComponent<Animate>();
        }

        public void TakeMeleeDamage(float damage, params Action[] damageEvents)
        {
            stats.TakeDamage(damage);
            TakeDamage(damageEvents);
        }

        public void TakeProjectileDamage(float damage, params Action[] damageEvents)
        {
            stats.TakeDamage(damage);
            TakeDamage(damageEvents);       
        }

        void TakeDamage(Action[] damageEvents)
        {
            //GameObject.Find("DamageMenu").GetComponent<DamageMenuBehavior>().ShowDamage(transform, damage, damageTextColor, assets);

            foreach (Action damageEvent in damageEvents)
                damageEvent();

            if (animate != null)
                animate.Hit();
        }
    }
}
