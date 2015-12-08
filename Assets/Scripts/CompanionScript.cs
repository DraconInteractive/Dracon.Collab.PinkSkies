using UnityEngine;
using System.Collections;

public class CompanionScript : MonoBehaviour {
	public GameObject player;
	public Rigidbody thisRigidBody;
	public int playerDistance, regenCap, regenAmount;
	public float companionSpeed, healthBuild;
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
		transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x, player.transform.position.y + 1, transform.position.z), 0.01f);

		Vector3 playerDirection = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) - transform.position;
		if (Vector3.Distance(player.transform.position, this.transform.position) > playerDistance){
			thisRigidBody.MovePosition(transform.position + playerDirection * companionSpeed * Time.deltaTime);
		}
	}

	public void HealPlayer(){
		if (player.GetComponent<PlayerScript>().inCombat == false){
			if (player.GetComponent<PlayerScript>().finalHealth < regenCap){
				healthBuild += 1 * Time.deltaTime;
				if (healthBuild >= 1){
					healthBuild = 0;
					player.GetComponent<PlayerScript>().finalHealth += 1;
				}

			}
		}
	}
}
