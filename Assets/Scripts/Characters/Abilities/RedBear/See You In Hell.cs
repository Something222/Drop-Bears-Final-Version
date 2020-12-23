using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeYouInHell : Ability
{
    public SeeYouInHell()
    {
        AltRange = false;
        Name = "See You In Hell";
        CastRange = 1;
        Aoe = 0;
        Alt = true;
        DamageMod = 1f;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears[] targets = tileTooCastOn.GetComponentsInChildren<Bears>();
        Bears target = null;
        foreach(Bears enemy in targets)
        {
            if (enemy.enabled)
                target = enemy;
        }
        int damage = GetComponent<Bears>().Hp;
        if (target.IsAlive && target != null)
        {
            GetComponent<Bears>().DealDamage(damage, target);
            GetComponent<Bears>().Hp = 0;
        }
    }

    public override string GetAbilityDesc(int attack)
    {
     
        return "Go down with me:\n" +
            "Deals damage equal to \n" +
            "red bear's remaining hp\n";
    }
}
