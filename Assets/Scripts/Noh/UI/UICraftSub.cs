using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftSub : UIBase {
    public GameObject[] shopPanel = new GameObject[3];
    private GameObject[] selectedIcon = new GameObject[3];
    public UIShop[] resourceShop = new UIShop[3];
    public UISelecter typeSelecter;
    public Image[] selectedMetalPanel = new Image[3];
    public Text weaponName;
    public Text costValue;
    private GameMoney totalCost = new GameMoney();
    public UnityEngine.UI.Extensions.UIPolygon graph;
    public bool allOpen = false;
    public void LoadLeather(UIShop _shop)
    {
        _shop.dbItemList = DataManager.instance.FindAllTable(DataManager.instance.subresourceTable, "Type", "0");
        for (int i = 0; i < _shop.dbItemList.Count; i++)
        {
            
            GameObject newItem = Instantiate(_shop.itemPrefab, _shop.transform);
            //newItem.transform.SetParent(_shop.transform);
            Item newItemInfo = newItem.GetComponent<Item>();
            newItemInfo.ItemEntry = int.Parse(_shop.dbItemList[i]["Key"].ToString());
            newItemInfo.ItemName = _shop.dbItemList[i]["Key_Name"].ToString();
            newItemInfo.ItemContext = _shop.dbItemList[i]["Key_Define"].ToString();
            newItemInfo.ItemPrice = int.Parse(_shop.dbItemList[i]["Price"].ToString());
            newItemInfo.IconEntry = int.Parse(_shop.dbItemList[i]["Key_Icon"].ToString());
            newItemInfo.BanPrice = int.Parse(_shop.dbItemList[i]["CancelABanPrice"].ToString());
            newItemInfo.iconSound = _shop.dbItemList[i]["Icon_Sound"].ToString();
            float.TryParse(_shop.dbItemList[i]["V1"].ToString(), out newItemInfo.options.parameter[0]);
            float.TryParse(_shop.dbItemList[i]["V2"].ToString(), out newItemInfo.options.parameter[1]);
            newItemInfo.options.rareity = int.Parse(_shop.dbItemList[i]["Class"].ToString());
            newItemInfo.itemIcon.sprite = newItemInfo.LoadIcon();
            if (Gamemanager.instance.saveManaged.ownSubr.Exists(element => element == newItemInfo.ItemEntry))
            {
                newItemInfo.IsBought = true;
            }
            else
            {
                if(newItemInfo.BanPrice == 0)
                {
                    Gamemanager.instance.saveManaged.ownSubr.Add(newItemInfo.ItemEntry);
                    newItemInfo.IsBought = true;
                }else
                    newItemInfo.IsBought = false;
            }
            ResetItemText(_shop, newItemInfo);
            newItemInfo.root = _shop;
            _shop.itemList.Add(newItemInfo);
        }
    }
    public void LoadWood(UIShop _shop)
    {
        _shop.dbItemList = DataManager.instance.FindAllTable(DataManager.instance.subresourceTable, "Type", "1");

        for (int i = 0; i < _shop.dbItemList.Count; i++)
        {

            GameObject newItem = Instantiate(_shop.itemPrefab, _shop.transform);
            //newItem.transform.SetParent(_shop.transform);
            Item newItemInfo = newItem.GetComponent<Item>();
            newItemInfo.ItemEntry = int.Parse(_shop.dbItemList[i]["Key"].ToString());
            newItemInfo.ItemName = _shop.dbItemList[i]["Key_Name"].ToString();
            newItemInfo.ItemContext = _shop.dbItemList[i]["Key_Define"].ToString();
            newItemInfo.IconEntry = int.Parse(_shop.dbItemList[i]["Key_Icon"].ToString());
            newItemInfo.ItemPrice = int.Parse(_shop.dbItemList[i]["Price"].ToString());
            newItemInfo.BanPrice = int.Parse(_shop.dbItemList[i]["CancelABanPrice"].ToString());
            newItemInfo.iconSound = _shop.dbItemList[i]["Icon_Sound"].ToString();
            float.TryParse(_shop.dbItemList[i]["V1"].ToString(), out newItemInfo.options.parameter[0]);
            float.TryParse(_shop.dbItemList[i]["V2"].ToString(), out newItemInfo.options.parameter[1]);
            newItemInfo.options.rareity = int.Parse(_shop.dbItemList[i]["Class"].ToString());
            newItemInfo.itemIcon.sprite = newItemInfo.LoadIcon();
            if (Gamemanager.instance.saveManaged.ownSubr.Exists(element => element == newItemInfo.ItemEntry))
            {
                newItemInfo.IsBought = true;
            }
            else
            {
                if (newItemInfo.BanPrice == 0)
                {
                    Gamemanager.instance.saveManaged.ownSubr.Add(newItemInfo.ItemEntry);
                    newItemInfo.IsBought = true;
                }else
                    newItemInfo.IsBought = false;
            }
            ResetItemText(_shop, newItemInfo);
            newItemInfo.root = _shop;
            _shop.itemList.Add(newItemInfo);
        }
    }
    public void LoadCube(UIShop _shop)
    {
        _shop.dbItemList = DataManager.instance.manacubeTable;
        for (int i = 0; i < _shop.dbItemList.Count; i++)
        {

            GameObject newItem = Instantiate(_shop.itemPrefab, _shop.transform);
            //newItem.transform.SetParent(_shop.transform);
            Item newItemInfo = newItem.GetComponent<Item>();
            newItemInfo.ItemEntry = int.Parse(_shop.dbItemList[i]["Key"].ToString());
            newItemInfo.ItemName = _shop.dbItemList[i]["Key_Name"].ToString();
            newItemInfo.IconEntry = int.Parse(_shop.dbItemList[i]["Key_Icon"].ToString());
            newItemInfo.ItemContext = _shop.dbItemList[i]["Key_Define"].ToString();
            newItemInfo.ItemPrice = int.Parse(_shop.dbItemList[i]["Price"].ToString());
            newItemInfo.options.rareity = int.Parse(_shop.dbItemList[i]["Class"].ToString());
            newItemInfo.iconSound = _shop.dbItemList[i]["Icon_Sound"].ToString();
            float.TryParse(_shop.dbItemList[i]["Attack_Form"].ToString(), out newItemInfo.options.parameter[0]);
            float.TryParse(_shop.dbItemList[i]["Deign_Form"].ToString(), out newItemInfo.options.parameter[1]);
            float.TryParse(_shop.dbItemList[i]["Mass_Form"].ToString(), out newItemInfo.options.parameter[2]);
            float.TryParse(_shop.dbItemList[i]["Durability_Form"].ToString(), out newItemInfo.options.parameter[3]);
            float.TryParse(_shop.dbItemList[i]["Mana_Form"].ToString(), out newItemInfo.options.parameter[4]);
            newItemInfo.itemIcon.sprite = newItemInfo.LoadIcon();
            newItemInfo.IsBought = true;
            ResetItemText(_shop, newItemInfo);
            newItemInfo.root = _shop;
            _shop.itemList.Add(newItemInfo);
        }
        ResetPanel();
    }
    public void OpenNextRare()
    {
        for (int i = 0; i < resourceShop[0].itemList.Count; i++)
        {
            if (resourceShop[0].itemList[i].gameObject.activeInHierarchy == true && resourceShop[0].itemList[i].IsBought != true)
            {
                allOpen = false;
                return;
            }
        }
        for (int i = 0; i < resourceShop[1].itemList.Count; i++)
        {
            if (resourceShop[1].itemList[i].gameObject.activeInHierarchy == true && resourceShop[1].itemList[i].IsBought != true)
            {
                allOpen = false;
                return;
            }
        }
        allOpen = true;
        if (UIManager.instance.metalChoicePanel.allOpen == true)
        {
            Gamemanager.instance.saveManaged.hazardCount = 0;
            Gamemanager.instance.saveManaged.openRare = CraftManager.instance.resultWeapon.Rareity + 1;
        }
    }
    public void ResetItemText(UIShop _shop, Item _item)
    {
        int maxRare = (CraftManager.instance.resultWeapon.Rareity + 1) * 10;
        int minRare = (CraftManager.instance.resultWeapon.Rareity) * 10;
        minRare = minRare <= 0 ? 0 : minRare;
        if (_item.options.rareity >= minRare && _item.options.rareity < maxRare)
        {
            _item.gameObject.SetActive(true);
        }
        else
            _item.gameObject.SetActive(false);
        Button buyButton = _item.GetComponent<Button>();
        if (_item.textName)
        {
            _item.textName.text = _item.ItemName;
        }
        if (_item.IsBought == true)
        {
            _item.GetComponentInChildren<UIactiveable>().Active = true;
            if (_item.textPrice)
            {
                _item.textPrice.text = ((int)(_item.ItemPrice *
                        (CraftManager.instance.resultWeapon.AdditionalWork == 0 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f) *
                        (CraftManager.instance.resultWeapon.AdditionalWork == 1 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f)))
                        .ToString() + "G";
            }
            if (_item.textContext)
            {
                _item.textContext.text = _item.ItemContext;
            }
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => { CreateCheckUI(_shop, _item); });
        }
        else
        {
            _item.GetComponentInChildren<UIactiveable>().Active = false;
            if (_item.textPrice)
            {
                _item.textPrice.text = _item.BanPrice + "G";
            }
            if (_item.textContext)
            {
                _item.textContext.text = "";
            }
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => { ParchaseItem(_shop, _item); });
        }



    }
    void ParchaseItem(UIShop _shop, Item _item)
    {
        if (Gamemanager.instance.PlayerMoney >= _item.BanPrice)
        {
            UIManager.instance.confirmPanel.CreateUIConfirm(null, () => { ParchaseItemConfirm(_shop, _item); }, "구매확인", "정말 구매하시겠습니까?");
        }
        else
            UIManager.instance.confirmPanel.CancelByMoney();//구매불가 돈이 부족합니다.구매불가", "돈이 부족합니다");
    }

    void ParchaseItemConfirm(UIShop _shop, Item _item)
    {
        Gamemanager.instance.PlayerMoney -= _item.BanPrice;
        _item.IsBought = true;
        Gamemanager.instance.saveManaged.ownSubr.Add(_item.ItemEntry);
        UIAlarm.instance.TextAlarm(_item.ItemName + "의 수입루트가 생겼다.");
        Gamemanager.instance.saveManaged.hazardCount++;
        ResetItemText(_shop, _item);
        OpenNextRare();
    }
    public override void OpenUI()
    {
        for(int i = 0; i < resourceShop.Length; i++)//스크롤바 위치 초기화
        {
            Vector3 pos = resourceShop[i].transform.position;
            resourceShop[i].transform.position = new Vector3(pos.x, 0);
        }
        for (int i = 0; i < CraftManager.instance.resultWeapon.resourceItem.Length; i++)
        {
            CraftManager.instance.resultWeapon.resourceItem[i] = new ItemOptions();
            Destroy(selectedIcon[i]);
            selectedIcon[i] = null;
        }
        base.OpenUI();
        typeSelecter.SelectResult = 0;
        totalCost = new GameMoney(CraftManager.instance.WorkTotalCost);
        for (int j = 0; j < resourceShop.Length; j++)
        {
            for (int i = 0; i < resourceShop[j].dbItemList.Count; i++)
            {
                ResetItemText(resourceShop[j], resourceShop[j].itemList[i]);
            }
        }
        float temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfBalance() / (10.0f / 0.9f);
        graph.VerticesDistances[0] = temp <= 0.1f ? 0.1f : temp;
        temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfUsefull() / (10.0f / 0.9f);
        graph.VerticesDistances[1] = temp <= 0.1f ? 0.1f : temp;
        temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfSense() / (10.0f / 0.9f);
        graph.VerticesDistances[2] = temp <= 0.1f ? 0.1f : temp;
        temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfDesign() / (10.0f / 0.9f);
        graph.VerticesDistances[3] = temp <= 0.1f ? 0.1f : temp;
        temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfPopular() / (28.0f / 0.9f);
        graph.VerticesDistances[4] = temp <= 0.1f ? 0.1f : temp;



    }
    public override void CloseUI()
    {
        base.CloseUI();
    }
    public override void ResetPanel()
    {
        base.ResetPanel();
        for (int i = 0; i < shopPanel.Length; i++)
        {
            shopPanel[i].gameObject.SetActive(false);
            typeSelecter.buttonList[i].GetComponentInChildren<Text>().color = ConstManager.hexToColor("909296");
        }
        shopPanel[typeSelecter.SelectResult].gameObject.SetActive(true);
        typeSelecter.buttonList[typeSelecter.SelectResult].GetComponentInChildren<Text>().color = ConstManager.hexToColor("ffffff");
        costValue.text = totalCost.ToString();
        if (CraftManager.instance.resultWeapon != null)
        {
            float temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfBalance() / (10.0f / 0.9f);
            graph.VerticesDistances[0] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfUsefull() / (10.0f / 0.9f);
            graph.VerticesDistances[1] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfSense() / (10.0f / 0.9f);
            graph.VerticesDistances[2] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfDesign() / (10.0f / 0.9f);
            graph.VerticesDistances[3] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfPopular() / (28.0f / 0.9f);
            graph.VerticesDistances[4] = temp <= 0.1f ? 0.1f : temp;
        }
    }
    public void RemoveItem(int _count)
    {
        if (CraftManager.instance.resultWeapon.resourceItem[_count].itemEntry == 0)
            return;
        Destroy(selectedIcon[_count]);
        int recentPrice = (int)(CraftManager.instance.resultWeapon.resourceItem[_count].itemPrice
            * (CraftManager.instance.resultWeapon.AdditionalWork == 0 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f) //추가작업요금
            * (CraftManager.instance.resultWeapon.AdditionalWork == 1 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f)); //추가작업요금2
        totalCost -= recentPrice;
        CraftManager.instance.resultWeapon.resourceItem[_count] = new ItemOptions();
        selectedIcon[_count] = null;

        ResetPanel();
    }
    public void CompleteButton()
    {
        for (int i = 0; i < CraftManager.instance.resultWeapon.resourceItem.Length; i++)
        {
            if (CraftManager.instance.resultWeapon.resourceItem[i].itemEntry == 0)
            {
                UIManager.instance.confirmPanel.CreateUIConfirm(null, null, 1013, 1014);
                return;
            }
        }
        UIManager.instance.confirmPanel.CreateUIConfirm(this, WorkComplete, 1015, 1016);
    }

    public void WorkComplete()
    {
        CraftManager.instance.WorkTotalCost += totalCost;
        Player.instance.AnimationState = 2;
        CraftManager.instance.GotoNext(ConstManager.THIRDWORKLIMIT);
    }

    public void CreateCheckUI(UIShop _shop, Item _item)
    {
        int recentPrice = (int)(_item.ItemPrice
            * (CraftManager.instance.resultWeapon.AdditionalWork == 0 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f) //추가작업요금
            * (CraftManager.instance.resultWeapon.AdditionalWork == 1 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f)); //추가작업요금2
        if (totalCost+ recentPrice
            > Gamemanager.instance.PlayerMoney)//돈이 부족한경우
            return;
        int temp = typeSelecter.SelectResult;
        if (CraftManager.instance.resultWeapon.resourceItem[temp].itemEntry != 0)
        {
            RemoveItem(temp);
        }
        GameObject tempIcon = Instantiate(_item.itemIcon.gameObject, _item.itemIcon.transform.position, Quaternion.identity);
        tempIcon.transform.SetParent(gameObject.transform);
        tempIcon.transform.localScale = Vector3.one;
        selectedIcon[temp] = tempIcon;
        string sound = _item.iconSound;
        StartCoroutine(MoveMetalIcon(tempIcon, selectedMetalPanel[temp], sound));
        CraftManager.instance.resultWeapon.resourceItem[temp] = _item.options;
        totalCost += recentPrice;
        ResetPanel();
    }
    IEnumerator MoveMetalIcon(GameObject _item,Image _dest,string sound)
    {
        yield return null;
        _dest.GetComponent<Button>().interactable = false;
        while (Vector3.Distance(_item.transform.position,_dest.transform.position) > 0.1f)
        {
            _item.transform.position = Vector3.MoveTowards(_item.transform.position, _dest.transform.position,ConstManager.ICONSPEED);
            yield return null;
        }
        _item.transform.SetParent(_dest.transform);
        if(sound != null)
            AudioManager.instance.PlayEffect(sound);
        _dest.GetComponent<Button>().interactable = true;
    }
    // Use this for initialization
    void Start () {
        LoadWood(resourceShop[0]);
        LoadLeather(resourceShop[1]);
        
        LoadCube(resourceShop[2]);
        for (int i = 0; i < shopPanel.Length; i++)
        {
            shopPanel[i].gameObject.SetActive(false);
        }
        shopPanel[typeSelecter.SelectResult].gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
