using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnortAPixieStick :Ability
{

   public SnortAPixieStick()
    {
        AltRange = false;
        Name = "Snort a Pixie Stick";
        CastRange = 1;
        Aoe = 0;
        Alt = true;
        DamageMod = 0;
    }
    public override void AltAttackRange(TileSelector tileselector, Tile tileToCastOn)
    {
        throw new System.NotImplementedException();
    }

    public override void CastAbility(Tile tileToCastOn, int attack)
    {

        Movement target = tileToCastOn.GetComponentInChildren<Movement>();
        if (target)
        {
            target.ResetMovement();
            target.GetComponent<Bears>().TurnComplete = false;
        }
    }

    public override string GetAbilityDesc(int damage)
    {
        return "Hookup a bro with some good stuff! Allows a bear to move again if they have already done so";
    }


   
}
