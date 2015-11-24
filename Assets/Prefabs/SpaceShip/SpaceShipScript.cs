using UnityEngine;
using System.Collections;

public class SpaceShipScript : MonoBehaviour {
	public AudioSource myAudioSource;
	public AudioClip takeOffClip;
	public Animation myAnimation;
	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource>();
		myAnimation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeOff(){
		myAnimation.Play();
		myAudioSource.clip = takeOffClip;
		myAudioSource.Play();
	}
}
