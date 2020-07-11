using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class CharacterAnimation : MonoBehaviour
{
    private Animator Anim { get; set; }

    private CharacterAudio Audio { get; set; }
    
    void Awake()
    {
        Anim = gameObject.GetComponent<Animator>();
        Audio = gameObject.GetComponent<CharacterAudio>();
    }



    public void PlayerSwimAnimation()
    {
        if(Anim.GetBool(("isSwimming")))
        {
            Anim.SetBool("isSwimming",false);
        }
        else
        {
            Anim.SetBool("isSwimming",true);
        }
    }

    public void PlayerAttackAnimation(System.Random rnd, bool isAnimating)
    {
        int atk  = rnd.Next(0, 2);  // creates a number between 1 and 12

        if(isAnimating)
        {
            if(atk == 0)
            {
                Anim.SetBool("isAttacking_A",true);
                Anim.SetBool("isAttacking_B",false);
            }
            else
            {
                Anim.SetBool("isAttacking_A",false);
                Anim.SetBool("isAttacking_B",true);
            }
        }
        else if( Anim.GetBool("isAttacking_A") || Anim.GetBool("isAttacking_B"))
        {
            Anim.SetBool("isAttacking_A",false);
            Anim.SetBool("isAttacking_B",false);
        }

    }

    public void PlayerTakeDamageAnimation(bool isAnimating)
    {
        if(isAnimating)
        {
            Anim.SetBool("isTakingDamage",true);
            Anim.SetBool("isBlocking",false);
        }
        else if(Anim.GetBool("isTakingDamage"))
        {
            Anim.SetBool("isTakingDamage",false);
            Anim.SetBool("isBlocking",false);
        }
        
    }

    public void PlayerBlockAnimation(bool isAnimating)
    {
        if(isAnimating)
        {
            Anim.SetBool("isBlocking",true);
            //Anim.SetBool("isBlocking",false);
        }
        else if(Anim.GetBool("isBlocking"))
        {
            Anim.SetBool("isBlocking",false);
        }
    }


    public void PlayerRunAnimation(NavMeshAgent agent)
    {
        if (agent.velocity != Vector3.zero)
        {
            if(Audio != null && !Audio.FootStepAudioIsPlaying())
            {
                Audio.FootStepAudio(true);
            }
            
            Anim.SetBool("isRunning",true);
        }
        else
        {
            if(Audio != null && Audio.FootStepAudioIsPlaying())
            {
                Audio.FootStepAudio(false);
            }
            
            Anim.SetBool("isRunning",false);
        }
    }


}
