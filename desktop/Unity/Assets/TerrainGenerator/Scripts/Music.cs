using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> audioClips = new List<AudioClip> { };
    [SerializeField]
    List<AudioClip> ambienceClips = new List<AudioClip> { };
    AudioSource[] audioSource;
    int musicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = player.GetComponents<AudioSource>();
        //audioSource[0].clip = ambienceClips[0];
        //audioSource[0].loop = true;
        //audioSource[0].Play();
        //audioSource[1].PlayOneShot(audioClips[0]);
        //musicIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!audioSource[1].isPlaying)
        //{
        //musicIndex++;
        //musicIndex = musicIndex % audioClips.Count;
        //audioSource[1].PlayOneShot(audioClips[musicIndex]);
        //}
    }
}
