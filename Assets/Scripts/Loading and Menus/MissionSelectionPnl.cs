using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionSelectionPnl : MonoBehaviour
{
    [SerializeField] private Button btnBoss;
    //1 Sheep,2 Buck, 3 Lion
    [SerializeField] private Button[] btnLevels;
    [SerializeField] public static bool[] levelsComplete=new bool[3];
    private bool bossUnlock;
    
    void Start()
    {
      
    }
    public void MissionDesc(int missionnmb)
    {
        PnlDebrieffing.MissionNMB = missionnmb;
    }
    // Update is called once per frame
    private void OnEnable()
    {
        bossUnlock = true;
        for (int i = 0; i < levelsComplete.Length; i++)
        {
            if (levelsComplete[i] == true)
            {
                btnLevels[i].interactable = false;
            }
            else
                bossUnlock = false;
        }
        if (bossUnlock == true)
            btnBoss.interactable = true;
        else
            btnBoss.interactable = false;
    }
    void Update()
    {
      
    }
}
