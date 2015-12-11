using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScript : MonoBehaviour {
	public bool isRed, isWhite, colorChanging;
	Color myColor;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		myColor = GetComponent<Text>().color;

		if (!colorChanging){
			StartCoroutine("ColorTimer");
		}
		if (colorChanging){
			if (isWhite){
				myColor = Vector4.Lerp(myColor, Color.red, 0.005f);
				GetComponent<Text>().color = myColor;
			} else {
				myColor = Vector4.Lerp (myColor, Color.black, 0.02f);
				GetComponent<Text>().color = myColor;
			}
		}
	}

	public IEnumerator ColorTimer (){
		colorChanging = true;
		if (isWhite){
			yield return new WaitForSeconds(10);
		} else {
			yield return new WaitForSeconds(5);
		}

		isWhite = !isWhite;
		colorChanging = false;
	}
}
