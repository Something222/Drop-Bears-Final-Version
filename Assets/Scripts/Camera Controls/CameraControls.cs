using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControls : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    private SquadSelection squadmanager;
    private TileSelector tileSelector;
    private CinemachineVirtualCamera camera;
    private EnemyManager enemyManager;

    void Start()
    {
        code = GameManager.instance;
        squadmanager = SquadSelection.instance;
        tileSelector = TileSelector.instance;
        enemyManager = EnemyManager.instance;
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (code.CurrPhase)
        {
            case GameManager.Phase.menuPhase:
                camera.LookAt = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
                camera.Follow = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
                break;
            case GameManager.Phase.movementPhase:
            case GameManager.Phase.attackPhase:
            case GameManager.Phase.mapPhase:
                if (tileSelector.CurrentTile != null && !squadmanager.Squad[squadmanager.Selected].GetComponent<Movement>().Moving)
                {
                    camera.LookAt = tileSelector.CurrentTile.GetComponent<Transform>();
                    camera.Follow = tileSelector.CurrentTile.GetComponent<Transform>();
                }
                else if (squadmanager.Squad[squadmanager.Selected].GetComponent<Movement>().Moving)
                {
                    camera.LookAt = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
                    camera.Follow = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
                }
                break;
            case GameManager.Phase.enemyPhase:
                if (enemyManager.ActingEnemy != null)
                {
                    camera.LookAt = enemyManager.ActingEnemy.transform;
                    camera.Follow = enemyManager.ActingEnemy.transform;
                }
                break;


        }
        #region zachfuckupsnonenum
        //if (code.MenuPhase)
        //{
        //    camera.LookAt = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
        //    camera.Follow = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
        //}
        //else if (code.MovementPhase || code.AttackPhase)
        //{
        //    if (tileSelector.CurrentTile != null&&!squadmanager.Squad[squadmanager.Selected].GetComponent<Movement>().Moving)
        //    {
        //        camera.LookAt = tileSelector.CurrentTile.GetComponent<Transform>();
        //        camera.Follow = tileSelector.CurrentTile.GetComponent<Transform>();
        //    }
        //   else if(squadmanager.Squad[squadmanager.Selected].GetComponent<Movement>().Moving)
        //    {
        //        camera.LookAt = squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
        //        camera.Follow= squadmanager.Squad[squadmanager.Selected].GetComponent<Transform>();
        //    }
        //}
        //else if (code.EnemyPhase)
        //{
        //    if (enemyManager.ActingEnemy != null)
        //    {
        //        camera.LookAt = enemyManager.ActingEnemy.transform;
        //        camera.Follow = enemyManager.ActingEnemy.transform;
        //    }
        //}
        #endregion zachfuckupsnonenum
    }
}
