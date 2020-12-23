using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBase : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<AttackTilePairings> pairs = new List<AttackTilePairings>();
    protected bool getstats = false;
    protected GameManager code;
    [SerializeField] protected int attackRange;
    [SerializeField] protected int x;
    [SerializeField] protected int y;
    protected const float healMult = 2.5f;
    protected Vector2 position;
    protected TileManager tileManager;
    [SerializeField] protected List<GameObject> tilesInMovementRange = new List<GameObject>();
    protected Dictionary<Vector2, Tile> playersInRange = new Dictionary<Vector2, Tile>();
    private bool onlyOnce = false;
    //private List<Vector2> playerPositions;
    protected AttackRange atkRangeMethods;
    protected SquadSelection squadManager;
    [SerializeField] protected Tile finalAttackTarget;
    [SerializeField] private Vector2 finalMoveTarget;
    protected bool getvariables = false;
    protected Movement mover;
    [SerializeField] private bool turnCompleted = false;
    protected float timer;
    protected Bears stats;
    private bool acting = false;
    [SerializeField] private bool takeTurn = false;
    protected int lowesthp;
    protected Tile tileToAttack;
    protected Tile tileToMoveTo;

    #endregion Variables
    //not really neccesary but i wanna learn how to use delegates so imma try that
    protected delegate Vector2 WeakestOnMap(Vector2 playerPos, int lowestHp);
    protected delegate void WeakestInRange(int lowesthp, Tile tileToAttack, Tile TileToMoveTo);
    #region Properties
    public Dictionary<Vector2, Tile> PlayersInRange { get => playersInRange; set => playersInRange = value; }
    public bool TakeTurn { get => takeTurn; set => takeTurn = value; }
    public bool OnlyOnce { get => onlyOnce; set => onlyOnce = value; }
    public bool TurnCompleted { get => turnCompleted; set => turnCompleted = value; }
    public bool Acting { get => acting; set => acting = value; }
    public List<AttackTilePairings> Pairs { get => pairs; set => pairs = value; }
    protected Vector2 FinalMoveTarget { get => finalMoveTarget; set => finalMoveTarget = value; }
    #endregion Properties


   protected void CheckEnemyTurns()
    {
        bool check = false;
        for (int i = 0; i <EnemyManager.instance.Enemies.Length; i++)
        {
            if (EnemyManager.instance.Enemies[i].GetComponent<EnemyAIBase>().TurnCompleted==false)
            {
                check = true;

            }
        }
        if (check == false)
        {
            GameManager.instance.onlyOnce = false;

            EnemyManager.instance.SwitchToPlayerTurnsEnum();
       
        }
       
    }
    protected void ClearArrays()
    {
        tilesInMovementRange.Clear();
        playersInRange.Clear();
        pairs.Clear();
    }
    protected void EndTurn()
    {
        AttackRange.ClearTileAttackValues(tileManager);
        Movement.ClearTileMovementValues(tileManager);
        ClearArrays();
        Acting = false;
        TurnCompleted = true;
        if(mover!=null)
        mover.Anim.SetBool("isMoving", false);
        CheckEnemyTurns();
     
    }

    protected IEnumerator EnemyHeal(float timer, Tile tileToHeal)
    {
        yield return new WaitForSeconds(timer);
        if (tileToHeal.GetComponentInChildren<Bears>() != null)
        {
            mover.Anim.SetTrigger("Attacking");
            Bears[] targets = tileToHeal.GetComponentsInChildren<Bears>();
            Bears target = null;
            foreach (Bears enemy in targets)
            {
                if (enemy.enabled)
                    target = enemy;
            }
            target.Hp += (int)(stats.AttackStrength * healMult);
            target.Anim.SetTrigger("GettingAttacked");
            if (target.Hp > target.TotalHP)
            {
                target.Hp = target.TotalHP;
            }
        }
    }
    protected IEnumerator SelfHeal(float timer)
    {
        yield return new WaitForSeconds(timer);
        mover.Anim.SetTrigger("Attacking");
        stats.Hp += (int)(stats.AttackStrength * healMult);
        if (stats.Hp > stats.TotalHP)
        {
            stats.Hp = stats.TotalHP;
        }
    }
    protected IEnumerator EnemyAttackEnum(float timer, Tile tileToAttack)
    {
        yield return new WaitForSeconds(timer);
        mover.Anim.SetTrigger("Attacking");
        stats.EnemyAttack(tileToAttack);
        
        //trigger for attack animations 
    }
  protected IEnumerator EnemyBasicAbility(float timer, Tile tileToAttack)
    {
        yield return new WaitForSeconds(timer);
        mover.Anim.SetTrigger("Attacking");
        stats.Ability1(tileToAttack);

    }
    protected IEnumerator EnemySpecialAbility(float timer, Tile tileToAttack)
    {
        yield return new WaitForSeconds(timer);
        mover.Anim.SetTrigger("Attacking");
        stats.Ability2(tileToAttack);

    }
    protected IEnumerator EnemyMeleeBossSpecial(float timer,Tile tileToAttack)
    {
        yield return new WaitForSeconds(timer);
        mover.Anim.SetTrigger("Attacking");

    }
    protected void AssignEnemyAttackSpaces()
    {
        foreach (GameObject tile in tilesInMovementRange)
        {
            atkRangeMethods.GetAttackRangeIgnoreObstaclesEnemies(stats.Range, tile.GetComponent<Tile>(), this);
        }
    }
    protected void AssignEnemyAttackSpacesHealer()
    {
        foreach (GameObject tile in tilesInMovementRange)
        {
            atkRangeMethods.GetAttackRangeIgnoreObstaclesEnemyHealer(stats.Range, tile.GetComponent<Tile>(), this);
        }
    }
    
    protected void CheckForPlayersInRange()
    {
        foreach (GameObject playertile in squadManager.Squad)
        {
            #region ZachNotes
            //this takes the players positions and cross refernces them with the enemies attack range
            #endregion ZachNotes
            if (tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponent<Tile>().Attackvalue > 0 && tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponentInChildren<Bears>().IsAlive)
            {
                PlayersInRange.Add(playertile.GetComponent<Movement>().Position, tileManager.TileDic[playertile.GetComponent<Movement>().Position].GetComponent<Tile>());
            }
        }
    }

    protected void FindAttackPosition()
    {
        AttackRange.ClearTileAttackValues(tileManager);
        atkRangeMethods.AssignDescendingTileAttackRange(finalAttackTarget, attackRange + 1);
        int furthestblock = 0;
        #region ZachNotes
        //So this section has the enemy move to his max range to attack the player instead of min range.
        //basically earlier I assigned from the player attack values to blocks
        //I just tell the enemy go to the block with the lowest attack value in your movement range
        #endregion ZachNotes
        foreach (GameObject tile in tilesInMovementRange)
        {
            if (tile.GetComponent<Tile>().Attackvalue > 0 && !tile.GetComponent<Tile>().IsEnemy && !tile.GetComponent<Tile>().IsPlayer)
            {
                if (furthestblock == 0)
                {
                    furthestblock = tile.GetComponent<Tile>().Attackvalue;
                    FinalMoveTarget = tile.GetComponent<Tile>().Loc;
                }
                else if (furthestblock > tile.GetComponent<Tile>().Attackvalue)
                {
                    furthestblock = tile.GetComponent<Tile>().Attackvalue;
                    FinalMoveTarget = tile.GetComponent<Tile>().Loc;
                }
            }
        }

    }
    #region nondelegate
    protected Vector2 FindWeakestPlayerOnMap()
    {
        Vector2 playerPos = new Vector2();
        int lowestHp = -1;
        foreach (GameObject player in squadManager.Squad)
        {
            #region ZachNotes
            //checks all players for lowest hp takes that position
            #endregion ZachNotes
            if (lowestHp == -1 && player.GetComponent<Bears>().IsAlive)
            {
                lowestHp = player.GetComponent<Bears>().Hp;
                playerPos = player.GetComponent<Movement>().Position;

            }
            else if (lowestHp > player.GetComponent<Bears>().Hp && player.GetComponent<Bears>().IsAlive)
            {
                lowestHp = player.GetComponent<Bears>().Hp;
                playerPos = player.GetComponent<Movement>().Position;
            }
        }
        return playerPos;
    }

    protected Vector2 FindWeakestEnemyOnMap()
    {
        //this dont account for obstacles so its abit weird
        //i would need to basically figure out a* to account for obstacles
        Vector2 playerPos = new Vector2();
        int lowestHp = -1;

        foreach (GameObject enemy in EnemyManager.instance.Enemies)
        {
            #region ZachNotes
            //checks all players for lowest hp takes that position
            #endregion ZachNotes
            if (lowestHp == -1 && enemy.GetComponent<Bears>().IsAlive)
            {
                lowestHp = enemy.GetComponent<Bears>().Hp;
                playerPos = enemy.GetComponent<Movement>().Position;

            }
            else if (lowestHp > enemy.GetComponent<Bears>().Hp && enemy.GetComponent<Bears>().IsAlive)
            {
                lowestHp = enemy.GetComponent<Bears>().Hp;
                playerPos = enemy.GetComponent<Movement>().Position;
            }
        }
        return playerPos;
    }
    #endregion nondelegate
    #region delegateversion
    protected Vector2 PlayerFind(Vector2 playerPos, int lowestHp)
    {
        foreach (GameObject player in squadManager.Squad)
        {
            #region ZachNotes
            //checks all players for lowest hp takes that position
            #endregion ZachNotes
            if (lowestHp == -1 && player.GetComponent<Bears>().IsAlive)
            {
                lowestHp = player.GetComponent<Bears>().Hp;
                playerPos = player.GetComponent<Movement>().Position;

            }
            else if (lowestHp > player.GetComponent<Bears>().Hp && player.GetComponent<Bears>().IsAlive)
            {
                lowestHp = player.GetComponent<Bears>().Hp;
                playerPos = player.GetComponent<Movement>().Position;
            }
        }
        return playerPos;
    }
    protected Vector2 EnemyFind(Vector2 playerPos, int lowestHp)
    {
        foreach (GameObject enemy in EnemyManager.instance.Enemies)
        {
            #region ZachNotes
            //checks all players for lowest hp takes that position
            #endregion ZachNotes
            if (lowestHp == -1 && enemy.GetComponent<Bears>().IsAlive)
            {
                lowestHp = enemy.GetComponent<Bears>().Hp;
                playerPos = enemy.GetComponent<Movement>().Position;

            }
            else if (lowestHp > enemy.GetComponent<Bears>().Hp && enemy.GetComponent<Bears>().IsAlive)
            {
                lowestHp = enemy.GetComponent<Bears>().Hp;
                playerPos = enemy.GetComponent<Movement>().Position;
            }
        }
        return playerPos;

    }
    protected Vector2 FindWeakestOnMap(WeakestOnMap typetofind)
    {
        
        Vector2 playerPos = new Vector2();
        int lowestHp = -1;
        return (typetofind(playerPos,lowestHp));
        
    }
  
    #endregion delegateversion
    protected void FindWeakestPlayerInRange()
    {
        if (pairs.Count <= 0)
            return;
         lowesthp = Pairs[0].PlayerTile.GetComponentInChildren<Bears>().Hp;
         tileToAttack = Pairs[0].PlayerTile;
         tileToMoveTo = Pairs[0].EnemyTile;
       
        foreach (AttackTilePairings attackpairs in Pairs)
        {
            if (lowesthp > attackpairs.PlayerTile.GetComponentInChildren<Bears>().Hp)
            {
                lowesthp = attackpairs.PlayerTile.GetComponentInChildren<Bears>().Hp;
             
                tileToAttack = attackpairs.PlayerTile;
                tileToMoveTo = attackpairs.EnemyTile;
               
            }
        }
    }
    protected void FindWeakestEnemyInRange()
    {
        if (pairs.Count <= 0)
            return;
        lowesthp = Pairs[0].PlayerTile.GetComponentInChildren<Bears>().Hp;
        tileToAttack = Pairs[0].PlayerTile;
        tileToMoveTo = Pairs[0].EnemyTile;

        foreach (AttackTilePairings attackpairs in Pairs)
        {
            if (lowesthp > attackpairs.PlayerTile.GetComponentInChildren<Bears>().Hp)
            {
                lowesthp = attackpairs.PlayerTile.GetComponentInChildren<Bears>().Hp;
                tileToAttack = attackpairs.PlayerTile;
                tileToMoveTo = attackpairs.EnemyTile;
            }
        }
    }
    protected void FindTileNearWeakestPlayerOnMap(Vector2 playerPos,GameObject startingtile)
    {

        int lowestdistance = -1;
        Vector2 finalMoveTarget = startingtile.GetComponent<Tile>().Loc;
        foreach (GameObject tile in tilesInMovementRange)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsEnemy && !tileshort.IsPlayer)
            {
                #region ZachNotes
                //now i check all the tile in the bears movement range that arent occupied
                //And i see how far they are to the weakest bear
                //I then so go to the closest tile
                #endregion ZachNotes
                int distance = ((int)Mathf.Abs(tileshort.X - playerPos.x)) + ((int)Mathf.Abs(tileshort.Y - playerPos.y));
                if (lowestdistance == -1)
                {
                    lowestdistance = distance;
                    FinalMoveTarget = tileshort.Loc;
                }
                else if (lowestdistance > distance)
                {
                    lowestdistance = distance;
                    FinalMoveTarget = tileshort.Loc;
                }
            }
        }
    }
    protected void FindTileNearWeakestPlayerOnMapAccountForObstacles(Vector2 playerPos, GameObject startingtile)
    {
        //I kinda cheat it a little bit for the account for obstacles
        int lowestdistance = -1;
        FinalMoveTarget = startingtile.GetComponent<Tile>().Loc;
        foreach (GameObject tile in tilesInMovementRange)
        {
            Tile tileshort = tile.GetComponent<Tile>();
            if (!tileshort.IsEnemy && !tileshort.IsPlayer)
            {
            
                int distance = ((int)Mathf.Abs(tileshort.X - playerPos.x)) + ((int)Mathf.Abs(tileshort.Y - playerPos.y));
                Tile adjacentTile = tileManager.GetAdjacentTile(1, tileshort);
                if (adjacentTile != null && adjacentTile.IsObstacle)
                    distance += 1;
                adjacentTile = tileManager.GetAdjacentTile(2, tileshort);
                if (adjacentTile != null && adjacentTile.IsObstacle)
                    distance += 1;
                adjacentTile = tileManager.GetAdjacentTile(3, tileshort);
                if (adjacentTile != null && adjacentTile.IsObstacle)
                    distance += 1;
                adjacentTile = tileManager.GetAdjacentTile(4, tileshort);
                if (adjacentTile!=null &&adjacentTile.IsObstacle)
                    distance += 1;
                if (lowestdistance == -1)
                {
                    lowestdistance = distance;
                    FinalMoveTarget = tileshort.Loc;
                }
                else if (lowestdistance > distance)
                {
                    lowestdistance = distance;
                    FinalMoveTarget = tileshort.Loc;
                }
            }
        }
    }
    public void AssignTileMovementValue(GameObject tile, int move)
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
        if (tile != null)
        {
            if (tile == tileManager.TileDic[position])
            {
                Tile tileshort = tile.GetComponent<Tile>();
                if (!tileshort.IsObstacle)
                {
                    if (tileshort.Movementvalue >= 0 && tileshort.Movementvalue < move)
                    {
                        tileshort.Movementvalue = move;
                        if (move >= 0 )
                        {
                            tilesInMovementRange.Add(tile);
                            tileshort.Moveable = true;
                        }

                        GameObject nexttile = tileManager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tileManager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tileManager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tileManager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                    }
                }
            }
            else
            {
                Tile tileshort = tile.GetComponent<Tile>();
                if (!tileshort.IsObstacle && !tileshort.IsEnemy && !tileshort.IsPlayer)
                {
                    if (tileshort.Movementvalue >= 0 && tileshort.Movementvalue < move)
                    {
                        tileshort.Movementvalue = move;
                        if (move >= 0)
                        {
                            tilesInMovementRange.Add(tile);
                            tileshort.Moveable = true;
                        }
                        GameObject nexttile = tileManager.GetTileDic(new Vector2(tileshort.X - 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tileManager.GetTileDic(new Vector2(tileshort.X + 1, tileshort.Y));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tileManager.GetTileDic(new Vector2(tileshort.X, tileshort.Y - 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);
                        nexttile = tileManager.GetTileDic(new Vector2(tileshort.X, tileshort.Y + 1));
                        AssignTileMovementValue(nexttile, tileshort.Movementvalue - 1);

                    }
                }
                else if (tileshort.IsEnemy || tileshort.IsPlayer || tileshort.IsObstacle)
                {
                    tile.GetComponent<Tile>().Movementvalue = -1;
                }
            }
        }



    }

    protected void IndividualRangeCheck(int atkRange, GameObject currentTile)
    {
        atkRangeMethods.AssignDescendingTileAttackRange(currentTile, atkRange);
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tile")
        {
            #region ZachNotes

            #endregion ZachNotes
            this.x = other.GetComponent<Tile>().X;
            this.y = other.GetComponent<Tile>().Y;
            position = new Vector2(x, y);
            transform.parent = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            other.GetComponent<Tile>().IsSelected = false;
        }
    }
    // Start is called before the first frame update

    protected void GetVariables()
    {
        atkRangeMethods = GetComponent<AttackRange>();
        tileManager = TileManager.instance;
        code = GameManager.instance;
        
        attackRange = GetComponent<AttackRange>().Range;
        squadManager = SquadSelection.instance;
        mover = GetComponent<Movement>();
        stats = GetComponent<Bears>();
    }
   

    // Update is called once per frame
   
}
