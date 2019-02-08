using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponComp : UIBase {
    public GameObject itemIcon;
    public Text itemName;
    public Text itemRarity;
    public Text itemContext;
    public Text itemContext2;
    public Text itemCost;
    private Image[] selectedItemImage = new Image[3];
    // Use this for initialization
    void SelectedItemIcon(bool _temp)
    {
        itemIcon.gameObject.SetActive(_temp);
        if (!_temp)
            return;
        Weapon weaponData = CraftManager.instance.resultWeapon;
        if (gameObject.activeInHierarchy == false)
            return;
        if (selectedItemImage[0] == null)
            selectedItemImage[0] = itemIcon.transform.Find("BackGround").GetComponent<Image>();
        if (selectedItemImage[1] == null)
            selectedItemImage[1] = itemIcon.transform.Find("IconImage").GetComponent<Image>();
        if (selectedItemImage[2] == null)
            selectedItemImage[2] = itemIcon.transform.Find("Outline").GetComponent<Image>();
        int temp = (int)(weaponData.TotalScore / 33.3f);
        temp = temp == 0 ? temp = 1 : temp;
        //Debug.Log("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
        selectedItemImage[0].sprite = Resources.Load<Sprite>("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
        selectedItemImage[2].sprite = Resources.Load<Sprite>("Icon\\icon_ui_" + weaponData.weaponElement.ToString() + temp.ToString());
        selectedItemImage[1].sprite = weaponData.LoadIcon();
    }
    public override void ResetPanel()
    {
        base.ResetPanel();
        if(CraftManager.instance.resultWeapon != null)
        {
            SelectedItemIcon(true);
            itemName.text = CraftManager.instance.resultWeapon.itemName;
            int temp = CraftManager.instance.resultWeapon.Rareity;
            itemRarity.text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == (temp+1008))["Text"].ToString();
            itemContext.text =
                (
                 CraftManager.instance.resultWeapon.AttackPower +
            "\n" + CraftManager.instance.resultWeapon.AttackSpeed +
            "\n" + CraftManager.instance.resultWeapon.Durability +
            "\n" + CraftManager.instance.resultWeapon.MagicPower +
            "\n" + CraftManager.instance.resultWeapon.OptionCount +
            "\n" + CraftManager.instance.resultWeapon.Special
                );
            itemContext2.text = CraftManager.instance.resultWeapon.itemContext;
            itemCost.text = CraftManager.instance.resultWeapon.ItemPriceData.ToString();
        }
    }
    public override void CloseUI()
    {
        base.CloseUI();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
