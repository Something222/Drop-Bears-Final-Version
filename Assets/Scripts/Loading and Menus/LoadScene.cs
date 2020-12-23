﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    private AsyncOperation async;
    private GameManager code;
    // Start is called before the first frame update
    public void BtnLoadScene()
    {
        if (async==null)
        {
            Scene currentScene = SceneManager.GetActiveScene();//current scene
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
            async.allowSceneActivation = true;
                
        }
    }
    public void SetSceneToLoad(int scene)
    {
        Loading.sceneToLoad = scene;
    }
   public void SetSceneToLoadNextLevel()
    {
        Loading.sceneToLoad = Loading.nextLevel;
    }
    public void DeclareNextLevel(int levelIndex)
    {
        Loading.nextLevel = levelIndex;
    }
    public void BtnLoadScene(int i)
    {
        if (async == null)
        {

            async = SceneManager.LoadSceneAsync(i);//load next scene
            async.allowSceneActivation = true;//you can change it to false to allow for loading in cut scenes and menus and stuff
        }
    }
    public void BtnLoadScene(string s)
    {
        if (async == null)
        {

            async = SceneManager.LoadSceneAsync(s);//load next scene
            async.allowSceneActivation = true;//you can change it to false to allow for loading in cut scenes and menus and stuff
        }
    }
    public void EnterLoadingScreen()
    {
        if (async == null)
        {

            async = SceneManager.LoadSceneAsync("LoadingScreen");//load next scene
            async.allowSceneActivation = true;//you can change it to false to allow for loading in cut scenes and menus and stuff
        }
    }
    void Start()
    {
        code = GameManager.instance;
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
