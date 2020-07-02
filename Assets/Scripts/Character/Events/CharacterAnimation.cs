using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterAnimation : MonoBehaviour
{
    private Animator Anim { get; set; }
    

    void Awake()
    {
        Anim = gameObject.GetComponent<Animator>();
    }

    public void PlayerRunAnimation(NavMeshAgent agent)
    {
        if (agent.velocity != Vector3.zero)
        {
            Anim.SetBool("isRunning",true);
        }
        else
        {
            Anim.SetBool("isRunning",false);
        }
    }

    public void PlayerAttackAnimation()
    {
        
    }

}
