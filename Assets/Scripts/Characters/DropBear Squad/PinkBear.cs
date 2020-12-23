using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinkBear : BearColor
{
    #region UnitNotes
    //Support Ranged Unit (Healer)
    //High HP, High Range
    //Average Movement, Average Attack

    //Support Ability
    //Individual Heal (Itself or Teammates)

    //Special Abilities
    //Revive (Can Revive One Teammate per Battle)
    #endregion UnitNotes

    #region BearFields
    public PinkBear()
    {

        Hp = 100;
        TotalHp = 100;
        Defense = 5;
        AttackStrength = 30;
        Movement = 3;
        AttackRange = 1;
        BearRace = Color.magenta;
        Countdown = 2;
        FirstAbility = new Heal();
        Special = new Resurrect();
    }
    #endregion BearFields


    public override string GetAttackName()
    {
        return "Love Beam";
    }
    public override string GetAttackDesc(int attack)
    {
        return "OWWW Love Hurts: \nDamage = " + attack.ToString();
    }
 
}
