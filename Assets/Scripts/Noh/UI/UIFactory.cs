using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIFactory : UIBase
{
    public GameObject factoryStore;
    public GameObject prodution;
    private FactoryOption selected;

    public GameObject factorys;
    public GameObject possessionFactorys;

    public GameObject possessionPatents;
    public GameObject possessionPatentList;
    ArrayList patents = new ArrayList();

    public GameObject AddFactory;
    public Transform activedFactory;

    public void SwitchtradeForm(bool _value)
    {
        factoryStore.SetActive(_value);
        prodution.SetActive(!_value);
    }

    public override void OpenUI()
    {
        Vector3 pos = factorys.transform.position;
        factorys.transform.position = new Vector3(pos.x, 0);
        base.OpenUI();
    }
    private void Start()
    {
        for (int i = 0; i < factorys.transform.childCount; i++)
        {
            FactoryOption factoryOption = factorys.transform.GetChild(i).GetComponent<FactoryOption>();
            factoryOption.itemName =
                factorys.transform.GetChild(i).transform.Find("NameAndExplanation").Find("FactoryName").GetComponent<Text>().text; //공장이름텍스트 넣기
            factoryOption.itemContext
                = factorys.transform.GetChild(i).transform.Find("LimitAndValue").Find("RateText").GetComponentInChildren<Text>().text; //공장설명텍스트 넣기
            factorys.transform.GetChild(i).Find("IconBack").Find("FactoryIcon").GetComponent<Image>().sprite
                = Resources.Load<Sprite>("Icon\\" + factoryOption.iconEntry.ToString());
            factorys.transform.GetChild(i).Find("LimitAndValue").Find("RateText").GetComponentInChildren<Text>().text
                += string.Format("{0:P0}", (factoryOption.earningRate - 1));

            possessionFactorys.transform.GetChild(i).transform.Find("Name").GetComponent<Text>().text = factoryOption.itemName;
            possessionFactorys.transform.GetChild(i).transform.Find("Content").GetComponent<Text>().text
                = DataManager.instance.FindTextTable("Entry", "204002")["Text"] + string.Format("{0:P0}", (factoryOption.earningRate - 1));
        }



    }
    public void BuyFactoryBtn(FactoryOption _value)
    {
        selected = _value;
        UnityAction confirm;
        if (_value.itemPrice <= Gamemanager.instance.PlayerMoney)
        { confirm = BuyConfirm; }
        else
        { confirm = BuyCancle; }
        UIManager.instance.confirmPanel.CreateUIConfirm(null, confirm, "구매확인", "정말 구매하시겠습니까?");
    }
    public void ProductionBtn()
    {
        UIManager.instance.confirmPanel.CreateUIConfirm(null, confirm2, "확인", "대량생산을 시작하시겠습니까?");
    }
    public override void ResetPanel()
    {

        for (int i = 0; i < Gamemanager.instance.saveManaged.ownFactory.Count; i++)
        {

            //소유공장활성화
            Transform activeFactory = possessionFactorys.transform.Find(Gamemanager.instance.saveManaged.ownFactory[i].name);
            Color alpha = activeFactory.GetComponent<Image>().color;
            alpha.a = 255 / 255f;
            activeFactory.GetComponent<Image>().color = alpha;
            activeFactory.Find("IconImage").GetComponent<Image>().color = alpha;
            activeFactory.Find("Name").GetComponent<Text>().color = alpha;
            activeFactory.Find("Content").GetComponent<Text>().color = alpha;
            //

            for (int j = 0; j < factorys.transform.childCount; j++) //구매한것 버튼 비활성화
            {
                if (factorys.transform.GetChild(j).name == Gamemanager.instance.saveManaged.ownFactory[i].name)
                    factorys.transform.GetChild(j).GetComponent<Button>().enabled = false;
            }


        }

        int ownRoyaltyCount = Gamemanager.instance.saveManaged.ownRoyalty.Count;
        int rowCount = possessionPatentList.transform.childCount;
        int needRowCount = (ownRoyaltyCount / (rowCount + 1)) + 1;

        for (int i = 0; i < needRowCount; i++)
        {
            if (possessionPatents.transform.childCount < needRowCount)
            {
                patents.Add(Instantiate(possessionPatentList, possessionPatents.transform));
            }
            if (possessionPatents.transform.childCount > needRowCount)
            {
                patents.RemoveAt(needRowCount);
                Destroy(possessionPatents.transform.GetChild(needRowCount).gameObject);
            }
            GameObject patentList = (GameObject)patents[i];

            if (ownRoyaltyCount < rowCount * (i + 1))
            {
                int inactiveCount = rowCount * (i + 1) - ownRoyaltyCount;
                while (inactiveCount > 0)
                {
                    patentList.transform.GetChild(rowCount - inactiveCount).gameObject.SetActive(false);
                    inactiveCount--;
                }
            }
            for (int j = i * rowCount; j < ownRoyaltyCount; j++) //행의 소유특허 정렬(초기화)
            {
                if (j == (i + 1) * rowCount)
                { continue; }

                patentList.transform.GetChild(j % rowCount).gameObject.SetActive(true);

                patentList.transform.GetChild(j % rowCount).Find("IconPanel").GetComponent<Image>().sprite
                    = Resources.Load<Sprite>("Icon\\Royal0" + Gamemanager.instance.saveManaged.ownRoyalty[j].weaponData.Rareity.ToString());//특허패널

                patentList.transform.GetChild(j % rowCount).Find("IconPanel").transform.Find("IconImage").GetComponent<Image>().enabled = true;
                patentList.transform.GetChild(j % rowCount).Find("IconPanel").transform.Find("IconImage").GetComponent<Image>().sprite
                    = Gamemanager.instance.saveManaged.ownRoyalty[j].weaponData.LoadIcon();//무기아이콘
                int temp = (int)(Gamemanager.instance.saveManaged.ownRoyalty[j].weaponData.TotalScore / 33.3f);
                temp = temp == 0 ? temp = 1 : temp;
                patentList.transform.GetChild(j % rowCount).Find("IconPanel").transform.Find("BackGround").GetComponent<Image>().enabled = true;
                patentList.transform.GetChild(j % rowCount).Find("IconPanel").transform.Find("BackGround").GetComponent<Image>().sprite
                    = Resources.Load<Sprite>("Icon\\icon_bg_" + Gamemanager.instance.saveManaged.ownRoyalty[j].weaponData.Rareity.ToString() + temp.ToString());//백그라운드이미지
                patentList.transform.GetChild(j % rowCount).Find("IconPanel").transform.Find("Outline").GetComponent<Image>().enabled = true;
                patentList.transform.GetChild(j % rowCount).Find("IconPanel").transform.Find("Outline").GetComponent<Image>().sprite
                    = Resources.Load<Sprite>("Icon\\icon_sui_" + Gamemanager.instance.saveManaged.ownRoyalty[j].weaponData.weaponElement.ToString() + temp.ToString());//아웃라인이미지

                patentList.transform.GetChild(j % rowCount).Find("PatentName").GetComponent<Text>().text
                    = Gamemanager.instance.saveManaged.ownRoyalty[j].weaponData.itemName; //무기이름텍스트
            }
        }

        if (ownRoyaltyCount < 1)
        {
            possessionPatents.transform.DetachChildren();
        }
    }
    void BuyConfirm()
    {


        Gamemanager.instance.PlayerMoney -= selected.itemPrice;
        Factory factory = new Factory();
        factory.name = selected.name;
        factory.itemOption.itemName = selected.itemName;
        factory.itemOption.itemContext = selected.itemContext;
        factory.itemOption.iconEntry = selected.iconEntry;
        factory.earningRate = selected.earningRate;
        Gamemanager.instance.saveManaged.ownFactory.Add(factory);
        ResetPanel();


    }
    void BuyCancle()
    {
        print("금액이 부족합니다");
    }
    void confirm2()
    {

    }
}
