using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GottaGoQuick : Ability
{
 public GottaGoQuick()
    {
        AltRange = false;
        Name = "Gotta Go Quick";
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
        target.Movement += 2;
        target.themBuffs["buffMovement"] = 2;
        
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Give an ally +2\nMovement (One Turn)";
    }
}
