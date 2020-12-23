using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TileManager tilemanager;
    [SerializeField] private GameManager code;
    [SerializeField] private SquadSelection squadManager;
    [SerializeField] private int range;
    private Tile currentTileShort;
   private Bears currentPlayer;
    public bool attackConf=false;
    private bool tilecheck = false;
    private GameObject currentTile;
    [SerializeField]private bool canBeSelected=true;
    public static TileSelector instance;
    private int abilityToUse;
    public GameObject CurrentTile { get => currentTile; set => currentTile = value; }
    public int AbilityToUse { get => abilityToUse; set => abilityToUse = value; }
    public int Range { get => range; set => range = value; }
    public Tile CurrentTileShort { get => currentTileShort; set => currentTileShort = value; }

    public int x = 0;
    public int y = 0;
    private bool isSubmitted = false;
    private bool isCancel = false;

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

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.started)
        {
          
                isCancel = !isCancel;
            AttackRange.ClearTileAttackValues();
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            
                isSubmitted = !isSubmitted;
            
        }
    }

    public void NavigateTile(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 selection = context.ReadValue<Vector2>();
            x = (int)selection.x;
            y = (int)selection.y;
        }
        
        
    }
    public void ResetXAndY()
    {
        x = 0;
        y = 0;
    }
    void Start()
    {
        tilemanager = tilemanager.GetComponent<TileManager>();
      code= GameManager.instance;
        squadManager = SquadSelection.instance;
        tilecheck = false;
    }
    private void MoveTile(Vector2 tilecoordinates,Tile currentTileShort)
    {
        #region ZachNotes
        //This function allows the cursor to move from tile to tile 
        //pass it the the coordinates of the tile you want to go to 
        //and the current tile
        #endregion ZachNotes
        GameObject nextTile = tilemanager.GetTileDicMove(tilecoordinates,currentTileShort);
        currentTileShort.IsSelected = false;
            nextTile.GetComponent<Tile>().IsSelected = true;
            CurrentTile = nextTile;
        
    }
    private void ClearValue()
    {
        x = 0;
        y = 0;
    }
    private void ReplaceTileMat(Tile tile)
    {
        if (tile.Movementvalue > 0)
        {
            tile.Renderer.material = tile.Movemat;
        }
        else if(tile.Attackvalue>0)
        {
            tile.Renderer.material = tile.AttackMat;
        }
        else if(tile.IsObstacle)
        {
            tile.Renderer.material = tile.ObstacleMat;
        }
        else
        {
            tile.Renderer.material = tile.Defaultmat;
        }
    }
    public void SetAttackRangeJustOnceToFalse()
    {
        squadManager.Squad[squadManager.Selected].GetComponent<AttackRange>().JustOnce = false;
    }
    public void ExecuteAttack()
    {
        currentPlayer.PlayerAttack(CurrentTileShort, abilityToUse);
        currentPlayer.HasAttacked = true;
        BtnManager.instance.x = 0;
    }
    // Update is called once per frame
    void Update()
    {
        #region ZachNotes
        //We would only want to see the characters movement range if hes selected and we are in 
        //a movement phase I would think
        #endregion ZachNotes
        //  if (code.CurrPhase==GameManager.Phase.attackPhase||code.CurrPhase==GameManager.Phase.movementPhase)
        if (code.CurrPhase != GameManager.Phase.pausePhase)
        {
           
            //canBeSelected is the variable that allows you to maneover the grid
            if (canBeSelected)
            {
                if (!tilecheck)
                {
                    #region ZachNotes
                    //Replace this later with selected character position
                    //needs to be sent from an outside script the one that switches to movement phase
                    CurrentTile = tilemanager.GetTileDic(squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Position);
                    #endregion ZachNotes
                    if(CurrentTile)
                    CurrentTile.GetComponent<Tile>().IsSelected = true;
                    tilecheck = true;

                }
                if (currentTile == null)
                {
                    CurrentTile = tilemanager.GetTileDic(squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Position);
                    if(currentTile!=null &&currentTile.GetComponent<Tile>())
                    CurrentTile.GetComponent<Tile>().IsSelected = true;
                }
                if(currentTile!=null)
                CurrentTileShort = CurrentTile.GetComponent<Tile>();
                #region ZachNotes
                //We can replace this with the new input system later
                //Also maybe support mouse if you guys want
                #endregion ZachNotes
                if ( x < 0 && Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //MoveTile Moves the selected tile it takes a x and y coordinate to move too
                    Tile subTile = currentTileShort;
                    ReplaceTileMat(subTile);
                    MoveTile(new Vector2(CurrentTileShort.Loc.x - 1, CurrentTileShort.Loc.y), CurrentTileShort);
                    ClearValue();
                }
                else if (x > 0 && Mathf.Abs(x) > Mathf.Abs(y))
                {
                    Tile subTile = currentTileShort;
                    ReplaceTileMat(subTile);
                    MoveTile(new Vector2(CurrentTileShort.Loc.x + 1, CurrentTileShort.Loc.y), CurrentTileShort);
                    ClearValue();
                }
                else if (y > 0 && Mathf.Abs(x) < Mathf.Abs(y))
                {
                    Tile subTile = currentTileShort;
                    ReplaceTileMat(subTile);
                    MoveTile(new Vector2(CurrentTileShort.Loc.x, CurrentTileShort.Loc.y + 1), CurrentTileShort);
                    ClearValue();
                }
                else if (y < 0 && Mathf.Abs(x) < Mathf.Abs(y))
                {
                    Tile subTile = currentTileShort;
                    ReplaceTileMat(subTile);
                    MoveTile(new Vector2(CurrentTileShort.Loc.x, CurrentTileShort.Loc.y - 1), CurrentTileShort);
                    ClearValue();
                }
               
                    #region ZachNotes
                    //so this will pass an to the player depending on what we want to do 
                    //currently we only pass it the move;
                    #endregion ZachNotes
                    if (CurrentTileShort!=null &&CurrentTileShort.Moveable && !CurrentTileShort.IsPlayer && !CurrentTileShort.IsEnemy&&code.CurrPhase==GameManager.Phase.movementPhase&& isSubmitted && !squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Moving)
                    {
                        squadManager.Squad[squadManager.Selected].GetComponent<Movement>().MoveDestination = CurrentTile;
                        squadManager.Squad[squadManager.Selected].GetComponent<Movement>().ExecuteMovement = true;
                       
                    }
                      if (CurrentTileShort != null && CurrentTileShort.Attackvalue > 0 && isSubmitted &&code.CurrPhase == GameManager.Phase.attackPhase)
                    {
                        currentPlayer = squadManager.Squad[squadManager.Selected].GetComponent<Bears>();
                        code.CurrPhase = GameManager.Phase.confPhase;

                    }
                   
                
                if (isCancel&&code.CurrPhase!=GameManager.Phase.confPhase && isCancel && code.CurrPhase == GameManager.Phase.attackPhase && !squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Moving)
                {
                    Movement.ClearTileMovementValues();
                    AttackRange.ClearTileAttackValues();
                    code.CurrPhase = GameManager.Phase.attackSelectionPhase;
                  
                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Movementcheck = false;
                    squadManager.Squad[squadManager.Selected].GetComponent<AttackRange>().JustOnce = false;
                   
                }
               else  if (isCancel && code.CurrPhase != GameManager.Phase.confPhase && isCancel &&( code.CurrPhase == GameManager.Phase.movementPhase || code.CurrPhase == GameManager.Phase.mapPhase) && !squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Moving)
                {
                    Movement.ClearTileMovementValues();
                    AttackRange.ClearTileAttackValues();
                    code.CurrPhase = GameManager.Phase.menuPhase;

                    squadManager.Squad[squadManager.Selected].GetComponent<Movement>().Movementcheck = false;
                    squadManager.Squad[squadManager.Selected].GetComponent<AttackRange>().JustOnce = false;

                }

            }
            if (code.CurrPhase == GameManager.Phase.confPhase)
            {
                canBeSelected=false;
                if (isCancel)
                {
                    squadManager.Squad[squadManager.Selected].GetComponent<AttackRange>().JustOnce = false;
                    code.CurrPhase = GameManager.Phase.attackPhase;
                    isSubmitted = false;
                    isCancel = false;
                    BtnManager.instance.GoBack();
                }
            }
            if (code.CurrPhase == GameManager.Phase.attackPhase || code.CurrPhase == GameManager.Phase.movementPhase||code.CurrPhase==GameManager.Phase.mapPhase)
                {
                canBeSelected = true;
                isSubmitted = false;

            }
            else if(code.CurrPhase!=GameManager.Phase.confPhase)
            {
                isCancel = false;
                canBeSelected = false;
                tilecheck = false;
                isSubmitted = true;
                #region ZachNotes
                //So this makes it so that when you select an option the first tile selected is 
                //the players tile
                #endregion ZachNotes
                if (CurrentTile != null)
                {
                    CurrentTile.GetComponent<Tile>().IsSelected = false;
                  
                    CurrentTile = null;
                }

            }
        }
        ResetXAndY();
    }
}
