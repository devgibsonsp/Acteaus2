using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SellItemSlotBehavior : MonoBehaviour {


	private playerInventory inventory;
	
	// Use this for initialization
	void Start () {
		// Setting up a reference to the player inventory
		inventory = GameObject.Find("Inventory").GetComponent<playerInventory>();

	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.childCount > 0){
			ItemProperties itemReference = this.transform.GetChild(0).GetComponent<ItemProperties>();

			// If the item is not a merchant good, then it can be sold :)
			if(!itemReference.getIsMerchantGood()){
				inventory.setGoldAmount(inventory.getGoldAmount()+itemReference.getItemValue());
				//inventory.getGoldAmount();
				//Debug.Log(itemReference.getItemValue());
			}

			// After the item is sold, destroy it - if the item cannot be sold, still destroy it
			Destroy(this.transform.GetChild(0).gameObject);
		}
	}
}
