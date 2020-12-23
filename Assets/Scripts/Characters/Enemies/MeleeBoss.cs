using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoss :BearColor
{
    public MeleeBoss()
    {
        Hp = 240;
        TotalHp = 240;
        Defense = 11;
        AttackStrength = 40;
        Movement = 4;
        AttackRange = 1;
        BearRace = Color.white;
        Countdown = 2;
        FirstAbility = new BossPowerStrike();
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
