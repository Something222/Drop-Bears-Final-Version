using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongMeleeEnemy : BearColor
{
    #region Stats
    public StrongMeleeEnemy()
    {
        Hp = 140;
        TotalHp = 140;
        Defense = 9;
        AttackStrength = 35;
        Movement = 4;
        AttackRange = 1;
        BearRace = Color.white;
        Countdown = 2;
    }
    #endregion Stats

    

    public override string GetAttackDesc(int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAttackName()
    {
        throw new System.NotImplementedException();
    }
}
