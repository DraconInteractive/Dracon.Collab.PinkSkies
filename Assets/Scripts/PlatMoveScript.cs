using UnityEngine;
using System.Collections;

public class PlatMoveScript : MonoBehaviour {
	public bool activated, moveForward, moveBackward;
	public float speed, moveDist, moveTime;
	public Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		ActivatePlatform();
	}

	public void ActivatePlatform (){
		if (activated){
			if (moveForward){

				if (moveTime > 0){
					moveTime -= Time.deltaTime;
					transform.Translate(Vector3.forward * speed * Time.deltaTime);
				}

			} else if (moveBackward){

				if (moveTime > 0){
					moveTime -= Time.deltaTime;
					transform.Translate(-Vector3.forward * speed * Time.deltaTime);
				}
			}
		}
	}
}
