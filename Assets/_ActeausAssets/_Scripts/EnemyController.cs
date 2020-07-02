
 // MoveToClickPoint.cs
	using System.Collections;
	using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;
    
    public class EnemyController : MonoBehaviour {

	
		private GameObject statsScreen;


        private NavMeshAgent agent;
		private Animator anim;
		private Transform target;

		[SerializeField]
		public int baddyHealth;
		private int baddyMaxHealth;
		
		public int experienceWorth;
		

		private bool combatAnimationRunning;
		private bool endHit;
		public float attackDistance;

		public int damage;

		// this is set by the player
		// This flag will determine whether an enemy has recently died
		private bool isDead;
        
        void Start() {
			// I need to make sure later, that this works on both clientside/serverside players
			statsScreen = GameObject.Find("StatsScreen");
			baddyMaxHealth = baddyHealth;
            agent = GetComponent<NavMeshAgent>();
			anim =  GetComponent<Animator>();
			target = null;
			combatAnimationRunning = false;
			endHit = false;
			isDead = false;
        }
        
        void Update() {

			
			if (agent.velocity != Vector3.zero) {
				anim.SetBool("isChasing",true);
			} else {
				anim.SetBool("isChasing",false);
			}

			if(baddyHealth <= 0) {
				target = null;
				this.transform.GetComponent<CapsuleCollider>().enabled = false;
				this.transform.GetComponent<NavMeshAgent>().enabled = false;
				anim.SetBool("isDead", true);
				anim.SetBool("isChasing",false);
				anim.SetBool("isAttacking",false);
			}

			if(target) {
				agent.destination = target.position;

				if(Vector3.Distance(target.position,this.transform.position) <= attackDistance) {
					anim.SetBool("isAttacking",true);
					anim.SetBool("isChasing",false);

					int testHealth = statsScreen.GetComponent<playerStats>().getCurrentHealth();
					if(testHealth <= 0){
						target = null;
						anim.SetBool("isAttacking",false);
						anim.SetBool("isChasing",false);
					}
					/* I am saving this for later, it was a cool way to stop the enemy from hitting me when my health was at 1 ;)
					if(testHealth-1 <= 0){
						target = null;
						anim.SetBool("isAttacking",false);
						anim.SetBool("isChasing",false);
					}*/

					// This name will need to be changed and made more generic overtime
					if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack1")){
						this.transform.LookAt(target);
						if(!combatAnimationRunning){
							combatAnimationRunning = true;
							//int testHealth = statsScreen.GetComponent<playerStats>().getCurrentHealth();

							endHit = true;


						}
					} else {

							if(endHit) {


								statsScreen.GetComponent<playerStats>().setCurrentHealth(testHealth-damage);
								//GameObject playerReference = target.gameObject;
								//int playerHealth = playerReference.GetComponent<playerMovement>().getPlayerHealth();
								//playerReference.GetComponent<playerMovement>().setPlayerHealth(playerHealth - 1);
								endHit = false;
							}
							
						combatAnimationRunning = false;
					}




				} else {
					anim.SetBool("isAttacking",false);
					anim.SetBool("isChasing",true);
				}

			}
			

			

        }

		public int getExperienceWorth() {
			return experienceWorth;
		}

		public int getBaddyHealth() {
			return baddyHealth;
		}

		public int getBaddyMaxHealth() {
			return baddyMaxHealth;
		}

		public void setBaddyHealth(int health) {
			baddyHealth = health;
		}

		public void setIsDead(bool d){
			isDead = d;
		}

		public bool getIsDead(){
			return isDead;
		}

		private void OnTriggerEnter(Collider other)
		{
			
			if(other.transform.tag == "Player"){
				target = other.gameObject.transform;
			}
			
			//
		}

		private void OnTriggerExit(Collider other) {
			if(other.transform.tag == "Player" && other.transform == target){

				target = null;
			}
		}



    }