using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class CharacterMovement : MonoBehaviourPunCallbacks//, IPunObservable
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
        Target = this.gameObject.transform;
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

    public Vector3 NormalizeLookAt(Transform t)
    {
            Vector3 lookAtPosition = t.position;
            lookAtPosition.y = this.gameObject.transform.position.y;
            return lookAtPosition;
    }

//////public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
////// {
//////
//////     if(stream.IsWriting)
//////     {
//////         //stream.SendNext(this.transform.position);
//////         //stream.SendNext(Vector3.Lerp(this.transform.position, HitPoint, agent.speed * Time.deltaTime));
//////         //stream.SendNext(transform.rotation);
//////         
//////         //this is our payer. we need to send our ctual osition to the network.
//////         if(Target != null)
//////         {
//////             //Debug.Log("A");
//////             stream.SendNext(Target.position);
//////         }
//////         else if(IsHit)
//////         {
//////             //Debug.Log("B");
//////             stream.SendNext(HitPoint);
//////             
//////         }
//////         else 
//////         {
//////             //Debug.Log("C");
//////             stream.SendNext(null);
//////         }
//////         
//////         stream.SendNext(transform.position);
//////         stream.SendNext(transform.rotation);
//////
//////     }
//////     else
//////     {
//////
//////
//////         //if(Target != null)
//////         //{
//////
//////         //    
//////         //    Debug.Log("A");
//////         //    transform.position = Vector3.Lerp((Vector3)stream.ReceiveNext(), HitPoint, agent.speed * Time.deltaTime);
//////         //    //transform.position = (Vector3)stream.ReceiveNext();
//////         //}
//////         //else if(IsHit)
//////         //{
//////         //    Debug.Log("B");
//////         //    transform.position = Vector3.Lerp((Vector3)stream.ReceiveNext(), Target.position, agent.speed * Time.deltaTime);
//////         //}
//////         //else
//////         //{
//////         //    Debug.Log("C");
//////         //    transfsorm.position = (Vector3)stream.ReceiveNext();
//////         //}
//////         //Debug.Log(stream.ReceiveNext());
//////         Vector3 test = (Vector3)stream.ReceiveNext();
//////         Debug.Log(test);
//////
//////         transform.position = Vector3.Lerp((Vector3)stream.ReceiveNext(), test, agent.speed * Time.deltaTime);
//////         //transform.position = (Vector3)stream.ReceiveNext();
//////         transform.rotation = (Quaternion)stream.ReceiveNext();
//////         
//////        // transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
//////     }
////// }


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