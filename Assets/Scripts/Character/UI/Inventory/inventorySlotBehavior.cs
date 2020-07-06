using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySlotBehavior : MonoBehaviour {

	public enum SlotType                                                    
    {
        All,                                                               
        Weapon,                                                         
        Shield,
		Armor,
		Helmet,
		Ring,
		Gloves,
		Boots,
		Amulet,
    }

	public string SlotType2;

	private bool itemHasBeenUpdatedAlready;
	private playerInventory inventory;
	private Canvas inventoryCanvas;

	private bool allSlotsSetToDragOnly;


	public SlotType slotType = SlotType.All; 

	// Use this for initialization
	void Start () {
		inventory = GameObject.Find("Inventory").GetComponent<playerInventory>();
		inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();
		itemHasBeenUpdatedAlready = false;
		allSlotsSetToDragOnly = false;
	}
	
	// Update is called once per frame
	void Update () {



		// itemHasBeenUpdatedAlready is set to true to prevent re-running this code all the time
		if(this.transform.childCount > 0 && !itemHasBeenUpdatedAlready)
		{
			Debug.Log("item Updated");
			itemHasBeenUpdatedAlready = true;
			ItemProperties itemReference = this.transform.GetChild(0).GetComponent<ItemProperties>();


			// ******* This is obviously wrong but this is sort of the idea
			//**************************
			if(SlotType2 != itemReference.Item2.Type || SlotType2 != "All")
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.DragOnly;
			}
			else
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.Swap;
			}



			// If the item is being bought from the merchant
			if(itemReference.getIsMerchantGood())
			{
				if(inventory.getGoldAmount() >= itemReference.getItemValue())
				{
					inventory.setGoldAmount(inventory.getGoldAmount()-itemReference.getItemValue());
					itemReference.setIsMerchantGood(false);

				} 
				else 
				{
					itemReference.setIsMerchantGood(false);
					//Destroy(this.transform.GetChild(0).gameObject);
				}
				
			}

		} 
		else if(this.transform.childCount <= 0 && itemHasBeenUpdatedAlready)
		{
			itemHasBeenUpdatedAlready = false;
		}


		if(inventoryCanvas.isActiveAndEnabled)
		{
			// If the player does not have enough money to buy
			if(inventory.getNotEnoughMoneyToBuy())
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.DragOnly;
				allSlotsSetToDragOnly = true;
			}
			else if(this.transform.childCount > 0 && inventory.getMerchantGoodBeingBought())
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.DragOnly;
			} else if (this.transform.childCount > 0 && !inventory.getMerchantGoodBeingBought())
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.Swap;
			} 

			if(allSlotsSetToDragOnly && !inventory.getNotEnoughMoneyToBuy()) 
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.Swap;
				allSlotsSetToDragOnly = false;
			}
		}


	}
}


/*


	maybe ondrag + merchant item means turn all slots to undraggable
 */
