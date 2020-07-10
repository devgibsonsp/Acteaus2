using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectData.CharacterData;
using ObjectData.CharacterData.Models;
using ObjectData.CharacterData.Types;
using ObjectData.CharacterData.Utilties;
using UI;
using Photon.Pun;

public class CharacterStatistics : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Character Player { get; set; }

    // Start is called before the first frame update
    void Awake()
    { 
        //Player = new Character("Steve", 0, 1, Generate.BarStats(), Generate.CoreStats(), Generate.ModifierStats());
        Player = new Character(RaceType.Human);
        Player.SetClass(CharacterType.Warrior);
        Player.Name = "Steve";
        while(Player.SkillPoints > 0) {
            Player.DistributeSkillPoint(CoreStatType.Strength);
        }


        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
