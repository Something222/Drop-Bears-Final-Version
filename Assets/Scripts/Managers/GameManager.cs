using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
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
    #endregion Singleton
   public enum Phase { menuPhase,attackPhase,enemyPhase,movementPhase,mapPhase,confPhase,winLosePhase,tutorial,pausePhase,attackSelectionPhase}
    [SerializeField]private Phase currPhase;
  
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private GameObject selectedPlayer;
    [SerializeField] private GameObject selectedTile;
    [SerializeField] private TileManager tilemanager;
    [SerializeField] private GameObject confPanel;
    [SerializeField] public Canvas txtDamage;
    public static int levelsComplete = 0;
    private bool enemySubtracted=false;
    private TileSelector tileSelector;
    private bool enteredConfPhase = false;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pauseMenuUI;
    #region Buttons
    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private Button moveBtn;
    [SerializeField] private Button attackBtn;
    [SerializeField] private Button Ability1Btn;
    [SerializeField] private Button Ability2Btn;
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private Button submenuBtn;
    private BtnManager btnManager;
    #endregion Buttons
    [SerializeField] private SquadSelection squadSelector;
    //For the turn system
    [SerializeField] private bool playerTurn;
    public bool onlyOnce = false;
    private int endTurn;
    private EnemyManager enemyManager;
   [SerializeField] private Button btnWin;
    [SerializeField] private Button btnLose;
    public GameObject InteractionMenu { get => interactionMenu; set => interactionMenu = value; }
    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }
    public Phase CurrPhase { get => currPhase; set => currPhase = value; }
    bool substracted = false;
    public Phase savedPhase;
    void Start()
    {
        squadSelector = SquadSelection.instance;
        btnManager = BtnManager.instance;
        enemyManager = EnemyManager.instance;
        tileSelector = TileSelector.instance;
        playerTurn = true;
        CurrPhase = Phase.menuPhase;
        //allBears = FindObjectsOfType(typeof(Bears)) as Bears[]; <-My Mistakes (nian)
        #region oldnonenum
        //EnemyPhase = false;
        //MenuPhase = true;
        //movementPhase = false;
        #endregion oldnonenum
    }
    void Update()
    {
        if (enemyManager.EnemiesAlive > 0 && squadSelector.PlayersAlive > 0)
        {

                switch (CurrPhase)
                {
                    case Phase.menuPhase:
                case Phase.attackSelectionPhase:
                    enemySubtracted = false;
                        StartCoroutine(CheckTurns());
                        selectedPlayer = squadSelector.Squad[squadSelector.Selected];
                        HighlightTileUnderSelectedPlayer(selectedPlayer);
                        interactionMenu.SetActive(true);
                        if (selectedPlayer.GetComponent<Bears>().HasAttacked)
                        {
                            DisableAttackButtons();
                        }
                        else
                        {
                            EnableAttackBtns();
                        }
                        if (selectedPlayer.GetComponent<Movement>().HasMoved)
                            moveBtn.interactable = false;
                        else
                            moveBtn.interactable = true;
                    if (selectedPlayer.GetComponent<Movement>().HasMoved && selectedPlayer.GetComponent<Bears>().HasAttacked)
                    {
                        selectedPlayer.GetComponent<Bears>().TurnComplete = true;

                    }
                        if (!onlyOnce)
                        {
                          
                        tilemanager.Invoke("DeSelectAllTiles", .25f);
                            onlyOnce = true;
                        }
                    statsText.gameObject.SetActive(false) ;
                    if (!substracted)
                    {
                        substracted = true;
                        for (int i = 0; i < squadSelector.Squad.Length; i++)
                        {
                            Bears currentMember = squadSelector.Squad[i].GetComponent<Bears>(); 
                            if (currentMember.themBuffs["stun"]>0)
                            {
                                currentMember.TurnComplete = true;
                            }
                            EndTurnAllBuff(currentMember);
                          
                        }
                        
                    }
                    Debug.Log(selectedPlayer.GetComponent<Movement>().ExecuteMovement);
                    break;
                    case Phase.attackPhase:
                    case Phase.movementPhase:
                    case Phase.mapPhase:
                    interactionMenu.SetActive(false);
                    statsText.gameObject.SetActive(true);
                    if(statsText!=null)
                    GetStatsText();
                     confPanel.SetActive(false);
                     enteredConfPhase = false;

                    break;
                    case Phase.confPhase:
                    confPanel.SetActive(true);
                    if(!enteredConfPhase)
                    {
                        AttackRange.ClearTileAttackValues();
                        enteredConfPhase = true;
                    }
                    break;
                    case Phase.enemyPhase:
                    substracted = false;
                        interactionMenu.SetActive(false);
                        if (enemyManager.Enemies != null)
                        {
                            for (int i = 0; i < enemyManager.Enemies.Length; i++)
                            {
                                if (enemyManager.Enemies[i].GetComponent<Bears>().IsAlive && enemyManager.FirstEnemyHasActed == false)
                                {
                                    enemyManager.Enemies[i].GetComponent<EnemyAIBase>().TakeTurn = true;
                                    enemyManager.FirstEnemyHasActed = true;
                                }
                            }
                            StartCoroutine(enemyManager.WaitForTurn());
                            //if (enemyManager.Enemies[enemyManager.Enemies.Length - 1].GetComponent<EnemyAIBase>().TurnCompleted)
                            //{
                            //    onlyOnce = false;
                            //    enemyManager.SwitchToPlayerTurnsEnum();

                            //}
                        if (!enemySubtracted)
                        {
                            enemySubtracted = true;
                            for (int i = 0; i < enemyManager.Enemies.Length; i++)
                            {
                                EndTurnAllBuff(enemyManager.Enemies[i].GetComponent<Bears>());
                            }
                        }
                    }   
                        
                   statsText.gameObject.SetActive(false) ;
                    break;
                case Phase.pausePhase:
                    if (currPhase == Phase.menuPhase || currPhase == Phase.enemyPhase)
                    {
                        savedPhase = CurrPhase;
                        pauseMenuUI.SetActive(true);
                        Time.timeScale = 0f;
                    }

                    break;
              

                }
           
        }
        else if (enemyManager.EnemiesAlive <= 0)
        {
            winPanel.SetActive(true);
            btnWin.Select();
            currPhase = Phase.winLosePhase;
        }
        else if (squadSelector.PlayersAlive <= 0)
        {
            losePanel.SetActive(true);
            btnLose.Select();
            currPhase = Phase.winLosePhase;
        }
        if(currPhase!=Phase.confPhase)
        {
            confPanel.SetActive(false);
            enteredConfPhase = false;         
        }
        Debug.Log(currPhase);
    }
    void HighlightTileUnderSelectedPlayer(GameObject selectedplayer)
    {
        if (selectedPlayer != null &&squadSelector.Squad!=null)
        {
            foreach (GameObject player in squadSelector.Squad)
            {
                if (player == selectedplayer)
                {
                    if (selectedplayer.GetComponentInParent<Tile>())
                        selectedplayer.GetComponentInParent<Tile>().IsSelected = true;
                }
                else
                {
                   if (player.GetComponentInParent<Tile>())
                        player.GetComponentInParent<Tile>().IsSelected = false;
                }
            }
        }
    }
    private void EnableAttackBtns()
    {
        submenuBtn.interactable = true;
        attackBtn.interactable = true;
        if (Tutorial.instance == null || (Tutorial.instance.Section != 2 && Tutorial.instance.Section != 3))
        {
      
            Bears selectedBear = squadSelector.Squad[squadSelector.Selected].GetComponent<Bears>();
            if (selectedBear.Support == true)
            {
                Ability1Btn.interactable = true;
            }
            else
            {
                Ability1Btn.interactable = false;
            }
            if (selectedBear.Special == true)
                Ability2Btn.interactable = true;
            else
                Ability2Btn.interactable = false;
        }
    }
    private void DisableAttackButtons()
    {
        submenuBtn.interactable = false;
        attackBtn.interactable = false;
        Ability1Btn.interactable = false;
        Ability2Btn.interactable = false;
    }

    IEnumerator CheckTurns()
    {
        bool check = false;
        for (int i = 0; i < squadSelector.Squad.Length; i++)
        {
            if (squadSelector.Squad[i].GetComponent<Bears>().IsAlive && !squadSelector.Squad[i].GetComponent<Bears>().TurnComplete)
            {
                check = true;

            }
        }
        if (check == false)
        {
            //this is when the player turn ends it swithces to enemy so put cooldownstuff here
            playerTurn = false;
            for (int i = 0; i < squadSelector.Squad.Length; i++)
            {
                squadSelector.Squad[i].GetComponent<Bears>().HasAttacked = false;
                squadSelector.Squad[i].GetComponent<Bears>().TurnComplete = false;
                squadSelector.Squad[i].GetComponent<Movement>().HasMoved = false;
                squadSelector.Squad[i].GetComponent<Movement>().Movementcheck = false;

            }
            //EnemyPhase = true;
            CurrPhase = Phase.enemyPhase;

           
        }
        yield return new WaitForSeconds(.1f);
    }
   
    void GetStatsText()
    {
        if (tileSelector.CurrentTile != null)
        {
            Tile tileSelected = tileSelector.CurrentTile.GetComponent<Tile>();

            if (tileSelected.IsEnemy || tileSelected.IsPlayer)
            {
                Bears[] displays = tileSelected.GetComponentsInChildren<Bears>();
                foreach (Bears display in displays)
                {
                    if (display.enabled)
                    {
                        if (statsText != null && display != null)
                            statsText.text = display.ToString();
                        else
                            statsText.text = "";
                    }
                }
                
            }
            else
                statsText.text = "";

        }
    }
    private void EndTurnAllBuff(Bears currentMember)
    {
        currentMember.TakeDot();
        currentMember.GetHot();
        currentMember.counterSupport--;
        currentMember.DeductThemBuffs();
        currentMember.CheckThemBuffs();
    }
}
