using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;

	private GameObject player;
	private float speed = 20, maxSpeed = 2, attackSpdTimer = 0, attackSpd = 1, triggerDisMax, triggerDisMin, meleeDis;
	private Rigidbody rb;

	void Start () {
		health = 100;
		attackable = false;
		player = GameObject.FindWithTag ("Player");
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		HealthCheck();
		EnemyMovement ();
	}

	void EnemyMovement(){

		attackSpdTimer -= Time.deltaTime;

		rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxSpeed);

		float distance = Vector3.Distance (transform.position, player.transform.position);

		if (distance <= triggerDisMax && distance >= triggerDisMin) {//chasing
			Vector3 playerPos = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
			transform.LookAt (playerPos);
			GetComponent<Rigidbody> ().AddForce (transform.forward * speed);
		} 
	}

	public void AttackSequence(int damage){
		health -= damage;
	}

	public void HealthCheck(){
		if (health <= 0){
			Destroy(this.gameObject);
		}
	}
}
