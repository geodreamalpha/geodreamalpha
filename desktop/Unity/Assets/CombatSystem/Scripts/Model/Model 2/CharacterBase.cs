using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.ObjectModel;

namespace CombatSystemComponent
{
    //Manages common fields and initializer methods that can be used by each character in the game
    abstract class CharacterBase : MonoBehaviour
    {
        //declare immutable world directions a character can walk in
        public static ReadOnlyCollection<Vector3> directions = new ReadOnlyCollection<Vector3>(new List<Vector3>()
            { Vector3.up, Vector3.down, Vector3.left, Vector3.right });

        //Declare SoundFX
        AudioSource soundFX;
        [SerializeField]
        protected soundFXGroup stepFX1;
        void PlayStepFX1()
        {
            stepFX1.Play();
        }
        [SerializeField]
        protected soundFXGroup stepFX2;
        void PlayStepFX2()
        {
            stepFX2.Play();
        }
        [SerializeField]
        protected soundFXGroup meleeAttackFX;
        void PlayMeleeAttackFX()
        {
            meleeAttackFX.Play();
        }
        [SerializeField]
        protected soundFXGroup hitFX;
        void PlayHitFX()
        {
            hitFX.Play();
        }

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
        public LevelStats levelStats { get; set; } = new LevelStats();
        [SerializeField]
        DerivedStats baseStats;
        public static MultiplierStats multiplier { get; private set; }
        public DerivedStats gameStats { get; protected set; } = new DerivedStats();
        public float health { get; protected set; } = 0;
        public float stamina { get; protected set; } = 0;

        //Declare other field attributes
        protected CombatSystemAssets assets;
        public readonly static Vector3 gravity = new Vector3(0, -9.8f, 0);
        protected Vector3 velocity = Vector3.zero;
        protected Vector3 rotation = Vector3.forward;

        //Initializers
        public static void InitializeMultiplierStats(MultiplierStats multiplier)
        {
            CharacterBase.multiplier = multiplier;
        }
        protected void InitializeSoundFX()
        {
            //audio FX
            soundFX = GetComponent<AudioSource>();
            stepFX1.source = soundFX;
            stepFX2.source = soundFX;
            meleeAttackFX.source = soundFX;
            hitFX.source = soundFX;
        }
        protected void InitializeInGameStats()
        {
            AdjustInGameStats();

            //other fields
            health = (int)gameStats.healthPoints;
            stamina = (int)gameStats.staminaPoints;           
        }

        //Adjusts in-game stats
        protected void AdjustInGameStats()
        {
            //health related
            gameStats.healthPoints = baseStats.healthPoints * multiplier.healthPoints.curve.Evaluate(levelStats.GetHealth());

            //strength related-
            gameStats.strength = baseStats.strength * multiplier.strength.curve.Evaluate(levelStats.GetStrength());
            gameStats.defense = baseStats.defense * multiplier.defense.curve.Evaluate(levelStats.GetStrength());
            gameStats.jumpHeight = baseStats.jumpHeight * multiplier.jumpHeight.curve.Evaluate(levelStats.GetStrength());

            //stamina related
            gameStats.staminaPoints = baseStats.staminaPoints * multiplier.staminaPoints.curve.Evaluate(levelStats.GetStamina());
            gameStats.energy = baseStats.energy * multiplier.energy.curve.Evaluate(levelStats.GetStamina());
            gameStats.aura = baseStats.aura * multiplier.aura.curve.Evaluate(levelStats.GetStamina());

            //speed related-
            gameStats.walkSpeed = baseStats.walkSpeed * 10 * multiplier.speed.curve.Evaluate(levelStats.GetSpeed());
            gameStats.sprintSpeed = baseStats.sprintSpeed * 16 * multiplier.speed.curve.Evaluate(levelStats.GetSpeed());
            gameStats.projectileCheck = 0.5f / (multiplier.speed.curve.Evaluate(levelStats.GetSpeed()) * baseStats.projectileCheck);
            gameStats.projectileSpeed = baseStats.projectileSpeed * 60 * multiplier.speed.curve.Evaluate(levelStats.GetSpeed());

            //animator
            animator.speed = multiplier.speed.curve.Evaluate(levelStats.GetSpeed());
        }     
    }

    //sound FX class that handles all sound FX of characters
    [System.Serializable]
    public class soundFXGroup
    {
        [SerializeField]
        protected List<AudioClip> clips;
        public AudioSource source;
        [SerializeField]
        float minVolume;
        [SerializeField]
        float maxVolume;
        [SerializeField]
        float minPitch;
        [SerializeField]
        float maxPitch;

        public void Play()
        {
            foreach (AudioClip clip in clips)
            {
                source.volume = UnityEngine.Random.Range(0.5f, 1f);
                source.pitch = UnityEngine.Random.Range(1f, 2.5f);
                source.PlayOneShot(clip);
            }
        }
    }
}
