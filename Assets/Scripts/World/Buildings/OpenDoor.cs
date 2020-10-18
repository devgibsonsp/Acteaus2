using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

	public float rotationSpeed;
	public Transform doorHinge;
	private float closedPos;
	private float openPos;
	private bool openTriggered;
	private bool closeTriggered;
	private float currPosY;
	private bool correcting;

	// Use this for initialization
	void Start () {
	
		closedPos = doorHinge.transform.localEulerAngles.y;
		openPos = closedPos + 90;
		openTriggered = false;
		closeTriggered = false;
		correcting = true;
	}
	
	// Update is called once per frame
	void Update () {
		currPosY= doorHinge.transform.localEulerAngles.y;
		if(doorHinge.transform.localEulerAngles.y > 350f){
			doorHinge.transform.localEulerAngles = new Vector3(doorHinge.transform.localEulerAngles.x,0,doorHinge.transform.localEulerAngles.z);
			correcting = true;
		} else if(doorHinge.transform.localEulerAngles.y > 90f) {
			doorHinge.transform.localEulerAngles = new Vector3(doorHinge.transform.localEulerAngles.x,90,doorHinge.transform.localEulerAngles.z);
			correcting = true;
		}

		if((doorHinge.transform.localEulerAngles.y <= openPos && doorHinge.transform.localEulerAngles.y >= closedPos) && openTriggered && !closeTriggered && !correcting){
			doorHinge.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
		} else if((doorHinge.transform.localEulerAngles.y <= openPos && doorHinge.transform.localEulerAngles.y >= closedPos) && !openTriggered && closeTriggered && !correcting){
			doorHinge.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed*-1, Space.World);
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		openTriggered = true;
		closeTriggered = false;
		correcting = false;
	}

	private void OnTriggerExit(Collider other) {
		openTriggered = false;
		closeTriggered = true;
		correcting = false;
	}




}