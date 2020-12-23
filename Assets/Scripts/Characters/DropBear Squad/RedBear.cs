using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBear : BearColor
{
    #region UnitNotes
    //All-Rounder Unit
    //Average Everything

    //Support Ability
    //High Damage Attack but loses HP

    //Special Abilities   
    //Makes One teammates special Ability Avaliable Again
    #endregion UnitNotes
    #region BearFields
    public RedBear()
    {
        Hp = 100;
        TotalHp = 100;
        Defense = 7;
        AttackStrength = 35;
        Movement = 3;
        AttackRange = 2;
        BearRace = Color.red;
        Countdown = 2;
        FirstAbility = new BodyFire();
        Special = new Energize();
    }
    #endregion BearFields

    public override string GetAttackName()
    {
        return "Play With Matches";
    }
    public override string GetAttackDesc(int attack)
    {
        return "Start a small fire \n" +
            "on your enemies\n" +
            "Damage= " + attack.ToString();
    }

}