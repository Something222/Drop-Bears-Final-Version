using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyHug : Ability
{
    public FriendlyHug()
    {
        AltRange = false;
        Name = "Friendly Hug";
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
        int damage = (int)(attack*1.5);
        if (target != null && target.IsAlive)
        {
            GetComponent<Bears>().DealDamage(damage, target);
            Bears self=GetComponent<Bears>();
            self.Hp += damage;
            if (self.Hp>self.TotalHP)
            {
                self.Hp = self.TotalHP;
            }
        }
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 1.5);
        return "Deals " + damage.ToString() + " damage" + "\n and heals itself also"; 
    }
}
