using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitTrigger : MonoBehaviour {
	public bool isAttacking;
	public int playerDamage;
	public GameObject player;
	public List<GameObject> enemiesInRange;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		playerDamage = player.GetComponent<PlayerScript>().damage;
	}

	public void TriggerAttack(int damage){
		foreach (GameObject i in enemiesInRange){
			i.GetComponent<EnemyScript>().AttackSequence(damage);
		}
	}

	void OnTriggerEnter(Collider col){
			if (col.gameObject.tag == "Enemy"){
				enemiesInRange.Add (col.gameObject);
			}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Enemy"){
			enemiesInRange.Remove(col.gameObject);
		}
	}
}
