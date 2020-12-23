using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBloodBite :Ability
{
    public BossBloodBite()
    {
        AltRange = false;
        Name = "Blood Bite";
        CastRange = 1;
        Aoe = 1;
    }




    public override string GetAbilityDesc(int damage)
    {
        int number = (int)(damage * 1.75);
        return "BLOOD " + number.ToString();
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        int damage = (int)(attack * (1.75));
        this.transform.LookAt(tileToCastOn.GetComponent<Transform>().position);

        Bears hostbear = GetComponent<Bears>();
        hostbear.Hp += damage;
        if(hostbear.Hp>hostbear.TotalHP)
        {
            hostbear.Hp = hostbear.TotalHP;
        }
        Bears[] targets = tileToCastOn.GetComponentsInChildren<Bears>();
        Bears target = null;
        foreach (Bears enemy in targets)
        {
            if (enemy.enabled)
                target = enemy;
        }

        if (!target.Invincible)
        {
            hostbear.DealDamage(damage, target);
        }
        else
            hostbear.DealDamage(0, target);
    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }
}
