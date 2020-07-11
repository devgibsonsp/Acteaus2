using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ObjectData.ItemData.Utilities;
using ObjectData.ItemData.Models;
using UI;

public class playerInventory : MonoBehaviour 
{

	private InventorySlotModel inventorySlots;

	private bool IsPerformingEquipmentUpdate { get; set; } = false;



	// Use this for initialization
	void Awake () {
		
		// Initializing the item lookup table
		ItemLookup.InitializeItemData();

		inventorySlots = new InventorySlotModel
		{
			HeadSlot    = GameObject.Find("HeadSlot"),
			GloveSlot   = GameObject.Find("GloveSlot"),
			NeckSlot1   = GameObject.Find("NeckSlot1"),
			NeckSlot2   = GameObject.Find("NeckSlot2"),
			BodySlot    = GameObject.Find("BodySlot"),
			HandSlot1   = GameObject.Find("HandSlot1"),
			HandSlot2   = GameObject.Find("HandSlot2"),
			FeetSlot    = GameObject.Find("FeetSlot"),
			FingerSlot1 = GameObject.Find("Finger1Slot"), // Numbers reversed, not changing it
			FingerSlot2 = GameObject.Find("Finger2Slot"),
			FingerSlot3 = GameObject.Find("Finger3Slot"),
			FingerSlot4 = GameObject.Find("Finger4Slot")
		};


	}

	void Update()
	{
		if(UserInterfaceLock.IsDragging && !IsPerformingEquipmentUpdate)
		{
			IsPerformingEquipmentUpdate = true;
		}
		if(IsPerformingEquipmentUpdate && !UserInterfaceLock.IsDragging)
		{
			IsPerformingEquipmentUpdate = false;
			CalculateEquipment();
		}
	}

	private void CalculateEquipment()
	{

		// Todo: On equip, push list of items to be passed to the damage calculator for looping through
		Properties equipmentProperties = new Properties();

		// In the future this will only fire on an equipment change rather than
		// any inventory slot change
		if(inventorySlots.HeadSlot.transform.childCount > 0)
		{
			equipmentProperties.Armor += inventorySlots.HeadSlot.transform.GetChild(0).GetComponent<ItemProperties>().Item.Properties.Armor;
			Debug.Log("HeadSlot");
		}
		if(inventorySlots.GloveSlot.transform.childCount > 0)
		{
			equipmentProperties.Armor += inventorySlots.GloveSlot.transform.GetChild(0).GetComponent<ItemProperties>().Item.Properties.Armor;
			Debug.Log("GloveSlot");
		}
		if(inventorySlots.NeckSlot1.transform.childCount > 0)
		{
			Debug.Log("NeckSlot1");
		}
		if(inventorySlots.NeckSlot2.transform.childCount > 0)
		{
			Debug.Log("NeckSlot2");
		}
		if(inventorySlots.BodySlot.transform.childCount > 0)
		{
			equipmentProperties.Armor += inventorySlots.BodySlot.transform.GetChild(0).GetComponent<ItemProperties>().Item.Properties.Armor;
			Debug.Log("BodySlot");
		}
		if(inventorySlots.HandSlot1.transform.childCount > 0)
		{
			equipmentProperties.Physical += inventorySlots.HandSlot1.transform.GetChild(0).GetComponent<ItemProperties>().Item.Properties.Physical;
			Debug.Log("BodySlot");
		}
		if(inventorySlots.HandSlot2.transform.childCount > 0)
		{
			equipmentProperties.Armor += inventorySlots.HandSlot2.transform.GetChild(0).GetComponent<ItemProperties>().Item.Properties.Armor;
			Debug.Log("HandSlot2");
		}
		if(inventorySlots.FeetSlot.transform.childCount > 0)
		{
			equipmentProperties.Armor += inventorySlots.FeetSlot.transform.GetChild(0).GetComponent<ItemProperties>().Item.Properties.Armor;
			Debug.Log("FeetSlot");
		}
		if(inventorySlots.FingerSlot1.transform.childCount > 0)
		{
			Debug.Log("FingerSlot1");
		}
		if(inventorySlots.FingerSlot2.transform.childCount > 0)
		{
			Debug.Log("FingerSlot2");
		}
		if(inventorySlots.FingerSlot3.transform.childCount > 0)
		{
			Debug.Log("FingerSlot3");
		}
		if(inventorySlots.FingerSlot4.transform.childCount > 0)
		{
			Debug.Log("FingerSlot4");
		}
		UserInterfaceLock.CharacterReference.Player.CalculateStats(equipmentProperties);
		
	}
	

}
