using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserStats
{
    [System.Serializable]
    public class Base
    {
        //health
        [Range(0.1f, 1)]
        public float health = 1f;

        //strength
        [Range(0.1f, 1)]
        public float strength = 1f;
        [Range(0.1f, 1)]
        public float defense = 1f;
        [Range(0.1f, 1)]
        public float jumpHeight = 1f;

        //stamina
        [Range(0.1f, 1)]
        public float stamina = 1f;
        [Range(0.1f, 1)]
        public float energy = 1f;
        [Range(0.1f, 1)]
        public float aura = 1f;

        //speed
        [Range(0.1f, 1)]
        public float walkSpeed = 1f;
        [Range(0.1f, 1)]
        public float sprintSpeed = 1f;
        [Range(0.1f, 1)]
        public float projectileCheck = 1f;
        [Range(0.1f, 1)]
        public float projectileSpeed = 1f;
    }
}