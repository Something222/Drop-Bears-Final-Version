using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability:MonoBehaviour
{
  
    private string name;
   private int castRange;
   private int aoe;
    private bool alt;
    private float damageMod;
    private bool altRange;
    //alt is used to determine what audio clips to use basically derek in the constructors of your abilities set alt to true
    #region variablenotes
    //cast range is how the range of the skill 
    //aoe is used if its an aoe skill
    //Altrange is a bool that will be checked to determine to use a custom function to display the attacked blocks 
    //basically cause the shockwave skill does damage down a linear path and i added it last second 
    // i needed a cheap dirty way to display its range without completly reprogramming a bunch of shit
    #endregion variablenotes

    #region MethodNotes
    //CastAbility is where you program what the actual ability does it take the the tile you cast it on
    //(Usually use the getcomponentinchildren<Bears>() with that) and the attack value coming from the bear casting it
    //so you can scale the abilities based on the bears attack strength

    //GetAbilityDesc write down the abilities flabor text here return it as a string

    //AltAttackRange if this is a attack that attack in a weird way program the tiles it attack here

    #endregion MethodNotes
        //Also after you make the ability assign it in the currentabilities script 
    public string Name { get => name; set => name = value; }
    public int CastRange { get => castRange; set => castRange = value; }
    public int Aoe { get => aoe; set => aoe = value; }
    public bool Alt { get => alt; set => alt = value; }
    public float DamageMod { get => damageMod; set => damageMod = value; }
    public bool AltRange { get => altRange; set => altRange = value; }

    public abstract void CastAbility(Tile tileToCastOn,int attack);

    public abstract string GetAbilityDesc(int damage);

    public abstract void AltAttackRange(TileSelector tileselector,Tile tileToCastOn);


    protected void AoeAttack(Tile tile, Bears currentBear, int damage)
    {
        #region ZachNotes
        //this will treat all tiles the same
        //this is usefull for attacks that disregard range
        #endregion ZachNotes
        if (aoe == 0)
        {
            return;
        }
        if (Tutorial.instance == null || !Tutorial.instance.LockAttack)
        {
            for (int i = (int)tile.Loc.x - Aoe; i <= (int)tile.Loc.x + Aoe; i++)
            {
                int cal1 = Mathf.Abs(Aoe - i);
                int diff = Mathf.Abs(cal1 - Aoe);
                for (int k = (int)tile.Loc.y - diff; k <= (int)tile.Loc.y + diff; k++)
                {
                    GameObject thistile = TileManager.instance.GetTileDic(new Vector2(i, k));
                    if (thistile != null)
                    {
                        Bears[] targets = thistile.GetComponentsInChildren<Bears>();
                        Bears target = null;
                        foreach (Bears enemy in targets)
                        {
                            if (enemy.enabled)
                                target = enemy;
                        }

                        currentBear.DealDamage(damage, target);
                    }
                }
            }
        }
    }
}
