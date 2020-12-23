using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiAgressiveV2 : EnemyAIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        //if(code.Phase=enemyPhase &&TakeTurn)
        if (code.CurrPhase == GameManager.Phase.enemyPhase && TakeTurn && stats.themBuffs["stun"] <= 0)
        {
            if (!OnlyOnce && stats.IsAlive)
            {
                Acting = true;
                GameObject startingTile;
                OnlyOnce = true;

                startingTile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingTile, stats.Movement + 1);
                AssignEnemyAttackSpaces();

                if (Pairs.Count != 0 && tilesInMovementRange.Count != 0)
                {

                    FindWeakestPlayerInRange();

                    timer = mover.MoveToFinalTile(tileToMoveTo, startingTile.GetComponent<Tile>(), TileManager.instance);
                    StartCoroutine(EnemyAttackEnum(timer + .5f, tileToAttack));

                    tileToMoveTo.IsEnemy = true;
                    AttackRange.ClearTileAttackValues(tileManager);
                    Movement.ClearTileMovementValues(tileManager);
                    //this would need to be commented
                    Invoke("EndTurn", timer + .5f);
                }
                else if (Pairs.Count == 0 && stats.Movement > 0)
                {
                    Vector2 playerPos = FindWeakestOnMap(PlayerFind);
                    FindTileNearWeakestPlayerOnMapAccountForObstacles(playerPos, startingTile);
                    //bug
                    timer = mover.MoveToFinalTile(tileManager.TileDic[FinalMoveTarget], startingTile, TileManager.instance);
                    tileManager.TileDic[FinalMoveTarget].GetComponent<Tile>().IsEnemy = true;
                    Invoke("EndTurn", timer);
                }



            }
            else if (!stats.IsAlive)
            {
                EndTurn();
            }


        }
        else if ((!stats.IsAlive || stats.themBuffs["stun"] > 0 )&& GameManager.instance.CurrPhase == GameManager.Phase.enemyPhase && !OnlyOnce)
        {
            OnlyOnce = true;
            EndTurn();
        }

    }
}