using UnityEngine;
using System.Collections;

public class CameraScript2 : MonoBehaviour {
	public GameObject playerObj;
	public PlayerScript playerScript;

	public Transform myTransform;
	public Rigidbody myRigidbody;

	public Vector3 myPosition;

	public float forwardDist, camHeight, camRotation;
	// Use this for initialization
	void Start () {
		playerObj = GameObject.Find ("Player");
		playerScript = playerObj.GetComponent<PlayerScript>();
		//transform.position = playerObj.transform.position -(playerObj.transform.forward*7)+ (playerObj.transform.up*4);
		myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		CamRotate();
		/*myPosition = myTransform.position;

		myTransform.LookAt(playerObj.transform.position + (Vector3.up * 2));

		if (Input.GetAxis("Mouse Y") != 0){
			camHeight += Input.GetAxis("Mouse Y") * Time.deltaTime;
		}
		if (Input.GetAxis("Mouse X") != 0){
			camRotation += Input.GetAxis("Mouse X") * 0.3f;
		}else{
			camRotation = Mathf.Lerp (camRotation, 0, 0.8f);
		}
	//	transform.RotateAround(playerObj.transform.position,Vector3.up,)
		myTransform.position = Vector3.Lerp(myTransform.position, (playerObj.transform.position - (playerObj.transform.forward * forwardDist)+ (playerObj.transform.up * camHeight) /* + (transform.right * camRotation)), 0.05f);
	*/
	}

	void CamRotate(){	
		//adjust rotation based off mouse input
		if (Input.GetAxis("Mouse Y") != 0){
			transform.RotateAround(playerObj.transform.position+ playerObj.transform.up*3,playerObj.transform.right,180*Input.GetAxis("Mouse Y") * -1 *Time.deltaTime);
		}

		if (Input.GetAxis("Mouse X") != 0){
			transform.RotateAround(playerObj.transform.position+ playerObj.transform.up*3,playerObj.transform.up,180*Input.GetAxis("Mouse X")*Time.deltaTime);
		}
		//rotate to look above the players feet by three units
		transform.LookAt(playerObj.transform.position+ playerObj.transform.up*3);

		if((transform.position - playerObj.transform.position).magnitude > 6){ // if outside range move forward, but don't change y pos
			Vector3 desired = playerObj.transform.position - (playerObj.transform.forward*7);
			transform.position = Vector3.Lerp(transform.position,new Vector3(desired.x,transform.position.y,desired.z),0.05f);
		}

	}
}
