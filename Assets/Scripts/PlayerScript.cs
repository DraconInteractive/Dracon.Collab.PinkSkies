using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Rigidbody playerRigid;
	public Transform playerTrans;
	public Toggle invCamYToggle;
	public GameObject hitTrigger;
	public GameObject healthText, healthSlider, optionsPanel;
	public GameObject[] scrapTextArray;
	public GameObject consolePanel, consoleInput, consoleText, consolePlaceHolder;
	public GameObject workBenchPanel;
	public float  speedForward, speedStrafe, speedMod, rotateAngle, jumpForce, attackWait;
	public bool isFullSpeed, isGrounded, menuOpen, isInvertingCamY, isAttacking, showConsole, inCombat;
	public bool workbenchInteractable, showingWorkbench;
	public int scrapCount;

	public int finalHealth, initialHealth, armour, damage;
	// Use this for initialization
	void Start () {
		playerRigid = GetComponent<Rigidbody>();
		playerTrans = GetComponent<Transform>();
		healthText = GameObject.Find("HealthText");
		healthSlider = GameObject.Find ("HealthSlider");
		optionsPanel = GameObject.Find ("OptionsPanel");
		consolePanel = GameObject.Find ("ConsolePanel");
		consoleInput = GameObject.Find ("ConsoleInput");
		scrapTextArray = GameObject.FindGameObjectsWithTag("ScrapText");
		consoleText = GameObject.Find ("ConsoleText");
		consolePlaceHolder = GameObject.Find ("ConsolePlaceHolder");
		workBenchPanel = GameObject.Find ("WorkbenchPanel");

		initialHealth = 100;
		armour = 0;
		finalHealth = initialHealth + armour;
		healthSlider.GetComponent<Slider>().maxValue = finalHealth;
		healthSlider.GetComponent<Slider>().minValue = 0;

		optionsPanel.SetActive(false);
		consolePanel.SetActive(false);
		workBenchPanel.SetActive(false);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!showConsole){
			PlayerMovement();
		}
	}

	void Update (){
		HealthUpdate();
		SetGUI();
		OpenOptions();
		DetectGround();
		if (Input.GetMouseButtonDown(0)){
			PlayerAttack();
		}
		ConsoleCommand();
		Interact();

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Scrap"){
			Destroy(col.gameObject);
			scrapCount++;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Workbench"){
			workbenchInteractable = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Workbench"){
			workbenchInteractable = false;
		}
	}

	public void HealthUpdate(){
		finalHealth = initialHealth + armour ;
		healthSlider.GetComponent<Slider>().maxValue = finalHealth;
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
			foreach (GameObject i in scrapTextArray){
				i.SetActive(false);
			}
		} else {
			healthSlider.SetActive(true);
			foreach (GameObject i in scrapTextArray){
				i.SetActive(true);
			}
		}
		healthSlider.GetComponent<Slider>().value = finalHealth;
		foreach (GameObject i in scrapTextArray){
			i.GetComponent<Text>().text = "Scrap: " + scrapCount.ToString();
		}
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
			initialHealth -= 20;

		}
		consoleText.GetComponent<Text>().text = "";
		consolePlaceHolder.GetComponent<Text>().text = "";
		consoleInput.GetComponent<InputField>().text = "";
		showConsole = false;

	}

	public void Interact(){

		if (Input.GetButtonDown("Interact")){
			if (workbenchInteractable){
				if (!showingWorkbench){
					workBenchPanel.SetActive(true);
					showingWorkbench = true;
				} else {
					workBenchPanel.SetActive(false);
					showingWorkbench = false;
				}
					
			}
		}
	}

	public void UpgradeArmour(){
		armour = armour + 10;
	}

	public void UpgradeWeapon(){
		damage = damage + 2;
	}

	public IEnumerator AttackTimer(){
		yield return new WaitForSeconds(attackWait);
		isAttacking = false;
	}
}
