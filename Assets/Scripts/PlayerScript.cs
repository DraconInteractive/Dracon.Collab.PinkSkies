using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Rigidbody playerRigid;
	public Transform playerTrans;
	public Toggle invCamYToggle;
	public GameObject hitTrigger, FOGreatsword;
	public GameObject healthText, healthSlider, optionsPanel;
	public GameObject[] scrapTextArray;
	public GameObject[] barricadeArray;
	public GameObject consolePanel, consoleInput, consoleText, consolePlaceHolder, interactPanel;
	public GameObject workBenchPanel, menuPanel;
	public float  speedForward, speedStrafe, speedMod, speedGrounded, speedAired, rotateAngle, jumpForce, attackWait, elevatorSpeed, immuneTime;
	public bool isFullSpeed, isGrounded, menuOpen, isInvertingCamY, isAttacking, showConsole, inCombat, optionsOpen;
	public bool workbenchInteractable, showingWorkbench, showInteractPanel;
	public bool inElevator, onPlatform, nearShip, immune;
	public int scrapCount;
	public Animator playerAnimator;
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
		interactPanel = GameObject.Find ("InteractPanel");
		scrapTextArray = GameObject.FindGameObjectsWithTag("ScrapText");
		consoleText = GameObject.Find ("ConsoleText");
		consolePlaceHolder = GameObject.Find ("ConsolePlaceHolder");
		workBenchPanel = GameObject.Find ("WorkbenchPanel");
		menuPanel = GameObject.Find ("MenuPanel");
		barricadeArray = GameObject.FindGameObjectsWithTag("Barricade");
		playerAnimator = GetComponent<Animator>();

		initialHealth = 100;
		armour = 0;
		finalHealth = initialHealth + armour;
		healthSlider.GetComponent<Slider>().maxValue = finalHealth;
		healthSlider.GetComponent<Slider>().minValue = 0;

		optionsPanel.SetActive(false);
		consolePanel.SetActive(false);
		workBenchPanel.SetActive(false);
		interactPanel.SetActive(false);

		menuOpen = false;
		optionsOpen = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!showConsole){
			PlayerMovement();
		}
	}

	void Update (){

		if (Input.GetKeyDown(KeyCode.Space)){
			if (isGrounded){
				playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}
		
		HealthUpdate();
		SetGUI();
		OpenMenu();
		DetectGround();
		if (Input.GetMouseButtonDown(0)){
			PlayerAttack();
		}
		ConsoleCommand();
		Interact();
		//CombatChange();
		CombatActions();
		AnimationUpdate();
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
		} else if (col.gameObject.tag == "ElevatorTrigger"){
			inElevator = true;
		} else if (col.gameObject.tag == "PlatformTrigger"){
			onPlatform = true;
		} else if (col.gameObject.tag == "PodTrigger"){
			nearShip = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Workbench"){
			workbenchInteractable = false;
		} else if (col.gameObject.tag == "ElevatorTrigger"){
			inElevator = false;
		} else if (col.gameObject.tag == "PlatformTrigger"){
			onPlatform = false;
		} else if (col.gameObject.tag == "PodTrigger"){
			nearShip = false;
		}
	}

	public void HealthUpdate(){
		finalHealth = initialHealth + armour ;
		healthSlider.GetComponent<Slider>().value = finalHealth;
	}

	public void PlayerMovement(){

		if (!menuOpen){
			if (isGrounded){
				playerRigid.drag = 15;
				speedMod = speedGrounded;
			} else {
				playerRigid.drag = 0;
				speedMod = speedAired;
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

		}
	}

	public void SetImmune(){
		StartCoroutine("ImmuneTimer");
	}

	public void PlayerAttack(){
		if (!menuOpen){
			if (!showingWorkbench){
				if (!isAttacking){
					isAttacking = true;
					playerAnimator.SetBool("isAttacking", true);
					hitTrigger.GetComponent<HitTrigger>().TriggerAttack(damage);
					StartCoroutine("AttackTimer");
				}
			}
		}
	}

	public void SetGUI(){

		healthSlider.GetComponent<Slider>().value = finalHealth;
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

		if (menuOpen == true){
			menuPanel.SetActive(true);
		} else {
			menuPanel.SetActive(false);
		}

		if (optionsOpen == true){
			optionsPanel.SetActive(true);
		} else {
			optionsPanel.SetActive(false);
		}

		if (showingWorkbench){
			workBenchPanel.SetActive(true);
		} else {
			workBenchPanel.SetActive(false);
		}
	}

	public void OpenMenu(){
		if (Input.GetButtonDown ("Menu")){
			if (showConsole){
				showConsole = false;
				return;
			} 
			if (showingWorkbench){
				showingWorkbench = false;
				return;
			} 
			if (optionsOpen){
				optionsOpen = false;
				return;
			}

			menuOpen = !menuOpen;
		}

	}

	public void OpenOptions(){
		optionsOpen = true;
		menuOpen = false;
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
		if (Physics.Raycast(transform.position + transform.up * 0.5f, Vector3.down, out hit, 0.75f)){
			if (hit.collider.gameObject.tag == "Ground"){
				isGrounded = true;
			} else if (hit.collider.gameObject.tag == "Elevator"){
				isGrounded = true;
			} else if (hit.collider.gameObject.tag == "PlatformBase"){
				isGrounded = true;
			} else {
				Debug.Log (hit.collider.gameObject.name);
			}
		} else {
			isGrounded = false;
		}
		Debug.DrawRay(transform.position, Vector3.down * 0.25f, Color.green);
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
		Text cText = consoleText.GetComponent<Text>();
		if (cText.text == "Help"){
			string helpString = "Help //n PlayerDamage //n AddScrap //n UpgradeArmour //n UpgradeWeapon //n GameSave //n GameLoad";
			Debug.Log (helpString.Replace("//n", "\n"));
		} else if (cText.text == "PlayerDamage"){
			initialHealth -= 20;
		} else if (cText.text == "AddScrap"){
			scrapCount += 10;
		} else if (cText.text == "UpgradeArmour"){
			armour += 10;
		} else if (cText.text == "UpgradeWeapon"){
			damage += 10;
		} else if (cText.text == "GameSave"){
			GameSave();
		} else if (cText.text == "GameLoad"){
			GameLoad();
		} else if (cText.text == "RaiseBarricades"){
			foreach (GameObject i in barricadeArray){
				i.GetComponent<BarricadeScript>().activated = true;
			}
		}
		consoleText.GetComponent<Text>().text = "";
		consolePlaceHolder.GetComponent<Text>().text = "";
		consoleInput.GetComponent<InputField>().text = "";
		showConsole = false;

	}

	public void Interact(){
		if (workbenchInteractable || inElevator || onPlatform || nearShip){
			interactPanel.SetActive(true);
		} else {
			interactPanel.SetActive(false);
		}

		if (Input.GetButtonDown("Interact")){

			if (workbenchInteractable){
				showingWorkbench = !showingWorkbench;
				return;
			} 
			if (inElevator){
				RaycastHit hit;
				if (Physics.Raycast(transform.position, Vector3.down, out hit, 2)){
					if (hit.collider.gameObject.tag == "Elevator"){
						hit.collider.gameObject.GetComponent<ElevatorScript>().activated = true;
					}
				}
			} else if (onPlatform){
				RaycastHit hit;
				if (Physics.Raycast(transform.position, Vector3.down, out hit, 2)){
					if (hit.collider.gameObject.tag == "PlatformBase"){
						hit.collider.gameObject.GetComponent<PlatMoveScript>().activated = true;
					}
				}
			} else if (nearShip){
				GameObject.FindGameObjectWithTag("SpaceShip").GetComponent<SpaceShipScript>().TakeOff();
			} else {
				Debug.Log ("No Interact Zone Entered");
			}
		}
	}

	public void UpgradeArmour(){
		if (scrapCount >= 10){
			armour = armour + 10;
			scrapCount -= 10;
		}
	}

	public void UpgradeWeapon(){
		if (scrapCount >= 10){
			damage = damage + 2;
			scrapCount -= 10;
		}
	}

	public void GameQuit(){
		Application.Quit();
	}

	public void GameSave(){
		PlayerPrefs.SetInt("ScrapCount", scrapCount);
		PlayerPrefs.SetInt("Health", initialHealth);
		PlayerPrefs.SetInt("Armour", armour);
		PlayerPrefs.SetInt("Damage", damage);
		PlayerPrefs.SetFloat("PlayerX", transform.position.x);
		PlayerPrefs.SetFloat("PlayerY", transform.position.y);
		PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
		if (isInvertingCamY){
			PlayerPrefs.SetInt("InverseCam", 0);
		} else {
			PlayerPrefs.SetInt("InverseCam", 1);
		}


	}

	public void GameLoad(){
		scrapCount = PlayerPrefs.GetInt("ScrapCount");
		initialHealth = PlayerPrefs.GetInt("Health");
		armour = PlayerPrefs.GetInt("Armour");
		damage = PlayerPrefs.GetInt("Damage");
		Vector3 pos = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
		transform.position = pos;
		if (PlayerPrefs.GetInt("InverseCam") == 0){
			isInvertingCamY = true;
		} else if (PlayerPrefs.GetInt("InverseCam") == 1){
			isInvertingCamY = false;
		}
	}

	public void CombatChange(){
		inCombat = !inCombat;
	}

	public void CombatActions(){
		
	}

	public void AnimationUpdate(){
		if (speedForward > 0.05f || speedStrafe > 0.05f){
			playerAnimator.SetBool("isRunning", true);
		} else {
			playerAnimator.SetBool("isRunning", false);
		}

	}

	public IEnumerator ImmuneTimer(){
		immune = true;
		yield return new WaitForSeconds(immuneTime);
		immune = false;
	}

	public IEnumerator AttackTimer(){
		yield return new WaitForSeconds(attackWait);
		playerAnimator.SetBool("isAttacking", false);
		isAttacking = false;
	}
}
