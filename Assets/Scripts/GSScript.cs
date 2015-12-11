using UnityEngine;
using System.Collections;

public class GSScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		RotateTheGS();
	}

	public void RotateTheGS(){
		GetComponent<Transform>().Rotate(Vector3.up, 45 * Time.deltaTime);
	}
}
