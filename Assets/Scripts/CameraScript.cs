using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Vector3 desiredPos, currentPos;
	public int rotateSpeed, camYInversionControl;
	public float camSyncRate, camDistance;
	public GameObject playerObj, camDistSlider;
	public bool isFree, isGrounded;
	public Transform thisTransform;
	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag("Player");
		thisTransform = this.gameObject.GetComponent<Transform>();
		currentPos = playerObj.transform.position - Vector3.back * 10 + Vector3.up * 5;

		camDistance = camDistSlider.GetComponent<Slider>().value;
	}
	
	// Update is called once per frame
	void Update () {
		DetectPlayerOptions();
		DetectInputs();
		DetectGUI();
		CamMove();

	}

	public void DetectPlayerOptions(){
		if (playerObj.GetComponent<PlayerScript>().isInvertingCamY == true){
			camYInversionControl = 1;
		} else {
			camYInversionControl = -1;
		}
	}

	public void DetectInputs(){
		if (Input.GetKey(KeyCode.Mouse0)){
			isFree = true;
		} else {
			isFree = false;
		}
	}

	public void DetectGUI(){
		if (camDistSlider.activeSelf == true){
			camDistance = camDistSlider.GetComponent<Slider>().value;
		}
	}

	public void CamMove (){

		thisTransform.RotateAround(playerObj.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
		thisTransform.RotateAround(playerObj.transform.position, transform.right, Input.GetAxis("Mouse Y") * camYInversionControl * rotateSpeed * Time.deltaTime);



		thisTransform.LookAt(playerObj.transform.position);

		transform.position = playerObj.transform.position - transform.forward * camDistance;
	//	desiredPos = playerObj.transform.position - thisTransform.position;
		//Debug.Log(desiredPos.ToString());

		//if (transform.position != transform.position + desiredPos){
//			currentPos = Vector3.Lerp (currentPos, desiredPos, camSyncRate);
			
		//}




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
