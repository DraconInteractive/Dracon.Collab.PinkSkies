using UnityEngine;
using System.Collections;

public class HitTrigger : MonoBehaviour {
	public bool isAttacking;
	public int playerDamage;
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		playerDamage = player.GetComponent<PlayerScript>().damage;
	}

	public void TriggerAttack(int d){
		isAttacking = true;
	}

	void OnTriggerEnter(Collider col){
		//if (isAttacking){
			//isAttacking = false;
			if (col.gameObject.tag == "Enemy"){
				col.GetComponent<EnemyScript>().ToggleAttackable(playerDamage);
			}
		//}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Enemy"){
			col.GetComponent<EnemyScript>().ToggleAttackable(playerDamage);
		}
	}
}
