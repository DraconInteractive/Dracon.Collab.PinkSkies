using UnityEngine;
using System.Collections;

public class BarricadeScript : MonoBehaviour {
	public bool activated;
	public float initialY;
	public Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		initialY = transform.position.y;
		GetComponent<BoxCollider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		ActivateBarricades();
	}

	public void ActivateBarricades (){
		if (activated){
			if (transform.position.y < initialY + 2){
				myRigid.MovePosition(transform.position + Vector3.up * Time.deltaTime);
			} else {
				GetComponent<BoxCollider>().enabled = true;
			}

		}
	}
}
