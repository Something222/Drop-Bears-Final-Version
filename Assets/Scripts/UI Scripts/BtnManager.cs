using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class BtnManager : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerDownHandler
{

    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    [SerializeField] private Button btnAbilities, btnMove, btnMap, btnEndTurn, btnAttack, btnAbility1, btnAbility2, btnBack,btnYes,btnNo;
    [SerializeField] private GameObject SubMenu;
    [SerializeField] private Text text;
    [SerializeField] private TextMeshProUGUI header;
    [SerializeField] private TextMeshProUGUI body;
    [SerializeField] private GameManager code;
    public int x = 0;
    public int y = 0;
    string info;
    public static BtnManager instance=null;
    private Color textColor = Color.red;
    public bool AttackIsSelected = false;
    public bool onlyOnce = false;
    private SquadSelection selectedSquad;
    

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        buttons.Add("0,1", btnAbilities);
        buttons.Add("0,0", btnMove);
        buttons.Add("0,-1", btnMap);
        buttons.Add("0,-2", btnEndTurn);
        buttons.Add("1,1", btnAttack);
        buttons.Add("1,0", btnAbility1);
        buttons.Add("1,-1", btnAbility2);
        buttons.Add("1,-2", btnBack);
        buttons.Add("2,0", btnYes);
        buttons.Add("3,0", btnNo);

    }
    // Start is called before the first frame update
    void Start()
    {
        header.fontStyle = FontStyles.Underline;
        buttons["0,-2"].Select();
        btnEndTurn.Select();
        selectedSquad = SquadSelection.instance;
        code = GameManager.instance;
    }

    public void OnClickAttack()
    {
        if (/*!AttackIsSelected*/code.CurrPhase != GameManager.Phase.attackSelectionPhase)
        {
           // AttackRange.ClearTileAttackValues();
            onlyOnce = true;
            AttackIsSelected = true;
            code.CurrPhase = GameManager.Phase.attackSelectionPhase;
        }
        else if (/*AttackIsSelected*/code.CurrPhase == GameManager.Phase.attackSelectionPhase)
        {
            onlyOnce = true;
            AttackIsSelected = false;
            Invoke("ChangePhase", 0.1f);
        }

    }

    public void ChangePhase()
    {
        code.CurrPhase = GameManager.Phase.menuPhase;
    }

    public void OnClickAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (/*AttackIsSelected*/ code.CurrPhase == GameManager.Phase.attackSelectionPhase)
            {
                AttackIsSelected = false;
                Invoke("ChangePhase", 0.1f);
                DisplaySubMenu();
            }
        }

    }
    Color DecideColor()
    {
        return selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>().BearRace;
    }

    // Update is called once per frame
    void Update()
    {
        if (code.CurrPhase != GameManager.Phase.pausePhase && (code.CurrPhase==GameManager.Phase.menuPhase ||code.CurrPhase==GameManager.Phase.attackSelectionPhase))
        {

            //Debug.Log(x + "," + y);
            AbilityDescriptionsDamageScaled();
            BtnSelection();
            GetText();
            text.color = DecideColor();
            //still need to get the colour to work with the text
            if (onlyOnce)
            {
                DisplaySubMenu();
            }
            if (onlyOnce && AttackIsSelected)
            {
                OnClickAttack();
            }
          
           
        }
        else if(code.CurrPhase==GameManager.Phase.confPhase)
        {
            if(!onlyOnce)
            {
                onlyOnce = true;
                y = 0;
                x = 3;
            }
            DisableAbilityDescription();
            BtnSelection();
            text.color = DecideColor();
            
        }
    }

    void BtnSelection()
    {

        OnSelectUp();
        //OnSelectSide();
        info = x + "," + y;
        if(buttons.ContainsKey(info))
        buttons[info].Select();
        GetText();
        //buttons[info].GetComponentInChildren<Text>().color = Color.red;
        text.color = Color.red;      
    }


    [SerializeField] private GameObject[] attackDes;
    [SerializeField] private GameObject[] supportDes; //Ability 1
    [SerializeField] private GameObject[] specialDes; //Ability 2
    public void DisableAbilityDescription()
    {
        body.text = "";
        header.text = "";
    }
  public  void AbilityDescriptionsDamageScaled()
    {
        
       
        info = x + "," + y;
        Bears currentMember = selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>();
        if (info == 1 + "," + 1)
        {
            header.text = currentMember.BearJob.GetAttackName();
           
            body.text = currentMember.BearJob.GetAttackDesc(currentMember.AttackStrength);
        }
        else if (info == 1 + "," + 0)
        {
            header.text = currentMember.BasicAbility.Name;
            body.text = currentMember.BasicAbility.GetAbilityDesc(currentMember.AttackStrength);
        }
        else if (info == 1 + "," + -1)
        {
            header.text = currentMember.SpecialAbility.Name;
            body.text = currentMember.SpecialAbility.GetAbilityDesc(currentMember.AttackStrength);
        }
        else
        {
            body.text = "";
            header.text = "";
        }
    }
    public void GoBack()
    {
        onlyOnce = false;
        switch (TileSelector.instance.AbilityToUse)
        {
            case 1:
                x = 1;
                y = 1;
                break;
            case 2:
                x = 1;
                y = 0;
                break;
            case 3:
                x = 1;
                y = -1;
                break;
        }
        info = x + "," + y;
        Bears currentMember = selectedSquad.Squad[selectedSquad.Selected].GetComponent<Bears>();
        if (info == 1 + "," + 1)
        {
            header.text = currentMember.BearJob.GetAttackName();

            body.text = currentMember.BearJob.GetAttackDesc(currentMember.AttackStrength);
        }
        else if (info == 1 + "," + 0)
        {
            header.text = currentMember.BasicAbility.Name;
            body.text = currentMember.BasicAbility.GetAbilityDesc(currentMember.AttackStrength);
        }
        else if (info == 1 + "," + -1)
        {
            header.text = currentMember.SpecialAbility.Name;
            body.text = currentMember.SpecialAbility.GetAbilityDesc(currentMember.AttackStrength);
        }
        else
        {
            body.text = "";
            header.text = "";
        }
    }

    private void DisplaySubMenu()
    {
        text.color = Color.white;
        if (AttackIsSelected)
        {
            code.CurrPhase = GameManager.Phase.attackSelectionPhase;
            SubMenu.SetActive(true);
            buttons["1,1"].Select();
            x = 1;
            y = 1;
        }
        else
        {
            SubMenu.SetActive(false);
            buttons["0,1"].Select();
            x = 0;
            y = 1;
        }
        onlyOnce = false;
    }

    private void GetText()
    {
        if(buttons.ContainsKey(info))
        text = buttons[info].GetComponentInChildren<Text>();
    }

    //couldnt figure out how to use the new input system for this
    public void Selection(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (code.CurrPhase == GameManager.Phase.menuPhase || code.CurrPhase == GameManager.Phase.confPhase||code.CurrPhase==GameManager.Phase.attackSelectionPhase)
            {
                Vector2 navigate = context.ReadValue<Vector2>();
                if (code.CurrPhase == GameManager.Phase.confPhase)
                {
                    x = x + (int)navigate.x;
                    if (x > 3)
                        x = 3;
                    if (x < 2)
                        x = 2;
                }
                if (code.CurrPhase == GameManager.Phase.menuPhase || code.CurrPhase == GameManager.Phase.attackSelectionPhase)
                    y = y + (int)navigate.y;
                if (y == 2)
                {
                    y = -2;
                }
            
            }
        }
    }


    void OnSelectUp(/*/*InputAction.CallbackContext context*/)
    {

        
            if (y == 1)
            {
                y = -2;
            }
            else
            {
                y += 1;
            }
            text.color = Color.white;
        
        
            if (y == -2)
            {
                y = 1;
            }
            else
            {
                y -= 1;
            }
            text.color = Color.white;
        
        
    }
    #region NianMistakes
    //void OnSelectSide()
    //{
    //    if (AttackIsSelected)
    //    {
    //        if (Input.GetAxisRaw("Horizontal") > 0.1f && Input.GetButtonDown("Horizontal") && y != -2f)
    //        {
    //            if (x == 1)
    //            {
    //                x = 0;
    //            }
    //            else
    //            {
    //                x += 1;
    //            }
    //            text.color = Color.white;
    //        }
    //        if (Input.GetAxisRaw("Horizontal") > 0.1f && Input.GetButtonDown("Horizontal") && y == -2f)
    //        {
    //            y = -1;
    //            x = 1;

    //            text.color = Color.white;
    //        }
    //        else if (Input.GetAxisRaw("Horizontal") < -0.1f && Input.GetButtonDown("Horizontal"))
    //        {
    //            if (x == 0)
    //            {
    //                x = 1;
    //            }
    //            else
    //            {
    //                x -= 1;
    //            }
    //            text.color = Color.white;
    //        }
    //        //  text.color = Color.white;
    //    }
    //}
    #endregion NianMistakes

    #region standardButtonStuff
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Selectable>().Select();
       
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
        GetComponent<Selectable>().OnPointerExit(null);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.Invoke();
            Input.ResetInputAxes();
        }
    }
    #endregion standardButtonStuff

    //void OnSelectDown(InputAction.CallbackContext context)
    //{
    //    Vector2 selection = context.ReadValue<Vector2>();
    //    if (selection.y > 0)
    //    {
    //        if (y == -1)
    //        {
    //            y = -1;
    //        }
    //        else
    //        {
    //            y -= 1;
    //        }
    //    }
    //}
}
