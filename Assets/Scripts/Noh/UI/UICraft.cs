using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraft : UIBase {
    private GameObject[] selectedIcon = new GameObject[3];
    public UIShop metalShop;
    public Image[] selectedMetalPanel = new Image[3];
    public Text costValue;
    public Text weaponName;
    public GameMoney totalCost;
    public UnityEngine.UI.Extensions.UIPolygon graph;
    public bool allOpen = false;
    override public void OpenUI()//UI를 아래로 정렬후 활성화
    {
        Vector3 pos = metalShop.transform.position;
        metalShop.transform.position = new Vector3(pos.x, 0);
        totalCost = new GameMoney(CraftManager.instance.WorkTotalCost);
        for (int i = 0; i < metalShop.dbItemList.Count; i++)
        {
            ResetItemText(metalShop, metalShop.itemList[i]);
        }
        for (int i = 0; i < CraftManager.instance.resultWeapon.resourceMetal.Length; i++)
        {
            CraftManager.instance.resultWeapon.resourceMetal[i] = new ItemOptions();
            Destroy(selectedIcon[i]);
            selectedIcon[i] = null;
        }
        weaponName.text = CraftManager.instance.resultWeapon.itemName;
        base.OpenUI();
        
    }
    public override void CloseUI()
    {
        base.CloseUI();
    }
    public void LoadItemShop(UIShop _shop)
    {
        _shop.dbItemList = DataManager.instance.metalTable;


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
            newItemInfo.options.rareity = int.Parse(_shop.dbItemList[i]["Class"].ToString());
            newItemInfo.BanPrice = int.Parse(_shop.dbItemList[i]["CancelABanPrice"].ToString());
            newItemInfo.iconSound = _shop.dbItemList[i]["Icon_Sound"].ToString();
            newItemInfo.itemIcon.sprite = newItemInfo.LoadIcon();
            float temp;
            if (float.TryParse(_shop.dbItemList[i]["Hardness"].ToString(), out temp))
                newItemInfo.options.parameter[0] = temp;
            if (float.TryParse(_shop.dbItemList[i]["Variability"].ToString(), out temp))
                newItemInfo.options.parameter[1] = temp;
            if (float.TryParse(_shop.dbItemList[i]["Durability"].ToString(), out temp))
                newItemInfo.options.parameter[2] = temp;
            if (float.TryParse(_shop.dbItemList[i]["Mass"].ToString(), out temp))
                newItemInfo.options.parameter[3] = temp;
            if (float.TryParse(_shop.dbItemList[i]["Mana"].ToString(), out temp))
                newItemInfo.options.parameter[4] = temp;

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
        ResetPanel();
    }
    public void OpenNextRare(UIShop _shop)
    {
        for (int i = 0; i < _shop.itemList.Count; i++)
        {
            if (_shop.itemList[i].gameObject.activeInHierarchy == true && _shop.itemList[i].IsBought != true)
            {
                allOpen = false;
                return;
            }
        }
        allOpen = true;
    }
    public void ResetItemText(UIShop _shop, Item _item)
    {
        //등급에 따른 비활성화
        int maxRare = (CraftManager.instance.resultWeapon.Rareity+1) * 10;
        int minRare = (CraftManager.instance.resultWeapon.Rareity) * 10;
        minRare = minRare <= 0 ? 0 : minRare;
        if(_item.options.rareity >= minRare && _item.options.rareity <maxRare)
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
        if(_item.IsBought == true)
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
            UIManager.instance.confirmPanel.CancelByMoney();//구매불가 돈이 부족합니다.
    }

    void ParchaseItemConfirm(UIShop _shop, Item _item)
    {
        Gamemanager.instance.PlayerMoney -= _item.BanPrice;
        _item.IsBought = true;
        Gamemanager.instance.saveManaged.ownSubr.Add(_item.ItemEntry);
        UIAlarm.instance.TextAlarm(_item.ItemName + "의 수입루트가 생겼다.");
        Gamemanager.instance.saveManaged.hazardCount++;
        ResetItemText(_shop, _item);
        OpenNextRare(_shop);
    }
    public override void ResetPanel()
    {
        base.ResetPanel();
        costValue.text = totalCost.ToString();
        if (CraftManager.instance.resultWeapon.resourceMetal[0].itemEntry == 0 && CraftManager.instance.resultWeapon.resourceMetal[1].itemEntry == 0&& CraftManager.instance.resultWeapon.resourceMetal[2].itemEntry == 0)
        {
            graph.VerticesDistances[0] = 0.1f;
            graph.VerticesDistances[1] = 0.1f;
            graph.VerticesDistances[2] = 0.1f;
            graph.VerticesDistances[3] = 0.1f;
        }
        else
        {
            float temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfBalance() / (10.0f / 0.9f);
            graph.VerticesDistances[0] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfUsefull() / (10.0f / 0.9f);
            graph.VerticesDistances[1] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfSense() / (10.0f / 0.9f);
            graph.VerticesDistances[2] = temp <= 0.1f ? 0.1f : temp;
            temp = 0.1f + CraftManager.instance.resultWeapon.ScoreOfDesign() / (10.0f / 0.9f);
            graph.VerticesDistances[3] = temp <= 0.1f ? 0.1f : temp;
        }
        //graph.VerticesDistances[4] = CraftManager.instance.resultWeapon.ScoreOfPopular() / 28.0f;
    }
    public void RemoveItem(int _count)
    {
        if (CraftManager.instance.resultWeapon.resourceMetal[_count].itemEntry== 0)
            return;
        Destroy(selectedIcon[_count]);
        int recentPrice = (int)(CraftManager.instance.resultWeapon.resourceMetal[_count].itemPrice
            * (CraftManager.instance.resultWeapon.AdditionalWork == 0 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f) //추가작업요금
            * (CraftManager.instance.resultWeapon.AdditionalWork == 1 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f)); //추가작업요금2
        totalCost -= recentPrice;
        CraftManager.instance.resultWeapon.resourceMetal[_count] = new ItemOptions();
        selectedIcon[_count] = null;
        
        ResetPanel();
    }
    public void CompleteButton()
    {
        for(int i = 0; i< CraftManager.instance.resultWeapon.resourceMetal.Length;i++)
        {
            if (CraftManager.instance.resultWeapon.resourceMetal[i].itemEntry ==0)
            {
                UIManager.instance.confirmPanel.CreateUIConfirm(null, null, 1013, 1014);
                return;
            }
        }
        
        UIManager.instance.confirmPanel.CreateUIConfirm(this, WorkComplete,1015,1016);
    }
    public void WorkComplete()
    {
        CraftManager.instance.WorkTotalCost += totalCost;
        Player.instance.AnimationState = 1;
        CraftManager.instance.GotoNext(ConstManager.SECONDWORKLIMIT);
    }

    public void CreateCheckUI(UIShop _shop, Item _item)
    {
        int recentPrice = (int)(_item.ItemPrice
            * (CraftManager.instance.resultWeapon.AdditionalWork == 0 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f) //추가작업요금
            * (CraftManager.instance.resultWeapon.AdditionalWork == 1 || CraftManager.instance.resultWeapon.AdditionalWork == 2 ? ConstManager.ADDWORKCOST : 1.0f)); //추가작업요금2
        if ((totalCost+ recentPrice) > Gamemanager.instance.PlayerMoney)//돈이 부족한경우
            return;
        for(int i = 0; i < CraftManager.instance.resultWeapon.resourceMetal.Length;i++)
        {
            if(CraftManager.instance.resultWeapon.resourceMetal[i].itemEntry == 0)
            {
                GameObject tempIcon = Instantiate(_item.itemIcon.gameObject, _item.itemIcon.transform.position, Quaternion.identity);
                tempIcon.transform.SetParent(gameObject.transform);
                tempIcon.transform.localScale = Vector3.one;
                selectedIcon[i] = tempIcon;
                string sound = _item.iconSound;
                StartCoroutine(MoveMetalIcon(tempIcon, selectedMetalPanel[i], sound));
                CraftManager.instance.resultWeapon.resourceMetal[i] = _item.options;
                totalCost += recentPrice;
                ResetPanel();
                break;
            }
        }
            //UIManager.instance.toolChoicePanel.OpenUI();
            //UIManager.instance.toolChoicePanel.yesButton.onClick.RemoveAllListeners();
            //UIManager.instance.toolChoicePanel.yesButton.onClick.AddListener(() => { BuyingItem(_shop, _item); });
            //UIManager.instance.toolChoicePanel.title.MText = _item.ItemName;
            //UIManager.instance.toolChoicePanel.context.MText = _item.ItemContext;
    }
    IEnumerator MoveMetalIcon(GameObject _item,Image _dest, string sound)
    {
        yield return null;
        _dest.GetComponent<Button>().interactable = false;
        while (Vector3.Distance(_item.transform.position,_dest.transform.position) > 0.1f)
        {
            _item.transform.position = Vector3.MoveTowards(_item.transform.position, _dest.transform.position,ConstManager.ICONSPEED);
            yield return null;
        }
        _item.transform.SetParent(_dest.transform);
        if (sound != null)
            AudioManager.instance.PlayEffect(sound);
        _dest.GetComponent<Button>().interactable = true;
    }
    // Use this for initialization
    void Start () {
        LoadItemShop(metalShop);
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
