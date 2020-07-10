using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using ObjectData.ItemData.Models;
using ObjectData.ItemData.Utilities;
using UI;
public class ItemProperties : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

	public Item Item { get; set; }
	public bool isMerchantGood;

	private bool merchantGoodBeingDragged;
	private Vector3 offset;
	private GameObject tooltip;
	private Canvas tooltipCanvas;
	private GameObject inventory;
	//private playerInventory invReference;

	// This all needs to be majorly cleaned up


	void Start() {

		offset = new Vector3(150f,35f,0f);
		Item = ItemLookup.FindItem(this.gameObject.name);

		tooltip = GameObject.Find("ToolTip");
		tooltipCanvas = tooltip.GetComponent<Canvas>();
		inventory = GameObject.Find("Inventory");

		merchantGoodBeingDragged = false;

        //EventTrigger trigger = GetComponent<EventTrigger>();
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.Drag;
        //entry.callback.AddListener((data) => { dragEvent((PointerEventData)data); });
        //trigger.triggers.Add(entry);
		
	}



	

    public void OnBeginDrag(PointerEventData eventData)
    {
		UserInterfaceLock.DraggedItem = this;
		UserInterfaceLock.IsDragging = true;
		Debug.Log("Smap");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		UserInterfaceLock.IsDragging = false;
 		Debug.Log("ddddddddddd");
    }

	public void enableTooltip() {
		tooltipCanvas.enabled = true;
		tooltip.transform.position = Input.mousePosition + offset;
		tooltip.transform.GetChild(0).GetComponent<Text>().text = Item.Name;
		tooltip.transform.GetChild(1).GetComponent<Text>().text = Item.Description;
		tooltip.transform.GetChild(2).GetComponent<Text>().text = "Type: " + Item.Type;
		tooltip.transform.GetChild(3).GetComponent<Text>().text = "Level Requirement: " + Item.Requirement.Level;
		if(Item.SlotType == "Weapon")
		{
			tooltip.transform.GetChild(4).GetComponent<Text>().text = "Damage: " + Item.Properties.Physical;
		}
		else if(Item.SlotType == "Shield")
		{
			tooltip.transform.GetChild(4).GetComponent<Text>().text = "Armor: " + Item.Properties.Armor;
		}
		else if(Item.SlotType == "Armor")
		{
			tooltip.transform.GetChild(4).GetComponent<Text>().text = "Armor: " + Item.Properties.Armor;
		}
		else if(Item.SlotType == "Armor")
		{
			tooltip.transform.GetChild(4).GetComponent<Text>().text = "Armor: " + Item.Properties.Armor;
		}
		else if(Item.SlotType == "Consumable")
		{
			tooltip.transform.GetChild(4).GetComponent<Text>().text = "Consumable";
		}
		tooltip.transform.GetChild(5).GetComponent<Text>().text = "Value: "+ Item.Value + " Gold";
	}



	private void ArmorToolTip()
	{

	}

	public void disableTooltip() {
		//tooltip.SetActive(false);
		tooltipCanvas.enabled = false;
	}

}

