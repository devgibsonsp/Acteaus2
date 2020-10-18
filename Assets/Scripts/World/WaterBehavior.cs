using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    private Transform WaterController { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        WaterController = GameObject.Find("WaterController").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(WaterController.position.x,this.transform.position.y, WaterController.position.z);
    }
}
