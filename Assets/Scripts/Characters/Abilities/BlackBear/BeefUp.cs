using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefUp : Ability
{
    public BeefUp()
    {
        Name = "Beef Up";
        AltRange = false;
        CastRange = 3;
        Aoe = 0;
        Alt = false;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {

        Bears[] targets = tileToCastOn.GetComponentsInChildren<Bears>();
        Bears target = null;
        foreach (Bears enemy in targets)
        {
            if (enemy.enabled)
                target = enemy;
        }
        target.AttackStrength = (int)(1.5 * target.AttackStrength);
        target.themBuffs["buffAttack"] = 2;
       
    }

   public override string GetAbilityDesc(int damage)
    {
        return "Eat Your Beans: \nAttack Up x1.5 (Two Turns)";
    }
}
