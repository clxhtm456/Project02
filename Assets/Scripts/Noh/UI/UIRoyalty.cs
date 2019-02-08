using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoyalty : UIBase
{
    public UISelecter inventorySlot;
    public List<GameObject> inventoryIcon = new List<GameObject>();
    public GameObject selectedItemIcon;
    public Text[] selectedItemState;
    public Button[] itemButton;
    public Combination comb;
    private Image[] selectedItemImage = new Image[3];

    void SelectedItemIcon(bool _temp)
    {
        selectedItemIcon.gameObject.SetActive(_temp);
        if (!_temp)
            return;
        Weapon weaponData = Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData;
        if (gameObject.activeInHierarchy == false)
            return;
        if (selectedItemImage[0] == null)
            selectedItemImage[0] = selectedItemIcon.transform.Find("BackGround").GetComponent<Image>();
        if (selectedItemImage[1] == null)
            selectedItemImage[1] = selectedItemIcon.transform.Find("IconImage").GetComponent<Image>();
        if (selectedItemImage[2] == null)
            selectedItemImage[2] = selectedItemIcon.transform.Find("Outline").GetComponent<Image>();
        int temp = (int)(weaponData.TotalScore / 33.3f);
        temp = temp == 0 ? temp = 1 : temp;
        //Debug.Log("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
        selectedItemImage[0].sprite = Resources.Load<Sprite>("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
        selectedItemImage[2].sprite = Resources.Load<Sprite>("Icon\\icon_ui_" + weaponData.weaponElement.ToString() + temp.ToString());
        selectedItemImage[1].sprite = weaponData.LoadIcon();
    }
    //public Button royalListButton;
    //public Button royalStateButton;
    //public void SwitchState()
    //{
    //    switch(royalField.Active)
    //    {
    //        case true://특허관리창
    //            royalField.Active = false;
    //            royalListButton.gameObject.SetActive(false);
    //            royalStateButton.gameObject.SetActive(true);
    //            break;
    //        case false://특허현황창
    //            royalField.Active = true;
    //            royalListButton.gameObject.SetActive(true);
    //            royalStateButton.gameObject.SetActive(false);
    //            break;
    //    }
    //}
    // Use this for initialization

    public override void OpenUI()
    {
        Vector3 pos = inventorySlot.transform.position;
        inventorySlot.transform.position = new Vector3(pos.x, 0);
        base.OpenUI();
    }
    public void OpenCombination()
    {
        comb.RegWeapon(Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData);
        AudioManager.instance.PlayEffect("menuChoice2");
    }
    public void SellRoyal()
    {
        int select = inventorySlot.SelectResult;
        if (Gamemanager.instance.saveManaged.ownRoyalty[select].weaponData == null)
            return;
        Gamemanager.instance.PlayerMoney += Gamemanager.instance.saveManaged.ownRoyalty[select].weaponData.ItemPriceData;
        Gamemanager.instance.saveManaged.ownRoyalty.Remove(Gamemanager.instance.saveManaged.ownRoyalty[select]);
        inventorySlot.SelectResult -= 1;
        AudioManager.instance.PlayEffect("menuChoice2");
        ResetPanel();
    }
    public void ActiveRoyal()
    {
        Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].ActiveRoyal(1);
        Gamemanager.instance.ResetTurnPerMoney();
        AudioManager.instance.PlayEffect("menuChoice2");
        ResetPanel();
    }
    public override void ResetPanel()
    {
        base.ResetPanel();


        int itemCount = Gamemanager.instance.saveManaged.ownRoyalty.Count;//아이템개수
        int iconCount = inventoryIcon.Count;//인벤토리 리스트 개수
        if (inventorySlot.SelectResult + 1 <= Gamemanager.instance.saveManaged.ownRoyalty.Count && inventorySlot.SelectResult != -1)//아이템이 선택되있는경우 판매 특허등록버튼 클릭가능
        {
            for (int i = 0; i < itemButton.Length; i++)
                itemButton[i].interactable = true;
            AudioManager.instance.PlayEffect("computerMENU");
            SelectedItemIcon(true);
            selectedItemState[0].text = Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.itemName;//이름
            int temp = Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.Rareity;
            selectedItemState[1].text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == (temp + 1008))["Text"].ToString();
            selectedItemState[2].text = Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.ItemPriceData.ToString();
            selectedItemState[3].text = "공격력 : " + Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.AttackPower.ToString()
                + "\n공격속도 : " + Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.AttackSpeed.ToString()
                    + "\n내구도 : " + Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.Durability.ToString()
                    + "\n마력 : " + Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.MagicPower.ToString()
                    + "\n옵션 : " + Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.OptionCount.ToString()
                    + "\n특수능력 : " + Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.Special.ToString();
            selectedItemState[4].text = Gamemanager.instance.saveManaged.ownRoyalty[inventorySlot.SelectResult].weaponData.itemContext.ToString();//이름
        }
        else//그외에는 버튼 클릭불가능
        {
            for (int i = 0; i < itemButton.Length; i++)
                itemButton[i].interactable = false;
            SelectedItemIcon(false);
            for (int i = 0; i < selectedItemState.Length; i++)
                selectedItemState[i].text = null;
        }
        switch (itemCount >= iconCount)
        {
            case true:
                for (int i = 0; i < itemCount; i++)//아이콘리스트가 아이템소지수보다 적은경우
                {
                    if (i >= iconCount)//리스트가 부족할경우 리스트 먼저 생성
                    {
                        GameObject icon = EffectManager.instance.MakeEffect("RoyaltyPrefab");
                        icon.transform.SetParent(inventorySlot.buttonList[i].transform);
                        icon.transform.localPosition = Vector3.zero;
                        inventoryIcon.Add(icon);
                    }
                    inventoryIcon[i].gameObject.SetActive(true);
                    inventoryIcon[i].GetComponent<RoyaltyIcon>().Weapondata = Gamemanager.instance.saveManaged.ownRoyalty[i].weaponData;
                }
                break;
            case false:
                for (int i = 0; i < iconCount; i++)//아이콘리스트가 아이템소지수보다 많은경우
                {
                    if (i >= itemCount)//아이템생성이 끝났을경우 나머지를 setactive false로 돌림
                    {
                        inventoryIcon[i].gameObject.SetActive(false);
                        continue;
                    }
                    inventoryIcon[i].gameObject.SetActive(true);
                    inventoryIcon[i].GetComponent<RoyaltyIcon>().Weapondata = Gamemanager.instance.saveManaged.ownRoyalty[i].weaponData;
                }
                break;
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
