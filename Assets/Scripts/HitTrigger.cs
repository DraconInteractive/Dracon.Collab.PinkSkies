using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitTrigger : MonoBehaviour {
	public bool isAttacking;
	public int playerDamage;
	public GameObject player;
	public List<GameObject> enemiesInRange;
	public List<GameObject> destructableInRange;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		playerDamage = player.GetComponent<PlayerScript>().damage;
	}

	public void TriggerAttack(int damage){
		if (enemiesInRange.Count > 0){
			foreach (GameObject i in enemiesInRange){
				if (i == null){

				} else if (i.GetComponent<EnemyScript>()){
					i.GetComponent<EnemyScript>().EnemyTakingDamage(damage);
				}
			}
		}
		if (destructableInRange.Count > 0){
			foreach (GameObject i in destructableInRange){
				if (i == null){

				} else if (i.GetComponent<BarrelScript>()){
					i.GetComponent<BarrelScript>().timesHit ++;
				}
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Enemy"){
			enemiesInRange.Add (col.gameObject);
		} else if (col.gameObject.tag == "Barrel"){
			destructableInRange.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Enemy"){
			enemiesInRange.Remove(col.gameObject);
		} else if (col.gameObject.tag == "Barrel"){
			destructableInRange.Remove(col.gameObject);
		}
	}
}
