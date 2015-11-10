using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Rigidbody playerRigid;
	public float speed;
	// Use this for initialization
	void Start () {
		playerRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerInput();
	}

	public void PlayerInput(){
		//Set the players direction = to the horizontal and vertical input axis of the keyboard input (WASD and arrow keys)
		Vector3 _Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		//Apply the direction to the character, with modifiers of speed and incrementation
		playerRigid.MovePosition (transform.position + _Direction * speed * Time.deltaTime);
		if (_Direction.magnitude > 0){
			transform.forward = _Direction;
		}
		
		//Keep a jumping function tucked away JIC
		//if (Input.GetKeyDown(KeyCode.Space))
		//    rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	}
}
