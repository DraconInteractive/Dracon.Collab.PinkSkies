using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;

	public float speed = 1500, maxSpeed = 1.5f, attackSpdTimer = 0, attackSpd = 3, maxTriggerDis = 20, jumpHeight = 15, meleeDis = 2.75f, stillTime = 0;
	public int attackDmg = 10;
	public Transform enemyFeet;
	public GameObject playerHitTrig;
	public Transform jumpDownPoint;
	public bool enemyType, grounded, chasing, canHit, boss;//enemyType: if true jumpdown enemy / if false point enemy
	public Transform[] movingPoints = new Transform[4];

	private GameObject player;
	public int currPoint = 0;
	private bool atPoint = false;
	private PlayerScript playerScript;
	private Rigidbody rb;
	private Animator anim;

	void Start () {
		playerHitTrig = GameObject.Find ("HitTrigger");
		anim = GetComponent<Animator> ();
		health = 100;
		attackable = false;
		player = GameObject.FindWithTag ("Player");
		playerScript = player.GetComponent<PlayerScript> ();
		rb = GetComponent<Rigidbody> ();

		if (boss) {
			health = 600;
			attackSpd = 3;
			meleeDis = 18;
			attackDmg = 15;
		}
	}

	void Update () {

		float mySpeed = new Vector3 (rb.velocity.x, 0, rb.velocity.z).magnitude;

		if (mySpeed >= 0.1f) {			
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
		}

		if (boss) {//boss
			HealthCheck ();
			EnemyAttack ();
			IsGrounded ();
			EnemyMovement ();
			EnemyReset ();
		} else {//not boss
			if(enemyType) {//jumping down enemy
				HealthCheck();
				if(!atPoint){
					JumpToPoint ();
				} else {
					EnemyMovement ();
				}
				IsGrounded ();
				EnemyReset ();
				EnemyAttack ();	
			} else {//moving point to point enemy
				HealthCheck ();
				if (!chasing) MoveToPoint ();
				EnemyMovement ();
				IsGrounded ();
				EnemyReset ();
				EnemyAttack ();	
			}
		}

	}

	void MoveToPoint (){

		float mySpeed = new Vector3 (rb.velocity.x, 0, rb.velocity.z).magnitude;

		switch (currPoint) {

		case 0:
			Vector3 pointToGoTo = new Vector3 (movingPoints [0].position.x, transform.position.y, movingPoints [0].position.z);

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((pointToGoTo - transform.position), Vector3.up), 1f);
			if (mySpeed < maxSpeed){
				rb.AddForce (transform.forward * speed * Time.deltaTime);
			}
			if (Vector3.Distance(transform.position, pointToGoTo) < 0.1f){
				currPoint++;
			}
			break;

		case 1:
			Vector3 pointToGoTo1 = new Vector3 (movingPoints [1].position.x, transform.position.y, movingPoints [1].position.z);
			
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((pointToGoTo1 - transform.position), Vector3.up), 1f);
			if (mySpeed < maxSpeed){
				rb.AddForce (transform.forward * speed * Time.deltaTime);
			}
			if (Vector3.Distance(transform.position, pointToGoTo1) < 0.1f){
				currPoint++;
			}
			break;

		case 2:
			Vector3 pointToGoTo2 = new Vector3 (movingPoints [2].position.x, transform.position.y, movingPoints [2].position.z);
			
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((pointToGoTo2 - transform.position), Vector3.up), 1f);
			if (mySpeed < maxSpeed){
				rb.AddForce (transform.forward * speed * Time.deltaTime);
			}
			if (Vector3.Distance(transform.position, pointToGoTo2) < 0.1f){
				currPoint++;
			}
			break;

		case 3:
			Vector3 pointToGoTo3 = new Vector3 (movingPoints [3].position.x, transform.position.y, movingPoints [3].position.z);
			
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((pointToGoTo3 - transform.position), Vector3.up), 1f);
			if (mySpeed < maxSpeed){
				rb.AddForce (transform.forward * speed * Time.deltaTime);
			};
			if (Vector3.Distance(transform.position, pointToGoTo3) < 0.1f){
				currPoint = 0;
			}
			break;

		}
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

	void OnTriggerExit(Collider other){
		if(other.tag == "Player") canHit = false;
	}

	void EnemyAttack(){
		attackSpdTimer -= Time.deltaTime;
		float armour = playerScript.armour;

		if (canHit && attackSpdTimer <= 0) {//can attack
			if (!playerScript.immune){
				playerScript.SetImmune ();
				Debug.Log (gameObject.name + " Can Attack");
				StartCoroutine(AttackBool());//animation starter
				Debug.Log (gameObject.name + " Started Attack Anim");
				attackSpdTimer = attackSpd;//reseting the attack timer
				Debug.Log (gameObject.name + " Reset attack timer");
				if(armour > 0) {//if player has armour
					
					int leftOverDmg = attackDmg - playerScript.armour;//e.g 10-4 = 6 or 10-15 = -5
					
					playerScript.armour -= attackDmg;//taking armour away
					
					if(leftOverDmg > 0){
						playerScript.initialHealth -= leftOverDmg;//if the armour wasnt enough to defend the attack take health away
					}
					
				} else {
					playerScript.initialHealth -= attackDmg;//take health away
				}
			}
		}

	}

	IEnumerator AttackBool(){
		anim.SetBool ("Attack", true);//start animaiton
		yield return new WaitForSeconds (1.5f);//wait 1.5 seconds
		anim.SetBool ("Attack", false);//stop animation
	}

	void EnemyReset(){//getting the enemy unstuck

		float distance = Vector3.Distance (transform.position, player.transform.position);//distance between player and enemy

		if (chasing) {//if chasing
			if (distance >= meleeDis) {//if enemy is not in range to attack 
				if (rb.velocity.magnitude < 0.1f) {//if enemy is not moving
					stillTime += Time.deltaTime; //start timer
				}
				
				if(stillTime >= 1){//once timer equals 1 second
					rb.AddForce (-transform.forward * 200 * Time.deltaTime, ForceMode.Impulse);//jump backwards
					stillTime = 0;//reset timer
				}
			}
		}

	}

	void IsGrounded(){//is grounded
		RaycastHit hit;

		if (Physics.SphereCast (enemyFeet.position + Vector3.up, 1, -transform.up, out hit, 1.2f)) {
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

		float turnSpeed;
		float mySpeed = new Vector3 (rb.velocity.x, 0, rb.velocity.z).magnitude;
		float distance = Vector3.Distance (transform.position, player.transform.position);

		if (boss) {
			turnSpeed = 0.02f;
		} else {
			turnSpeed = 0.3f;
		}

		Vector3 playerPos = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((playerPos - transform.position), Vector3.up), turnSpeed);

		if (distance <= maxTriggerDis && distance >= meleeDis && mySpeed < maxSpeed) {//chasing
			if(boss){
				speed = 900000;
				maxSpeed = 8;
			} else {
				speed = 1500;
				maxSpeed = 5;
			}
			chasing = true;
			playerScript.inCombat = true;
			rb.AddForce (transform.forward * speed * Time.deltaTime);
		} else if (distance >= maxTriggerDis) {
			playerScript.inCombat = false;
			chasing = false;
			if(boss){
				speed = 3000;
				maxSpeed = 5;
			} else {
				speed = 1000;
				maxSpeed = 4;
			}
		}
	}

	public void EnemyTakingDamage(int dmg){

		health -= dmg;

	}

	public void HealthCheck(){
		if (health <= 0){
			HitTrigger hitTriggerScript = playerHitTrig.GetComponent<HitTrigger>();

			hitTriggerScript.enemiesInRange.Remove(gameObject);
			Destroy(gameObject);
		}
	}
}
