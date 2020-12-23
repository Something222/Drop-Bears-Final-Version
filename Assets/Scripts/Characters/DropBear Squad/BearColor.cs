using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BearColor:MonoBehaviour
{
    private int hp;
    private int totalHp;
    private int defense;
    private int attackStrength;
    private int movement;
    private int attackRange;
    private Color bearRace;
    private int countDown;
    private Ability firstAbility;
    private Ability special;
    
    public int Hp { get => hp; set => hp = value; }
    public int TotalHp { get => totalHp; set => totalHp = value; }
    public int Defense { get => defense; set => defense = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public int Movement { get => movement; set => movement = value; }
    public int AttackRange { get => attackRange; set => attackRange = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }
    public int Countdown { get => countDown; set => countDown = value; }
    public Ability FirstAbility { get => firstAbility; set => firstAbility = value; }
    public Ability Special { get => special; set => special = value; }

    public abstract string GetAttackName();
    public abstract string GetAttackDesc(int attack);

}
