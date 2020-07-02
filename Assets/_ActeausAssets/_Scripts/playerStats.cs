using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour {

	// Character Attributes
	public int strength;
	public int dexterity;
	public int vitality;
	public int wisdom;
	public int intellect;

	public int healthMax;
	public int healthCurrent;

	public int magicMax;
	public int magicCurrent;

	public int experienceMax;
	public int experienceCurrent;
	public int experienceStart;
	
	public string pName;
	public string pClass;
	public int level;
	
	public string skillPointsText;
	public int skillPoints;


	// These skills are not rendered on this screen, but are calculated here
	public int movementSpeed;
	public int attackSpeed;
	public int dodgeChance;
	public int baseMeleeDamage;

	//---------------------------

	public Text characterNameVal;
	public Text characterDescriptionVal;

	// Character Attribute text references
	public Text strengthVal;
	public Text dexterityVal;
	public Text vitalityVal;
	public Text wisdomVal;
	public Text intellectVal;
	
	public Text healthVal;
	public Text magicVal;
	public Text experienceVal;

	public Text skillPointVal;

	//---------------------------

	public Transform[] buttonArray;

	private bool pointsToSpend;
	private bool buttonsActive;


	//---HP MP EXP Bars----------
	private Slider healthBar;
	private Slider magicBar;
	private Slider experienceBar;

	// Text on the status bars
	public Text healthBarText;
	public Text magicBarText;
	public Text experienceBarText;

	// Use this for initialization
	void Start () {
		// Some text never needs to be updated ;)
		characterNameVal.text = pName;
		characterDescriptionVal.text = "Level " + level + ' ' + pClass;

		// Character Attributes
		strengthVal.text = strength.ToString();
		dexterityVal.text = dexterity.ToString();
		vitalityVal.text = vitality.ToString();
		wisdomVal.text = wisdom.ToString();
		intellectVal.text = intellect.ToString();

		healthVal.text = healthCurrent.ToString() + '/' + healthMax.ToString();
		magicVal.text = magicCurrent.ToString() + '/' + magicMax.ToString();
		experienceVal.text = experienceCurrent.ToString() + '/' + experienceMax.ToString();

		healthBarText.text = healthVal.text;
		magicBarText.text = magicVal.text;
		experienceBarText.text = experienceVal.text;

		skillPointVal.text = skillPoints.ToString() + skillPointsText;

		buttonsActive = false;
		pointsToSpend = false;

		healthBar = GameObject.Find("HealthBar").GetComponent<Slider>(); 
		magicBar = GameObject.Find("MagicBar").GetComponent<Slider>(); 
		experienceBar= GameObject.Find("ExperienceBar").GetComponent<Slider>(); 

		// Default the stats buttons to false
		foreach(Transform button in buttonArray)
		{
			button.gameObject.SetActive(false);
		}


	}

	//--Getters & Setters--------------------------------------

	/***********************************************************************************
	 * These are going to be used to update the actual incoming values for HP, MP, EXP *
	 * so that they correspond with the UI elements                                    *
	 ***********************************************************************************
	 */

	// Health Getter & Setter
	public int getCurrentHealth(){
		return healthCurrent;
	}
	public void setCurrentHealth(int h){
		healthCurrent = h;
	}
	public int getMaxHealth(){
		return healthMax;
	}
	public void setMaxHealth(int h) {
		healthMax = h;
	}

	// Magic Getter & Setter
	public int getCurrentMagic(){
		return magicCurrent;
	}
	public void setCurrentMagic(int m){
		magicCurrent = m;
	}

	// Experience Getter & Setter
	public int getCurrentExperience(){
		return experienceCurrent;
	}
	public void setCurrentExperience(int e){
		experienceCurrent = e;
	}


	//---------------------------------------------------------

	// Update is called once per frame
	void Update () {


		healthBar.value = (float)healthCurrent / (float)healthMax;
		healthVal.text = healthCurrent.ToString() + '/' + healthMax.ToString();

		magicBar.value = (float)magicCurrent / (float)magicMax;
		magicVal.text = magicCurrent.ToString() + '/' + magicMax.ToString();

		experienceBar.value = (float)(experienceCurrent - experienceStart) / (float)(experienceMax - experienceStart);
		experienceVal.text = experienceCurrent.ToString() + '/' + experienceMax.ToString();

		healthBarText.text = healthVal.text;
		magicBarText.text = magicVal.text;
		experienceBarText.text = experienceVal.text;

		// If there are skillpoints available, flip pointsToSpend
		if(skillPoints > 0 && !pointsToSpend) {
			pointsToSpend = true;
		} else if(skillPoints <= 0) {
			pointsToSpend = false;
			buttonsActive = false;
			foreach(Transform button in buttonArray)
			{
				button.gameObject.SetActive(false);
			}
		}

		if(pointsToSpend) {
			if(!buttonsActive) {
				buttonsActive = true;
				foreach(Transform button in buttonArray)
				{
					button.gameObject.SetActive(true);
				}
			}
		}

		if(experienceCurrent >= experienceMax) {
			// trigger a level up
			levelUp();
		}

	}

	public void levelUp() {
		level += 1;
		experienceStart = experienceMax;
		experienceMax *= 2;
		
		skillPoints += 1;

		healthMax += (level * 2);
		healthCurrent = healthMax;

		magicMax += (level * 2);
		magicCurrent = magicMax;

		baseMeleeDamage += (level/2);
		// Rerendering UI
		magicVal.text = magicCurrent.ToString() + '/' + magicMax.ToString();
		healthVal.text = healthCurrent.ToString() + '/' + healthMax.ToString();
		characterDescriptionVal.text = "Level " + level + ' ' + pClass;
		skillPointVal.text = skillPoints.ToString() + skillPointsText;
		experienceVal.text = experienceCurrent.ToString() + '/' + experienceMax.ToString();
	}

	public void expButtn() {
		experienceCurrent += 10;
		experienceVal.text = experienceCurrent.ToString() + '/' + experienceMax.ToString();
	}

	public void UpdateStrength() {
		strength += 1;
		skillPoints -= 1;
		baseMeleeDamage += (strength);
		strengthVal.text = strength.ToString();
		if(skillPoints <= 0) {
			skillPointVal.text = "";
		} else {
			skillPointVal.text = skillPoints.ToString() + skillPointsText;
		}
	}

	public void UpdateDexterity() {
		dexterity += 1;
		skillPoints -= 1;
		movementSpeed += 1;
		attackSpeed += 1;
		dodgeChance += 1;
		dexterityVal.text = dexterity.ToString();
		if(skillPoints <= 0) {
			skillPointVal.text = "";
		} else {
			skillPointVal.text = skillPoints.ToString() + skillPointsText;
		}
	}
	public void UpdateVitality() {
		vitality += 1;
		skillPoints -= 1;
		healthMax += (vitality * 2);
		healthVal.text = healthCurrent.ToString() + '/' + healthMax.ToString();
		vitalityVal.text = vitality.ToString();
		if(skillPoints <= 0) {
			skillPointVal.text = "";
		} else {
			skillPointVal.text = skillPoints.ToString() + skillPointsText;
		}
	}
	public void UpdateWisdom() {
		wisdom += 1;
		skillPoints -= 1;
		magicMax += (wisdom * 2);
		magicVal.text = magicCurrent.ToString() + '/' + magicMax.ToString();
		wisdomVal.text = wisdom.ToString();
		if(skillPoints <= 0) {
			skillPointVal.text = "";
		} else {
			skillPointVal.text = skillPoints.ToString() + skillPointsText;
		}
	}
	public void UpdateIntellect() {
		intellect += 1;
		skillPoints -= 1;
		magicMax += (intellect);
		magicVal.text = magicCurrent.ToString() + '/' + magicMax.ToString();
		intellectVal.text = intellect.ToString();
		if(skillPoints <= 0) {
			skillPointVal.text = "";
		} else {
			skillPointVal.text = skillPoints.ToString() + skillPointsText;
		}
	}
	
	
	
}
