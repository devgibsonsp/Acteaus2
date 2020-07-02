using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class CharacterMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    private NavMeshAgent agent;
    private Transform Target { get; set; }

    private Vector3 HitPoint { get; set;}
    private bool IsHit { get; set; }

    private float ActionTime { get; set; } = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void CharacterMove(RaycastHit hit, Camera cam)
    {
        IsHit = true;
        HitPoint = hit.point;
        Target = null;
        ActionTime = 0f;
        agent.destination = hit.point;
    }

    public void CharacterFollow(Transform target)
    {
        IsHit = false;
        //HitPoint = null;
        Target = target;
        agent.destination = Target.position;   
    }

   public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("wrting");
        if(stream.IsWriting)
        {
            //this is our payer. we need to send our ctual osition to the network.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //this is someone elses player we need to recieve their position as of a few mliseeconds ago, and update our version of that platyer.
            //transform.position = (Vector3)stream.ReceiveNext();
            //transform.rotation = (Quaternion)stream.ReceiveNext();

            //Vector3 pos = (Vector3)stream.ReceiveNext();
           // Quaternion rot = (Quaternion)stream.ReceiveNext();

            //float speed = agent.speed;
            if(Target != null)
            {
                transform.position = Vector3.Lerp((Vector3)stream.ReceiveNext(), HitPoint, agent.speed * Time.deltaTime);
                transform.position = (Vector3)stream.ReceiveNext();
            }
            else if(IsHit)
            {
                transform.position = Vector3.Lerp((Vector3)stream.ReceiveNext(), Target.position, agent.speed * Time.deltaTime);
            }
            else
            {
                transform.position = (Vector3)stream.ReceiveNext();
            }
            
            
            transform.rotation = (Quaternion)stream.ReceiveNext();
            
           // transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
    }


}



/*

 Types of Hits
  A. Facing
  B. Not Facing
  C. Facing and Attacking


    A. Facing:
        1. Check Enemy Dodge
        2. Check Enemy Block
        3. Check Player Damage
    B. Not Facing
        2. Check Enemy Dodge *With penalty
    C. Facing and Attacking
        Player Turn
            1. Check Enemy Dodge
            2. Check Enemy Block
            3. Check Player Damage
        Enemy



*/