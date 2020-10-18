using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightCycle : MonoBehaviour
{
    private DateTime CurrentTime { get; set; } = DateTime.UtcNow;
    // Start is called before the first frame update
    
    private List<Light> Lights { get; set; } 

    private int CurrentMinute { get; set; }

    private float[] BrightnessArray { get; set; }

    private bool IsInDungeon { get; set; } = false;
    void Start()
    {

        // Dungeon lights are going to need to change for this to work properly

        Lights = new List<Light>();
        BrightnessArray = new float[] { 
                0f,
                0f,
                0f,
                .04f,
                .04f,
                .04f,
                .08f,
                .08f,
                .08f,
                .12f,
                .12f,
                .12f,
                .16f,
                .16f,
                .16f,
                .2f,
                .2f,
                .2f,
                .24f,
                .24f,
                .24f,
                .28f,
                .28f,
                .28f,
                .32f,
                .32f,
                .32f,
                .36f,
                .36f,
                .36f,
                .4f,
                .4f,
                .4f,
                .36f,
                .36f,
                .36f,
                .32f,
                .32f,
                .32f,
                .28f,
                .28f,
                .28f,
                .24f,
                .24f,
                .24f,
                .2f,
                .2f,
                .2f,
                .16f,
                .16f,
                .16f,
                .12f,
                .12f,
                .12f,
                .08f,
                .08f,
                .08f,
                .04f,
                .04f,
                .04f
            };

        Light[] l = this.gameObject.GetComponentsInChildren<Light>();

        for(int i = 0; i < l.Length; i++)
        {
            Lights.Add(l[i]);
        }
        SetBrightness();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = DateTime.UtcNow;
        if(CurrentTime.Minute != CurrentMinute)
        {
            CurrentMinute = CurrentTime.Minute;

            // If player is currently in a dungeon, don't change lighting
            if(!IsInDungeon)
            {
                SetBrightness();
            }



        }

        Debug.Log(CurrentTime.Minute);

        //0 to 30 dark to bright
        // 30 to 59 bright to dark
    }

    private void SetBrightness()
    {
        foreach(Light light in Lights)
        {
            light.intensity = BrightnessArray[CurrentMinute];
        }
    }

    public void DungeonLightToggle(bool toggle)
    {
        IsInDungeon = toggle;
        if(toggle)
        {
            foreach(Light light in Lights)
            {
                light.intensity = 0f;
            }
        }
        else
        {
            SetBrightness();
        }

    }

}

/*
        BrightnessArray = new float[] { 
            0f,
            .013f,
            .026f,
            .039f,
            .052f,
            .065f,
            .078f,
            .091f,
            .104f,
            .117f,
            .13f,
            .143f,
            .156f,
            .169f,
            .182f,
            .195f,
            .208f,
            .221f,
            .234f,
            .247f,
            .26f,
            .273f,
            .286f,
            .299f,
            .312f,
            .325f,
            .338f,
            .351f,
            .364f,
            .377f,
            .39f,
            .4f,
            .377f,
            .364f,
            .351f,
            .338f,
            .325f,
            .312f,
            .299f,
            .286f,
            .273f,
            .26f,
            .247f,
            .234f,
            .221f,
            .208f,
            .195f,
            .182f,
            .169f,
            .156f,
            .143f,
            .13f,
            .117f,
            .104f,
            .091f,
            .078f,
            .065f,
            .052f,
            .039f,
            .026f,
            .013f,
            */