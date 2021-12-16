using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundFX
{
    public class SoundFX : MonoBehaviour
    {
        //Declare SoundFX
        AudioSource soundFX;
        [SerializeField]
        protected soundFXGroup stepFX1;
        public void PlayStepFX1()
        {
            stepFX1.Play();
        }
        [SerializeField]
        soundFXGroup stepFX2;
        public void PlayStepFX2()
        {
            stepFX2.Play();
        }
        [SerializeField]
        soundFXGroup stepFX3;
        public void PlayStepFX3()
        {
            stepFX3.Play();
        }
        [SerializeField]
        soundFXGroup stepFX4;
        public void PlayStepFX4()
        {
            stepFX4.Play();
        }
        [SerializeField]
        soundFXGroup meleeAttackFX;
        public void PlayMeleeAttackFX()
        {
            meleeAttackFX.Play();
        }
        [SerializeField]
        soundFXGroup TakeDamageFX;
        public void PlayTakeDamageFX()
        {
            TakeDamageFX.Play();
        }
    }
}