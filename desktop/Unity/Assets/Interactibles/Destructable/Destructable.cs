using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UserStats;

[RequireComponent(typeof(Stats))]
public class Destructable : MonoBehaviour
{
    GameObject impact;
    Stats stats;

    public void Reset()
    {
        if (GetComponent<Stats>() == null)
            gameObject.AddComponent<Stats>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.health < 0)
        {
            if (impact != null)
                Instantiate(impact);
            Destroy(gameObject);           
        }
            
    }

    public void AddDependancies()
    {

    }
}
