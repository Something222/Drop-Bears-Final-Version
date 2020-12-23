using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BearPanel : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] Button[] originalSkills;
   [SerializeField] Button[] newSkills;
    [SerializeField ]GameObject subPanel;
   [SerializeField] public int color;
    [SerializeField] private TextMeshProUGUI newAbilityName;
    [SerializeField] private TextMeshProUGUI newAbilityDesc;
    private CurrentAbilities currentBear;
    private int basicOrSpecial=1;
    [SerializeField] Button btnSwap;
  
    public void SetCurrentPanel(int colour)
    {
        color = colour;
    }
    private void CheckBear()
    {
        switch (color)
        {
            case 0:
                currentBear = gameObject.AddComponent<RedBearAbilities>();
                originalSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = RedBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = RedBearAbilities.specialAbility.Name;
                break;
            case 1:
                currentBear = gameObject.AddComponent<BlueBearAbilities>();
                originalSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = BlueBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = BlueBearAbilities.specialAbility.Name;
                break;
            case 2:
                currentBear = gameObject.AddComponent<BlackBearAbilities>();
                originalSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = BlackBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = BlackBearAbilities.specialAbility.Name;
                break;
            case 3:
                currentBear = gameObject.AddComponent<PinkBearAbilities>();
                originalSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = PinkBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = PinkBearAbilities.specialAbility.Name;
                break;
            case 4:
                currentBear = gameObject.AddComponent<GreenBearAbilities>();
                originalSkills[0].GetComponentInChildren<TextMeshProUGUI>().text = GreenBearAbilities.basicAbility.Name;
                originalSkills[1].GetComponentInChildren<TextMeshProUGUI>().text = GreenBearAbilities.specialAbility.Name;
                break;
        }
        subPanel.SetActive(false);

    }
    private void OnEnable()
    {
        CheckBear();
      
    }
    public void EnableBasicAbilitySubPanel()
    {
        switch (color)
        {
            case 0:
                newAbilityName.text = RedBearAbilities.notEquippedBasicAbility.Name;
                newAbilityDesc.text = RedBearAbilities.notEquippedBasicAbility.GetAbilityDesc(new RedBear().AttackStrength);
              
                break;
            case 1:
                newAbilityName.text = BlueBearAbilities.notEquippedBasicAbility.Name;
                newAbilityDesc.text = BlueBearAbilities.notEquippedBasicAbility.GetAbilityDesc(new BlueBear().AttackStrength);
                break;
            case 2:
                newAbilityName.text = BlackBearAbilities.notEquippedBasicAbility.Name;
                newAbilityDesc.text = BlackBearAbilities.notEquippedBasicAbility.GetAbilityDesc(new BlackBear().AttackStrength);
                break;
            case 3:
                newAbilityName.text = PinkBearAbilities.notEquippedBasicAbility.Name;
                newAbilityDesc.text = PinkBearAbilities.notEquippedBasicAbility.GetAbilityDesc(new PinkBear().AttackStrength);
                break;
            case 4:
                newAbilityName.text = GreenBearAbilities.notEquippedBasicAbility.Name;
                newAbilityDesc.text = GreenBearAbilities.notEquippedBasicAbility.GetAbilityDesc(new GreenBear().AttackStrength);
                break;
        }
        DisableLeftButtons();
        subPanel.SetActive(true);
        basicOrSpecial = 1;
        btnSwap.Select();
    }
    public void EnableSpecialAbilitySubPanel()
    {
        switch (color)
        {
            case 0:
              
                newAbilityName.text = RedBearAbilities.notEquippedSpecialAbility.Name;
                newAbilityDesc.text = RedBearAbilities.notEquippedSpecialAbility.GetAbilityDesc(new RedBear().AttackStrength);
                break;
            case 1:
                newAbilityName.text = BlueBearAbilities.notEquippedSpecialAbility.Name;
                newAbilityDesc.text = BlueBearAbilities.notEquippedSpecialAbility.GetAbilityDesc(new BlueBear().AttackStrength);
                break;
            case 2:
                newAbilityName.text = BlackBearAbilities.notEquippedSpecialAbility.Name;
                newAbilityDesc.text = BlackBearAbilities.notEquippedSpecialAbility.GetAbilityDesc(new BlackBear().AttackStrength);
                break;
            case 3:
                newAbilityName.text = PinkBearAbilities.notEquippedSpecialAbility.Name;
                newAbilityDesc.text = PinkBearAbilities.notEquippedSpecialAbility.GetAbilityDesc(new PinkBear().AttackStrength);
                break;
            case 4:
                newAbilityName.text = GreenBearAbilities.notEquippedSpecialAbility.Name;
                newAbilityDesc.text = GreenBearAbilities.notEquippedSpecialAbility.GetAbilityDesc(new GreenBear().AttackStrength);
                break;
        }
        DisableLeftButtons();
        subPanel.SetActive(true);
        basicOrSpecial = 2;
        btnSwap.Select();
    }
    public void DisableLeftButtons()
    {
        newSkills[0].Select();
        foreach (Button button in originalSkills)
        {
            button.interactable = false;
        }
     
     
        
    }
    public void EnableLeftButtons()
    {
        originalSkills[0].Select();
        foreach (Button button in originalSkills)
        {
            button.interactable = true;
        }
      
    }
   public void Back()
    {
        subPanel.SetActive(false);
        originalSkills[0].Select();
        EnableLeftButtons();
        
       
    }
    public void SwapAbilities()
    {
        switch (basicOrSpecial)
        {
            case 1:
                currentBear.SwapBasicAbility(); 
                break;
            case 2:
                currentBear.SwapSpecialAbility();
                break;
        }
        Back();
        CheckBear();
      
    }

}
