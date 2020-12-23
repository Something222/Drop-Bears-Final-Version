using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy :EnemyAIBase
{
    private bool Onlyonce = false;
    protected void EndTurnTutorial()
    {
        AttackRange.ClearTileAttackValues(TileManager.instance);
        Movement.ClearTileMovementValues(TileManager.instance);
        Tutorial.instance.Section++;
        Acting = false;
        TurnCompleted = true;
        Tutorial.instance.OnlyOnce = false;
        EndTurn();
       // code.CurrPhase = GameManager.Phase.menuPhase;
        Onlyonce = false;
        
      
    }
    // Start is called before the first frame update
    void Start()
    {
        GetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.CurrPhase == GameManager.Phase.enemyPhase &&TakeTurn)
        {
           
            
            EndTurnTutorial();
            Onlyonce = true;
        }
    }
}
