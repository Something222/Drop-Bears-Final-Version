using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    private int section;
    public static Tutorial instance = null;
    [SerializeField] private Button btnEndTurn;
    [SerializeField] private Button[] attackButtons;
    [SerializeField] private Bears tutEnemy;
    [SerializeField] private Button btnMap;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI body;
    [SerializeField] private GameObject[] Squad;
    [SerializeField] private GameObject blinkyTile;
    private Bears selectedPlayer;
    private bool onlyOnce = false;
    private bool lockMovement = true;
    private bool lockAttack = true;
    private Transform originalPos;
    [SerializeField]private Tile tileToMoveTo;
    [SerializeField] private Tile tileToAttack;
    [SerializeField] private GameObject subpanel;
    public int Section { get => section; set => section = value; }
    public bool LockMovement { get => lockMovement; set => lockMovement = value; }
    public bool LockAttack { get => lockAttack; set => lockAttack = value; }
    public bool OnlyOnce { get => onlyOnce; set => onlyOnce = value; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        code = GameManager.instance;
        code.CurrPhase = GameManager.Phase.tutorial;
       
        Section = 0;
    }
    public IEnumerator Blinky()
    {
       
        while (gameObject)
        {
           blinkyTile.GetComponent<MeshRenderer>().material = tileToMoveTo.Defaultmat;
            yield return new WaitForSeconds(.5f);
            blinkyTile.GetComponent<MeshRenderer>().material = tileToMoveTo.SelectedTile;
            yield return new WaitForSeconds(.5f);
        }
    }
   private void ChangeToTutorial()
    {
        code.CurrPhase = GameManager.Phase.tutorial;
    }
    // Update is called once per frame
    void Update()
    {
        
        switch (Section)
        {
            case 0:
               
                if (!onlyOnce)
                {
                  selectedPlayer = SquadSelection.instance.Squad[SquadSelection.instance.Selected].GetComponent<Bears>();
                    StartCoroutine( Blinky());
                    
                    originalPos = Squad[4].transform;
                    selectedPlayer.HasAttacked = true;
                    onlyOnce = true;
                    tileToMoveTo.Moveable = true;
                    name.text = "Movement";
                    body.text = "Press the move button and maneuver to the flashing block";
                }
        
            if (selectedPlayer.GetComponent<Movement>().HasMoved)
            {
                btnEndTurn.interactable = true;
            }
            else
            {
                btnEndTurn.interactable = false;
            }
                break;
            case 1:
                Destroy(blinkyTile);
                StopAllCoroutines();
            LockMovement = false;
                tileToAttack.Attackvalue = 1;
                if (!OnlyOnce)
            {
                    ResetTurn();
                    LockMovement = false;
                    selectedPlayer.HasAttacked = false;
                OnlyOnce = true;
                    name.text = "Attack";
                    body.text = "Press the Attack Button then use a basic attack on the enemy.";
                    btnMap.interactable = false;
                    selectedPlayer.GetComponent<Movement>().HasMoved = true;
                    selectedPlayer.Special = false;
                    selectedPlayer.counterSupport = 10;
                    selectedPlayer.Support = false;
               
                    selectedPlayer.GetComponent<Movement>().HasMoved = true;
                }
          
            if (selectedPlayer.HasAttacked)
                btnEndTurn.interactable = true;
            else
                btnEndTurn.interactable = false;
                break;
            case 2:
                attackButtons[0].interactable = false;
                btnEndTurn.interactable = false;
                attackButtons[1].interactable = true;
                if (!OnlyOnce)
            {
                    ResetTurn();
                    name.text = "Support Ability";
                    body.text = "Each Bear has abilites they can use every 2 turns, try eating some beans!";
              
                    lockAttack = false;
                    selectedPlayer.HasAttacked = false;
                OnlyOnce = true;
                    selectedPlayer.GetComponent<Movement>().HasMoved = true;
                    selectedPlayer.Special = false;               
                    selectedPlayer.counterSupport = 0;
                    selectedPlayer.Support = true;            
                }
         
                break;
            case 3:
                if (!OnlyOnce)
                {
                    ResetTurn();
                    selectedPlayer.HasAttacked = false;
                    OnlyOnce = true;
                    selectedPlayer.GetComponent<Movement>().HasMoved = true;
                    selectedPlayer.Special = true;
        

                    selectedPlayer.counterSupport = 10;
                    selectedPlayer.Support = false;
                    lockAttack = false;
                    tutEnemy.Hp = tutEnemy.TotalHP;

                    name.text = "Special";
                    body.text = "Each Bear also have a Special move they can use once per match, try popping one in that mofo!";
                }
                btnEndTurn.interactable = false;
                attackButtons[0].interactable = false;
                attackButtons[1].interactable = false;
                attackButtons[2].interactable = true;
          
   
                break;
            case 4:
         
            if (!OnlyOnce)
            {
                    name.text = "Turns";
                    body.text = "Each Bear can move and attack once per turn then their turn is up! End the turn to continue";
                    lockAttack = false;

                    ResetTurn();
                    selectedPlayer.GetComponent<Movement>().HasMoved = false;
                    tutEnemy.Hp = tutEnemy.TotalHP;
                    btnMap.interactable = true;
                btnEndTurn.interactable = true;
                selectedPlayer.Special = true;
                selectedPlayer.counterSupport = 0;
                    onlyOnce = true;
            }
                break;
            case 5:
                if (!onlyOnce)
                {
                    GameManager.instance.CurrPhase = GameManager.Phase.menuPhase;
                    ResetTurn();
                    Squad[4].gameObject.transform.position = originalPos.position;
                    for (int i = 0; i < Squad.Length; i++)
                    {
                        Squad[i].SetActive(true);
                    }
                    name.text = "Explore";
                    body.text = "Play around with the different abilites your bears have to offer and kill that Baddy!";
                    SquadSelection.instance.FindTeam();
                    onlyOnce = true;
                }
                break;

        }
       
        Debug.Log(code.CurrPhase); 
    }
    private void ResetTurn()
    {
        BtnManager.instance.x = 0;
        BtnManager.instance.y = 1;
        subpanel.SetActive(false);

    }
}
