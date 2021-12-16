using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundFX
{
    //sound FX class that handles all sound FX of characters
    [System.Serializable]
    public class soundFXGroup
    {
        [SerializeField]
        protected List<AudioClip> clips;
        [HideInInspector]
        public AudioSource source;
        [Range(0, 2)]
        [SerializeField]
        float minVolume;
        [Range(0, 2)]
        [SerializeField]
        float maxVolume;
        [Range(0, 2)]
        [SerializeField]
        float minPitch;
        [Range(0, 2)]
        [SerializeField]
        float maxPitch;

        public void Play()
        {
            if (clips.Count > 0)
            {
                source.volume = UnityEngine.Random.Range(minVolume, maxVolume);
                source.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
                int i = UnityEngine.Random.Range(0, clips.Count);
                source.PlayOneShot(clips[i]);
            }
        }
    }
}