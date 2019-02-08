using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour {
    private Weapon weaponData;
    private Image outLine;
    private Image bg;
    private Image icon;
    // Use this for initialization

    void Start () {
		
	}
    
    public Weapon Weapondata
    {
        set
        {
            if (gameObject.activeInHierarchy == false)
                return;
            weaponData = value;
            if (bg == null)
                bg = transform.Find("BackGround").GetComponent<Image>();
            if (icon == null)
                icon = transform.Find("IconImage").GetComponent<Image>();
            if (outLine == null)
                outLine = transform.Find("Outline").GetComponent<Image>();
            int temp = (int)(weaponData.TotalScore / 33.3f);
            temp = temp == 0 ? temp = 1 : temp;
            //Debug.Log("Icon\\icon_bg_" + weaponData.Rareity.ToString() + temp.ToString());
            bg.sprite = Resources.Load<Sprite>("Icon\\icon_bg_" + weaponData.Rareity.ToString()+ temp.ToString());
            outLine.sprite = Resources.Load<Sprite>("Icon\\icon_sui_" + weaponData.weaponElement.ToString() + temp.ToString());
            icon.sprite = weaponData.LoadIcon();
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
