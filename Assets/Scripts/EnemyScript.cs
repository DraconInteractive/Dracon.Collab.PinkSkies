using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;

	public float speed = 500, maxSpeed = 3, attackSpdTimer = 0, attackSpd = 2, maxTriggerDis = 20, jumpHeight = 15, meleeDis = 0.5f, stillTime = 0;
	public int attackDmg = 10;
	public Transform enemyFeet;
	public GameObject hitTrig;
	public Transform jumpDownPoint;
	public bool enemyType;//if true jumpdown enemy / if false point enemy

	private GameObject player;
	private bool atPoint = false;
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

		if(enemyType) {
			HealthCheck();
			if(!atPoint){
				JumpToPoint ();//jumping down enemy		
			} else {
				EnemyMovement ();
			}
			IsGrounded ();
			EnemyReset ();
			EnemyAttack ();	
		} else {
			HealthCheck ();
			MoveToPoint ();
			EnemyMovement ();
			IsGrounded ();
			EnemyReset ();
			EnemyAttack ();	
		}

	}

	void MoveToPoint (){
		//move between points
	}

	void JumpToPoint (){
		float mySpeed = new Vector3 (rb.velocity.x, 0, rb.velocity.z).magnitude;
		Vector3 point = new Vector3 (jumpDownPoint.transform.position.x, transform.position.y, jumpDownPoint.transform.position.z);
		transform.LookAt (point);

		if (Vector3.Distance(transform.position, jumpDownPoint.position) < 1){
			atPoint = true;
		}

		if (mySpeed < maxSpeed){
			rb.AddForce (transform.forward * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player") canHit = true;
	}

	void EnemyAttack(){

		attackSpdTimer -= Time.deltaTime;
		float armour = playerScript.armour;

		if (!playerScript.immune){//if can hit
			playerScript.SetImmune;
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

		if (distance <= maxTriggerDis && distance >= meleeDis && mySpeed < maxSpeed) {//chasing
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
			HitTrigger hitTriggerScript = hitTrig.GetComponent<HitTrigger>();

			hitTriggerScript.enemiesInRange.Remove(gameObject);
			Destroy(gameObject);
		}
	}
}
