using UnityEngine;
using System.Collections;
//Script design to manage Companion dialogue
public class AudioPLayScript : MonoBehaviour {

	public AudioClip myAudio;
	bool hasPlayed;


	void Start () {

	}
	

	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		//WHen the player enters the trigger, play the designated sound
		if (!hasPlayed && other.tag == "Player") {
			GetComponent <AudioSource> ().PlayOneShot (myAudio);
			hasPlayed = true;
		}
	}
}
