using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudioQueue : MonoBehaviour
{
    private AudioSource [] audioSource;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        audioSource = GetComponents<AudioSource>();
        audioSource[0].PlayScheduled(AudioSettings.dspTime);
        double clipLength = audioSource[0].clip.samples / audioSource[0].clip.frequency;

        // There is bug that wont loop the scource in WebGL
        //audioSource[1].PlayScheduled(AudioSettings.dspTime + clipLength);

        yield return new WaitForSeconds(audioSource[1].clip.length);
        audioSource[1].Play();
    }
}
