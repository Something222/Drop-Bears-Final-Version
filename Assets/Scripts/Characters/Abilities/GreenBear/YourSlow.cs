using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourSlow : Ability
{
    public YourSlow()
    {
        AltRange = false;
        Name = "Your Slow";
        CastRange = 3;
        Aoe = 0;
        Alt = true;
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

        if (target != null && target.IsAlive)
        {
            GetComponent<Bears>().DealDamage(attack, target);
            target.themBuffs["stun"] = 1;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Deals " + damage.ToString() + " damage" + "\nStuns the target for 1 turn"; ;
    }
}
