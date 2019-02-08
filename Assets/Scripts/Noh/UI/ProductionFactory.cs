using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionFactory : MonoBehaviour
{
   public bool isSelected;
    public int iconEntry;
    public int itemEntry;
    public string itemName;
    public float earningRate;
    public Sprite LoadIcon()
    {
        Sprite itemIcon;
        itemIcon = Resources.Load<Sprite>("Icon\\" + iconEntry.ToString());
        return itemIcon;
    }
    public GameObject PossessionFactory;
    public GameObject PossessionFactorys;

    public Button[] buttonList;

    private void Awake()
    {
        buttonList = PossessionFactorys.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttonList.Length; i++)
        {
            int temp = i;
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].onClick.AddListener(() => SelectFactory(temp));

        }

    }
    public void SelectFactory(int index)
    {

        for (int i = 0; i < Gamemanager.instance.saveManaged.ownFactory.Count; i++)
        {

            if (Gamemanager.instance.saveManaged.ownFactory[i].name == buttonList[index].transform.name)
            {
                iconEntry = Gamemanager.instance.saveManaged.ownFactory[i].itemOption.iconEntry;
                itemName = Gamemanager.instance.saveManaged.ownFactory[i].itemOption.itemName;
                earningRate = Gamemanager.instance.saveManaged.ownFactory[i].earningRate;

                UIManager.instance.confirmPanel.CreateUIConfirm(null, SetFactory, "확인", GetComponent<ProductionFactory>().itemName + "을 선택하시겠습니까?");
            }
        }

    }

    public void SetFactory()
    {
        PossessionFactory.SetActive(false);
        isSelected = true;
        transform.Find("IconPanel").Find("IconImage").GetComponent<Image>().sprite
            = LoadIcon();
        transform.Find("Name").GetComponent<Text>().text
            = itemName;
    }
}
