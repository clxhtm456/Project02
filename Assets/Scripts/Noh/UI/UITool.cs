using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITool : UIBase {
    public UIShop[] toolPanel = new UIShop[3];
    /*
     * 0 : 화로
     * 1 : 모루
     * 2 : 망치
     * 
     */

    public Text[] selectedText = new Text[3];
    public Image[] selectedIcon = new Image[3];
    // Use this for initialization
    void Start () {
        for(int i = 0;i<3;i++)
            LoadItemShop(toolPanel[i], i);
    }
    public override void OpenUI()
    {
        for (int i = 0; i < toolPanel.Length; i++)//스크롤바 위치 초기화
        {
            Vector3 pos = toolPanel[i].transform.localPosition;
            toolPanel[i].transform.localPosition = new Vector3(0, pos.y);
        }
        base.OpenUI();
    }
    override public void ResetPanel()
    {
        for(int i = 0;i<3;i++)
        {
            if (toolPanel[i].resultItem != null)
            {
                selectedIcon[i].gameObject.SetActive(true);
                UIManager.instance.createPanel.ItemOptions[i] = toolPanel[i].resultItem.options;
                selectedText[i].text = toolPanel[i].resultItem.ItemName;
                selectedIcon[i].sprite = toolPanel[i].resultItem.LoadIcon();
                //selectedFurnace.text = furnacePanel.resultItem.ItemName;
            }
            else
            {
                UIManager.instance.createPanel.ItemOptions[i].itemEntry = 0;
                selectedText[i].text = "";
                selectedIcon[i].gameObject.SetActive(false);
            }
        }
    }
    public void LoadItemShop(UIShop _shop,int type)
    {
        _shop.dbItemList = DataManager.instance.FindAllTable(DataManager.instance.toolTable,"Categorize", type.ToString());
        for (int i = 0; i < _shop.dbItemList.Count; i++)
        {
            GameObject newItem = Instantiate(_shop.itemPrefab, _shop.transform);
            Item newItemInfo = newItem.GetComponent<Item>();
            newItemInfo.ItemEntry = int.Parse(_shop.dbItemList[i]["Key"].ToString());
            newItemInfo.ItemName = _shop.dbItemList[i]["Key_Name"].ToString();
            newItemInfo.ItemContext = _shop.dbItemList[i]["Key_Define"].ToString();
            newItemInfo.ItemPrice = int.Parse(_shop.dbItemList[i]["Price"].ToString());
            newItemInfo.options.rareity = int.Parse(_shop.dbItemList[i]["Class"].ToString());
            newItemInfo.IconEntry = int.Parse(_shop.dbItemList[i]["Key_Icon"].ToString());
            newItemInfo.BanPrice = type;//BanPrice 항목 임시 타입으로 사용(아이템가격이랑 중복)
            float.TryParse(_shop.dbItemList[i]["Score_Min01"].ToString(), out newItemInfo.options.parameter[1]);
            float.TryParse(_shop.dbItemList[i]["Score_Max01"].ToString(), out newItemInfo.options.parameter[2]);
            float.TryParse(_shop.dbItemList[i]["Score_Min02"].ToString(), out newItemInfo.options.parameter[3]);
            float.TryParse(_shop.dbItemList[i]["Score_Max02"].ToString(), out newItemInfo.options.parameter[4]);
            float.TryParse(_shop.dbItemList[i]["Probability01"].ToString(), out newItemInfo.options.parameter[0]);
            if (Gamemanager.instance.saveManaged.ownTool.Exists(element => element == newItemInfo.ItemEntry))
            {
                newItemInfo.IsBought = true;
            }
            else
                newItemInfo.IsBought = false;
            ResetItemText(_shop,newItemInfo);
            newItemInfo.root = _shop;
            _shop.itemList.Add(newItemInfo);
        }
    }
    public void CreateCheckUI(UIShop _shop,Item _item)
    {
        AudioManager.instance.PlayEffect("MenuChoice");
        if (_item.IsBought)//이미 구매한경우
        {
            SelectItem(_shop, _item);
        }
        else//구매해야하는경우
        {
            UIManager.instance.toolChoicePanel.CreateUIConfirm(null, () => { BuyingItem(_shop, _item); }, _item.ItemName, _item.ItemContext);
            UIManager.instance.toolChoicePanel.icon.sprite = _item.LoadIcon();
        }
    }
    public void ResetItemText(UIShop _shop,Item _item)
    {
        if (_item.textName)
        {
            _item.textName.text = _item.ItemName;
        }
        if (_item.textPrice)
        {
            if (_item.IsBought)
                _item.textPrice.text = "";
            else
                _item.textPrice.text = "가격 : " + _item.ItemPrice.ToString();
        }
        if (_item.textContext)
        {
            _item.textContext.text = _item.ItemContext;
        }
        _item.buyButton.onClick.RemoveAllListeners();
        _item.buyButton.onClick.AddListener(() => { CreateCheckUI(_shop,_item); });

        

        ResetPanel();
        
    }
    public void SelectItem(UIShop _shop,Item _item)
    {
        if (_shop.resultItem == this)
        {
            _shop.SelectItem(null);
        }
        else
        {
            _shop.SelectItem(_item);
        }
        ResetItemText(_shop,_item);
    }
    public void BuyingItem(UIShop _shop, Item _item)
    {
        if (Gamemanager.instance.PlayerMoney >= _item.ItemPrice)
        {
            UIManager.instance.confirmPanel.CreateUIConfirm(null, () => { CanBuyTrigger(_shop, _item); }, "구매확인", "정말 구매하시겠습니까?");
        }else
            UIManager.instance.confirmPanel.CancelByMoney();//구매불가 돈이 부족합니다.
    }
    public void CanBuyTrigger(UIShop _shop, Item _item)
    {
        Gamemanager.instance.PlayerMoney -= _item.ItemPrice;
        _item.IsBought = true;
        Gamemanager.instance.saveManaged.ownTool.Add(_item.ItemEntry);
        switch(_item.BanPrice)
        {
            case 0:
                AudioManager.instance.PlayEffect("Brazier(Normal~Magic)");
                break;
            case 1:
                if(_item.options.rareity > 2)
                    AudioManager.instance.PlayEffect("Anvil(Rare~Legend)");
                else
                    AudioManager.instance.PlayEffect("Anvil(Normal~Magic)");
                break;
            case 2:
                AudioManager.instance.PlayEffect("Hammer");
                break;
        }
        
        UIAlarm.instance.TextAlarm(_item.ItemName+" 구매");
        SelectItem(_shop, _item);
        ResetItemText(_shop, _item);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
