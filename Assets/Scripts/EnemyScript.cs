using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;

	public float speed = 500, maxSpeed = 1.5f, attackSpdTimer = 0, attackSpd = 1, jumpHeight = 15, triggerDisMax = 10, meleeDis = 0.5f, stillTime = 0;
	private GameObject player;
	private Rigidbody rb;
	public bool grounded, chasing;

	void Start () {
		health = 100;
		attackable = false;
		player = GameObject.FindWithTag ("Player");
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		HealthCheck();
		EnemyMovement ();
		IsGrounded ();
		EnemyReset ();
	}

	void EnemyReset(){

		if (chasing) {
			if (rb.velocity.magnitude < 0.1f) {
				stillTime += Time.deltaTime; 
			}
			
			if(stillTime >= 1){
				rb.AddForce (-transform.forward * 200 * Time.deltaTime, ForceMode.Impulse);
				stillTime = 0;
			}
		}

	}

	void IsGrounded(){
		RaycastHit hit;
		Vector3 enemyFeet = new Vector3 (transform.position.x, transform.position.y - 0.6f, transform.position.z);

		//-----------------------------testing-----------------------------------------
		Vector3 enemyFeet2 = new Vector3 (enemyFeet.x, enemyFeet.y - 0.5f, enemyFeet.z);
		Debug.DrawLine (enemyFeet, enemyFeet + transform.forward, Color.red);
		//-----------------------------testing-----------------------------------------

		if (Physics.SphereCast (enemyFeet + Vector3.up, 1, -transform.up, out hit, 0.4f)) {
			if (hit.collider.tag == "Ground") {
				grounded = true;
				Debug.Log ("grounded");
				if (Physics.Raycast (enemyFeet, transform.forward, out hit, 1)) {
					if (hit.collider.tag == "Ground") {
						Debug.Log ("Jumping");
						rb.AddForce (transform.up * jumpHeight * Time.deltaTime, ForceMode.Impulse);
					}
				}
			}
		} else {
			grounded = false;
		}
	}

	void EnemyMovement(){

		attackSpdTimer -= Time.deltaTime;
		float mySpeed = new Vector3 (rb.velocity.x, 0, rb.velocity.z).magnitude;
		float distance = Vector3.Distance (transform.position, player.transform.position);

		if (distance <= triggerDisMax && distance >= meleeDis && mySpeed < maxSpeed) {//chasing
			chasing = true;

			Vector3 playerPos = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
			transform.LookAt (playerPos);
			rb.AddForce (transform.forward * speed * Time.deltaTime);
		} else {
			chasing = false;
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
