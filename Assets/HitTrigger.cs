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
		Debug.Log ("Has trigger attacked");
		isAttacking = true;
	}

	void OnTriggerStay(Collider col){
		//if (isAttacking){
			//isAttacking = false;
		Debug.Log ("Object in player hit trigger");
			if (col.gameObject.tag == "Enemy"){
				Debug.Log ("Has enemy attacked");
				col.GetComponent<EnemyScript>().AttackSequence();
			}
		//}
	}
}
