using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour {
	public bool activated;
	public float initialY, speed;
	public Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		initialY = transform.position.y;
		myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		ActivateElevator();
	}

	public void ActivateElevator (){
		if (activated){
			if (transform.position.y < initialY + 10){
				myRigid.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
			}
		}
	}
}
