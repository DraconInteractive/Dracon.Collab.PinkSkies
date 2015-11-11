using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public int health;
	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AttackSequence(){
		Debug.Log ("has started attack sequence");
		health -= 10;
	}
}
