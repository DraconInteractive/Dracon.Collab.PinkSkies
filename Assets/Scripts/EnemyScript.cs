using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;
	public float speed = 17, maxSpeed = 1.5f, attackSpdTimer = 0, attackSpd = 1, jumpHeight = 50;

	private float triggerDisMax = 10, meleeDis = 0.5f;
	private GameObject player;
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
		IsGrounded ();
	}

	void IsGrounded(){
		RaycastHit hit;
		Vector3 enemyFeet = new Vector3 (transform.position.x, transform.position.y - 0.6f, transform.position.z);

		//-----------------------------testing-----------------------------------------
		Vector3 enemyFeet2 = new Vector3 (enemyFeet.x, enemyFeet.y - 0.5f, enemyFeet.z);
		Debug.DrawLine (enemyFeet, enemyFeet + transform.forward, Color.red);
		Debug.DrawLine (enemyFeet, enemyFeet2, Color.green);
		//-----------------------------testing-----------------------------------------

		if (Physics.Raycast (enemyFeet, -transform.up, out hit, 0.5f)) {
			if(hit.collider.tag == "Ground"){
				Debug.Log("grounded");
				if(Physics.Raycast(enemyFeet, transform.forward, out hit, 4)){
					if(hit.collider.tag == "Ground"){
						Debug.Log ("Jumping");
						rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
					}
				}
			}
		}
	}

	void EnemyMovement(){

		attackSpdTimer -= Time.deltaTime;

		rb.velocity = Vector3.ClampMagnitude (rb.velocity, maxSpeed);

		float distance = Vector3.Distance (transform.position, player.transform.position);

		Debug.Log (distance);

		if (distance <= triggerDisMax && distance >= meleeDis) {//chasing
			Vector3 playerPos = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
			transform.LookAt (playerPos);
			rb.AddForce (transform.forward * speed);
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
