using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Shit to do neccesary
//LEVEL COMPLETE SYSTEM
//end turn only after enemy has attacked
//pause selection bug

public class Bears : MonoBehaviour
{
    //Stats
   [SerializeField] private BearColor bearJob;
    [Header("Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int totalHP;
    [SerializeField] private int defense;
    [SerializeField] private int attackStrength;
    [SerializeField] private int range;
    [SerializeField] private int movement;
    [SerializeField] private bool hasAttacked;
    [SerializeField] private Ability basicAbility;
    [SerializeField] private Ability specialAbility;
    private AudioSource audioS;
    private Color bearRace;
    [SerializeField] private bool turnComplete;
    [SerializeField] private int countDown;

    [SerializeField] private Image abilityIcon;
    [SerializeField] private Image SpecialIcon;
    //Sounds Clips
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private AudioClip[] ability1Sounds;
    [SerializeField] private AudioClip[] ability2Sounds;
    private BearSFX bearSFX;
    private Animator anim;
    private Canvas txtDamage;
    private Color iconcolor;
    [Space()]

    [SerializeField] private bool selected;

    //Needed for turn system
    [SerializeField] private bool isAlive = true;
    //If we do enemy soundclips I will have to add alot of enumtypes
    public enum EnumColour { Green, Black, Blue, Pink, Red, MeleeEnemy, StrongMeleeEnemy, RangedEnemy, MeleeBoss,SupportBoss,SupportEnemy};
    [SerializeField] private EnumColour color;
    //[Range(1, 6)] [Tooltip(" 1 = Green \n 2 = Black \n 3 = Blue \n 4 = Pink \n 5 = Red \n 6 = Melee Enemy")]
    //[SerializeField] private int colour;
    [SerializeField] private Bears Target;
    public int avatarNumber;
    public bool onlyOnce = true;

    public int counterSupport = 0;

    public Dictionary<string, int> themBuffs = new Dictionary<string, int>();

    public string[] buffNames;

    //Ablilities
    private bool support; //Can be activated every "n" turns;
    private bool special; //Only Happens once every Battle
   [SerializeField] private bool invincible;
    #region Properties
    public int Hp { get => hp; set => hp = value; }
    public int TotalHP { get => totalHP; set => totalHP = value; }
    public int AttackStrength { get => attackStrength; set => attackStrength = value; }
    public int Movement { get => movement; set => movement = value; }
    public int Defense { get => defense; set => defense = value; }
    public int Range { get => range; set => range = value; }
    public bool Special { get => special; set => special = value; }
    public bool Support { get => support; set => support = value; }
    public bool Selected { get => selected; set => selected = value; }
    public bool IsAlive { get => (hp > 0) ? true : false; set => isAlive = value; }
    public bool TurnComplete { get => turnComplete; set => turnComplete = value; }
    public Color BearRace { get => bearRace; set => bearRace = value; }
    public bool HasAttacked { get => hasAttacked; set => hasAttacked = value; }
    public bool Invincible { get => invincible; set => invincible = value; }

    public int CountDown { get => countDown; set => countDown = value; }
    public AudioClip[] MoveSounds { get => moveSounds; }
    public AudioSource AudioS { get => audioS; }
    public BearColor BearJob { get => bearJob; set => bearJob = value; }
    public Ability BasicAbility { get => basicAbility; set => basicAbility = value; }
    public Ability SpecialAbility { get => specialAbility; set => specialAbility = value; }
    public Animator Anim { get => anim; set => anim = value; }
    #endregion Properties
    public void DistributeStats()
    {
        this.isAlive = true;
        this.Hp = this.bearJob.Hp;
        this.attackStrength = this.bearJob.AttackStrength;
        this.totalHP = this.bearJob.TotalHp;
        this.defense = this.bearJob.Defense;
        this.Movement = this.bearJob.Movement;
        this.Range = this.bearJob.AttackRange;
        this.BearRace = this.bearJob.BearRace;
        this.CountDown = this.bearJob.Countdown;
        special = true;
    }
    public void Awake()
    {
        anim = GetComponent<Animator>();
    }
    /// <summary>
    /// populates the bears idle sounds hurt sounds and attack sounds arrays
    /// </summary>
    private void GetSoundFX()
    {
        string type = color.ToString() + "IdleClips";
        idleSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "AttackClips";
        attackSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "MoveClips";
        moveSounds = GetSpecificSoundClips(type);
        type = color.ToString() + "HurtClips";
        hurtSounds = GetSpecificSoundClips(type);
        if (basicAbility != null)
        {
            if (!BasicAbility.Alt)
                type = color.ToString() + "Ability1Clips";
            else
                type = color.ToString() + "Ability1AltClips";
        }
        ability1Sounds = GetSpecificSoundClips(type);
        if (SpecialAbility != null)
        {
            if (!SpecialAbility.Alt)
                type = color.ToString() + "Ability2Clips";
            else
                type = color.ToString() + "Ability2AltClips";
        }
        ability2Sounds = GetSpecificSoundClips(type);

    }
    /// <summary>
    /// Give it the name of the audioclip[] you want from the BearSfx and it will return the array
    /// </summary>
    /// <param name="typeofclip"></param>
    /// <returns></returns>
    AudioClip[] GetSpecificSoundClips(string typeofclip)
    {
        #region ZachWin
        //K so im super proud of this line of code it looks complicated but basically 
        //it allows the script to dynamically go into the sound script and populate the 
        //bears voice clips lines in like very little lines of code
        //as long as the string will match the name of the variable in the bearsfx script
        //as long as the audio clips exist in the audio manager
        #endregion ZachWin
        if (bearSFX)
        {
            if (bearSFX.GetType().GetProperty(typeofclip) != null)
            {
                return (AudioClip[])bearSFX.GetType().GetProperty(typeofclip).GetValue(bearSFX);
            }
            else return null;
        }
        else return null;
    }
 
    private void GiveColour(EnumColour colour)
    {
        //k so this looks complex now but essentially i had to figure out a way to dynamically allocate the abilities for the ability tree
        //while still having them monobehaviour for the animation stuff so this is how its done, i think everything should work
        switch (colour)
        {
        
            case EnumColour.Green:
                bearJob = gameObject.AddComponent<GreenBear>();
                if (GreenBearAbilities.started == false)
                    new GreenBearAbilities(true);
                System.Type bAbility = GreenBearAbilities.basicAbility.GetType();
                System.Type sAbility = GreenBearAbilities.specialAbility.GetType();
                specialAbility = (Ability)gameObject.AddComponent(sAbility);
                basicAbility = (Ability)gameObject.AddComponent(bAbility);
                iconcolor = Color.green;
                this.avatarNumber = 3;
                break;
            case EnumColour.Black:
                this.bearJob = gameObject.AddComponent<BlackBear>();
                if (BlackBearAbilities.started == false)
                    new BlackBearAbilities(true);
               bAbility = BlackBearAbilities.basicAbility.GetType();
               sAbility = BlackBearAbilities.specialAbility.GetType();
                specialAbility = (Ability)gameObject.AddComponent(sAbility);
                iconcolor = Color.gray;
                basicAbility = (Ability)gameObject.AddComponent(bAbility);
                this.avatarNumber = 0;
                break;
            case EnumColour.Blue:
                bearJob = gameObject.AddComponent<BlueBear>();
                if (BlueBearAbilities.started == false)
                    new BlueBearAbilities(true);
                bAbility = BlueBearAbilities.basicAbility.GetType();
                sAbility = BlueBearAbilities.specialAbility.GetType();
                iconcolor = Color.blue;
                specialAbility = (Ability)gameObject.AddComponent(sAbility);
                basicAbility = (Ability)gameObject.AddComponent(bAbility);
                this.avatarNumber = 1;
                break;
            case EnumColour.Pink:
                bearJob = gameObject.AddComponent<PinkBear>();
                if (PinkBearAbilities.started == false)
                    new PinkBearAbilities(true);
                bAbility = PinkBearAbilities.basicAbility.GetType();
                sAbility = PinkBearAbilities.specialAbility.GetType();
                specialAbility = (Ability)gameObject.AddComponent(sAbility);
                basicAbility = (Ability)gameObject.AddComponent(bAbility);
                iconcolor = Color.magenta;
                this.avatarNumber = 2;
                break;

            case EnumColour.Red:

                bearJob = gameObject.AddComponent<RedBear>();
                this.avatarNumber = 4;
                if (RedBearAbilities.started == false)
                    new RedBearAbilities(true);
                bAbility = RedBearAbilities.basicAbility.GetType();
                sAbility = RedBearAbilities.specialAbility.GetType();
                specialAbility = (Ability)gameObject.AddComponent(sAbility);
                basicAbility = (Ability)gameObject.AddComponent(bAbility);
                iconcolor = Color.red;
                break;

            case EnumColour.MeleeEnemy:

                bearJob = gameObject.AddComponent<MeleeEnemy>();
                break;

            case EnumColour.StrongMeleeEnemy:

                bearJob = gameObject.AddComponent<StrongMeleeEnemy>();
                break;

            case EnumColour.RangedEnemy:

                bearJob = gameObject.AddComponent<RangedEnemy>();
                break;
            case EnumColour.MeleeBoss:
                bearJob = gameObject.AddComponent<MeleeBoss>();
                basicAbility = gameObject.AddComponent<BossPowerStrike>();
                specialAbility = gameObject.AddComponent<BossBloodBite>();
                break;
            case EnumColour.SupportBoss:
                BearJob = gameObject.AddComponent<SupportBoss>();
                basicAbility = gameObject.AddComponent<BossHeal>();
                specialAbility = gameObject.AddComponent<BossThrowIceChunk>();
                break;
            case EnumColour.SupportEnemy:
                BearJob = gameObject.AddComponent<SupportEnemy>();
                break;
            default:

                bearJob = gameObject.AddComponent<RedBear>(); ;
                break;
        }


    }
    public override string ToString()
    {
        string info = "HP: " + Hp.ToString() + "/" + totalHP.ToString() + "\n" +
           "Attack: " + attackStrength.ToString() + "\n" +
           "Defense: " + defense.ToString() + "\n" +
           "Movement: " + movement + "\n" +
           "Range: " + range;
        return info;
    }
    public void GetTarget(Tile selectedTile)
    {
        Bears[] Targets =selectedTile.GetComponentsInChildren<Bears>();
        foreach (Bears enemy in Targets)
        {
            if (enemy.enabled)
            Target = enemy;

        }
    }

    public void GetTarget(Vector2 selectedTile)
    {
        Bears[] Targets = TileManager.instance.TileDic[selectedTile].GetComponentsInChildren<Bears>();
       
        foreach (Bears enemy in Targets)
        {
            if(enemy.enabled)
                Target = enemy;

        }
    }


    public void Attack(Tile tileToAttack)
    {
        GetTarget(tileToAttack);
        anim.SetTrigger("Attacking");
        if (Target != null)
        {
            if (Target.Invincible == false)
            {

                if (attackSounds != null)
                    if (attackSounds.Length > 0)
                    {
                        int random = Random.Range(0, attackSounds.Length);
                        AudioS.PlayOneShot(attackSounds[random]);
                    }
                if (Target != null)
                {
                    DealDamage(attackStrength, Target);
                }
            }
        }

    }
    public void EnemyAttack(Tile tileToAttack)
    {
        this.transform.LookAt(tileToAttack.GetComponent<Transform>().position);
        GetTarget(tileToAttack);
        if (Target.Invincible == false &&Target!=null)
        {
            GetTarget(tileToAttack);
            if (Target != null)
                DealDamage(attackStrength, Target);
            if (attackSounds != null)
                if (attackSounds.Length > 0)
                {
                    int random = Random.Range(0, attackSounds.Length);
                    AudioS.PlayOneShot(attackSounds[random]);
                }

            //if (Target.hp <= 0)
            //{
            //    SquadSelection.instance.PlayersAlive--;
            //}
            if(Target.Anim!=null)
           Target.anim.SetTrigger("GettingAttacked");
        }

    }
    public void PlayerAttack(Tile tileToAttack, int ability)
    {
        this.transform.LookAt(tileToAttack.GetComponent<Transform>().position);
        GetTarget(tileToAttack);
        switch (ability)
        {
            case 1:
                Attack(tileToAttack);
                break;
            case 2:
                Ability1(tileToAttack);
                break;
            case 3:
                Ability2(tileToAttack);
                break;
        }
        anim.SetTrigger("Attacking");
      
        AttackRange.ClearTileAttackValues();
        GameManager.instance.CurrPhase = GameManager.Phase.menuPhase;
        BtnManager.instance.OnClickAttack();
        //GameManager.instance.AttackPhase = false;
        //GameManager.instance.MenuPhase = true;
    }
    public void Attack(Vector2 tileToAttack)
    {
        if (Invincible == false)
        {
            GetTarget(tileToAttack);
            Target.anim.SetTrigger("GettingAttacked");
            Target.Hp -= this.AttackStrength - Target.Defense;
        }

    }
    public void Ability1(Tile selectedTile)
    {

        GetTarget(selectedTile);
        if (ability1Sounds != null)
        {
            if (ability1Sounds.Length > 0)
            {
                int random = Random.Range(0, ability1Sounds.Length);
                Target.AudioS.PlayOneShot(ability1Sounds[random]);
                if(Target.anim!=null)
                Target.anim.SetTrigger("GettingAttacked");
            }
        }
        BasicAbility.CastAbility(selectedTile, AttackStrength);
        support = false;
        counterSupport = CountDown;
    }
    public void Ability2(Tile selectedTile)
    {
        GetTarget(selectedTile);
        if (ability2Sounds != null)
        {
            if (ability2Sounds.Length > 0)
            {
                int random = Random.Range(0, ability2Sounds.Length);
                AudioS.PlayOneShot(ability2Sounds[random]);
                if(Target!=null && Target.anim!=null)
                Target.anim.SetTrigger("GettingAttacked");
            }
        }
        SpecialAbility.CastAbility(selectedTile, AttackStrength);
        special = false;
    }


    public void Die()
    {
        if(this.anim)
        this.anim.SetBool("isDead", true);
       // this.gameObject.transform.Rotate(90f, 0f, 0f);
        if (this.gameObject.tag=="Enemy")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<CapsuleCollider>().enabled = false;

            Vector3 scale = gameObject.transform.localScale;
            
            InvokeRepeating("Shrink",.5f,.01f);
            GetComponentInParent<Tile>().IsEnemy = false;
            Invoke("Gone", 4f);
            this.GetComponentInChildren<Canvas>().enabled = false;
            this.enabled = false;
            
        }
    }
    private void Shrink()
    {
       
        Vector3 scale = gameObject.transform.localScale;
        float scaler;
            scale = gameObject.transform.localScale;
            scaler = (scale.x * .99f);
            gameObject.transform.localScale = new Vector3(scaler, scaler, scaler);
     
    }
    public void Gone()
    {
        //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //this.gameObject.GetComponent<Light>().enabled = false;
        CancelInvoke("Shrink");
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false; ;
        this.gameObject.GetComponent<Light>().enabled = false;
        
    }
    private void Start()
    {
        GiveColour(color);
        DistributeStats();
        bearSFX = BearSFX.instance;
        audioS = GetComponent<AudioSource>();
        GetSoundFX();
        buffNames = new string[] { "buffAttack", "buffMovement", "buffDefence", "invincible", "stun", "burn", "heal", "venom"};
        for (int i = 0; i < buffNames.Length; i++)
        {
            themBuffs[buffNames[i]] = 0;
        }
        anim = GetComponent<Animator>();
        if(GameManager.instance)
        txtDamage = GameManager.instance.txtDamage;
 
    }

    //Added this for the turn system
    private void Update()
    {
        if (Hp > 0)
        {
            IsAlive = true;
        }
        else if (Hp <= 0)
        {
            IsAlive = false;

            if (onlyOnce)
            {
                onlyOnce = false;
                Die();
            }

        }
        if (counterSupport <= 0)
        {
            support = true;
        }
        if(special==true &&SpecialIcon!=null)
        {
            SpecialIcon.color = iconcolor;
        }
        else if(special == false && SpecialIcon != null)
        {
            SpecialIcon.color = Color.white;
        }
        if(support==true && abilityIcon != null)
        {
            abilityIcon.color = iconcolor;
        }
        if (support == false && abilityIcon != null)
        {
            abilityIcon.color = Color.white;
        }
        if (GameManager.instance)
        {
            if (gameObject.tag == "Player")
            {
                if (GameManager.instance.CurrPhase == GameManager.Phase.menuPhase)
                {
                    GetComponent<Rigidbody>().freezeRotation = true;
                }
                else
                    GetComponent<Rigidbody>().freezeRotation = false;
            }
           else if(gameObject.tag=="Enemy" && GetComponent<Movement>().Moving == false && GameManager.instance.CurrPhase==GameManager.Phase.enemyPhase)
            {
                GetComponent<Rigidbody>().freezeRotation = false;
            }
            else
                GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    public void CheckThemBuffs()
    {
        for (int i = 0; i < buffNames.Length; i++)
        {
            if (themBuffs[buffNames[i]] <= 0)
            {
                switch (i)
                {
                    case 0:
                        this.AttackStrength = this.bearJob.AttackStrength;
                        break;
                    case 1:
                        this.Movement = this.bearJob.Movement;
                        break;
                    case 2:
                        this.Defense = this.bearJob.Defense;
                        break;
                    case 3:
                        this.Invincible = false;
                        break;
                }
            }
            
        }
    }
    //TAKE DOT 
    public void TakeDot()
    {
        if(themBuffs["burn"]>0)
        {
            Hp -=(int) (totalHP * .1f);
        }
        else if (themBuffs["venom"] > 0)
        {
            Hp -= (int)(totalHP * .1f);
        }
    }
    public void GetHot()
    {
        if (themBuffs["heal"] > 0)
        {
            if (Hp != TotalHP)
            {
                Hp += (int)(totalHP * .1f);
            }
        }
    }
    public void DeductThemBuffs()
    {
        for (int i = 0; i < buffNames.Length; i++)
        {
            themBuffs[buffNames[i]]--;
        }
    }
    public void DealDamage(int damage, Bears target)
    {
        if (target != null)
        {
            int reducedDamage;
            if(!target.invincible)
            reducedDamage = damage - target.defense;
            else
                reducedDamage = 0;
            target.Hp -= reducedDamage;
            if(target.anim)
                target.anim.SetTrigger("GettingAttacked");
            GenerateDamageNumbers(reducedDamage, target);
            if(target.Hp<=0)
            {
                if (target.tag == "Player")
                    SquadSelection.instance.PlayersAlive--;
                else if(target.tag=="Enemy")
                {
                    EnemyManager.instance.EnemiesAlive--;
                }
                
            }
        }

    }

    public void GenerateDamageNumbers(int damage,Bears target)
    {
       
        Canvas dmgNumbers = Instantiate(txtDamage,target.transform.position,new Quaternion());
        dmgNumbers.GetComponentInChildren<Text>().text = damage.ToString();
        dmgNumbers.transform.parent = target.transform;
    }


}