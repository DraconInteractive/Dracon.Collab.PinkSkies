using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Rigidbody playerRigid;
	public Transform playerTrans;
	public Toggle invCamYToggle;
	public GameObject hitTrigger;
	public GameObject healthText, healthSlider, optionsPanel, scrapText;
	public GameObject consolePanel, consoleInput, consoleText, consolePlaceHolder;
	public float  speedForward, speedStrafe, speedMod, rotateAngle, jumpForce, attackWait;
	public bool isFullSpeed, isGrounded, menuOpen, isInvertingCamY, isAttacking, showConsole, inCombat;
	public int scrapCount;

	public int health, armour, damage;
	// Use this for initialization
	void Start () {
		playerRigid = GetComponent<Rigidbody>();
		playerTrans = GetComponent<Transform>();
		healthText = GameObject.Find("HealthText");
		healthSlider = GameObject.Find ("HealthSlider");
		optionsPanel = GameObject.Find ("OptionsPanel");
		consolePanel = GameObject.Find ("ConsolePanel");
		consoleInput = GameObject.Find ("ConsoleInput");
		scrapText = GameObject.Find ("ScrapText");
		consoleText = GameObject.Find ("ConsoleText");
		consolePlaceHolder = GameObject.Find ("ConsolePlaceHolder");

		health = 100;
		healthSlider.GetComponent<Slider>().maxValue = health;
		healthSlider.GetComponent<Slider>().minValue = 0;

		optionsPanel.SetActive(false);
		consolePanel.SetActive(false);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!showConsole){
			PlayerMovement();
		}
	}

	void Update (){
		SetGUI();
		OpenOptions();
		DetectGround();
		if (Input.GetMouseButtonDown(0)){
			PlayerAttack();
		}
		ConsoleCommand();

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Scrap"){
			Destroy(col.gameObject);
			scrapCount++;
		}
	}

	public void PlayerMovement(){

		if (!menuOpen){
			if (isGrounded){
				playerRigid.drag = 10;
				speedMod = 4000;
			} else {
				playerRigid.drag = 0;
				speedMod = 20;
			}

			speedForward = Input.GetAxis("Vertical") * speedMod;
			speedStrafe = Input.GetAxis("Horizontal") * speedMod;
			playerRigid.AddForce (Vector3.Cross (Camera.main.transform.right, Vector3.up) * speedForward * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation(Vector3.Cross (Camera.main.transform.right, Vector3.up), Vector3.up);
			playerRigid.AddForce (Camera.main.transform.right * speedStrafe * Time.deltaTime);
			
			if (speedForward >= 6){
				isFullSpeed = true;
			} else {
				isFullSpeed = false;
			}
			if (Input.GetKeyDown(KeyCode.Space)){
				if (isGrounded){
					playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				}
			}
		}
	}

	public void PlayerAttack(){
		if (!menuOpen){
			if (!isAttacking){
				isAttacking = true;
				hitTrigger.GetComponent<HitTrigger>().TriggerAttack(damage);
				StartCoroutine("AttackTimer");
			}

		}
	}

	public void SetGUI(){
		if (menuOpen){
			healthSlider.SetActive(false);
			scrapText.SetActive(false);
		} else {
			healthSlider.SetActive(true);
			scrapText.SetActive(true);
		}
		healthSlider.GetComponent<Slider>().value = health;
		scrapText.GetComponent<Text>().text = scrapCount.ToString();
	}

	public void OpenOptions(){
		if (Input.GetButtonDown ("Menu")){
			menuOpen = !menuOpen;
		}

		if (menuOpen == true){
			optionsPanel.SetActive(true);
		} else {
			optionsPanel.SetActive(false);
		}
	}

	public void OnInvYToggle(bool i){
		if (invCamYToggle.isOn == true){
			isInvertingCamY = true;
		} else {
			isInvertingCamY = false;
		}
	}

	public void DetectGround(){
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f)){
			if (hit.collider.gameObject.tag == "Ground"){
				isGrounded = true;
			}
		} else {
			isGrounded = false;
		}
	}

	public void ConsoleCommand(){
		if (Input.GetButtonDown("Console")){
			showConsole = !showConsole;
		}

		if (showConsole){
			consolePanel.SetActive(true);
		} else {
			consolePanel.SetActive(false);
		}
	}

	public void ConsoleAction(){
		if (consoleText.GetComponent<Text>().text == "PlayerDamage"){
			health -= 20;

		}
		consoleText.GetComponent<Text>().text = "";
		consolePlaceHolder.GetComponent<Text>().text = "";
		consoleInput.GetComponent<InputField>().text = "";
		showConsole = false;

	}

	public IEnumerator AttackTimer(){
		yield return new WaitForSeconds(attackWait);
		isAttacking = false;
	}
}
