using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Vector3 desiredPos, currentPos;
	public int rotateSpeed, camYInversionControl;
	public float camSyncRate, camDistanceInitial, camDistanceEventual;
	public GameObject playerObj, camDistSlider;
	public GameObject target;
	public bool isFree, isGrounded, isUnobjstructed;
	public Transform thisTransform;
	public float camYDiff;
	public string playerType;
	// Use this for initialization
	void Start () {
		playerType = "PlayerObj";
		playerObj = GameObject.Find ("Player");
		thisTransform = this.gameObject.GetComponent<Transform>();
		currentPos = playerObj.transform.position - Vector3.back * 10 + Vector3.up * 5;

		camDistanceInitial = camDistSlider.GetComponent<Slider>().value;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerType == "PlayerObj"){
			DetectPlayerOptions();
			DetectInputs();
			DetectGUI();
			CamMove();
			DetectObstruction();
		} else if (playerType == "SpaceShip"){
			SpaceShipCam();
		}


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
			camDistanceInitial = camDistSlider.GetComponent<Slider>().value;
		}
	}

	public void DetectObstruction(){

		Vector3 cameraDirection = transform.position - (playerObj.transform.position);
		RaycastHit hit;
		if (Physics.Raycast(playerObj.transform.position, cameraDirection, out hit, camDistanceInitial)){
			isUnobjstructed = false;
			Debug.Log (hit.collider.gameObject.name);
			transform.position = Vector3.Lerp (transform.position, hit.point, 0.1f);
		} else {
			isUnobjstructed = true;
			transform.position = Vector3.Lerp (transform.position, playerObj.transform.position - transform.forward * (camDistanceInitial - camDistanceEventual), 0.1f);
		}

		Debug.DrawRay(playerObj.transform.position + playerObj.transform.up, cameraDirection, Color.red);
	}


	//This is for sync purpose
	public void CamMove (){

		camYDiff = playerObj.transform.position.y - transform.position.y;
		// -1 (low) to -7 (high)
		

		thisTransform.RotateAround(playerObj.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
		thisTransform.RotateAround(playerObj.transform.position, transform.right, Input.GetAxis("Mouse Y") * camYInversionControl * rotateSpeed * Time.deltaTime);

		thisTransform.LookAt(playerObj.transform.position);

	}

	public void SetPlayerType(GameObject i){
		if (i.GetComponent<PlayerScript>()){
			playerType = "PlayerObj";
		} else if (i.GetComponent<SpaceShipScript>()){
			playerType = "SpaceShip";
			target = i;
		} 
	}

	public void SpaceShipCam(){
		transform.position = target.transform.position + (Vector3.up * 4) + (Vector3.right * 25);
		thisTransform.LookAt(target.transform.position);
	}


}
