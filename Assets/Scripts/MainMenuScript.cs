using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	public GameObject instPanel, creditsPanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGameButton (){
		Application.LoadLevel("CITY_SCENE");
	}

	public void QuitButton (){
		Application.Quit();
	}

	public void CreditsButton (){
		creditsPanel.SetActive(true);
	}

	public void CloseCreditsButton () {
		creditsPanel.SetActive(false);
	}

	public void InstructionsButton (){
		instPanel.SetActive(true);
	}

	public void CloseInstButton () {
		instPanel.SetActive(false);
	}
}
