using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : Ability
{
  public ShockWave()
    {
        AltRange = true;
        Name = "ShockWave";
        CastRange = 1;
        Aoe = 0;
        Alt = true;
        DamageMod = 1.5f;
    }

public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
       if(GetComponentInParent<Tile>()!=null)
        {
            Tile nextTile=tileToCastOn;
            Tile parentTile = GetComponentInParent<Tile>();
            int xAxis = parentTile.X - tileToCastOn.X;
            int yAxis = parentTile.Y - tileToCastOn.Y;
            if(xAxis<0)
            {
                while (nextTile != null)
                {
                    if (TileManager.instance.GetTileDic(new Vector2(nextTile.X + 1, nextTile.Y)))
                    {
                        nextTile = TileManager.instance.GetTileDic(new Vector2(nextTile.X + 1, nextTile.Y)).GetComponent<Tile>();
                        nextTile.Attackvalue = 1;
                    }
                    else
                        nextTile = null; 
                        }
            }
           else if (xAxis > 0)
            {
                while (nextTile != null)
                {
                    if (TileManager.instance.GetTileDic(new Vector2(nextTile.X - 1, nextTile.Y)))
                    {
                        nextTile = TileManager.instance.GetTileDic(new Vector2(nextTile.X - 1, nextTile.Y)).GetComponent<Tile>();
                        nextTile.Attackvalue = 1;
                    }
                    else
                        nextTile = null;
                }
            }
            if(yAxis<0)
            {
                while (nextTile != null)
                {
                    if (TileManager.instance.GetTileDic(new Vector2(nextTile.X, nextTile.Y + 1)))
                    {
                        nextTile = TileManager.instance.GetTileDic(new Vector2(nextTile.X, nextTile.Y + 1)).GetComponent<Tile>();
                        nextTile.Attackvalue = 1;
                    }
                    else
                        nextTile = null;
                }
            }
          else  if (yAxis > 0)
            {
                while (nextTile != null)
                {
                    if (TileManager.instance.GetTileDic(new Vector2(nextTile.X, nextTile.Y - 1)))
                    {
                        nextTile = TileManager.instance.GetTileDic(new Vector2(nextTile.X, nextTile.Y - 1)).GetComponent<Tile>();
                        nextTile.Attackvalue = 1;
                    }
                    else
                        nextTile = null;
                }
            }

        }
    }
    
    public void DamageNextTile(Vector2 coordinates,Tile nextTile,Bears HostBear,int damage,int situation)
    {
        while (nextTile != null)
        {
           
                switch (situation)
                {
                    case 0:
                        coordinates = new Vector2(coordinates.x + 1, coordinates.y);
                        break;
                    case 1:
                        coordinates = new Vector2(coordinates.x - 1, coordinates.y);
                        break;
                    case 2:
                        coordinates = new Vector2(coordinates.x, coordinates.y + 1);
                        break;
                    case 3:
                        coordinates = new Vector2(coordinates.x, coordinates.y - 1);
                        break;
                }
            if (TileManager.instance.GetTileDic(coordinates))
            {
                nextTile = TileManager.instance.GetTileDic(coordinates).GetComponent<Tile>();
                if (nextTile.GetComponentInChildren<Bears>())
                {
                    Bears[] targets = nextTile.GetComponentsInChildren<Bears>();
                    Bears target = null;
                    foreach (Bears enemy in targets)
                    {
                        if (enemy.enabled)
                            target = enemy;
                    }
                    HostBear.DealDamage(damage, target);

                }
               
            }
            else
                nextTile = null;
        }
    }
    public override void CastAbility(Tile tileToCastOn, int attack)
    {
        int damage = (int)(attack * DamageMod);
        if (GetComponentInParent<Tile>() != null)
        {
            Bears HostBear = GetComponent<Bears>();
            Tile nextTile = tileToCastOn;
            Tile parentTile = GetComponentInParent<Tile>();
            Bears[] targets = tileToCastOn.GetComponentsInChildren<Bears>();
            Bears target = null;
            foreach (Bears enemy in targets)
            {
                if (enemy.enabled)
                    target = enemy;
            }
            HostBear.DealDamage(damage, target);
            int xAxis = parentTile.X - tileToCastOn.X;
            int yAxis = parentTile.Y - tileToCastOn.Y;
            if (xAxis < 0)
            {
                
                    DamageNextTile(new Vector2(nextTile.X, nextTile.Y), nextTile, HostBear, damage, 0);
                
            }
            else if (xAxis > 0)
            {
               DamageNextTile(new Vector2(nextTile.X, nextTile.Y), nextTile, HostBear, damage,1);
          
            }
            if (yAxis < 0)
            {
                DamageNextTile(new Vector2(nextTile.X, nextTile.Y), nextTile, HostBear, damage, 2);

            }
            else if (yAxis > 0)
            {
                    DamageNextTile(new Vector2(nextTile.X, nextTile.Y), nextTile, HostBear, damage, 3);

            }
        }

        
    }

    public override string GetAbilityDesc(int attack)
    {
        int damage = (int)(attack * DamageMod);
        return "Deal " + damage + " To all enemies in a straight line";
    }
}
