using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energize : Ability
{
    public Energize()
    {
        AltRange = false;
        Name = "Energize";
        CastRange = 3;
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
        target.Special = true;
    }

    public override string GetAbilityDesc(int damage)
    {

        return "Let an ally use there special again";
    }
}
