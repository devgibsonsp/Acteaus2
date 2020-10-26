using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ObjectData.ItemData.Utilities;
using ObjectData.ItemData.Models;
using UI;
using CharacterNS;

public class playerInventory : MonoBehaviour 
{

	// *** Leaving a note here for future that if there is a split second that items aren't
	// calculated then this could lead to players getting killed as soon as they log in
	// There will need to be a few seconds of player immunity while the player's data is being loaded. ***

	// *** Need to create unique player objects and store the player's identity somewhere so that
	// the find script only works for the player's gameobject ***

	private InventorySlotModel inventorySlots;

	private bool IsPerformingEquipmentUpdate { get; set; } = false;

	private Text StatsIndicator { get; set; }

	private bool InventoryCalcuated { get; set; } = false;

	private bool CharacterLoaded { get; set; } = false;

	private GameObject HeadRef {get; set; }


	// Use this for initialization
	void Awake () 
	{
		
		StatsIndicator = GameObject.Find("StatsIndicator").GetComponent<Text>();

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
		//HeadRef = new GameObject();
		//HeadRef = CharacterObject.Ref.transform.Find("Head").gameObject;
		
		//GameObject.Find("PlayerV11(Clone)").transform.Find("Head").gameObject;
		
		
	}

	void Update()
	{

		if(CharacterObject.RefSet && !CharacterLoaded)
		{
			CharacterLoaded = true;
			HeadRef = CharacterObject.Ref;//.transform.Find("Head").gameObject;
			HeadRef = CharacterObject.Ref.transform.Find("PlayerBody/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Bip001 HeadNub").gameObject;
		}

		// If the item master list is initialized, flip the flag and calculate items
		if(ItemLookup.IsInitialized && !InventoryCalcuated)
		{
			CalculateEquipment();
			InventoryCalcuated = true;
			//ItemLookup.IsInitialized = true;
		}

		if(UserInterfaceLock.IsDragging && !IsPerformingEquipmentUpdate)
		{
			IsPerformingEquipmentUpdate = true;
		}
		if(IsPerformingEquipmentUpdate && !UserInterfaceLock.IsDragging)
		{
			IsPerformingEquipmentUpdate = false;
			CalculateEquipment();
			//SetEquipmentModels();
		}
	}

	private void SetEquipmentModels()
	{
		// Whenever equipment is moved, detect the models on the player
		// IPORTANT: This must sync with MP

		// Detect Helmet Changes
		if(inventorySlots.HeadSlot.transform.childCount > 0)
		{
			// reference to the helmet
			HeadRef.transform.Find(inventorySlots.HeadSlot.transform.GetChild(0).name).GetComponent<MeshRenderer>().enabled = true;
			//inventorySlots.HeadSlot.transform.FindChild()
			//inventorySlots.HeadSlot.transform.GetChild(0).name;
			Debug.Log("HeadSlot");
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

		StatsIndicator.text = "Armor: " + equipmentProperties.Armor.ToString() + "\n";
		StatsIndicator.text += "Damage: " + equipmentProperties.Physical.ToString();
		UserInterfaceLock.CharacterReference.Player.CalculateStats(equipmentProperties);
		
	}
	

}
