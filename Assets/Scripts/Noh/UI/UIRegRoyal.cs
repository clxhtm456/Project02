using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRegRoyal : UIBase {
    //UI컴포넌트
    public Weapon weapon;
    public GameObject weaponIcon;
    public Text wname;
    public Text wrarity;
    public Text attackPower;
    public Text attackSpeed;
    public Text durability;
    public Text magicPower;
    public Text optionPower;
    public Text specialAbil;
    public Text itemContext;
    public Text weaponPrice;
    private Image[] selectedItemImage = new Image[3];

    public Combination resourceList;

    /////////
    // Use this for initialization
    void Start () {
		
	}
    void SelectedItemIcon(bool _temp)
    {
        weaponIcon.gameObject.SetActive(_temp);
        if (!_temp)
            return;
        Weapon weaponData = weapon;
        if (gameObject.activeInHierarchy == false)
            return;
        if (selectedItemImage[0] == null)
            selectedItemImage[0] = weaponIcon.transform.Find("BackGround").GetComponent<Image>();
        if (selectedItemImage[1] == null)
            selectedItemImage[1] = weaponIcon.transform.Find("IconImage").GetComponent<Image>();
        if (selectedItemImage[2] == null)
            selectedItemImage[2] = weaponIcon.transform.Find("Outline").GetComponent<Image>();
        int temp = (int)(weaponData.TotalScore / 33.3f);
        temp = temp == 0 ? temp = 1 : temp;
        //Debug.Log("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
        selectedItemImage[0].sprite = Resources.Load<Sprite>("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
        selectedItemImage[2].sprite = Resources.Load<Sprite>("Icon\\icon_ui_" + weaponData.weaponElement.ToString() + temp.ToString());
        selectedItemImage[1].sprite = weaponData.LoadIcon();
    }
    public void RegWeapon(Weapon _temp)
    {
        weapon = _temp;
        wname.text = weapon.itemName;
        int temp = weapon.Rareity;
        wrarity.text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == (temp + 1008))["Text"].ToString();
        SelectedItemIcon(true);
        attackPower.text = weapon.AttackPower.ToString();
        attackSpeed.text = weapon.AttackSpeed.ToString();
        durability.text = weapon.Durability.ToString();
        magicPower.text = weapon.MagicPower.ToString();
        optionPower.text = weapon.OptionCount.ToString();
        specialAbil.text = weapon.Special.ToString();
        itemContext.text = weapon.itemContext;
        weaponPrice.text = "가치 "+weapon.ItemPrice.ToString();
        resourceList.RegWeapon(weapon);

        
    }
    public void RegRoyaltyConfirm()
    {
        UIManager.instance.confirmPanel.CreateUIConfirm(null, RegRoyaltyConfirm2, "특허 등록", weapon.itemName + "을 특허등록하시겠습니까?");
    }
    public void RegRoyaltyConfirm2()
    {
        Royalty royalty = new Royalty();
        royalty.weaponData = weapon;
        royalty.royalState = 0;
        Gamemanager.instance.saveManaged.ownRoyalty.Add(royalty);
        if (weapon == CraftManager.instance.resultWeapon)
            CraftManager.instance.ResetManager();
        else if (weapon == Gamemanager.instance.saveManaged.ownWeapon[UIManager.instance.inventoryPanel.inventorySlot.SelectResult])
        {
            Gamemanager.instance.saveManaged.ownWeapon.Remove(Gamemanager.instance.saveManaged.ownWeapon[UIManager.instance.inventoryPanel.inventorySlot.SelectResult]);
            CloseUI();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    
}
