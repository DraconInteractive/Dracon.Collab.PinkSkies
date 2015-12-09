using UnityEngine;
using System.Collections;

public class BarrelScript : MonoBehaviour {
	public int timesHit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HitCounter();
	}

	public void HitCounter(){
		if (timesHit == 2){
			Destroy(this.gameObject);
			GameObject.Find ("Player").GetComponent<PlayerScript>().scrapCount++;
		}
	}
}
