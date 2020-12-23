using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using TMPro;
public class Loading : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] private Image loadingbar;
    [SerializeField] private TextMeshProUGUI txtPercent;
    [SerializeField] private Image image;
    private bool ready = false;
    private float speed = .7f;//for the fill speed of the image
    [SerializeField]private TextMeshProUGUI confTxt;
    [SerializeField] public static int sceneToLoad = -1; //so static allows other scripts to access this variable and change it, god damnit
    private bool activated = false;
    [SerializeField] private bool waitForUserInput = true;//If true, the user has to press a key
    [SerializeField] private TextMeshProUGUI txtTips;
    public static int nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        if (image)
        image.fillAmount = 0;
        confTxt.GetComponent<TextMeshProUGUI>().enabled = false;
        Time.timeScale = 1;//Reset timescale
        Input.ResetInputAxes();//Reset the input (for 1 frame)
        System.GC.Collect();//Call the garbage collector
        Scene currentScene = SceneManager.GetActiveScene();//current scene 
        if (sceneToLoad == -1)
        {
            async = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);//load next scene
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        async.allowSceneActivation = false;//Dont go to next scene right away
        int random = Random.Range(0, 5);
        switch (random)
        {
            case 0:
                txtTips.text = "The Green Bear sure is hardy make sure to keep him in the frontline. He's also the only bear without a proper name.";
                break;
            case 1:
                txtTips.text = "Pinky is a sweet one with the power of love she can heal you back even from death.";
                break;
            case 2:
                txtTips.text = "Despite what you may think berny (the red bear) actually grew up in Idaho";
                break;
            case 3:
                txtTips.text = "Tod the blue bear is an idiot";
                break;
            case 4:
                txtTips.text = "The black bear tries to convince others that he is dark and mysterious, its tragic that his name is Tiffany";
                break;
        }
    }
    
    public void Activate()
    {
        ready = true;
    }
   
    public IEnumerator BlinkyText(TextMeshProUGUI txt)
    {
        while (true)//infinite loop 
        {
            yield return new WaitForSeconds(.5f);
            txt.GetComponent<TextMeshProUGUI>().enabled = true;
            yield return new WaitForSeconds(.5f);
            txt.GetComponent<TextMeshProUGUI>().enabled = false;
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(async.progress>.89f && activated==false)
        {  
            StartCoroutine(BlinkyText(confTxt));
            activated = true;
        }
        if (waitForUserInput && Input.anyKey)
        {
            ready = true;
        }
        if (loadingbar)
        {
            loadingbar.fillAmount = async.progress + .1f;
        }
        if (txtPercent)
        {
            txtPercent.text = ((async.progress + .1f) * 100).ToString("f2") + " %";
        }
        if (image)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, 1+image.fillAmount, Time.deltaTime * speed);
            if (image.fillAmount>=1)
            {
                image.fillAmount = 0;
            }
        }
       
        if (async.progress > 0.89f && SplashScreen.isFinished && ready)
        {
          
            async.allowSceneActivation = true;
           
        }
    }
}
