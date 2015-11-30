using UnityEngine;
using System.Collections;

public class GSScript : MonoBehaviour {
	public GameObject playerObj;
	public Vector3 holsteredPos, attackPos;
	// Use this for initialization
	void Start () {
		holsteredPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		DetectAttackState();
	}

	public void DetectAttackState(){
		if (playerObj.GetComponent<PlayerScript>().inCombat == true){

		} else {

		}
	}
}
