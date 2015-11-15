using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health, playerDamageRecieved;
	public bool attackable;
	// Use this for initialization
	void Start () {
		health = 100;
		attackable = false;
	}
	
	// Update is called once per frame
	void Update () {
		AttackSequence();
		HealthCheck();
	}

	public void AttackSequence(){
		if (attackable){
			if (Input.GetMouseButtonDown(0)){
				health -= playerDamageRecieved;
			}
		}
	}

	public void ToggleAttackable(int d){
		playerDamageRecieved = d;
		attackable = !attackable;
	}

	public void HealthCheck(){
		if (health <= 0){
			Destroy(this.gameObject);
		}
	}
}
