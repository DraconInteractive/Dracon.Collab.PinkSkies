using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour {
	public GameObject playerObj;
	public bool isRunning, startedRunning;
	public AudioClip stepOne, stepTwo;
	public int stepStage;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (playerObj.GetComponent<PlayerScript>().isRunning == true){
			isRunning = true;
		} else {
			isRunning = false;
		}

		if (isRunning){
			if (!startedRunning){
				if (stepStage%2 == 0){
					GetComponent<AudioSource>().clip = stepOne;
					GetComponent<AudioSource>().Play();
					startedRunning = true;
				} else {
					GetComponent<AudioSource>().clip = stepTwo;
					GetComponent<AudioSource>().Play ();
					startedRunning = true;
				}
			}
		} else {
			GetComponent<AudioSource>().Stop();
		}
//		if (isWalking){
//			if (!startedWalking){
//				GetComponent<AudioSource>().Play();
//				startedWalking = true;
//			}
//		} else {
//			GetComponent<AudioSource>().Stop();
//			startedWalking = false;
//		}
	}
}
