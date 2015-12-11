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
		Debug.Log ("Ship Taking Off");
		Camera.main.GetComponent<CameraScript>().SetPlayerType(this.gameObject);
		myAnimation.Play();
		myAudioSource.clip = takeOffClip;
		myAudioSource.Play();
	}

	public void NextLevel(){
		Application.LoadLevel("TheForest");
	}
}
