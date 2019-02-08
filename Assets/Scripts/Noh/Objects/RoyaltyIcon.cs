using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoyaltyIcon : MonoBehaviour {
    private Weapon weaponData;
    private Image royalPanel;
    private Image bg;
    private Image icon;
    private Image outLine;
    private Text iname;
    private Text rareity;
    // Use this for initialization

    void Start()
    {

    }
    public Weapon Weapondata
    {
        set
        {
            if (gameObject.activeInHierarchy == false)
                return;
            if (royalPanel == null)
                royalPanel = GetComponent<Image>();
            if (bg == null)
                bg = transform.Find("BackGround").GetComponent<Image>();
            if (icon == null)
                icon = transform.Find("IconImage").GetComponent<Image>();
            if (outLine == null)
                outLine = transform.Find("Outline").GetComponent<Image>();
            if (iname == null)
                iname = transform.Find("Name").GetComponent<Text>();
            if (rareity == null)
                rareity = transform.Find("Rare").GetComponent<Text>();

            
            weaponData = value;
            
            int temp = (int)(weaponData.TotalScore / 33.3f);
            temp = temp == 0 ? temp = 1 : temp;
            //Debug.Log("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
            royalPanel.sprite = Resources.Load<Sprite>("Icon\\Royal0" + weaponData.Rareity.ToString());
            bg.sprite = Resources.Load<Sprite>("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
            outLine.sprite = Resources.Load<Sprite>("Icon\\icon_sui_" + weaponData.weaponElement.ToString() + temp.ToString());
            icon.sprite = weaponData.LoadIcon();
            iname.text = weaponData.itemName;
            temp = weaponData.Rareity;
            rareity.text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == (temp + 1008))["Text"].ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
