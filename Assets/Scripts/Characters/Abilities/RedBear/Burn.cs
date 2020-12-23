using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Ability
{
    public Burn()
    {
        AltRange = false;
        Name = "Burn";
        CastRange = 3;
        Aoe = 0;
        Alt = true;
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileTooCastOn, int attack)
    {
        Bears target = tileTooCastOn.GetComponentInChildren<Bears>();
               
        if (target.IsAlive && target != null)
        {
            GetComponent<Bears>().DealDamage(attack, target);
            target.themBuffs["burn"] = 3;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Deals " + damage.ToString() + " damage"+ "\nInflicts burn for 3 turns";
    }
}
