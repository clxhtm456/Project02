//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UiManager : MonoBehaviour
//{

//    public GameObject managementList;
//    public GameObject dayBtn;
//    public GameObject playerInfo;
//    public GameObject stockInfo;
//    public GameObject yesOrNoPanel;
//    public GameObject inAcivePanel;
//    public GameObject tradeUI;
//    public GameObject currentStateUI;
//    public GameObject storeTabBtn;
//    public GameObject productionTabBtn;
//    public GameObject storeUI;
//    public GameObject productionUI;

//    public static bool corBtnSet;
//    private void Update()
//    {

//        print(DayBtn.month+" 월 "+ DayBtn.day+" 일 ");

//        DayBtnActive();
//        InActivePanel();


//    }
//    public void DayBtnActive()
//    {
//        for (int i = 0; i < managementList.transform.childCount; i++)
//        {
//            if (managementList.transform.GetChild(i).gameObject.activeInHierarchy)
//            {
//                dayBtn.gameObject.SetActive(false);
//            }
//            else
//            {
//                dayBtn.gameObject.SetActive(true);
//            }

//        }
//    }
//    public void InActivePanel()
//    {
//        if (corBtnSet)
//        {
//            inAcivePanel.gameObject.SetActive(false); //회사목록 버튼이 활성화 되지않으면 패널위에달아서 클릭안되게하려고 만듬
//        }
//        if (!corBtnSet)
//        {

//            inAcivePanel.gameObject.SetActive(true);
//        }

//    }
//    public void StockInfOpen()

//    {
//        stockInfo.gameObject.SetActive(true);
//    }

//    public void StockInfExit()

//    {
//        corBtnSet = false;
//        stockInfo.gameObject.SetActive(false);
//    }
//    public void BuySellUI()

//    {
//        yesOrNoPanel.gameObject.SetActive(true);

//    }
//    public void BuySellYesBtn()

//    {
//        if (BuyAndSellTab.buySellConfimrCount == 1)
//        {
//            playerInfo.GetComponent<PlayerInfo>().BuyStock(PriceAWeek.price);
//        }
//        if (BuyAndSellTab.buySellConfimrCount == 2)
//        {
//            playerInfo.GetComponent<PlayerInfo>().SellStock();
//        }
//        yesOrNoPanel.gameObject.SetActive(false);

//    }

//    public void BuySellNoBtn()

//    {
//        yesOrNoPanel.gameObject.SetActive(false);
//    }


//    public void TradeUI()

//    {
//        tradeUI.gameObject.SetActive(true);
//        currentStateUI.gameObject.SetActive(false);
//    }
//    public void CurrentStateUI()

//    {
//        currentStateUI.gameObject.SetActive(true);
//        corBtnSet = false;
//        tradeUI.gameObject.SetActive(false);
//    }

//    public void StoreUI()
//    {
       
//        storeUI.gameObject.SetActive(true);
//        productionUI.gameObject.SetActive(false);
//    }
//    public void ProductionUI()
//    {
//        storeUI.gameObject.SetActive(false);
//        productionUI.gameObject.SetActive(true);
//    }
//}
