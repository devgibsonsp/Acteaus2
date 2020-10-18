using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class CharacterLighting : MonoBehaviour
{
    private GameObject OutdoorLighting { get; set;}

    private DayNightCycle Lighting { get; set; }

    private bool IsInDungeon { get; set; } = false;

    void Start()
    {
        OutdoorLighting = GameObject.Find("_lighting");
        Lighting = OutdoorLighting.GetComponent<DayNightCycle>();
    }

    void Update()
    {
        if(this.gameObject.transform.position.x > 2100f)
        {
            Lighting.DungeonLightToggle(true);
            //OutdoorLighting.SetActive(false);
            IsInDungeon = true;
        }
        else if(IsInDungeon)
        {
            Lighting.DungeonLightToggle(false);
            //OutdoorLighting.SetActive(true);
            IsInDungeon = false;
        }
    }
}
