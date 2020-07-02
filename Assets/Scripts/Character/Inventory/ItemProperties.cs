using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using ObjectData.itemData.Models;

public class ItemProperties : MonoBehaviour {


	public ItemModel Item { get; set; }


	public Vector3 offset;
	public bool isMerchantGood;

	private bool merchantGoodBeingDragged;

	private GameObject tooltip;
	private Canvas tooltipCanvas;
	private GameObject inventory;
	private playerInventory invReference;




	void Start() {

		Item = new ItemModel();

		tooltip = GameObject.Find("ToolTip");
		tooltipCanvas = tooltip.GetComponent<Canvas>();
		inventory = GameObject.Find("Inventory");

		merchantGoodBeingDragged = false;

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { dragEvent((PointerEventData)data); });
        trigger.triggers.Add(entry);
		
	}



	

	public void dragEvent(PointerEventData data){

		// If the item is worth more gold than the player possesses...
		if(isMerchantGood && Item.ItemValue > inventory.GetComponent<playerInventory>().getGoldAmount()){
			inventory.GetComponent<playerInventory>().setNotEnoughMoneyToBuy(true);
			inventory.GetComponent<playerInventory>().setMerchantGoodBeingBought(false);

		}
		else if(isMerchantGood){
			merchantGoodBeingDragged = true;
			inventory.GetComponent<playerInventory>().setNotEnoughMoneyToBuy(false);
			inventory.GetComponent<playerInventory>().setMerchantGoodBeingBought(true);
		} else if(!isMerchantGood) {
			merchantGoodBeingDragged = false;
			inventory.GetComponent<playerInventory>().setNotEnoughMoneyToBuy(false);
			inventory.GetComponent<playerInventory>().setMerchantGoodBeingBought(false);
		}
	}
	public void test2(){

		Debug.Log("dropped");
	}

	public void enableTooltip() {
		//tooltip.SetActive(true);
		tooltipCanvas.enabled = true;
		tooltip.transform.position = Input.mousePosition + offset;
		tooltip.transform.GetChild(0).GetComponent<Text>().text = Item.ItemName;
		tooltip.transform.GetChild(1).GetComponent<Text>().text = Item.Description;
		tooltip.transform.GetChild(2).GetComponent<Text>().text = "Type: " + Item.Type;
		tooltip.transform.GetChild(3).GetComponent<Text>().text = "Level Requirement: " + Item.LevelRequirement.ToString();
		tooltip.transform.GetChild(4).GetComponent<Text>().text = "Damage: " + Item.PhysicalDamage.ToString();
		tooltip.transform.GetChild(5).GetComponent<Text>().text = "Value: "+ Item.ItemValue.ToString() + " Gold";
	}

	public void disableTooltip() {
		//tooltip.SetActive(false);
		tooltipCanvas.enabled = false;
	}

	public int getArmor() {
		return Item.Armor;
	}

	public bool getIsMerchantGood(){
		return isMerchantGood;
	}

	public void setIsMerchantGood(bool b){
		isMerchantGood = b;
	}

	public int getItemValue(){
		return Item.ItemValue;
	}
}

