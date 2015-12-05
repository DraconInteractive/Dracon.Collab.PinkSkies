using UnityEngine;
using System.Collections;

public class BarTriggerOneScript : MonoBehaviour {
	public GameObject barricadeLinked;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player"){
			barricadeLinked.GetComponent<BarricadeScript>().activated = true;
			Destroy(this.gameObject);
		}
	}
}
