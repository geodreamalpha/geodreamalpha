using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CombatSystemComponent
{
    public class CharacterBase : MonoBehaviour
    {
        //Declare Damage Layers List
        [SerializeField]
        protected string damageTag;

        //Declare Damage Text Colors
        [SerializeField]
        protected Color damageTextColor;

        //Declare Current Animation
        protected string currentAnimation = idle;

        //Declare Animation Constants
        public const string idle = "idle";
        public const string grabWalking = "isWalking";
        public const string grabRunning = "isRunning";
        public const string grabGrounded = "isGrounded";
        public const string grabJump = "isJump";
        public const string grabMelee = "isMelee";
        public const string grabMagic = "isMagic";
        public const string grabBlock = "isBlock";
        public const string grabHit = "isHit";
        public const string grabDead = "isDead";
       
        //Declare Inpsector References
        public Animator animator;
        public CharacterController controller;
        public Transform target;

        //Declare Stats
        protected Stats levelStats;
        [SerializeField]
        DerivedStats baseStats;
        static DerivedStats multiplier;
        public DerivedStats gameStats;
        public int health { get; protected set; } = 0;
        public int stamina { get; protected set; } = 0;

        //Declare other field attributes
        protected CombatSystemAssets assets;
        public readonly static Vector3 gravity = new Vector3(0, -9.8f, 0);
        protected Vector3 velocity = Vector3.zero;
        protected Vector3 rotation = Vector3.forward;

        //Set Game Stats
        protected void SetInGameStats()
        {
            levelStats = new Stats();
            multiplier = new DerivedStats();
            gameStats = new DerivedStats();

            //------------------------------Temp
            multiplier.walkSpeed /= 16f;
            multiplier.sprintSpeed /= 8f;
            multiplier.jumpHeight /= 16f;
            multiplier.strength /= 10f;
            multiplier.defense /= 10f;
            multiplier.energy /= 10f;
            multiplier.aura /= 10f;
            //------------------------------

            //health related
            gameStats.healthPoints = baseStats.healthPoints * multiplier.healthPoints * levelStats.GetHealth();

            //strength related
            gameStats.strength = baseStats.strength * multiplier.strength * levelStats.GetStrength();
            gameStats.defense = baseStats.defense * multiplier.defense * levelStats.GetStrength();
            gameStats.jumpHeight = baseStats.jumpHeight * multiplier.jumpHeight * levelStats.GetStrength();

            //stamina related
            gameStats.staminaPoints = baseStats.staminaPoints * multiplier.staminaPoints * levelStats.GetStamina();
            gameStats.energy = baseStats.energy * multiplier.energy * levelStats.GetStamina();
            gameStats.aura = baseStats.aura * multiplier.aura * levelStats.GetStamina();

            //speed related
            gameStats.walkSpeed = baseStats.walkSpeed * multiplier.walkSpeed * levelStats.GetSpeed();
            gameStats.sprintSpeed = baseStats.sprintSpeed * multiplier.sprintSpeed * levelStats.GetSpeed();
            gameStats.projectileCheck = baseStats.projectileCheck * multiplier.projectileCheck * levelStats.GetSpeed();
            gameStats.projectileSpeed = baseStats.projectileSpeed * multiplier.projectileSpeed * levelStats.GetSpeed();

            //other fields
            health = (int)gameStats.healthPoints;
            stamina = (int)gameStats.staminaPoints;
        }
    }
}
