using UnityEngine;
using System.Collections;

public class CompanionScript : MonoBehaviour {
	public GameObject player;
	public Rigidbody thisRigidBody;
	public int playerDistance, regenCap, regenAmount;
	public float companionSpeed;
	// Use this for initialization
	void Start () {
		thisRigidBody = GetComponent<Rigidbody>();
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		MoveToPlayer();
		HealPlayer();
	}

	public void MoveToPlayer(){
		Vector3 playerDirection = player.transform.position - transform.position;
		if (Vector3.Distance(player.transform.position, this.transform.position) > playerDistance){
			thisRigidBody.MovePosition(transform.position + playerDirection * companionSpeed * Time.deltaTime);
		}
	}

	public void HealPlayer(){
		if (player.GetComponent<PlayerScript>().inCombat == false){
			if (player.GetComponent<PlayerScript>().health < regenCap){
				player.GetComponent<PlayerScript>().health += (regenAmount * Time.deltaTime);
			}
		}
	}
}
