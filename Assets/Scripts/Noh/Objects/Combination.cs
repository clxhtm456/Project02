using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combination : MonoBehaviour {
    public Image[] resourceIcon;
    public Text[] resourceName;
    public Text itemType;
    public Text itemAddition;
    // Use this for initialization
    public void RegWeapon(Weapon _temp)
    {
        resourceIcon[0].sprite = _temp.toolList[0].LoadIcon();
        resourceIcon[1].sprite = _temp.toolList[1].LoadIcon();
        resourceIcon[2].sprite = _temp.toolList[2].LoadIcon();

        resourceIcon[3].sprite = _temp.resourceMetal[0].LoadIcon();
        resourceIcon[4].sprite = _temp.resourceMetal[1].LoadIcon();
        resourceIcon[5].sprite = _temp.resourceMetal[2].LoadIcon();

        resourceIcon[6].sprite = _temp.resourceItem[0].LoadIcon();
        resourceIcon[7].sprite = _temp.resourceItem[1].LoadIcon();
        resourceIcon[8].sprite = _temp.resourceItem[2].LoadIcon();

        resourceName[0].text = _temp.toolList[0].itemName;
        resourceName[1].text = _temp.toolList[1].itemName;
        resourceName[2].text = _temp.toolList[2].itemName;

        resourceName[3].text = _temp.resourceMetal[0].itemName;
        resourceName[4].text = _temp.resourceMetal[1].itemName;
        resourceName[5].text = _temp.resourceMetal[2].itemName;

        resourceName[6].text = _temp.resourceItem[0].itemName;
        resourceName[7].text = _temp.resourceItem[1].itemName;
        resourceName[8].text = _temp.resourceItem[2].itemName;

        int temp = _temp.Type;
        itemType.text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == (temp + 1017))["Text"].ToString();
        temp = _temp.AdditionalWork;
        if (temp != -1)
            itemAddition.text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == (temp + 1022))["Text"].ToString();
        else
            itemAddition.text = "추가작업 없음";
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
