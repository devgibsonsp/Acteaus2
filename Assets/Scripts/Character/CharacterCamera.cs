using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharacterCamera : MonoBehaviourPunCallbacks {

    private Camera cam;
    private Vector3 CameraPos;
    private Vector3 Offset;

    [SerializeField]
    private const float X_OFFSET = 15.2f;

    [SerializeField]
    private const float Y_OFFSET = 20f;

    [SerializeField]
    private const float Z_OFFSET = 0f;

    void Start() {

        if (photonView.IsMine)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            Offset = new Vector3(X_OFFSET,Y_OFFSET,Z_OFFSET);
        }


    }

	void Update() {

        //Offset = new Vector3(X_OFFSET,Y_OFFSET,Z_OFFSET);
        if (photonView.IsMine)
        {
            CameraPos = new Vector3(
                this.gameObject.transform.position.x + Offset.x, 
                this.gameObject.transform.position.y + Offset.y, 
                this.gameObject.transform.position.z + Offset.z
            );
            cam.transform.position = CameraPos;
        }

	}
}
