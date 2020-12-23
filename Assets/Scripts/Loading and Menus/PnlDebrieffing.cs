using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PnlDebrieffing : MonoBehaviour
{
    public static int MissionNMB;
    [SerializeField] private TextMeshProUGUI missionDesc;
    [SerializeField] private TextMeshProUGUI missionName;
    private void OnEnable()
    {
        switch (MissionNMB)
        {
            //Sheep
            case 1:
                //enter your mission descriptions here if you dont like what i wrote
                missionName.text = "OPERATION: Breaking Baaaaad ";
                missionDesc.text = "Something is amiss in the sheep kingdom. They've been quite for some time now." +
                    " I for one don't trust them. They must be up to something! Perhaps it's the dark arts! " +
                    "Get over there NOW, prevent the resurrection of Diablo, and save the world! ";
                break;
                //Buck
            case 2:
                missionName.text = "OPERATION: OH DEER ";
                missionDesc.text = "The buck kingdom has grown more and more aggressive over the years. Combine this with the results of" +
                    " their new 3 month total calisthenics get yoked workout plan, they've become a real threat. Get over there NOW and stop them from" +
                    " becoming too powerful, and save the world!";
                break;
                //lion
            case 3:
                missionName.text = "OPERATION: Roaring Pride ";
                missionDesc.text = "The usually prideful and magestic lion clan is up to no good! They've become become pompous, brash " +
                    "and quite, quite rude. They also keep stealing our women and thats just not cool. Get over there NOW, sort this out, and save the world";
                break;
                //Boss
            case 4:
                missionName.text = "OPERATION: Bad News Bears ";
                missionDesc.text = "What is going on here two of our own kind have defected against our cause! GET OVER THERE NOW AND END THEM!";
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
