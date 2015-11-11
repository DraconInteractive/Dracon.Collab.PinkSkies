using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health;
	public bool attackable;
	// Use this for initialization
	void Start () {
		health = 100;
		attackable = false;
	}
	
	// Update is called once per frame
	void Update () {
		AttackSequence();
	}

	public void AttackSequence(){
		if (attackable){
			if (Input.GetMouseButtonDown(0)){
				health -= 10;
			}
		}
	}

	public void ToggleAttackable(){
		attackable = !attackable;
	}
}
