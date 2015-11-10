using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Vector3 desiredPos, currentPos;
	public int camZOffset, camYOffset, camSyncRate;
	public GameObject playerObj;
	public bool isFree;
	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DetectInputs(){
		if (Input.GetKey(KeyCode.Mouse0)){
			isFree = true;
		} else {
			isFree = false;
		}
	}

	public void CamMove (){
		desiredPos = playerObj.transform.position - playerObj.transform.forward * camZOffset + playerObj.transform.up * camYOffset;
		currentPos = Vector3.Lerp(currentPos, desiredPos, camSyncRate);
		transform.position = currentPos;
		Quaternion.LookRotation(currentPos - playerObj.transform.position, Vector3.up);
	}
}
