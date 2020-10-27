using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ObjectData.ItemData.Utilities;
using ObjectData.ItemData.Models;
using UI;
using CharacterNS;
using Photon.Pun;

public class playerInventory : MonoBehaviourPunCallbacks, IPunObservable

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





	private GameObject BodyRef { get; set; }

	private GameObject HeadRef { get; set; }
	private GameObject RightHandRef { get; set; }
	private GameObject LeftHandRef { get; set; }

	private GameObject WeaponRef { get; set; }
	private GameObject ShieldRef { get; set; }
	private GameObject ArmorRef { get; set; }
	private GameObject HelmRef { get; set; }


	string EqHelm { get; set; }

	string armor;

	string EqWep { get; set; }

	string EqShield { get; set; }


	//private PhotonView PhotonView { get; set; }

	// Use this for initialization
	void Awake () 
	{
		//PhotonView = PhotonView.Get(this);

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

		
		
	}


   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
   {
       if (stream.IsWriting)
       {
           // We own this player: send the others our data

		   if(WeaponRef != null)
		   {
			   stream.SendNext(WeaponRef.name);
		   }
		   else
		   {
			    stream.SendNext("No Weapon");
		   }

		   if(ShieldRef != null)
		   {
			   stream.SendNext(ShieldRef.name);
		   }
		   else
		   {
			    stream.SendNext("No Shield");
		   }

		   if(HelmRef != null)
		   {
			   stream.SendNext(HelmRef.name);
		   }
		   else
		   {
			    stream.SendNext("No Helm");
		   }
           
			//stream.SendNext(ShieldRef.name);
			//stream.SendNext(ArmorRef.name);
			//stream.SendNext(HelmRef.name);
           //stream.SendNext(Health);
       }
       else
       {
			string tWep   = (string)stream.ReceiveNext();
			string tHelm   = (string)stream.ReceiveNext();
			string tSheild = (string)stream.ReceiveNext();
			Debug.Log(tWep   );
			Debug.Log(tHelm  );
			Debug.Log(tSheild);

		   //// Network player, receive data
		   //if(!EqWep.Equals(tWep))
		   //{
			//   SetEquipmentModels();
			//   // do something
		   //}
		   //if(!EqWep.Equals(tHelm))
		   //{
			//   SetEquipmentModels();
			//   // do something
		   //}
//
		   //if(!EqWep.Equals(tSheild))
		   //{
			//   SetEquipmentModels();
			//   // do something
		   //}



          
       }    

   }



	void Update()
	{

		if(CharacterObject.RefSet && !CharacterLoaded)
		{
			CharacterLoaded = true;
			HeadRef = CharacterObject.Ref.transform.Find("PlayerBody/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head/Bip001 HeadNub").gameObject;
			BodyRef = CharacterObject.Ref.transform.Find("PlayerBody/Body").gameObject;
			RightHandRef = CharacterObject.Ref.transform.Find("PlayerBody/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand").gameObject;
			LeftHandRef = CharacterObject.Ref.transform.Find("PlayerBody/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand").gameObject;
			SetEquipmentModels();
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
			//PhotonView.RPC("SetEquipmentModels", RpcTarget.All);
			
			
			SetEquipmentModels();
		}
		
	}

	// *** One thing to always note, this is simply for asthetics, this is not where armor calculations go ***
	private void SetEquipmentModels()
	{
		// Whenever equipment is moved, detect the models on the player
		// IPORTANT: This must sync with MP

		// Detect Helmet Changes
		if(inventorySlots.HeadSlot.transform.childCount > 0)
		{
			if(HelmRef != null && !HelmRef.name.Equals(inventorySlots.HeadSlot.transform.GetChild(0).name))
			{
				HelmRef.transform.GetComponent<MeshRenderer>().enabled = false;
			}

			// reference to the helmet
			HelmRef = HeadRef.transform.Find(inventorySlots.HeadSlot.transform.GetChild(0).name).gameObject;
			HelmRef.transform.GetComponent<MeshRenderer>().enabled = true;
		}
		else if(HelmRef != null)
		{
			HelmRef.transform.GetComponent<MeshRenderer>().enabled = false;
			HelmRef = null;
		}

		if(inventorySlots.BodySlot.transform.childCount > 0)
		{
			if(ArmorRef != null && !BodyRef.name.Equals(inventorySlots.BodySlot.transform.GetChild(0).name))
			{
				ArmorRef.SetActive(false);
			}
			BodyRef.transform.Find("Body None").gameObject.SetActive(false);
			ArmorRef = BodyRef.transform.Find(inventorySlots.BodySlot.transform.GetChild(0).name).gameObject;
			ArmorRef.SetActive(true);
		}
		else if(ArmorRef != null)
		{
			BodyRef.transform.Find("Body None").gameObject.SetActive(true);
			ArmorRef.SetActive(false);
			ArmorRef = null;
		}

		if(inventorySlots.HandSlot1.transform.childCount > 0)
		{
			if(WeaponRef != null && !WeaponRef.name.Equals(inventorySlots.HandSlot1.transform.GetChild(0).name))
			{
				WeaponRef.transform.GetComponent<MeshRenderer>().enabled = false;
			}

			// reference to the helmet
			WeaponRef = RightHandRef.transform.Find(inventorySlots.HandSlot1.transform.GetChild(0).name).gameObject;
			WeaponRef.transform.GetComponent<MeshRenderer>().enabled = true;

		}
		else if(WeaponRef != null)
		{
			WeaponRef.transform.GetComponent<MeshRenderer>().enabled = false;
			WeaponRef = null;
		}

		if(inventorySlots.HandSlot2.transform.childCount > 0)
		{
			if(ShieldRef != null && !ShieldRef.name.Equals(inventorySlots.HandSlot2.transform.GetChild(0).name))
			{
				ShieldRef.transform.GetComponent<MeshRenderer>().enabled = false;
			}

			// reference to the helmet
			ShieldRef = LeftHandRef.transform.Find(inventorySlots.HandSlot2.transform.GetChild(0).name).gameObject;
			ShieldRef.transform.GetComponent<MeshRenderer>().enabled = true;

		}
		else if(ShieldRef != null)
		{
			ShieldRef.transform.GetComponent<MeshRenderer>().enabled = false;
			ShieldRef = null;
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
