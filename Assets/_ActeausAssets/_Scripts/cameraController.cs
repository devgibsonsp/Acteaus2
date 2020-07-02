using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *	Controls the position of the player's camera, so that it moves as the player moves and is oriented correctly *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class cameraController : MonoBehaviour {
	public GameObject player;
	public float xOffset;
	public float yOffset;
	public float zOffset;


	void Update() {
		Vector3 pos = player.transform.position;
		
		pos.x += xOffset;
		pos.y += yOffset;
		pos.z += zOffset;
		
		transform.position = pos;
	}
}
