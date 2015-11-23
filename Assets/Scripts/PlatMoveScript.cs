using UnityEngine;
using System.Collections;

public class PlatMoveScript : MonoBehaviour {
	public bool activated;
	public float initialZ, speed;
	public Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		initialZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		ActivatePlatform();
	}

	public void ActivatePlatform (){
		if (activated){
			if (transform.position.y < initialZ + 10){
				myRigid.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
			}
		}
	}
}
