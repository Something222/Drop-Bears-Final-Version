using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomBreath : Ability
{
    public VenomBreath()
    {
        AltRange = false;
        Name = "Venom Breath";
        CastRange = 2;
        Aoe = 0;
        Alt = true;
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

        if (target.IsAlive && target != null)
        {
            GetComponent<Bears>().DealDamage(attack, target);
            target.themBuffs["venom"] = 3;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Spit some venom:" +"\nDeals " + damage.ToString() + " damage" + "\nTarget is poisoned for 3 turns";
    }
}
