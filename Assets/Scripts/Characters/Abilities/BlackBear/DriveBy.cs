using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveBy :Ability
{
   public DriveBy()
    {
        AltRange = false;
        Name = "Drive By";
        CastRange = 4;
        Aoe = 0;
        Alt = false;
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
        int damage;
        if (!target.Invincible)
            damage = (int)(attack * 2);
        else
            damage = 0;
        GetComponent<Bears>().DealDamage(damage, target);
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 2);
        return "Load Up A SPECIAL bullet:\nDamage = " + damage;
    }
}
