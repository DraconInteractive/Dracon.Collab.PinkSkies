using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Vector3 desiredPos, currentPos;
	public int rotateSpeed, camDistance;
	public float camSyncRate;
	public GameObject playerObj;
	public bool isFree;
	public Transform thisTransform;
	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag("Player");
		thisTransform = this.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		DetectInputs();
		CamMove();

	}

	public void DetectInputs(){
		if (Input.GetKey(KeyCode.Mouse0)){
			isFree = true;
		} else {
			isFree = false;
		}
	}

	public void CamMove (){

		thisTransform.RotateAround(playerObj.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
		thisTransform.RotateAround(playerObj.transform.position, Vector3.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);

		thisTransform.LookAt(playerObj.transform.position);

		desiredPos = playerObj.transform.position - this.gameObject.transform.position;
		if (Vector3.Distance(playerObj.transform.position, this.transform.position) > camDistance){
			currentPos = Vector3.Lerp (currentPos, desiredPos, camSyncRate);
			transform.position = currentPos;
		}




//		desiredPos = playerObj.transform.position - playerObj.transform.forward * camZOffset + playerObj.transform.up * camYOffset;
//		if (playerObj.GetComponent<PlayerScript>().isFullSpeed == true){
//			camSyncRate = 0.04f;
//		} else {
//			camSyncRate = 0.01f;
//		}
//		currentPos = Vector3.Lerp(currentPos, desiredPos, camSyncRate);
//		transform.position = currentPos;
//		transform.rotation = Quaternion.LookRotation(playerObj.transform.position - currentPos, Vector3.up);


	}
}
