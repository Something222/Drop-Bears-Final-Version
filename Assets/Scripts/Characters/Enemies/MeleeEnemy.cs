using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : BearColor
{
    #region UnitNotes
    //basic melee Enemy for game
    #endregion UnitNotes
    #region Stats
    public MeleeEnemy()
    {
        Hp = 80;
        TotalHp = 80;
        Defense = 5;
        AttackStrength = 20;
        Movement = 3;
        AttackRange = 1;
        BearRace = Color.white;
        Countdown = 2;
    }
    #endregion Stats


    // Start is called before the first frame update


    public override string GetAttackDesc(int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAttackName()
    {
        throw new System.NotImplementedException();
    }
}
