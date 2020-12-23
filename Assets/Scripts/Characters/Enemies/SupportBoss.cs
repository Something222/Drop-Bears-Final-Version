using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBoss : BearColor
{
    public SupportBoss()
    {
        Hp = 175;
        TotalHp = 175;
        Defense = 10;
        AttackStrength = 25;
        Movement = 3;
        AttackRange = 3;
        BearRace = Color.white;
        Countdown = 2;
      
    }

    public override string GetAttackDesc(int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAttackName()
    {
        throw new System.NotImplementedException();
    }




}
