using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserSettings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        AudioSource music;
        [SerializeField]
        AudioSource ambience;
        [SerializeField]
        AudioSource sound;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetMusicVolume(float value)
        {
            music.volume = value;
        }
    }
}