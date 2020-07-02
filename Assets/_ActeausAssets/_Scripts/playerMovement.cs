
 // MoveToClickPoint.cs
    using UnityEngine;
    using UnityEngine.AI;
	using UnityEngine.Networking;
	using UnityEngine.UI;
	using System.Collections;
    
    public class playerMovement : NetworkBehaviour {
        private NavMeshAgent agent;
		private Animator anim;
		private bool isRunning;
		public float runningSpeed;
		public float walkingSpeed;
		public float rotationSpeed;

		private bool isFollowing;
		private Transform target;

		private GameObject[] camArr;
		private GameObject cam;

		public float xOffset;
		public float yOffset;
		public float zOffset;

		const float ATTACK_DISTANCE = 1.5f;

		//--UI elements--------------------
		private GameObject inventory;
		private Canvas inventoryCanvas;
		private bool inventoryOpen;

		private GameObject textInput;
		private Canvas textInputCanvas;
		private bool textInputOpen;
		private bool textDisplayTimerActive;
		public float textDisplayTimer;
		private float textTimeLeft;
		private GameObject chatBubble;

		private GameObject tooltip;
		private Canvas tooltipCanvas;

		private GameObject statsScreen;
		private Canvas statsScreenCanvas;
		private bool statsScreenOpen;

		// private GameObject mainMenu;
		// private Canvas mainMenuCanvas;


		private GameObject merchantScreen;
		private Canvas merchantScreenCanvas;
		private bool merchantScreenOpen;
		private Button merchantScreenCloseButton;


		//----------------------------------
		private bool combatAnimationRunning;
		private bool endHit;

		private int fullPlayerHealth;
		private int playerHealth;
		private bool isDead;
		private bool isIntendingToBuyFromMerchant;

		private Vector3 dir;

		//==============SPELLS=RELATED=THINGS===================================================
		public GameObject cloudSpell;
		public GameObject playerBody;
		private bool cloudSpellActive;
		public float cloudSpeed;
		private float previousSpeed;

		private bool spellBeingCast;
	
		public GameObject fireballSpell;
		public bool fireballBeingCast;

		private Vector3 projectileSpellTarget;
		public float spellForce;

		private bool transportBeingCast;
		public GameObject transportEffect1;
		public GameObject transportEffect2;

		public float spinSpeed;
		private bool spinAttackOccuring;
		private float spinAttackStartAngle;
		public float spinTimer;
		private float spinCountDown;
		private SphereCollider spinAttackZone;
		public GameObject spinAttackEffect1;
		public GameObject spinAttackEffect2;

		//------Setting spell cursor
		public Texture2D defaultCursor;
		public Texture2D spellCursor;
		private CursorMode cursorMode = CursorMode.Auto;
		private Vector2 hotSpot = Vector2.zero;

		//======================================================================================
        void Start() {
			// spells
			cloudSpellActive = false;
			spellBeingCast = false;

			//---------------

			// Merchant Screen Variables
			merchantScreen = GameObject.Find("MerchantScreen");
			merchantScreenCanvas = merchantScreen.GetComponent<Canvas>();
			merchantScreenCanvas.enabled = false;
			merchantScreenOpen = false;
			merchantScreenCloseButton = GameObject.Find("MerchantClose").GetComponent<Button>();
			merchantScreenCloseButton.onClick.AddListener(CloseMerchantScreen);

			// Main Menu Variables
			// mainMenu = GameObject.Find("MainMenu");
			// mainMenuCanvas = mainMenu.GetComponent<Canvas>();
			// mainMenuCanvas.enabled = false;

			// Inventory Variables
			inventory = GameObject.Find("Inventory");
			inventoryCanvas = inventory.GetComponent<Canvas>();
			inventoryCanvas.enabled = false;
			inventoryOpen = false;

			// TextInput Variables
			textInput = GameObject.Find("TextInput");
			textInputCanvas = textInput.GetComponent<Canvas>();
			textInputCanvas.enabled = false;
			textInputOpen = false;
			textDisplayTimerActive = false;
			textTimeLeft = textDisplayTimer;
			chatBubble = GameObject.Find("PlayerChatBubble");

			// Tooltip Variables
			tooltip = GameObject.Find("ToolTip");
			tooltipCanvas = tooltip.GetComponent<Canvas>();
			tooltipCanvas.enabled = false;

			// Stats Screen Variables
			statsScreen = GameObject.Find("StatsScreen");
			statsScreenCanvas = statsScreen.GetComponent<Canvas>();
			statsScreenCanvas.enabled = false;
			statsScreenOpen = false;

			// these health values need to be changed!
			fullPlayerHealth = statsScreen.GetComponent<playerStats>().getMaxHealth();
			playerHealth = statsScreen.GetComponent<playerStats>().getCurrentHealth();
			
			camArr = GameObject.FindGameObjectsWithTag("MainCamera");
			cam = camArr[0];

            agent = GetComponent<NavMeshAgent>();
			//agent.updateRotation = false;
			anim =  GetComponent<Animator>();

			combatAnimationRunning = false;
			endHit = false;
			
			isIntendingToBuyFromMerchant = false;
			isFollowing = false;
			isDead = false;

			fireballBeingCast = false;
			transportBeingCast = false;

			spinAttackOccuring = false;
			spinCountDown = spinTimer;
			spinAttackZone = this.GetComponent<SphereCollider>();
			spinAttackZone.enabled = false;
        }
        
        void Update() {

			if(!hasAuthority) {
				return;
			}

			



			fullPlayerHealth = statsScreen.GetComponent<playerStats>().getMaxHealth();
			playerHealth = statsScreen.GetComponent<playerStats>().getCurrentHealth();

			Vector3 pos = this.transform.position;
			
			pos.x += xOffset;
			pos.y += yOffset;
			pos.z += zOffset;
			
			cam.transform.position = pos;

			if(playerHealth <= 0) {
				isDead = true;
				CmdSetBooleanAnimation("isDead", true);
				CmdSetBooleanAnimation("isAttacking",false);
				CmdSetBooleanAnimation("isWalking", false);
				CmdSetBooleanAnimation("isRunning", false);

			}



			// If the player is still alive...
			if(!isDead) {

				// Should only be able to be casted after the conclusion of the cast animation
				//Cloud spell
				if(Input.GetKeyUp(KeyCode.LeftBracket) && !inventoryOpen && !statsScreenOpen && !merchantScreenOpen && !spellBeingCast && !textInputOpen){
					cloudSpellActive = !cloudSpellActive;
					cloudSpell.SetActive(cloudSpellActive);
					playerBody.SetActive(!cloudSpellActive);
					if(cloudSpellActive){
						CmdSetPlayerMovementSpeed(cloudSpeed);
						//CmdSetBooleanAnimation("isCasting",true);
					} else {
						CmdSetPlayerMovementSpeed(previousSpeed);
					}
					

				}

				if(spinAttackOccuring){
					this.transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed, Space.World);
					spinCountDown -= Time.deltaTime;
					//Debug.Log(this.transform.localEulerAngles.y);
					//Quaternion rotation = Quaternion.LookRotation(this.transform.forward);

					isFollowing = false;

					if(spinCountDown <= 0){
						spinAttackZone.enabled = false;
						spinAttackOccuring = false;
						CmdSetBooleanAnimation("isCasting",false);
						spellBeingCast = false;
					}

		
				}

				// Activating a spin attack
				if(Input.GetKeyUp(KeyCode.K) && !inventoryOpen && !statsScreenOpen && !merchantScreenOpen && !cloudSpellActive && !spinAttackOccuring && !textInputOpen){
					spinCountDown = spinTimer;
					spinAttackOccuring = true;
					spinAttackStartAngle = this.transform.localEulerAngles.y;
					CmdSetBooleanAnimation("isCasting",true);
					spinAttackZone.enabled = true;
					

					// Stop the player from moving after casting spin
					target = this.transform;
					agent.destination = target.position;
					isFollowing = false;
					spellBeingCast = true;

					GameObject sa1 = Instantiate(spinAttackEffect1,this.transform);
					GameObject sa2 = Instantiate(spinAttackEffect2,this.transform);
				}

				// priming fireball spell
				if(Input.GetKeyUp(KeyCode.RightBracket) && !inventoryOpen && !statsScreenOpen && !merchantScreenOpen && !Input.GetMouseButton(0) && !cloudSpellActive && !textInputOpen) {
					spellBeingCast = true;
					fireballBeingCast = true;
					Cursor.SetCursor(spellCursor, hotSpot, cursorMode);


				}

				// priming transport spell
				if(Input.GetKeyUp(KeyCode.L) && !inventoryOpen && !statsScreenOpen && !merchantScreenOpen && !Input.GetMouseButton(0) && !cloudSpellActive && !textInputOpen) {
					spellBeingCast = true;
					transportBeingCast = true;
					Cursor.SetCursor(spellCursor, hotSpot, cursorMode);

				}

				if(spellBeingCast) {
					isFollowing = false;
					CmdSetBooleanAnimation("isAttacking",false);
				}




/*
			// TextInput Variables
			textInput = GameObject.Find("TextInput");
			textInputCanvas = textInput.GetComponent<Canvas>();
			textInputCanvas.enabled = false;
			textInputOpen = false; */

				if(!spellBeingCast){

					// If the text input box is open, force focus
					if(textInputOpen){
						textInput.transform.GetChild(0).GetComponent<InputField>().Select();
					}

					if(textDisplayTimerActive){
						textTimeLeft -= Time.deltaTime;
						if(textTimeLeft <= 0){
							chatBubble.transform.GetChild(0).GetComponent<Text>().text = "";
							textDisplayTimerActive = false;
							textTimeLeft = textDisplayTimer;
						}
					}


					// Toggling stats screen
					if(Input.GetKeyUp(KeyCode.S) && !merchantScreenOpen && !textInputOpen){
						statsScreenOpen  = !statsScreenOpen;
						statsScreenCanvas.enabled = statsScreenOpen;
					}

					// Toggling text input
					if(Input.GetKeyUp(KeyCode.Return) && !merchantScreenOpen && !statsScreenOpen && !inventoryOpen){

						// If open, then close and return text
						if(textInputOpen){
							
							textInput.transform.GetChild(0).GetComponent<InputField>().Select();

							// if there is text worth outputting, output it!
							if(textInput.transform.GetChild(0).GetComponent<InputField>().text != ""){
								textTimeLeft = textDisplayTimer;
								textDisplayTimerActive = true;
								chatBubble.transform.GetChild(0).GetComponent<Text>().text = textInput.transform.GetChild(0).GetComponent<InputField>().text;
							}
							// clear text
							textInput.transform.GetChild(0).GetComponent<InputField>().text = "";
						}

						textInputOpen  = !textInputOpen;
						textInputCanvas.enabled = textInputOpen;
					}


					// Toggling inventory
					if(Input.GetKeyUp(KeyCode.I) && !merchantScreenOpen && !textInputOpen){
						inventoryOpen = !inventoryOpen;
						inventoryCanvas.enabled = inventoryOpen;
						inventory.GetComponent<Canvas>().enabled = inventoryOpen;
						if(tooltipCanvas.isActiveAndEnabled) {
							tooltipCanvas.enabled = false;
						}
					}
					// Toggle run-mode
					if (Input.GetKeyUp(KeyCode.Q)) {
						isRunning = !isRunning;
					}



					if(isFollowing) {
						agent.destination = target.position;
						//CmdPlayerRotation (target.position);
						if(Vector3.Distance(target.position,this.transform.position) <= ATTACK_DISTANCE && !cloudSpellActive) {
							CmdPlayerRotation(target.position);
							agent.stoppingDistance = ATTACK_DISTANCE;
							CmdSetBooleanAnimation("isAttacking",true);
							if (anim.GetCurrentAnimatorStateInfo(0).IsName("WK_heavy_infantry_07_attack_A")){
								if(!combatAnimationRunning){
									combatAnimationRunning = true;
									endHit = true;
								}
							} else {
								if(endHit) {
									GameObject baddyReference = target.parent.gameObject;
									int baddyHealth = baddyReference.GetComponent<EnemyController>().getBaddyHealth();
									if(baddyHealth > 0) {
										// this is pretty irrelevant if attacks still continue....
										// ^^^but it doesnt still continue! so ha!
										baddyReference.GetComponent<EnemyController>().setBaddyHealth(baddyHealth - 1);
									} else {
										// If this line was hit. Then the attack should be concluded (I hope)
										CmdSetBooleanAnimation("isAttacking",false);
										isFollowing = false;
										target = this.transform;
										
										// After baddy is killed, allocate experience
										int expGained = baddyReference.GetComponent<EnemyController>().getExperienceWorth();
										int currExp = statsScreen.GetComponent<playerStats>().getCurrentExperience();
										statsScreen.GetComponent<playerStats>().setCurrentExperience(currExp + expGained);
										
									}
									
									endHit = false;
								}
								combatAnimationRunning = false;
								
							}
						} else {

							CmdSetBooleanAnimation("isAttacking",false);
						}
					// end if(isFollowing)
					} else if(isIntendingToBuyFromMerchant){
						agent.destination = target.position;
						if(Vector3.Distance(target.position,this.transform.position) <= ATTACK_DISTANCE) {
							merchantScreenOpen = true;
							merchantScreenCanvas.enabled = true;
							isIntendingToBuyFromMerchant = false;
							agent.destination = this.transform.position;

							// Opening the inventory while merchant screen is open
							inventoryOpen = true;
							inventoryCanvas.enabled = true;

							// Closing all other screens while merchant screen is open
							statsScreenOpen = false;
							statsScreenCanvas.enabled = false;

							textInputOpen = false;
							textInputCanvas.enabled = false;
						}

					} else {
						agent.stoppingDistance = 0;
					}
				} //  END if(!spellBeingCast)

				// Setting run and walk animations
				if (agent.velocity != Vector3.zero && !isRunning && !cloudSpellActive) {
					CmdSetBooleanAnimation("isWalking",true);
					CmdSetBooleanAnimation("isRunning",false);
					previousSpeed = walkingSpeed;
					CmdSetPlayerMovementSpeed(walkingSpeed);
				} else if(agent.velocity != Vector3.zero && isRunning && !cloudSpellActive) {
					CmdSetBooleanAnimation("isRunning",true);
					CmdSetBooleanAnimation("isWalking",false);
					previousSpeed = runningSpeed;
					CmdSetPlayerMovementSpeed(runningSpeed);
				} else {
					CmdSetBooleanAnimation("isWalking",false);
					CmdSetBooleanAnimation("isRunning",false);
				}

				if ((Input.GetMouseButton(0) && !spellBeingCast) || Input.GetMouseButtonDown(0)) {
					RaycastHit hit;
					
					if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && !inventoryOpen && !statsScreenOpen && !merchantScreenOpen && !textInputOpen) {
						//temporary while working on fireball spell
						// ^^^ wut?...


						if(!spellBeingCast){
							if(hit.transform.tag == "attackable" && hit.transform.parent.gameObject.GetComponent<EnemyController>().getBaddyHealth() > 0) {
								target = hit.transform;
								isFollowing = true;
								
							} else if(hit.transform.tag == "merchant"){
								target = hit.transform;
								isIntendingToBuyFromMerchant = true;
							} else {
								if(isFollowing) {
									isFollowing = false;
									CmdSetBooleanAnimation("isAttacking",false);
								}
								if(isIntendingToBuyFromMerchant){
									isIntendingToBuyFromMerchant = false;
								}
								this.agent.destination = hit.point;
								CmdSetPlayerPosition(hit.point);
								Vector3 correctedLocation = new Vector3(hit.point.x,this.transform.position.y,hit.point.z);
								CmdPlayerRotation(correctedLocation);


							} // END else
						} else { // Spell Being Cast

							// Behavior shared across all spells!
							CmdSetBooleanAnimation("isCasting",true);
							target = this.transform;
							agent.destination = target.position;
							projectileSpellTarget = hit.point;
							CmdPlayerRotation (projectileSpellTarget);

							// Casting fireball
							if(fireballBeingCast){
								GameObject fb = Instantiate(fireballSpell,this.transform);
								fb.transform.SetParent(this.transform.parent);
								fb.GetComponent<Rigidbody>().AddForce((projectileSpellTarget - fb.transform.position).normalized * spellForce);
							}

							// Casting transport
							if(transportBeingCast){
								//this.transform.position = projectileSpellTarget;
								Instantiate(transportEffect1,this.transform);
								Instantiate(transportEffect2,this.transform);
								agent.Warp(projectileSpellTarget);
								target = this.transform;
								agent.destination = target.position;
							}

							// Setting the cursor back to normal
							Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
						}


					}
				}

				if ((Input.GetMouseButtonUp(0) && spellBeingCast)){
					fireballBeingCast = false;
					transportBeingCast = false;
					
					spellBeingCast = false;
					CmdSetBooleanAnimation("isCasting",false);
				}

			}
        } // END update


		public void CloseMerchantScreen(){
			merchantScreenOpen = false;
			merchantScreenCanvas.enabled = false;

			inventoryOpen = false;
			inventoryCanvas.enabled = false;

		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.transform.tag == "enemy"){
				GameObject baddyReference = other.transform.parent.gameObject;
				int baddyHealth = baddyReference.GetComponent<EnemyController>().getBaddyHealth();
				if(baddyHealth > 0){
					baddyReference.GetComponent<EnemyController>().setBaddyHealth(baddyHealth - 1);
				
				} else if(!baddyReference.GetComponent<EnemyController>().getIsDead()) {
					// flag the enemy as dead and get experience for the kill
					baddyReference.GetComponent<EnemyController>().setIsDead(true);
					Debug.Log("dead");
					// After baddy is killed, allocate experience
					int expGained = baddyReference.GetComponent<EnemyController>().getExperienceWorth();
					int currExp = statsScreen.GetComponent<playerStats>().getCurrentExperience();
					statsScreen.GetComponent<playerStats>().setCurrentExperience(currExp + expGained);
					baddyReference.GetComponent<EnemyController>().setIsDead(true);

				}
			
			}
		}


		//---------------------------------------------------

		public int getPlayerHealth() {
			return playerHealth;
		}

		public void setPlayerHealth(int health) {
			playerHealth = health;
		}

		public bool getPlayerAliveStatus() {
			return isDead;
		}

		//---------------------------------------------------
		[Command]
		void CmdSetBooleanAnimation(string a, bool v) {
			this.anim.SetBool(a,v);
			this.RpcSetBooleanAnimation(a,v);
		}
		[ClientRpc]
		void RpcSetBooleanAnimation(string a, bool v) {
			this.anim.SetBool(a,v);
		}
		//---------------------------------------------------
		[Command]
		void CmdSetPlayerPosition(Vector3 d) {
			this.agent.destination = d;
			this.RpcSetPlayerPosition(d);
		}
		[ClientRpc]
		void RpcSetPlayerPosition(Vector3 d) {
			this.agent.destination = d;
		}
		//---------------------------------------------------
		[Command]
		void CmdSetPlayerMovementSpeed(float s) {
			this.agent.speed = s;
			this.RpcSetPlayerMovementSpeed(s);
		}
		[ClientRpc]
		void RpcSetPlayerMovementSpeed(float s) {
			this.agent.speed = s;
		}

		//---------------------------------------------------
		[Command]
		void CmdPlayerRotation (Vector3 loc) {
			agent.updateRotation = false; 
			this.transform.LookAt(loc);
			agent.updateRotation = true;
			this.RpcPlayerRotation(loc);		
		}
		[ClientRpc]
		void RpcPlayerRotation(Vector3 loc) {
			agent.updateRotation = false; 
			this.transform.LookAt(loc);
			agent.updateRotation = true;
		}
		//---------------------------------------------------




    }