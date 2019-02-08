using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreate : UIBase {
    public UISelecter2 rarelityPanel;
    public UISelecter2 typePanel;
    public UISelecter2 additionalPanel;
    private string recentName;
    
    public Text costPanel;
    public GameMoney cost;

    public ToolPanel furnacePanel;
    public ToolPanel anvilPanel;
    public ToolPanel hammerPanel;
    
    public InputField nameInputPanel;
    private ItemOptions[] itemOptions = new ItemOptions[3];
    private int typeTemp = -1;

    public GameObject[] rareButton;

    public ItemOptions[] ItemOptions
    {
        get { return itemOptions; }
        set { itemOptions = value; }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void ResetPanel()
    {
        furnacePanel.itemName.text = null;
        anvilPanel.itemName.text = null;
        hammerPanel.itemName.text = null;
        furnacePanel.itemSub.text = null;
        anvilPanel.itemSub.text = null;
        hammerPanel.itemSub.text = null;
        furnacePanel.iconCurver.Active = false;
        anvilPanel.iconCurver.Active = false;
        hammerPanel.iconCurver.Active = false;
        costPanel.text = cost.ToString();
        if (itemOptions[0].itemEntry != 0)
        {
            furnacePanel.itemName.text = itemOptions[0].itemName;
            furnacePanel.itemSub.text = itemOptions[0].itemContext;
            furnacePanel.icon.sprite = Resources.Load<Sprite>("Icon\\" + itemOptions[0].iconEntry.ToString());
            furnacePanel.iconCurver.Active = true;
        }
        if (itemOptions[1].itemEntry != 0)
        {
            anvilPanel.itemName.text = itemOptions[1].itemName;
            anvilPanel.itemSub.text = itemOptions[1].itemContext;
            anvilPanel.icon.sprite = Resources.Load<Sprite>("Icon\\" + itemOptions[1].iconEntry.ToString());
            anvilPanel.iconCurver.Active = true;
        }
        if (itemOptions[2].itemEntry != 0)
        {
            hammerPanel.itemName.text = itemOptions[2].itemName;
            hammerPanel.itemSub.text = itemOptions[2].itemContext;
            hammerPanel.icon.sprite = Resources.Load<Sprite>("Icon\\" + itemOptions[2].iconEntry.ToString());
            hammerPanel.iconCurver.Active = true;
        }
        if (typeTemp != typePanel.SelectResult)
        {
            int temp = DataManager.instance.FindAllTable(DataManager.instance.context2Table, "Key_class", "32100"+(typePanel.SelectResult+1).ToString()).Count;
            (nameInputPanel.placeholder as Text).text = DataManager.instance.FindAllTable(DataManager.instance.context2Table, "Key_class", "32100" + (typePanel.SelectResult + 1).ToString())[Random.Range(0, temp)]["Key_Name"].ToString();
            recentName = (nameInputPanel.placeholder as Text).text;
            typeTemp = typePanel.SelectResult;
        }

    }
    public void LetsWork()//작업시작
    {
        int temp = itemOptions.Length;
        for(int i = 0; i < temp;i++)
        {
            if(itemOptions[i].itemEntry == 0)
            {
                UIManager.instance.confirmPanel.CreateUIConfirm(null, null, 1013, 1014);
                return;
            }
        }
        UIManager.instance.confirmPanel.CreateUIConfirm(this, WorkConfirm, 1015,1016);
    }
    void WorkConfirm()
    {
        CraftManager.instance.resultWeapon = new Weapon();
        CraftManager.instance.resultWeapon.Type = typePanel.SelectResult;
        CraftManager.instance.resultWeapon.AdditionalWork = additionalPanel.SelectResult;
        CraftManager.instance.resultWeapon.Rareity = rarelityPanel.SelectResult;
        CraftManager.instance.resultWeapon.toolList[0] = itemOptions[0];
        CraftManager.instance.resultWeapon.toolList[1] = itemOptions[1];
        CraftManager.instance.resultWeapon.toolList[2] = itemOptions[2];
        if (nameInputPanel.text == "")
        {
            nameInputPanel.text = (nameInputPanel.placeholder as Text).text;
        }
        CraftManager.instance.resultWeapon.itemName = nameInputPanel.text;
        CraftManager.instance.resultWeapon.contextName = recentName;
        nameInputPanel.text = ""; 
        CraftManager.instance.GotoWork();
    }

    
    override public void CloseUI()
    {
        base.CloseUI();
        typePanel.ResetTrigger();
        additionalPanel.ResetTrigger();
        rarelityPanel.ResetTrigger();
        gameObject.SetActive(false);
    }
    public override void OpenUI()
    {
        typeTemp = -1;
        if (Player.instance.Working == true || !Gamemanager.instance.saveManaged.canWork)//일중일경우 열리지않음 또는 하루 할당치 일을 다했을경우
        {
            StoryManager.instance.speakBlank.PlayerSpeak("더 못일하겠어..", 2);
            return;
        }
        for (int i = 0; i <= Gamemanager.instance.saveManaged.openRare; i++)
            rareButton[i].SetActive(true);
        AudioManager.instance.PlayEffect("swordScreen");
        base.OpenUI();
    }
    //public void OpenKeyBoard()
    //{
    //    TouchScreenKeyboard.Open(nameInputPanel.text,TouchScreenKeyboardType.Default,false,false,false,false);
    //}
    //public void CloseKeyBoard()
    //{
    //   // TouchScreenKeyboard.cl
    //}
}

