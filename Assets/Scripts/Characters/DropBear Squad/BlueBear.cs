using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBear : BearColor
{
    #region UnitNotes
    //Speed Unit
    //High Movement Ability, High Attack
    //Average HP, Average Defence

    //Support Abilities
    //Increases Movement Ability (Teammates)

    //Special Ability
    //High Damaged Ranged Attack
    #endregion UnitNotes

    #region BearFields
    public BlueBear()
    {
        Hp = 100;
        TotalHp = 100;
        Defense = 7;
        AttackStrength = 40;
        Movement = 5;
        AttackRange = 1;
        BearRace = Color.blue;
        Countdown = 2;
        FirstAbility = new GottaGoQuick();
        Special = new PowerStrike();
    }
    #endregion BearFields

    public override string GetAttackName()
    {
        return "Blue Balls";
    }
    public override string GetAttackDesc(int attack)
    {
        return "Give em the balls:\nDamage = "+attack.ToString();
    }
   
}
