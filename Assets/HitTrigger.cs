using UnityEngine;
using System.Collections;

public class HitTrigger : MonoBehaviour {
	public bool isAttacking;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void TriggerAttack(){
		isAttacking = true;
	}

	void OnTriggerEnter(Collider col){
		//if (isAttacking){
			//isAttacking = false;
			if (col.gameObject.tag == "Enemy"){
				col.GetComponent<EnemyScript>().ToggleAttackable();
			}
		//}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Enemy"){
			col.GetComponent<EnemyScript>().ToggleAttackable();
		}
	}
}
