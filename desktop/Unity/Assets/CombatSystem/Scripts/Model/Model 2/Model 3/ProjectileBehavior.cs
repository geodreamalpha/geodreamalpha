using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystemComponent
{
    public class ProjectileBehavior : MonoBehaviour
    {
        public GameObject sender;

        [SerializeField]
        GameObject impact;

        const float lerpDirection = 0.03f;
        public Timer lifetime = new Timer(10f);

        public Transform target = null;
        public Timer checkRate = new Timer(0.5f);
        public Vector3 direction = Vector3.zero;
        Vector3 newDirection = Vector3.zero;

        public AnimationCurve speed = new AnimationCurve(new Keyframe(0, 40));
        public AnimationCurve growth = new AnimationCurve(new Keyframe(0, 1));

        protected void ApplyProjectileDamageTo(Collider[] enemies)
        {
            foreach (Collider enemy in enemies)
            {
                //damage calculation
                int damageAmount = 0;
                HelperBase enemyStats = enemy.transform.root.GetComponent<HelperBase>();
                damageAmount = (int)DerivedStats.GetReductionDamage(sender.gameObject.GetComponent<HelperBase>().gameStats.energy, enemyStats.gameStats.aura);
                damageAmount = (int)(damageAmount * UnityEngine.Random.Range(0.8f, 1.2f));
                enemyStats.TakeDamage(damageAmount);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (impact != null)
            {
                Instantiate(impact, transform.position, Quaternion.identity);
                impact.GetComponent<ProjectileBehavior>().sender = sender;
                Destroy(gameObject);                
            }
        }

        private void FixedUpdate()
        {
            if (sender != null && impact != null)
                HelperBase.OnNearbyEnemies(transform, sender.layer, 0, 0.5f, ApplyProjectileDamageTo);
        }

        void Update()
        {           
            #region Updates Lifetime
            lifetime.Update();
            if (lifetime.isAtMax)
                Destroy(gameObject);
            #endregion

            #region Updates Growth Changes
            transform.localScale = Vector3.one * growth.Evaluate(lifetime.getAccumulator);
            #endregion

            if (target != null)
            {
                #region Recalculates Direction To Target At Specified Rate
                checkRate.Update();
                if (checkRate.isAtMax)
                {
                    newDirection = target.position - transform.position;
                    newDirection = newDirection.normalized;
                    checkRate.Reset();
                }
                direction = Vector3.Lerp(direction, newDirection, lerpDirection);
                #endregion               
            }

            #region Moves In Specified Direction At Specified Speed
            transform.position += (direction * speed.Evaluate(lifetime.getAccumulator) * Time.deltaTime);
            #endregion          
        }
    }
}
