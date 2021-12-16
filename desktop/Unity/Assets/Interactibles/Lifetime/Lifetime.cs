using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserLifetime
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField]
        float seconds;

        void Start()
        {
            Destroy(gameObject, seconds);
        }
    }
}