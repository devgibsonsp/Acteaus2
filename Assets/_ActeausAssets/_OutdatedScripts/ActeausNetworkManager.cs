using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
public class ActeausNetworkManager : NetworkManager {
	public GameObject MainMenuObject;
	private Canvas MainMenuCanvas;
	private NetworkClient myClient;

	void Start(){
		MainMenuCanvas = MainMenuObject.GetComponent<Canvas>();
		Debug.Log(base.networkAddress);
		Debug.Log(base.networkPort);
		 string hostName = System.Net.Dns.GetHostName();
 			string localIP = System.Net.Dns.GetHostEntry(hostName).AddressList[0].ToString();
		Debug.Log(hostName);
		Debug.Log(localIP);
	}

	public void StartHosting(){
		MainMenuCanvas.enabled = false;
		base.StartHost();
	}

	public void JoinServer(){
		MainMenuCanvas.enabled = false;
        base.StartClient();
    
	}
}


