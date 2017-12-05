using UnityEngine;
using System.Collections;

public class PlayAudio : MonoBehaviour {

	public enum AudioAttached{Yes,No}
	public AudioAttached isAudioAttached;


	public AudioSource source;



	void Awake(){

		if (isAudioAttached == AudioAttached.Yes)
			source = GetComponent<AudioSource> ();
	}

	public void PlaySound(){ // This is called by the animation clip Star1

		source.Play();
	}
}
