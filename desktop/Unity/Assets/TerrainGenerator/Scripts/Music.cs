using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneratorComponent
{
    [System.Serializable]
    public class Music
    {
        //this class manages the ambience and music files that play during execution
        [SerializeField]
        AudioClip musicClip;
        AudioSource musicSource;
        [SerializeField]
        AudioClip ambienceClip;
        AudioSource ambienceSource;

        public void Initialize()
        {
            
        }

        public void Play()
        {

        }
    }
}