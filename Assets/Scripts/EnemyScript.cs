using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;

	public float speed = 500, maxSpeed = 1.5f, attackSpdTimer = 0, attackSpd = 2, jumpHeight = 15, triggerDisMax = 10, meleeDis = 0.5f, stillTime = 0;
	public int attackDmg = 10;
	public Transform enemyFeet;

	private GameObject player;
	private PlayerScript playerScript;
	private Rigidbody rb;
	public bool grounded, chasing, canHit;

	void Start () {
		health = 100;
		attackable = false;
		player = GameObject.FindWithTag ("Player");
		playerScript = player.GetComponent<PlayerScript> ();
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		HealthCheck();
		EnemyMovement ();
		IsGrounded ();
		EnemyReset ();
		EnemyAttack ();	

		attackSpdTimer -= Time.deltaTime;

	}


	void OnTriggerEnter(Collider other){
		if(other.tag == "Player") canHit = true;
	}

	void EnemyAttack(){

		float armour = playerScript.armour;
		float currHealth = playerScript.initialHealth;

		if (canHit && attackSpdTimer <= 0) {//can attack

			attackSpdTimer = attackSpd;

			if(armour > 0) {//if player has armour

				int leftOverDmg = attackDmg - playerScript.armour;//e.g 10-4 = 6 or 10-15 = -5

				playerScript.armour -= attackDmg;

				if(leftOverDmg > 0){
					playerScript.initialHealth -= leftOverDmg;
				}

			} else {
				playerScript.initialHealth -= attackDmg;
			}

		}
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

		if (Physics.SphereCast (enemyFeet.position + Vector3.up, 1, -transform.up, out hit, 0.4f)) {
			if (hit.collider.tag == "Ground") {
				grounded = true;
				if (Physics.Raycast (enemyFeet.position, transform.forward, out hit, 1)) {//raycast forwards checking for ground
					if (hit.collider.tag == "Ground") {
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

		Vector3 playerPos = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((playerPos - transform.position), Vector3.up), 0.08f);

		if (distance <= triggerDisMax && distance >= meleeDis && mySpeed < maxSpeed) {//chasing
			chasing = true;

			rb.AddForce (transform.forward * speed * Time.deltaTime);
		} else {
			chasing = false;
		}
	}

	public void EnemyTakingDamage(int dmg){

		health -= dmg;

	}

	public void HealthCheck(){
		if (health <= 0){
			Destroy(gameObject);
		}
	}
}
