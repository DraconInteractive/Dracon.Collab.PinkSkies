using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour {
	public bool activated;
	public float initialY, speed, moveDist;
	public Rigidbody myRigid;
	public bool hasSetPlayer, hasBeenActivated;
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
			hasBeenActivated = true;
			Destroy (GameObject.Find ("ElevatorTrigger"));
			if (transform.position.y < initialY + moveDist){
				myRigid.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
				Camera.main.GetComponent<CameraScript>().SetPlayerType(this.gameObject);
			} else {
				activated = false;
			}
		} else {
			if (hasBeenActivated){
				if (!hasSetPlayer){
					Camera.main.GetComponent<CameraScript>().SetPlayerType(GameObject.Find ("Player"));
					hasSetPlayer = true;
					GameObject.Find ("Player").GetComponent<PlayerScript>().inElevator = false;
				}
			}
		}
	}
}
