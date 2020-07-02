using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorFloorChecker : MonoBehaviour {

	[SerializeField]
	private bool active;
	[SerializeField]
	private bool inside;

	public Transform otherFloorTile;
	public Transform exterior;

	
	// Use this for initialization
	void Start () {
		active = false;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		active = true;
		inside = true;
		exterior.gameObject.GetComponent<buildingFader>().SetFadeStatus(true);
		
	}

	private void OnTriggerExit(Collider other) {
		active = false;

		if(!otherFloorTile.gameObject.GetComponent<InteriorFloorChecker>().GetActiveStatus()){
			exterior.gameObject.GetComponent<buildingFader>().SetFadeStatus(false);
			otherFloorTile.gameObject.GetComponent<InteriorFloorChecker>().SetInsideStatus(false);
			inside = false;
		}
	}

	public bool GetActiveStatus(){
		return active;
	}

	public bool GetInsideStatus(){
		return inside;
	}

	public void SetInsideStatus(bool i){
		inside = i;
	}
}
