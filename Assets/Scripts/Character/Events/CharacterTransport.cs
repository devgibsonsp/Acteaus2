using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class CharacterTransport : MonoBehaviour
{
    private bool IsTransporting { get; set; } = false;
    private NavMeshAgent Agent { get; set; }

    void Start()
    {
        Agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.name=="Passage")
       {
           Agent.enabled = false;
           this.gameObject.transform.position = other.gameObject.GetComponent<PassageData>().EndLocation;
           Agent.enabled = true;
           //IsTransporting = true;
       }
    }

    //void Update()
    //{
    //    if(IsTransporting)
    //    {
    //        Agent.enabled = true;
    //        IsTransporting = false;
    //    }
    //}
}
