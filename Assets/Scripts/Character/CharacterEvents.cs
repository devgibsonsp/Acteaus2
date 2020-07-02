using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class CharacterEvents : MonoBehaviourPunCallbacks
{

    #region Event Constants

        private const float BASE_ACTION_TIME = 2f;

        private const float ACTION_STOP_DIST = 1.4f;

        private const float NORMAL_STOP_DIST = 0f;

    #endregion

    #region Component References

        private CharacterMovement MovementEvents { get; set; }
        private CharacterStatistics PlayerStatistics { get; set; }
        private CharacterStatistics TargetStatistics { get; set; }

        private CharacterAttack AttackEvents { get; set;}
        private Camera Cam { get; set; }
        private NavMeshAgent Agent { get; set; }

    #endregion

    public bool isNPC;

    
    private Transform target;



    private bool IsFollowing { get; set;} = false;


    private bool IsHitting { get; set; }

    private float ActionTime { get; set; }


    private bool HealthCheck()
    {
        if(PlayerStatistics.Player.BarStats.Health <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }



    // Start is called before the first frame update
    void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        MovementEvents = this.GetComponent<CharacterMovement>();
        PlayerStatistics = gameObject.GetComponent<CharacterStatistics>();
        AttackEvents = gameObject.GetComponent<CharacterAttack>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
    }

    ///<summary>Calculate Player attack</summary>
    private void PerformAttackEvent()
    {
        if(!IsHitting)
        {
            IsHitting = true;
            ActionTime = BASE_ACTION_TIME / PlayerStatistics.Player.ModifierStats.AttackSpeed;
            //Debug.Log("HERE");

            // Normalizing horizontal direction of character when facing enemy

            // Forcing player to look at enemy when initiating attack
            this.transform.LookAt(NormalizeLookAt(target));
        }
        else
        {
            // Subtracting from action timer to calculate time to attack
            ActionTime -= Time.deltaTime;
            if(ActionTime <= 0f)
            {
                System.Random rnd = new System.Random();

                if(AttackEvents.CalculateTargetDodge(rnd, TargetStatistics, PlayerStatistics, target))
                {
                    Debug.Log("DODGE!!!");
                }
                else if(AttackEvents.CalculateTargetBlock(rnd, TargetStatistics, PlayerStatistics, target))
                {
                    Debug.Log("BLOCK!!!");
                }
                else
                {
                    int damageToTarget = AttackEvents.CalculateDamage(rnd,TargetStatistics, PlayerStatistics);

                    AttackEvents.DealDamageToTarget(damageToTarget,TargetStatistics);
                    Debug.Log(AttackEvents.CalculateDamage(rnd,TargetStatistics, PlayerStatistics) + "Damage!");
                }

                // Calculate character damage

                
                IsHitting = false;
            }
        }
    }

    private void DirectionTest()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
         
             Vector3 toTarget = (target.position - transform.position).normalized;
             
             if (Vector3.Dot(toTarget, transform.forward) > 0) {
                 Debug.Log("Target is in front of this game object.");
             } else {
                 Debug.Log("Target is not in front of this game object.");
             }
         }
    }

    private void DirectionTest2()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
         
             Vector3 toTarget = (transform.position - target.position).normalized;
             
             if (Vector3.Dot(toTarget, target.forward) > 0) {
                 Debug.Log("Player is in front of this game object.");
             } else {
                 Debug.Log("Player is not in front of this game object.");
             }
         }
    } 

    private Vector3 NormalizeLookAt(Transform t)
    {
            Vector3 lookAtPosition = t.position;
            lookAtPosition.y = this.gameObject.transform.position.y;
            return lookAtPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }

        if(HealthCheck())
        {
            if(!isNPC)
            {
                if(IsFollowing) 
                {
                    // If the distance is small enough, initiate an attack **#
                    if(Vector3.Distance(this.gameObject.transform.position,target.position) < 1.5f) 
                    {
                        // Perform the sequnce, delays, calculations, etc associated with melee attack
                        PerformAttackEvent();
                    }
                    else
                    {
                        DirectionTest2();
                        DirectionTest();
                        MovementEvents.CharacterFollow(target);
                    }
                }

                
                if (Input.GetMouseButtonDown(0)) {
                    RaycastHit hit;
                    if (Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit, 100)) 
                    {
                        if(hit.transform.tag == "attackable")
                        {
                            Debug.Log("attack follow");
                            target = hit.transform;
                            TargetStatistics = target.GetComponent<CharacterStatistics>();
                            IsFollowing = true;
                            Agent.stoppingDistance = ACTION_STOP_DIST;
                        }
                        else 
                        {
                            IsFollowing = false;
                            MovementEvents.CharacterMove(hit, Cam);
                            Agent.stoppingDistance = NORMAL_STOP_DIST;
                        }
                        

                    }
                    
                }
            }
        }
        else
        {
            Debug.Log("You Are Dead");
        }


    } // END Update
}
