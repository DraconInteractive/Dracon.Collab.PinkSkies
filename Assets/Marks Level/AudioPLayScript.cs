using UnityEngine;
using System.Collections;

public class AudioPLayScript : MonoBehaviour {

	public AudioClip myAudio;
	bool hasPlayed;


	void Start () {

	}
	

	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (!hasPlayed && other.tag == "Player") {
			GetComponent <AudioSource> ().PlayOneShot (myAudio);
			hasPlayed = true;
		}
	}
}
