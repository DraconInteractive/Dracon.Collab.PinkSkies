using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	public GameObject instPanel, creditsPanel;
	public bool showInst;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGameButton (){
		Application.LoadLevel("CITY_SCENE");
	}

	public void CreditsButton(){
		Application.LoadLevel("Credits");
	}

	public void QuitButton (){
		Application.Quit();
	}
	
}
