using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionPatent : MonoBehaviour
{
    public bool isSelected;
    
    public int itemPrice;
    public int tier;

    private int iconEntry;
    private int rareity;
    private int element;
    private string itemName;
    private float totalScore;

    private Image royalPanel;
    private Image bg;
    private Image outLine;
    public Sprite LoadIcon()
    {
        Sprite itemIcon;
        itemIcon = Resources.Load<Sprite>("Icon\\" + iconEntry.ToString());
        return itemIcon;
    }
    public GameObject PossessionPatent;
    public GameObject PossessionPatents;

    public Button[] buttonList;

    public void OpenUI()
    {
        buttonList = PossessionPatents.GetComponentsInChildren<Button>();
        print(buttonList.Length);
        for (int i = 0; i < buttonList.Length; i++)
        {
            int temp = i;
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].onClick.AddListener(() => SelectPatent(temp));

        }

    }
    public void SelectPatent(int index)
    {
        Weapon royalty = Gamemanager.instance.saveManaged.ownRoyalty[index].weaponData;
        int temp = (int)(royalty.TotalScore / 33.3f);
        temp = temp == 0 ? temp = 1 : temp;
        itemPrice = royalty.ItemPrice;
        iconEntry= royalty.iconEntry;
        itemName = royalty.itemName;
        totalScore = royalty.TotalScore;
        tier = royalty.Tier;
        rareity = royalty.Rareity;
        element = royalty.weaponElement;

        UIManager.instance.confirmPanel.CreateUIConfirm(null, SetPatent, "확인", GetComponent<ProductionPatent>().itemName + "을 선택하시겠습니까?");


    }

    public void SetPatent()
    {
        int temp = (int)(totalScore / 33.3f);
        temp = temp == 0 ? temp = 1 : temp;
        PossessionPatent.SetActive(false);
        isSelected = true;
               print("티어" + tier);
        transform.Find("RoyalPanel").GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Icon\\Royal0" + rareity.ToString());//특허패널

        transform.Find("RoyalPanel").transform.Find("Background").GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Icon\\icon_bg_" + rareity.ToString() + temp.ToString());//백그라운드이미지
        transform.Find("RoyalPanel").transform.Find("Outline").GetComponent<Image>().sprite
            = Resources.Load<Sprite>("Icon\\icon_sui_" + element.ToString() + temp.ToString());//아웃라인이미지
        transform.Find("RoyalPanel").transform.Find("IconImage").GetComponent<Image>().sprite
            = LoadIcon();//무기아이콘

        transform.Find("Name").GetComponent<Text>().text
            = itemName; //이름텍스트
    }
}
