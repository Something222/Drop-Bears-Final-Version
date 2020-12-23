using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPowerStrike :Ability
{
    public BossPowerStrike()
    {
        AltRange = false;
        Name = "BIG DAMAGE";
        CastRange = 1;
        Aoe = 1;
    }

  

    private void AttackSquare(Vector2 tile, TileManager tilemanager, int damage)
    {
        GameObject tileToBurn = tilemanager.GetTileDic(tile);
        if (tileToBurn != null&&tileToBurn.GetComponent<Tile>()!=GetComponentInParent<Tile>())
        {
            Bears[] targets = tileToBurn.GetComponentsInChildren<Bears>();
            Bears target = null;
            foreach (Bears enemy in targets)
            {
                if (enemy.enabled)
                    target = enemy;
            }
            if (target!=null)
            {
                if (target.Invincible == false)
                    GetComponent<Bears>().DealDamage(damage, target);
                else
                    GetComponent<Bears>().DealDamage(0, target);
            }
        }
        
    }
    public override string GetAbilityDesc(int damage)
    {
        int number = (int)(damage * 1.5);
        return "BIG HURTS " + number.ToString();
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        int damage = (int)(attack * (1.5));
        this.transform.LookAt(tileToCastOn.GetComponent<Transform>().position);
        TileManager tilemanager = TileManager.instance;
        if (tileToCastOn.GetComponentInChildren<Bears>() != null)
            tileToCastOn.GetComponentInChildren<Bears>().Hp -= damage;
        Vector2 attackCoordinate = new Vector2(tileToCastOn.X - 1, tileToCastOn.Y);
        AttackSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X + 1, tileToCastOn.Y);
        AttackSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X, tileToCastOn.Y + 1);
        AttackSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X, tileToCastOn.Y - 1);
        AttackSquare(attackCoordinate, tilemanager, damage);
        

    }

    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }
}
