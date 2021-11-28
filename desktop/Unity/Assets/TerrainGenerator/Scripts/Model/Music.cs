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
        AudioSource ambienceSource;
        [SerializeField]
        AudioSource musicSource;

        float[] playTimes = new float[8] { 0f, 208f, 322f, 412f, 502f, 623f, 717f, 858f};

        public void ChooseStartMusic()
        {
            musicSource.time = playTimes[UnityEngine.Random.Range(0, playTimes.Length)];
            musicSource.Play();
        }
    }
}