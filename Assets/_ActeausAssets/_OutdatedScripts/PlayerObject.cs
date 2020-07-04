using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerObject : NetworkBehaviour {

	public GameObject playerUnitPrefab;
	//public GameObject playerCamera;
	// Use this for initialization
	private int playerID;
	void Start () {

		if(!isLocalPlayer) {
			return;
		}

		CmdSpawnMyUnit();
		
	}


	[Command]
	void CmdSpawnMyUnit() {
		GameObject go = Instantiate(playerUnitPrefab);
		//GameObject cam = Instantiate(playerCamera);
		NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
		//NetworkServer.SpawnWithClientAuthority(cam, connectionToClient);
		// test
	}
}
