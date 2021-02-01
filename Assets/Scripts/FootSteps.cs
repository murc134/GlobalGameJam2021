using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
	public AudioClip[] footSteps;
	private AudioSource audio;

	// Use this for initialization
	void Start()
	{
		audio = this.GetComponent<AudioSource>();
	}

	//Change "SoundYouWantToPlay" to the method name you want to call from the Animation Event.
	void FootStep(float volume = 1f)
	{
		audio.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
		audio.volume = volume;

		Debug.Log("FootStep");
	}
}