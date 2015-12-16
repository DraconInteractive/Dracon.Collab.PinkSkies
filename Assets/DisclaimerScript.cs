using UnityEngine;
using System.Collections;

public class DisclaimerScript : MonoBehaviour {
	public float timer = 0;
	public bool startedLoad;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 4){
			if (!startedLoad){
				if (Application.loadedLevelName == "Disclaimer"){
					Application.LoadLevel("TeamPresents");
				} else if (Application.loadedLevelName == "TeamPresents"){
					Application.LoadLevel("Main");
				}
				startedLoad = true;
			}
		}
	}
}
