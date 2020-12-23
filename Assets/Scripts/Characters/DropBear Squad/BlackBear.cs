using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackBear : BearColor
{
    #region UnitNotes
    //Offensive Melee Unit 
    //High Attack
    //Average Movement, Average Defence, Average HP, 

    //Support Ability
    //Increases Melee Damage (Itself or Teammates)

    //Special Ability
    //High Damage Melee Attack
    #endregion UnitNotes
  
    public BlackBear()
    {
        Hp = 100;
        TotalHp = 100;
        Defense = 7;
        AttackStrength = 40;
        Movement = 2;
        AttackRange = 3;
        BearRace = Color.black;
        Countdown = 2;
        FirstAbility = new BeefUp();
        Special = new DriveBy();
    }


    public override string GetAttackName()
    {
        return "Black Lightning";
    }
    public override string GetAttackDesc(int attack)
    {
        return "As swift as they come: \nDamage = " + attack.ToString();
    }
  
}
