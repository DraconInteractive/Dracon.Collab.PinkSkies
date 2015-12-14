using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour {
	public GameObject playerObj;
	public bool isWalking, startedWalking;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerObj.GetComponent<PlayerScript>().isRunning == true){
			isWalking = true;
		} else {
			isWalking = false;
		}
		if (isWalking){
			if (!startedWalking){
				GetComponent<AudioSource>().Play();
				startedWalking = true;
			}
		} else {
			GetComponent<AudioSource>().Stop();
			startedWalking = false;
		}
	}
}
