using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerEnemy : BearColor
{
    // Start is called before the first frame update


    #region Stats
    public HealerEnemy()
    {
        Hp = 70;
        TotalHp = 70;
        Defense = 6;
        AttackStrength = 10;
        Movement = 4;
        AttackRange = 2;
        BearRace = Color.white;
        Countdown = 2;
        FirstAbility = new Heal();
    }

    public override string GetAttackDesc(int attack)
    {
        throw new System.NotImplementedException();
    }

    public override string GetAttackName()
    {
        throw new System.NotImplementedException();
    }


    #endregion Stats

}
