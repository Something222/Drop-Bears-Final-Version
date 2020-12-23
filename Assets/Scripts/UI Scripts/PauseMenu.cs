using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    //public static PauseMenu instance;
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private GameManager manager;

    public GameObject PauseMenuUI;
    [SerializeField] private GameObject pnlOptions;
    float saveTimeScale;
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnPause;
    private bool activeOptions;

    //private AsyncOperation async; //<--For future Shit // nvm
    private void Start()
    {
        manager = GameManager.instance;
        activeOptions = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.started && (manager.CurrPhase == GameManager.Phase.menuPhase || manager.CurrPhase == GameManager.Phase.enemyPhase || manager.CurrPhase == GameManager.Phase.pausePhase))
        {
            if(manager.CurrPhase == GameManager.Phase.pausePhase&&!activeOptions)
            {
        
                Resume();
            }
            else if(manager.CurrPhase == GameManager.Phase.pausePhase && activeOptions)
            {
                Options();
            }
            else if (manager.CurrPhase == GameManager.Phase.menuPhase || manager.CurrPhase == GameManager.Phase.enemyPhase)
            {
                manager.savedPhase = manager.CurrPhase;
                manager.CurrPhase = GameManager.Phase.pausePhase;
                Pause();
                btnPause.Select();
            }
       
      
        }
    }
    // Update is called once per frame
    void Update()
    {
        #region OldStyleForMenu
        //if (onlyOnce)
        //{
        //    if (isGamePaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
        #endregion OldStyleForMenu

    }

    public void Resume()
    {
        manager.CurrPhase = manager.savedPhase;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
    }
    void Pause()
    {
        saveTimeScale = Time.timeScale;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
   public void Options()
    {
        if(!activeOptions)
        {
            activeOptions = true;
            pnlOptions.SetActive(true);
            PauseMenuUI.SetActive(false);
            btnBack.Select();
        }
        else
        {
            activeOptions = false;
            pnlOptions.SetActive(false);
            PauseMenuUI.SetActive(true);
            btnPause.Select();
        }
    }

    public void Quit()
    {
        
        Application.Quit();
//        #region NianMistakes
//        //#region QuitFromEditor
//        //#if UNITY_EDITOR
//        //        UnityEditor.EditorApplication.isPlaying = false;
//        //#else
//        Application.Quit();
////#endif
//        //#endregion QuitFromEditor
//        #endregion NianMistakes
    }
}
