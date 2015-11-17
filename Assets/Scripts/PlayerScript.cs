using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public Rigidbody playerRigid;
	public Transform playerTrans;
	public Toggle invCamYToggle;
	public GameObject hitTrigger;
	public GameObject healthText, healthSlider, optionsPanel;
	public GameObject consolePanel, consoleInput;
	public float  speedForward, speedStrafe, speedMod, rotateAngle, jumpForce, attackWait;
	public bool isFullSpeed, isGrounded, menuOpen, isInvertingCamY, isAttacking, showConsole;

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

		health = 100;
		healthSlider.GetComponent<Slider>().maxValue = health;
		healthSlider.GetComponent<Slider>().minValue = 0;

		optionsPanel.SetActive(false);
		consolePanel.SetActive(false);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		PlayerMovement();
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
			//Apply the direction to the character, with modifiers of speed and incrementation
			//playerRigid.MovePosition (transform.position + transform.forward * speedForward * Time.deltaTime);
			playerRigid.AddForce (Vector3.Cross (Camera.main.transform.right, Vector3.up) * speedForward * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation(Vector3.Cross (Camera.main.transform.right, Vector3.up), Vector3.up);
			//playerRigid.MovePosition (transform.position + transform.right * speedStrafe * Time.deltaTime);
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
		healthSlider.GetComponent<Slider>().value = health;
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

	public IEnumerator AttackTimer(){
		yield return new WaitForSeconds(attackWait);
		isAttacking = false;
	}
}
