using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudioQueue : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip mainLoop;
    private AudioSource [] audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        audioSource[1].PlayScheduled((double)intro.samples / intro.frequency);
    }
}
