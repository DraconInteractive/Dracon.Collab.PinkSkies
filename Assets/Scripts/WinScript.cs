﻿using UnityEngine;
using System.Collections;

public class WinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WinFunc (){
		Application.LoadLevel("Main Menu");
	}
}
