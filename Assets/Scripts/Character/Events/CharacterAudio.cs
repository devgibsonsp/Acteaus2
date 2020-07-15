using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class CharacterAudio : MonoBehaviour
{
    private AudioSource PlayerAudio { get; set; }
    
    void Awake()
    {
        PlayerAudio = transform.GetChild(1).GetComponent<AudioSource>();

    }

    public void FootStepAudio(bool isPlaying)
    {
        if(isPlaying)
        {
            PlayerAudio.Play();
        }
        else
        {
            PlayerAudio.Stop();
        }
        
    }

    public bool FootStepAudioIsPlaying()
    {
        return PlayerAudio.isPlaying;
    }




}
