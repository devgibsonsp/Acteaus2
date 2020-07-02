using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{

    private NavMeshAgent agent;
    public Transform target;
    
    void Awake(){
    agent = GetComponent<NavMeshAgent>();
    }
    
    void Update(){
        agent.SetDestination(target.position);
    }

}
