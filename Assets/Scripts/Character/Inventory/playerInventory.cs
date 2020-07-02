using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInventory : MonoBehaviour {

	public Text goldAmountVal;
	public int goldAmount;

	[SerializeField]
	private bool merchantGoodBeingBought;
	[SerializeField]
	private bool notEnoughMoneyToBuy;



	// Use this for initialization
	void Start () {
		merchantGoodBeingBought = false;
		notEnoughMoneyToBuy = false;
	}
	
	// Update is called once per frame
	void Update () {
		goldAmountVal.text = goldAmount.ToString() + " Gold";
	}

	//------------------------------------------------
	public int getGoldAmount(){
		return goldAmount;
	}
	public void setGoldAmount(int g){
		goldAmount = g;
	}
	//------------------------------------------------
	public void setMerchantGoodBeingBought(bool b){
		merchantGoodBeingBought = b;

	}
	public bool getMerchantGoodBeingBought(){
		return merchantGoodBeingBought;
	}
	//------------------------------------------------
	public bool getNotEnoughMoneyToBuy(){
		return notEnoughMoneyToBuy;
	}
	public void setNotEnoughMoneyToBuy(bool b){
		notEnoughMoneyToBuy = b;
	}
	//------------------------------------------------
}
