using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggernaut :Ability
{
  public Juggernaut()
    {
        AltRange = false;
        Name = "Juggernaut";
        CastRange = 2;
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
        target.Invincible = true;
         target.themBuffs["invincible"] = 2;
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Im the juggernaut bitch\nBecome Immune (one turn) ";
    }
}
