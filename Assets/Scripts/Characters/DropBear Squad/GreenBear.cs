using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenBear : BearColor
{
    #region UnitNotes
    //Deffensive Unit
    //High Defence, High HP,
    //Average Attack
    //Low Movement, Low Range 

    //Support Ability 
    //Increases Defence (Itself Or Teammates)

    //Special Ability
    //Nullifies Damage for 1 Turn
    #endregion UnitNotes

    public GreenBear()
    {
        Hp = 150;
        TotalHp = 150;
        Defense = 10;
        AttackStrength = 45;
        Movement = 3;
        AttackRange = 1;
        BearRace = Color.green;
        Countdown = 2;
        FirstAbility =new HunkerDown();
        Special = new Juggernaut();
    }

    public override string GetAttackName()
    {
        return "Skull Smash";
    }
    public override string GetAttackDesc(int attack)
    {
        return "I HATE SKULLS\nDamage = " + attack.ToString();
    }
  
}
