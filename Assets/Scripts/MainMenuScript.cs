using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
	public GameObject instPanel, creditsPanel;
	public bool showInst;
	// Use this for initialization
	void Start () {
		instPanel = GameObject.Find("InstPanel");
		if (instPanel){
			instPanel.SetActive(false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (instPanel){
			if (showInst == true){
				instPanel.SetActive(true);
			} else {
				instPanel.SetActive(false);
			}
		}
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

	public void InstButton(){
		showInst = true;
	}

	public void CloseInst(){
		showInst = false;
	}

	public void ReturnButton (){
		Application.LoadLevel("Main");
	}
	
}
