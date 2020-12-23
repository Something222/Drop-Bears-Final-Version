using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameManager code;
    [SerializeField]private bool selected = false;
    private AsyncOperation async;

    public bool Selected { get => selected; set => selected = value; }

    void Start()
    {
        code = GameManager.instance;
       
    }
   //public void SwitchToMovement()
   // {
   //     code.MovementPhase = true;
   //     code.MenuPhase = false;
   // }
   // public void SwitchToMap()
   // {
      
   //     code.MenuPhase = false;
   // }
   // public void SwitchToAttack()
   // {
   //     code.AttackPhase = true;
   //     code.MenuPhase = false;
   // }
    public void SwitchToAttackEnum()
    {
        AttackRange.ClearTileAttackValues();
        code.CurrPhase = GameManager.Phase.attackPhase;
    }
    public void SwitchToMovementEnum()
    {
        code.CurrPhase = GameManager.Phase.movementPhase;
    }
    public void PassAttack()
    {
        TileSelector.instance.AbilityToUse = 1;
    }
    public void PassAbility1()
    {
        TileSelector.instance.AbilityToUse = 2;
    }
    public void PassAbility2()
    {
        TileSelector.instance.AbilityToUse = 3;
    }
    public void SwitchToMapEnum()
    {
        code.CurrPhase = GameManager.Phase.mapPhase;
    }
    public void ReloadLevel()
    {
        if (async == null)
        {
           
            async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            async.allowSceneActivation = true;

        }
    }
    public void SheepLevelComplete()
    {
        MissionSelectionPnl.levelsComplete[0] = true;

    }
    public void BuckLevelComplete()
    {
        MissionSelectionPnl.levelsComplete[1] = true;

    }
    public void LionLevelComplete()
    {
        MissionSelectionPnl.levelsComplete[2] = true;

    }
    public void BtnLoadScene()
    {
        if (async == null)
        {
            Scene currentScene = SceneManager.GetActiveScene();//current scene
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
            async.allowSceneActivation = true;

        }
    }
    public void ConfirmAttack()
    {
        TileSelector.instance.attackConf = true;
    }
    public void CancelAttack()
    {
        AttackRange.ClearTileAttackValues();
        GameManager.instance.CurrPhase = GameManager.Phase.attackPhase;
    }
    public void EndTurn()
    {
        SquadSelection.instance.Squad[SquadSelection.instance.Selected].GetComponent<Bears>().TurnComplete = true;
        for (int i=0;i<SquadSelection.instance.Squad.Length;i++)
        {
            if (SquadSelection.instance.Squad[i].GetComponent<Bears>().IsAlive&&!SquadSelection.instance.Squad[i].GetComponent<Bears>().TurnComplete)
            {
                SquadSelection.instance.Selected = i;
            }
        }
    }
   
    // Update is called once per frame
    
}
