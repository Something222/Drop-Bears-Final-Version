using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMadeAnOuchie : Ability
{
    public IMadeAnOuchie()
    {
        AltRange = false;
        Name = "I Made An Ouchie";
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
        int damage = (int)(attack * 1.75); 
        if (target.IsAlive && target != null)
        {
            GetComponent<Bears>().DealDamage(damage, target);
            
        }
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 1.75);
        return "Deals " + damage.ToString() + " damage";
    }
}
