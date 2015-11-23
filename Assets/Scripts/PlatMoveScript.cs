using UnityEngine;
using System.Collections;

public class PlatMoveScript : MonoBehaviour {
	public bool activated;
	public float initialX, speed;
	public Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		initialX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		ActivatePlatform();
	}

	public void ActivatePlatform (){
		if (activated){
			if (transform.position.x <= initialX + 10){
				myRigid.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
			}
		}
	}
}
