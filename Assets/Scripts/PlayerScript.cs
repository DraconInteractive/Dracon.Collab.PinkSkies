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
	public float  speed, speedMod, rotateAngle, jumpForce, attackWait;
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
			speed = Input.GetAxis("Vertical") * speedMod;
			//Apply the direction to the character, with modifiers of speed and incrementation
			playerRigid.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);
			playerTrans.Rotate(Vector3.up, rotateAngle * Input.GetAxis("Horizontal") * Time.deltaTime);
			
			if (speed >= 6){
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
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 2)){
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
	}

	public IEnumerator AttackTimer(){
		yield return new WaitForSeconds(attackWait);
		isAttacking = false;
	}
}
