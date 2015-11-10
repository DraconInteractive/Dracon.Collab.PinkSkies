using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Rigidbody playerRigid;
	public Transform playerTrans;
	public float  speed, speedMod, rotateAngle, jumpForce;
	public bool isFullSpeed;
	// Use this for initialization
	void Start () {
		playerRigid = GetComponent<Rigidbody>();
		playerTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerInput();
	}

	public void PlayerInput(){
		speed = Input.GetAxis("Vertical") * speedMod;
		//Apply the direction to the character, with modifiers of speed and incrementation
		playerRigid.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);
		playerTrans.Rotate(Vector3.up, rotateAngle * Input.GetAxis("Horizontal") * Time.deltaTime);

		if (speed >= 6){
			isFullSpeed = true;
		} else {
			isFullSpeed = false;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		    playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
