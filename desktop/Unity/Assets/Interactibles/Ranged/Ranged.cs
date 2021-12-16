using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UserStats;
using UserMover;
using UserTargeting;
using UserProjectile;

namespace UserRanged
{
    [RequireComponent(typeof(Stats), typeof(Mover), typeof(Targeting))]
    public class Ranged : MonoBehaviour
    {
        Stats stats;
        Mover mover;
        Targeting targeting;

        void Start()
        {
            stats = gameObject.AddComponent<Stats>();
            mover = gameObject.AddComponent<Mover>();
            targeting = gameObject.AddComponent<Targeting>();
        }

        public void Fire(string projectileName)
        {          
            //GameObject projectile = Instantiate(assets.GetProjectileByName(name), controller.transform.position + (faceTarget.normalized + Vector3.up) * 5f, Quaternion.identity);
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = transform.position + transform.forward * 5;
            Projectile projectile = obj.AddComponent<Projectile>();
            Stats objStats = obj.GetComponent<Stats>();
            objStats = this.stats;

            if (targeting.GetTarget() != null)
            {
                mover.Rotate(targeting.GetDirectionToTarget());
                Targeting objTargeting = obj.GetComponent<Targeting>();
                objTargeting = this.targeting;
            }
        }
    }
}