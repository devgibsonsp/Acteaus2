using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UI;
public class CharacterEvents : MonoBehaviourPunCallbacks
{

    #region Event Constants

        private const float BASE_ACTION_TIME = 2f;

        private const float ACTION_STOP_DIST = 1.4f;

        private const float NORMAL_STOP_DIST = 0f;

    #endregion

    #region Component References
        private CharacterStatistics PlayerStatistics { get; set; }
        private CharacterStatistics TargetStatistics { get; set; }
        // starting to hate this...
        private CharacterAnimation TargetAnimationEvents { get; set; }
        private Camera Cam { get; set; }
        private NavMeshAgent Agent { get; set; }
        private Animator Anim { get; set; }

    #endregion

    #region Event Components
        private CharacterMovement MovementEvents { get; set; }
        private CharacterAnimation AnimationEvents { get; set; }
        private CharacterAttack AttackEvents { get; set;}
    #endregion


    System.Random rnd;

    // temp
    public bool isNPC;


    private Transform target;



    private bool IsFollowing { get; set;} = false;


    private bool IsHitting { get; set; }

    private float ActionTime { get; set; }

    private bool testBool = false;

    public int currentHealth;

    private bool HealthCheck()
    {
        if(PlayerStatistics.Player.BarStats.Health <= 0)
        {
            return false;
        }
        else
        {
            // If the player's health has decreased, run animation
            if(PlayerStatistics.Player.BarStats.Health < currentHealth)
            {
                AnimationEvents.PlayerTakeDamageAnimation(true);
            }
            else
            {
                AnimationEvents.PlayerTakeDamageAnimation(false);
            }

            if(PlayerStatistics.Player.BarStats.Health != currentHealth)
            {
                currentHealth = PlayerStatistics.Player.BarStats.Health;
            }


            return true;
        }



    }



    // Start is called before the first frame update
    void Awake()
    {
        
        rnd = new System.Random();
        InitializeComponents();
        currentHealth = PlayerStatistics.Player.BarStats.Health;
    }
    

    private void InitializeComponents()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        MovementEvents = this.GetComponent<CharacterMovement>();
        PlayerStatistics = gameObject.GetComponent<CharacterStatistics>();
        AttackEvents = gameObject.GetComponent<CharacterAttack>();
        AnimationEvents = gameObject.GetComponent<CharacterAnimation>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();

        // Not entirely sure this is necessary  but given that this is a shared component using a static value I am taking
        // a precaution
        if (!isNPC && !photonView.IsMine)
        {
            return;
        }
        // Setting the reference to this character specific to the client
        UserInterfaceLock.CharacterReference = PlayerStatistics;
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
            this.transform.LookAt(MovementEvents.NormalizeLookAt(target));
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
                    TargetAnimationEvents.PlayerBlockAnimation(true);
                    Debug.Log("BLOCK!!!");
                }
                else
                {
                    int damageToTarget = AttackEvents.CalculateDamage(rnd,TargetStatistics, PlayerStatistics);

                    AttackEvents.DealDamageToTarget(damageToTarget,TargetStatistics);
                    Debug.Log(AttackEvents.CalculateDamage(rnd,TargetStatistics, PlayerStatistics) + "Damage!");
                }

                
                IsHitting = false;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isNPC && !photonView.IsMine)
        {
            return;
        }

        AnimationEvents.PlayerRunAnimation(Agent);

        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    AnimationEvents.PlayerAttackAnimation(rnd, false);
        //}

        if(HealthCheck())
        {
            if(!isNPC)
            {
                if(IsFollowing) 
                {
                    // If the distance is small enough, initiate an attack **#
                    if(Vector3.Distance(this.gameObject.transform.position,target.position) < 2.5f) 
                    {
                        // Perform the sequnce, delays, calculations, etc associated with melee attack
                        PerformAttackEvent();
                        AnimationEvents.PlayerAttackAnimation(rnd, true);
                    }
                    else
                    {
                        AnimationEvents.PlayerAttackAnimation(rnd, false);
                        MovementEvents.CharacterFollow(target);
                    }
                }

                
                if (Input.GetMouseButtonDown(0)) {
                    if(!UserInterfaceLock.IsLocked)
                    {
                        PlayerMoveOrder();
                    }
                    
                }
            }
        }
        else
        {
            Debug.Log("You Are Dead");
        }


    } // END Update


    public void PlayerMoveOrder()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out hit, 100)) 
        {
            if(hit.transform.tag == "attackable")
            {
                Debug.Log("attack follow");
                target = hit.transform;
                TargetStatistics = target.GetComponent<CharacterStatistics>();
                TargetAnimationEvents = target.GetComponent<CharacterAnimation>();
                IsFollowing = true;
                Agent.stoppingDistance = ACTION_STOP_DIST;
            }
            else 
            {
                AnimationEvents.PlayerAttackAnimation(rnd, false);
                IsFollowing = false;
                MovementEvents.CharacterMove(hit, Cam);
                Agent.stoppingDistance = NORMAL_STOP_DIST;
            }
            

        }
    }
}
