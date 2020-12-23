using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiftyFifty : Ability
{
    public FiftyFifty()
    {
        AltRange = false;
        Name = "Fifty Fifty";
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

        int rnd = Random.Range(1, 3);

        int damage = (int)(attack * 10);
        
        if (rnd == 1)
        {
            if (target != null && target.IsAlive)
            {
                GetComponent<Bears>().DealDamage(damage, target);
            }
        }
        else if (rnd == 2)
        {
            GetComponent<Bears>().Hp = 0;
        }
        
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 10);
        return "50% to deal " + damage.ToString() + " damage" + "\n50% chance of killing yourself ";
    }
}
