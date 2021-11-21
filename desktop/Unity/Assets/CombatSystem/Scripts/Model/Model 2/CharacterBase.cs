using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

namespace CombatSystemComponent
{
    public class CharacterBase : MonoBehaviour
    {
        //declare immutable world directions a character can walk in
        public static ReadOnlyCollection<Vector3> directions = new ReadOnlyCollection<Vector3>(new List<Vector3>()
            { Vector3.up, Vector3.down, Vector3.left, Vector3.right });
        
        //Declare Damage Text Colors
        [SerializeField]
        protected Color damageTextColor;

        //Declare Main Decision Event Method
        protected Action OnDecision;

        //Declare Current Animation
        protected string currentAnimation = idle;

        //Declare Animation Constants
        public const string idle = "idle";
        public const string grabWalking = "isWalking";
        public const string grabSprinting = "isSprinting";
        public const string grabGrounded = "isGrounded";
        public const string grabJump = "isJump";
        public const string grabMelee = "isMelee";
        public const string grabMagic = "isProjectile";
        public const string grabBlock = "isBlock";
        public const string grabHit = "isHit";
        public const string grabDead = "isDead";
       
        //Declare Inpsector References
        public Animator animator;
        public CharacterController controller;
        public Transform target;

        //Declare Stats
        protected LevelStats levelStats;
        [SerializeField]
        DerivedStats baseStats;
        static DerivedStats multiplier = new DerivedStats();
        public DerivedStats gameStats { get; protected set; }
        public float health { get; protected set; } = 0;
        public float stamina { get; protected set; } = 0;

        //Declare other field attributes
        protected CombatSystemAssets assets;
        public readonly static Vector3 gravity = new Vector3(0, -9.8f, 0);
        protected Vector3 velocity = Vector3.zero;
        protected Vector3 rotation = Vector3.forward;

        //Initialize Multiplier Stats
        public static void InitializeMultiplierStats()
        {
            multiplier.walkSpeed /= 16f;
            multiplier.sprintSpeed /= 8f;
            multiplier.jumpHeight /= 16f;
            multiplier.strength /= 10f;
            multiplier.defense /= 10f;
            multiplier.energy /= 10f;
            multiplier.aura /= 10f;
        }

        //Set Game Stats
        protected void SetInGameStats()
        {
            //Initialize Stat Groups
            levelStats = new LevelStats();
            gameStats = new DerivedStats();

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
