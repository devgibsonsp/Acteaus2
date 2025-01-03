﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UI;
public class inventorySlotBehavior : MonoBehaviour
{

	public string SlotType;

	private bool itemHasBeenUpdatedAlready;
	private playerInventory inventory;
	private Canvas inventoryCanvas;

	private bool allSlotsSetToDragOnly;

	private CharacterStatistics CharacterStatsReference { get; set;}

	// Use this for initialization
	void Start () {

		inventory = GameObject.Find("Inventory").GetComponent<playerInventory>();
		inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();
		itemHasBeenUpdatedAlready = false;
		allSlotsSetToDragOnly = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(UserInterfaceLock.IsDragging && !itemHasBeenUpdatedAlready)
		{
			Debug.Log(UserInterfaceLock.CharacterReference.Player.Name);
			itemHasBeenUpdatedAlready = true;
			ItemProperties itemRef = UserInterfaceLock.DraggedItem;
			if((SlotType != itemRef.Item.SlotType && SlotType != "All") || (!HasMetItemRequirements(itemRef) && SlotType != "All"))
			{
				this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.DragOnly;
			}


		}
		else if(!UserInterfaceLock.IsDragging && itemHasBeenUpdatedAlready)
		{
			this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.Swap;
			itemHasBeenUpdatedAlready = false;
		}

		// itemHasBeenUpdatedAlready is set to true to prevent re-running this code all the time
		//if(this.transform.childCount > 0 && !itemHasBeenUpdatedAlready)
		//{
		//	Debug.Log("item Updated");
		//	itemHasBeenUpdatedAlready = true;
		//	ItemProperties itemReference = this.transform.GetChild(0).GetComponent<ItemProperties>();
//
		//	//if(SlotType != itemReference.Item.SlotType && SlotType != "All")
		//	//{
		//	//	this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.DragOnly;
		//	//}
		//	//else
		//	//{
		//	//	this.gameObject.GetComponent<DragAndDropCell>().cellType = DragAndDropCell.CellType.Swap;
		//	//}
//
		//} 
		//else if(this.transform.childCount <= 0 && itemHasBeenUpdatedAlready)
		//{
		//	itemHasBeenUpdatedAlready = false;
		//}


	}

	private bool HasMetItemRequirements(ItemProperties itemRef)
	{
			if(itemRef.Item.Requirement.Dexterity > UserInterfaceLock.CharacterReference.Player.CoreStats.Dexterity)
			{
				return false;
			}
			if(itemRef.Item.Requirement.Intellect > UserInterfaceLock.CharacterReference.Player.CoreStats.intellect)
			{
				return false;
			}
			if(itemRef.Item.Requirement.Level > UserInterfaceLock.CharacterReference.Player.Level)
			{
				return false;
			}
			if(itemRef.Item.Requirement.Strength > UserInterfaceLock.CharacterReference.Player.CoreStats.Strength)
			{
				return false;
			}
			if(itemRef.Item.Requirement.Vitality > UserInterfaceLock.CharacterReference.Player.CoreStats.Vitality)
			{
				return false;
			}
			if(itemRef.Item.Requirement.Wisdom > UserInterfaceLock.CharacterReference.Player.CoreStats.Wisdom)
			{
				return false;
			}
			return true;
	}


}
