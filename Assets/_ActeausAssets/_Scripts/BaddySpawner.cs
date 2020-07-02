using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaddySpawner : MonoBehaviour {

	public GameObject baddy;
	private GameObject baddyReference;
	private bool isDead;
	private bool respawnFrozen;
	private bool corpseDisappeared;

	public float timeTillCorpseDisappears;
	private float ttCD;

	public float timeTillRespawn;
	private float ttR;
	
	// Use this for initialization
	void Start () {
		ttR = timeTillRespawn;
		ttCD = timeTillCorpseDisappears;
		baddyReference = Instantiate(baddy,this.transform);
		isDead = false;
		respawnFrozen = false;
		corpseDisappeared = false;


	}

	void Update() {

		if(baddyReference.GetComponent<EnemyController>().getBaddyHealth() <= 0) {

			if(!isDead){
				corpseDisappeared = false;
			}
			isDead = true;
		}

		if(isDead) {
			if(!respawnFrozen){
				timeTillRespawn -= Time.deltaTime;
			}

			if(!corpseDisappeared){
				timeTillCorpseDisappears -= Time.deltaTime;
			}
			

			if(timeTillCorpseDisappears <= 0 && !corpseDisappeared) {
				baddyReference.transform.position = new Vector3(this.transform.position.x,this.transform.position.y-1000f,this.transform.position.z);
				corpseDisappeared = true;
				timeTillCorpseDisappears = ttCD;
			}
			if(timeTillRespawn <= 0) {
				baddyReference.transform.position = this.transform.position;
				int maxHealth = baddyReference.GetComponent<EnemyController>().getBaddyMaxHealth();
				baddyReference.GetComponent<EnemyController>().setBaddyHealth(maxHealth);
				baddyReference.GetComponent<EnemyController>().setBaddyHealth(maxHealth);
				baddyReference.GetComponent<EnemyController>().setIsDead(false);
				baddyReference.GetComponent<Animator>().SetBool("isDead", false);
				baddyReference.GetComponent<CapsuleCollider>().enabled = true;
				baddyReference.GetComponent<NavMeshAgent>().enabled = true;
				baddyReference.GetComponent<NavMeshAgent>().SetDestination(baddyReference.transform.position);

				isDead = false;
				timeTillRespawn = ttR;
			}
		}
	}

		private void OnTriggerEnter(Collider other)
		{
			
			if(other.transform.tag == "Player"){
				respawnFrozen = true;
				timeTillRespawn = ttR;
			}
			
			//
		}

		private void OnTriggerExit(Collider other) {
			if(other.transform.tag == "Player"){
				respawnFrozen = false;
			}
		}




}
