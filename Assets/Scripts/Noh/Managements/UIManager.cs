using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    public UIConfirm confirmPanel;
    public UICreate createPanel;
    public Text playerMoney;
    public ProgressBar progressBar;
    public UIConfirm toolChoicePanel;
    public Text turnPanel;
    public UICraft metalChoicePanel;
    public UICraftSub subChoicePanel;
    public UIWeaponComp craftCompPanel;
    public UIGrading gradingPanel;
    public UIInventory inventoryPanel;
    public Text playerTurnMoney;
    public UIRegRoyal regRoyalPanel;
    public GameObject turnOption;
    public GameObject safeRate;
    public GameObject profile;
    public GameObject alarm;
    public GameObject news;

    public UIBase topUI;
    public UIBase TopUI
    {
        get { return topUI; }
        set { topUI = value; }
    }
    // Use this for initialization
    void Start () {
    }
    void Update () {
		
	}
    public void ShowTopUI()
    {
        if (TopUI != null)
            Debug.Log(TopUI.name);
        else
            Debug.Log("topui is null");
    }
    public void ShutAllDown()
    {
        while (ShutDownUI()) ;
    }
    private bool ShutDownUI()
    {
        if (TopUI == null)
            return false;
        else
        {
            TopUI.CloseUI();
            return true;
        }
    }
    public void CheckMoney()
    {
        playerMoney.text = Gamemanager.instance.PlayerMoney.ToString();
    }
    public void CheckTurnMoney()
    {
        playerTurnMoney.text = Mathf.Round(Gamemanager.instance.turnPerMoney).ToString();
    }

}
