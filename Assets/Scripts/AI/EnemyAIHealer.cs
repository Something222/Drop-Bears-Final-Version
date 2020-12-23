using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHealer : EnemyAIBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }
    private bool healself = false;
    // Update is called once per frame
    void Update()
    {

        if (code.CurrPhase == GameManager.Phase.enemyPhase && TakeTurn && stats.themBuffs["stun"] <= 0)
        {
            if (!OnlyOnce && stats.IsAlive)
            {
                Acting = true;
                OnlyOnce = true;
                GameObject startingTile = tileManager.TileDic[new Vector2(position.x, position.y)];
                AssignTileMovementValue(startingTile, stats.Movement + 1);
                AssignEnemyAttackSpacesHealer();
                if (Pairs.Count != 0)
                {

                    FindWeakestEnemyInRange();
                    if (tileToAttack.GetComponentInChildren<EnemyAIBase>() == null)
                    {
                        healself = true;
                    }
                    timer = mover.MoveToFinalTile(tileToMoveTo, startingTile.GetComponent<Tile>(), TileManager.instance);
                    if (!healself)
                        StartCoroutine(EnemyHeal(timer + .5f, tileToAttack));
                    else
                    {
                        StartCoroutine(SelfHeal(timer + .5f));
                        healself = false;
                    }
                    tileToMoveTo.IsEnemy = true;
                    Invoke("EndTurn", timer + .5f);
                }
                else
                {
                    StartCoroutine(SelfHeal(timer));
                    Invoke("EndTurn", timer + .5f);
                }

            }
            else if (!stats.IsAlive)
            {
                EndTurn();
            }

        }
        else if ((!stats.IsAlive|| stats.themBuffs["stun"] > 0 )&& GameManager.instance.CurrPhase == GameManager.Phase.enemyPhase && !OnlyOnce)
        {
            OnlyOnce = true;
            EndTurn();
        }
    }
}
