using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//So if we wanted a bunch of skills i would make the not equipped abilities arrays and create a swap function for the 
//ability that is equipped to the not equipped ablities
public class CurrentAbilities:MonoBehaviour
{
    public static Ability basicAbility;
    public static Ability specialAbility;
    public static Ability notEquippedBasicAbility;
    public static Ability notEquippedSpecialAbility;
  

    public virtual void SwapBasicAbility()
    {
        Ability temp = basicAbility;
        basicAbility = notEquippedBasicAbility;
        notEquippedBasicAbility = temp;
    }
    public virtual void SwapSpecialAbility()
    {
        Ability temp = specialAbility;
        specialAbility = notEquippedSpecialAbility;
        notEquippedSpecialAbility = temp;
    }

}

public class BlueBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public static new Ability notEquippedBasicAbility;
    public static new Ability notEquippedSpecialAbility;
    public static  bool started;
    public BlueBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new GottaGoQuick();
            specialAbility = new PowerStrike();
            notEquippedBasicAbility = new SnortAPixieStick();
            notEquippedSpecialAbility = new ShockWave();

            started = true;
        }
    }
    public override void SwapSpecialAbility()
    {
        Ability temp = specialAbility;
        specialAbility = notEquippedSpecialAbility;
        notEquippedSpecialAbility = temp;
    }
    public override void SwapBasicAbility()
    {
        Ability temp = basicAbility;
        basicAbility = notEquippedBasicAbility;
        notEquippedBasicAbility = temp;
    }
}
public class BlackBearAbilities :CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public static new Ability notEquippedBasicAbility;
    public static new Ability notEquippedSpecialAbility;
    public static bool started;
    public BlackBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new BeefUp();
            specialAbility = new DriveBy();
            notEquippedBasicAbility = new VenomBreath();
            notEquippedSpecialAbility = new FiftyFifty();


            started = true;
        }
    }
    public override void SwapSpecialAbility()
    {
        Ability temp = specialAbility;
        specialAbility = notEquippedSpecialAbility;
        notEquippedSpecialAbility = temp;
    }
    public override void SwapBasicAbility()
    {
        Ability temp = basicAbility;
        basicAbility = notEquippedBasicAbility;
        notEquippedBasicAbility = temp;
    }
}
public class PinkBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public static new Ability notEquippedBasicAbility;
    public static new Ability notEquippedSpecialAbility;
    public static bool started;
    public PinkBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new Heal();
            specialAbility = new Resurrect();
            notEquippedBasicAbility = new SweetKisses();
            notEquippedSpecialAbility = new FriendlyHug();
            started = true;
        }
    }
    public override void SwapSpecialAbility()
    {
        Ability temp = specialAbility;
        specialAbility = notEquippedSpecialAbility;
        notEquippedSpecialAbility = temp;
    }
    public override void SwapBasicAbility()
    {
        Ability temp = basicAbility;
        basicAbility = notEquippedBasicAbility;
        notEquippedBasicAbility = temp;
    }
}
public class GreenBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public static new Ability notEquippedBasicAbility;
    public static new Ability notEquippedSpecialAbility;
    public static bool started;
    
    public GreenBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new HunkerDown();
            specialAbility = new Juggernaut();
            notEquippedBasicAbility = new YourSlow();
            notEquippedSpecialAbility = new IMadeAnOuchie();

            started = true;
        }
    }
    public override void SwapBasicAbility()
    {
        Ability temp = basicAbility;
        basicAbility = notEquippedBasicAbility;
        notEquippedBasicAbility = temp;
    }
    public override void SwapSpecialAbility()
    {
        Ability temp = specialAbility;
        specialAbility = notEquippedSpecialAbility;
        notEquippedSpecialAbility = temp;
    }
}
public class RedBearAbilities:CurrentAbilities
{
    public static new Ability basicAbility;
    public static new Ability specialAbility;
    public static new Ability notEquippedBasicAbility;
    public static new Ability notEquippedSpecialAbility;
    public static bool started;


    public RedBearAbilities(bool reinstate)
    {
        if (reinstate)
        {
            basicAbility = new BodyFire();
            specialAbility = new Energize();
            //testing reasons
            notEquippedBasicAbility = new Burn();
            notEquippedSpecialAbility = new SeeYouInHell();
            started = true;
        }
    }
  public override void SwapBasicAbility()
    {
        Ability temp = basicAbility;
        basicAbility = notEquippedBasicAbility;
        notEquippedBasicAbility = temp;
    }
    public override void SwapSpecialAbility()
    {
        Ability temp = specialAbility;
        specialAbility = notEquippedSpecialAbility;
        notEquippedSpecialAbility = temp;
    }
}


