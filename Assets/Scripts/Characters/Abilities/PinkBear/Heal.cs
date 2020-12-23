using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{

    public Heal()
    {
        AltRange = false;
        Name = "Heal";
        CastRange = 3;
        Aoe = 0;
        Alt = false;
        DamageMod = 1.334f;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears[] targets = tileTooCastOn.GetComponentsInChildren<Bears>();
        Bears target = null;
        foreach (Bears enemy in targets)
        {
            if (enemy.enabled)
                target = enemy;
        }
        int healamount = (int)(attack * DamageMod);
        //Heals Target
        if (target.IsAlive)
        {
            target.Hp += healamount;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        int number = (int)(damage * DamageMod);
        return "Well it heals: \nAlly/Own HP+ " + number.ToString();
    }
}
