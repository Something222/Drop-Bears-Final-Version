using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFire : Ability
{
    
    public BodyFire()
    {
        AltRange = false;
        Name = "Body Fire";
        CastRange = 0;
        Aoe = 1;
        Alt = false;
    }
    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        int damage= (int)(attack * (1.67));
        TileManager tilemanager = TileManager.instance;
        Bears currentBear = GetComponent<Bears>();
        if(tileToCastOn.GetComponentInChildren<Bears>()!=null)
            tileToCastOn.GetComponentInChildren<Bears>().Hp -= 20;
        //AoeAttack(tileToCastOn, currentBear, damage);
        //tileToCastOn.GetComponentInChildren<Bears>().Hp += 45;
        Vector2 attackCoordinate = new Vector2(tileToCastOn.X - 1, tileToCastOn.Y);
        BurnSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X + 1, tileToCastOn.Y);
        BurnSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X, tileToCastOn.Y + 1);
        BurnSquare(attackCoordinate, tilemanager, damage);
        attackCoordinate = new Vector2(tileToCastOn.X, tileToCastOn.Y - 1);
        BurnSquare(attackCoordinate, tilemanager, damage);

    }
    private void BurnSquare(Vector2 tile,TileManager tilemanager,int damage)
    {
        GameObject tileToBurn = tilemanager.GetTileDic(tile);
        if (tileToBurn != null)
        {
            Bears[] targets = tileToBurn.GetComponentsInChildren<Bears>();
            Bears target = null;
            foreach (Bears enemy in targets)
            {
                if (enemy.enabled)
                    target = enemy;
            }
            if (target)
            {
                if(target!=null)
                GetComponent<Bears>().DealDamage(damage, target);
            }
        }
    }
    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * 1.67);
        return "DAMNIT IT BURNS:\n" +
            "Hit all adjacent targets\n" +
            "Self damage = 20\n" +
            "Damage = " + damage.ToString();
    }

    public override void AltAttackRange(TileSelector tileselector,Tile tiletocaston)
    {
        throw new System.NotImplementedException();
    }
}
