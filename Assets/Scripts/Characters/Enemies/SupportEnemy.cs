using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemy :BearColor
{
    // Start is called before the first frame update
    public SupportEnemy()
    {
        Hp = 90;
        TotalHp = 90;
        Defense = 5;
        AttackStrength = 20;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
