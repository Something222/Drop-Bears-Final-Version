using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrect : Ability
{

    public Resurrect()
    {
        AltRange = false;
        Name = "Resurrect";
        CastRange = 3;
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
        if (!target.IsAlive)
        {
            target.Hp = target.TotalHP;
            target.onlyOnce = true;
            //target.transform.Rotate(-90f, 0f, 0f);
            SquadSelection.instance.PlayersAlive++;
            target.Anim.SetBool("isDead", false);
        }
        else
        {
            target.Hp = target.TotalHP;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Fully Heal \nor revive ally";
    }
}
