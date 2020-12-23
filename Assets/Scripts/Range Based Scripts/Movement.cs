using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    // [SerializeField] private List<Vector2> moveList;
    [SerializeField]private int x;
   [SerializeField] private int y;
    [SerializeField] private Vector2 position;
    //here for testing purposes 
    private bool onlyOnce=false;
    //navmesh stuff
    private NavMeshAgent agent;

    [SerializeField] private TileManager tilemanager;
   //Movementcheck is to say whether the player has already moved this turn
    [SerializeField]private bool movementcheck = false;
    [SerializeField]private bool moving;
    private GameObject[] playerTiles;
    private bool hasMoved = false;
    private Bears stats;
   [SerializeField] private bool executeMovement;
    private GameObject moveDestination;
    private float timer = 1f;
    private GameManager code;
    private Animator anim;
   
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }
    public bool ExecuteMovement { get => executeMovement; set => executeMovement = value; }
    public GameObject MoveDestination { get => moveDestination; set => moveDestination = value; }
    public bool Movementcheck { get => movementcheck; set => movementcheck = value; }
    public bool HasMoved { get => hasMoved; set => hasMoved = value; }
    public Vector2 Position { get => position; set => position = value; }
    public bool Moving { get => moving; set => moving = value; }
    public bool OnlyOnce { get => onlyOnce; set => onlyOnce = value; }
    public Animator Anim { get => anim; set => anim = value; }

    #region ZachNotes
    //this gets all the tiles moveable to not accounting for obstacles
    //currently no need but may be usefull for aoe attacks and stuff
    //would need to be slightly modified for that though
    #endregion ZachNotes


    public void AssignTileMovementValue(GameObject tile,int move)
    {
        #region ZachNotes
        //So this is the function that produces the movement range.
        //it works by starting from the player and assigning descending number values 
        //(starting from his movement range) to tiles around the 
        //player based on their distance to the player (further the tile lower the value).
        //With these movement values on the tiles we can determine if they are within the players
        //movement range if they are higher than 0
        //its a recursive function to get it to work pass it the tile the players currently on 
        //and the players movement range
        #endregion ZachNotes
        if (tile!=null)
        {

            if (tile == tilemanager.TileDic[position])
            {
                Tile tileshort = tile.GetComponent<Tile>();
                if (tileshort.Movementvalue >= 0 && tileshort.Movementvalue < move)
                    {
                        tileshort.Movementvalue = move;
                        if (move >= 0)
                        {   
                        if(Tutorial.instance==null ||!Tutorial.instance.LockMovement)
                            tileshort.Moveable = true;
                        }
                        GameObject nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                    }
               
            }
            else
            {
                Tile tileshort = tile.GetComponent<Tile>();
                if (!tileshort.IsObstacle &&!tileshort.IsEnemy &&!tileshort.IsPlayer)
                {
                    if (tileshort.Movementvalue >= 0 && tileshort.Movementvalue < move)
                    {
                        tileshort.Movementvalue = move;
                        if (move >= 0)
                        {
                            if (Tutorial.instance == null || !Tutorial.instance.LockMovement)
                                tileshort.Moveable = true;
                        
                        }
                        GameObject nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                    }
                }
                else if(tileshort.IsEnemy||tileshort.IsPlayer||tileshort.IsObstacle)
                {
                    tile.GetComponent<Tile>().Movementvalue = -1;
                }
            }
        }
     
        return;

    }
    public void AssignTileMovementValue(Tile tile, int move)
    {
   
        if (tile != null)
        {

            
            if (!tile.IsObstacle)
            {
                if (tile.Movementvalue >= 0 && tile.Movementvalue < move)
                {
                    tile.Movementvalue = move;
                    if (move >= 0)
                    {
                        tile.Moveable = true;
                    }
                    #region DicMethod(Broken)
                    
                    #endregion DicMethod(Broken)
                    GameObject nexttile = tilemanager.GetTileDic(new Vector2(tile.X - 1, tile.Y));
                    AssignTileMovementValue(nexttile, tile.Movementvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tile.X + 1, tile.Y));
                    AssignTileMovementValue(nexttile, tile.Movementvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y - 1));
                    AssignTileMovementValue(nexttile, tile.Movementvalue - 1);
                    nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y + 1));
                    AssignTileMovementValue(nexttile, tile.Movementvalue - 1);

                }
            }
            else
            {
                tile.GetComponent<Tile>().Movementvalue = -1;
            }
        }

        return;

    }

    public void TurnOffMovementEnum()
    {
        #region ZachNotes
        //I need this for an invoke it just tells the game the player aint moving
        #endregion ZachNotes
        Moving = false;
        code.CurrPhase = GameManager.Phase.menuPhase;
        Anim.SetBool("isMoving", false);

    }
    // VERY IMPORTANT ALSO CALL THIS METHOD WHENEVER SWITCHING CHARACTERS AND MOVEMENT RANGES HAVE BEEN GENERATED
    //IF NOT WILL HARD CRASH UNITY!!!
    public static void ClearTileMovementValues(TileManager tilemanager)
    {
        #region ZachNotes
        //this function clears the movement values of all the tiles
        //this will need to be called whenever new movement ranges are calculated if not it can cause crashes
        //Because of the pathfinding system
        #endregion ZachNotes
        foreach (GameObject tile in tilemanager.Tilearray)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                tileshort.Movementvalue = 0;
            }
        }
    }
    public static void ClearTileMovementValues()
    {
        
       
        foreach (GameObject tile in TileManager.instance.Tilearray)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsObstacle)
            {
                tileshort.Movementvalue = 0;
            }
        }
    }
    #region PlayerMovementMethods
    public float MoveToFinalTile(GameObject tile,GameObject playertile,TileManager tilemanager)
    {
        Tile tileshort = tile.GetComponent<Tile>();
        if (tileshort.Movementvalue<=0||tileshort.IsPlayer||tileshort.IsEnemy)
        {
            return 0;
        }
        #region ZachNotes
       
        #endregion ZachNotes
        Stack<Vector2> moves=new Stack<Vector2>();
        Tile originpoint = playertile.GetComponent<Tile>();
        
        moves.Push(tileshort.Loc);
        this.Moving = true;
        if (stats.MoveSounds != null)
        {
            if (stats.MoveSounds.Length > 0)
            {
                int random = Random.Range(0, stats.MoveSounds.Length);
                stats.AudioS.PlayOneShot(stats.MoveSounds[random]);
            }
        }
        while (tileshort.X!=originpoint.X ||tileshort.Y!=originpoint.Y)
        {
            Tile nexttile;
            #region ZachNotes
            //Creating the Stack
            #endregion ZachNotes
            if (tilemanager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y))!= null)
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y)).GetComponent<Tile>();
                tileshort = PathChecker(tileshort, nexttile, moves);
            }
            if (tilemanager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y)) != null)
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y)).GetComponent<Tile>();
                tileshort = PathChecker(tileshort, nexttile, moves);
            }
            if (tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1))!= null)
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1)).GetComponent<Tile>();
                tileshort = PathChecker(tileshort, nexttile, moves);
            }
            if (tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1)))
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1)).GetComponent<Tile>();
                tileshort = PathChecker(tileshort, nexttile, moves);
            }
        }
        float i = 0;
        while (moves.Count>0)
        {
            #region ZachNotes
            //So im using a coroutine to do the movement currently he just teleports to spots we will have 
            //to adjust that with animations later
            #endregion ZachNotes
            StartCoroutine(MoveToIndividualTile(moves.Pop(),i,tilemanager));
           
            i += .5f;
        }
        return i;
       
    }
 // I havent tested with this constructor it may not work 
 public void ResetMovement()
    {
        movementcheck = false;
        onlyOnce = false;
        HasMoved = false;

    }
    public float MoveToFinalTile(Tile tile, Tile playertile, TileManager tilemanager)
    {
   
        Stack<Vector2> moves = new Stack<Vector2>();
     
        moves.Push(tile.Loc);
        this.Moving = true;
        if (stats.MoveSounds != null)
        {
            int random = Random.Range(0, 5);
            if (stats.MoveSounds.Length > 0&&random<=3)
            {
                random = Random.Range(0, stats.MoveSounds.Length);
                stats.AudioS.PlayOneShot(stats.MoveSounds[random]);
            }
        }
        while (tile.X != playertile.X || tile.Y != playertile.Y)
        {
            #region ZachNotes
            //Creating the Stack
            #endregion ZachNotes
            Tile nexttile;
            if (tilemanager.GetTileDic(new Vector2(tile.X - 1, tile.Y)) != null)
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tile.X - 1, tile.Y)).GetComponent<Tile>();
                tile = PathChecker(tile, nexttile, moves);
            }
            if (tilemanager.GetTileDic(new Vector2(tile.X + 1, tile.Y)) != null)
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tile.X + 1, tile.Y)).GetComponent<Tile>();
                tile = PathChecker(tile, nexttile, moves);
            }
            if (tilemanager.GetTileDic(new Vector2(tile.X, tile.Y - 1)) != null)
            {
                nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y - 1)).GetComponent<Tile>();
                tile = PathChecker(tile, nexttile, moves);
            }
            if (tilemanager.GetTileDic(new Vector2(tile.X, tile.Y + 1))!=null)
            { 
            nexttile = tilemanager.GetTileDic(new Vector2(tile.X, tile.Y + 1)).GetComponent<Tile>();
            tile = PathChecker(tile, nexttile, moves);
                }
        }
        float i = 0;
        while (moves.Count > 0)
        {
            #region ZachNotes
            //So im using a coroutine to do the movement currently he just teleports to spots we will have 
            //to adjust that with animations later
            #endregion ZachNotes
            // StartCoroutine(MoveToIndividualTile(moves.Pop(), i, tilemanager));
            StartCoroutine(MoveToIndividualTile(moves.Pop(), i, tilemanager));
            i += .5f;
        }
        tilemanager.DeSelectAllTiles();
        return i;

    }
    //So this lerp is weird needs work to get the player from point a to be
    //Smooth movement would need to be added  in this MoveToIndividualTile;
    public Vector3 travelpoint;
    private IEnumerator MoveToIndividualTile(Vector2 tile, float time, TileManager tilemanager)
    {
        #region ZachNotes
        //So I tried using a lerp to get the player to each spot smoothly but that didnt work :(
        //this function will queue up a movement by giving it a tile to move to and a time to get there
        //but its abit broken currently. (Works but is ugly)
        #endregion ZachNotes
        yield return new WaitForSeconds(time);
  
        float timepassed = 0;
        travelpoint = new Vector3(tilemanager.TileDic[tile].transform.position.x, tilemanager.TileDic[tile].transform.position.y + 2, tilemanager.TileDic[tile].transform.position.z);
        timepassed += Time.deltaTime;
        //this.transform.position = Vector3.Lerp(this.transform.position, travelpoint, timepassed/2);
        if (Vector3.Distance(this.transform.position, travelpoint) > 0.35f)
        {
            this.agent.destination = travelpoint;
            if (Anim != null)
            Anim.SetBool("isMoving", true);
        }
        else
        {
            if(Anim!=null)
            Anim.SetBool("isMoving", false);
        }
    }
    private void MoveToIndividualTileV2(Vector2 tile, TileManager tilemanager)
    {

        travelpoint = new Vector3(tilemanager.TileDic[tile].transform.position.x, tilemanager.TileDic[tile].transform.position.y + 2, tilemanager.TileDic[tile].transform.position.z);
        if (Vector3.Distance(this.transform.position, travelpoint) > 0.1f)
        {
            this.agent.nextPosition = travelpoint;
            if (Anim != null)
                Anim.SetBool("isMoving", true);
        }
        else
        {
            if (Anim != null)
                Anim.SetBool("isMoving", false);
        }
    }
    private Tile PathChecker(Tile tileshort,Tile nexttile,Stack<Vector2> moves)
    {
        #region ZachNotes
        //this checks if a tile has a lower movement value that itself
        //its the main component of the pathfinding system and its used in the 
        //MovetoFinalTile method
        //its adds the tile to the stack
        #endregion ZachNotes
        if (tileshort.Movementvalue < nexttile.GetComponent<Tile>().Movementvalue)
        {
            tileshort = nexttile;
            moves.Push(nexttile.Loc);
        }
        return tileshort;
    }

    //flood fill notes have the base movement value each square will branch out four new tiles till the movement stat is dead


    #endregion PlayerMovementMethods
    public void DisplayMovementRange()
    {
        #region ZachNotes
        //This function checks after a movement value has been assigned to a tile if its moveable
        //if it is it gives it a material to indicate so
        #endregion ZachNotes
        foreach (GameObject tile in TileManager.instance.TileDic.Values)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (tileshort.Movementvalue > 0 && !tileshort.IsSelected)
            {
                tileshort.Renderer.material = tileshort.Movemat;
            }
            else if (tileshort.Movementvalue == 0 && !tileshort.IsObstacle && !tileshort.IsSelected)
            {
                tileshort.Renderer.material = tileshort.Defaultmat;
            }
        }

    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (tag == "Player")
        {
            if (other.tag == "Tile")
            {
                #region ZachNotes
                //these should be on the player stats script cause we may want to disable this script in the future
                //or we can have another check to see when to call the updates but that may be cumbersome
                #endregion ZachNotes
                this.X = other.GetComponent<Tile>().X;
                this.Y = other.GetComponent<Tile>().Y;
                Position = new Vector2(X, Y);
                transform.parent = other.transform;
            }

        }
     

    }
  
 
    
    void Start()
    {
        stats = GetComponent<Bears>();
        tilemanager = TileManager.instance;  
        code = GameManager.instance;
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        //we need a system to say we are moving when we hit the move button and something to say display this bears movement range 
        //not the other bears
        if (tag == "Player")
        {
           // if(code.CurrPhase==GameManager.Phase.movementPhase&& GetComponent<Bears>().Selected)
            if (code.CurrPhase==GameManager.Phase.movementPhase && GetComponent<Bears>().Selected)
            {
                if (tilemanager.TileDic != null)
                    if (!OnlyOnce)
                    {
                        DisplayMovementRange();
                        OnlyOnce = true;
                    }
                if (!Movementcheck && !Moving)
                {
                    #region ZachFuckUps
                    //GetMovementRange1();
                    //for (int i = 0; i < moveList.Count; i++)
                    //    tiles.Add(tilemanager.GetTileDic(new Vector2(moveList[i].x, moveList[i].y)));

                    //}
                    //int half = tiles.Count / 2;
                    //starting tile
                    #endregion ZachFuckUps
                    GameObject startingtile = tilemanager.TileDic[new Vector2(this.X, this.Y)];
                    AssignTileMovementValue(startingtile, stats.Movement+1);
                    Movementcheck = true;
                    //AssignTileMovementValue(tiles[half],move+1);

                    ///////TEST//////
                    //GameObject testmove = tilemanager.TileDic[pathfindtest];
                    //This needs to only happen if you hit move
                    //MoveToFinalTile(testmove, startingtile);

                }
                if (executeMovement == true && !Moving)
                {
                    #region ZachNotes
                    //This occurs when a tile has been selected to move to
                    #endregion ZachNotes
                    float movementTimer;
                    GameObject startingtile = tilemanager.TileDic[new Vector2(this.X, this.Y)];
                    //movedestination is gotten by the tile selector
                    movementTimer = MoveToFinalTile(MoveDestination, startingtile,tilemanager/*GetComponent<Bears>().Movement*/);
               
                    //movementcheck = false;
                    ClearTileMovementValues(tilemanager);
                    Invoke("TurnOffMovementEnum", movementTimer + .1f);
                    hasMoved = true;
                    executeMovement = false;
                }
            }
            OnlyOnce = false;
        
        }

    }
    }
