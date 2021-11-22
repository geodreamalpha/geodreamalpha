using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject sender;

    [SerializeField]
    GameObject impact;

    public bool destroyOnImpact = true;
    const float lerpDirection = 0.03f;
    public Timer lifetime = new Timer(10f);

    public Transform target = null;
    public Timer checkRate = new Timer(0.5f);
    public Vector3 direction = Vector3.zero;
    Vector3 newDirection = Vector3.zero;

    public AnimationCurve speed = new AnimationCurve(new Keyframe(0, 40));
    public AnimationCurve growth = new AnimationCurve(new Keyframe(0, 1));

    Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        if (rigidBody != null)
            rigidBody.position += (direction * speed.Evaluate(lifetime.getAccumulator) * Time.deltaTime);
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        //CharacterBase attackerStats = attacker.GetComponent<ProjectileBehavior>().sender.GetComponent<CharacterBase>();
        //damage = (int)DerivedStats.GetReductionDamage(attackerStats.gameStats.energy, this.gameStats.aura);

        if (sender != null && other.transform.root.gameObject.layer != sender.layer)
        {
            if (impact != null)
            {
                Instantiate(impact, transform.position, Quaternion.identity);
                impact.GetComponent<ProjectileBehavior>().sender = sender;
            }            
        }

        if (destroyOnImpact)
            Destroy(gameObject);
    }
}
