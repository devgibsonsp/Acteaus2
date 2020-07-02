using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterAttack : MonoBehaviour
{

    public bool CalculateTargetDodge(System.Random rnd, CharacterStatistics targetStatistics, CharacterStatistics playerStatistics, Transform target)
    {
        int dodgeCheck  = rnd.Next(0, targetStatistics.Player.ModifierStats._DodgeChanceMax + (playerStatistics.Player.ModifierStats.CounterChance/2));

        // Player dodge chance higher than dodge calc, sucessful dodge
        if(targetStatistics.Player.ModifierStats.DodgeChance < dodgeCheck || !IsInFrontOf(target))
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public bool CalculateTargetBlock(System.Random rnd, CharacterStatistics targetStatistics, CharacterStatistics playerStatistics, Transform target)
    {
        int blockCheck  = rnd.Next(0, targetStatistics.Player.ModifierStats._BlockChanceMax + (playerStatistics.Player.ModifierStats.CounterChance/2));

        // block chance higher than block calc, sucessful block
        if(targetStatistics.Player.ModifierStats.BlockChance < blockCheck || !IsInFrontOf(target))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DealDamageToTarget(int damage, CharacterStatistics targetStatistics)
    {
        targetStatistics.Player.BarStats.Health -= damage;
    }


    /// <summary>Determines if player is in front or behind target</summary>
    /// <summary>If the player is not in front, then no block/dodge</summary>
    private bool IsInFrontOf(Transform target)
    {
        Vector3 toTarget = (this.transform.position - target.position).normalized;
        if (Vector3.Dot(toTarget, target.forward) > 0) {
            return true;
        } else {
            return false;
        }
    }

    public int CalculateDamage(System.Random rnd, CharacterStatistics targetStatistics, CharacterStatistics playerStatistics)
    {
        playerStatistics.Player.ModifierStats.BaseMeleeDamage = 5;
        int armor = 0;
        if(targetStatistics.Player.ModifierStats.Armor > 0)
        {
            armor = rnd.Next(
                targetStatistics.Player.ModifierStats.Armor/2, 
                targetStatistics.Player.ModifierStats.Armor
            );
        }


        int damage = playerStatistics.Player.ModifierStats.BaseMeleeDamage - armor + 1;

        return damage;
    }



}
