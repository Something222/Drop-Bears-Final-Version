using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowIceChunk :Ability
{
   
    public BossThrowIceChunk()
    {
        AltRange = false;
        Name = "Stuns and does damage";
        CastRange = 3;
        Aoe = 1;
        DamageMod = 1.25f;
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
        this.transform.LookAt(tileToCastOn.GetComponent<Transform>().position);

        int damage = (int)(attack * DamageMod);
        if (target != null&&target.IsAlive)
        {
            GetComponent<Bears>().DealDamage(damage, target);
            target.themBuffs["stun"] = 1;
                }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Throws big ice you no move 1 turn";
    }

  

}
