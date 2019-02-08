using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStock : UIBase
{
    private int tradeCount;
    private bool tradeState = true;//참일경우 구매 거짓일경우 판매
    private IEnumerator countEvent = null;
    private bool isUp;
    public GameObject tradePanel;
    public GameObject statePanel;
  

    public StockData selected;
    public UISelecter3 uiselecter;
    public Text text_tradeCount;
    public Text text_totalCost;
    public Text text_ownStock;
    public Text text_avgStock;
    public Text text_recStockPrice;
    public Text text_stockRatio;
    public Text text_shareSum;
    public Text text_dividends;
    public UIGraph stockGraph;
    //주식 현황
    public StockState[] stockList; 




    public void SwitchtradeForm(bool _value)
    {
        tradePanel.SetActive(_value);
        statePanel.SetActive(!_value);
        if (_value)
            CheckTradeCount();
        else
            CheckCurrentCount();
        selected = new StockData();
        
        uiselecter.ResetTrigger();
        //selected.stockName = uiselecter.SelectResult;
        selected.stockName = -1;
        stockGraph.ResetGraph();
        tradeCount = 0;
        CheckTradeCount();
    }

    // Use this for initialization
    void Start()
    {
        selected = new StockData();
        //selected.stockName = uiselecter.SelectResult;
        selected.stockName = -1;
    }
    public override void OpenUI()
    {
        base.OpenUI();
        uiselecter.ResetTrigger();
        selected = new StockData();
        //selected.stockName = uiselecter.SelectResult;
        selected.stockName =-1;
        stockGraph.ResetGraph();
        tradeCount = 0;
        CheckTradeCount();
    }
    new private void OnDisable()
    {
        //selected = null;

        //CheckTradeCount();
    }
    public void ChangeTrade(bool _value)//매수 매도 전환
    {
        tradeState = _value;
        tradeCount = 0;
        CheckTradeCount();
    }
    public void CheckCurrentCount()//텍스트 갱신
    {
        for (int i = 0; i < stockList.Length; i++)
        {
            if (Gamemanager.instance.saveManaged.playerStock.ContainsKey(i))
            {
                if (Gamemanager.instance.saveManaged.playerStock[i] > 0) //주식구매시색변형
                {
                    stockList[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphic\\NewUI\\ComputerMain\\Images\\Stock\\CurrentStateParts\\Part1");
                }
                else
                {
                    stockList[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphic\\NewUI\\ComputerMain\\Images\\Stock\\CurrentStateParts\\Part2");
                }
                stockList[i].stockHoldingText.text = "보유주식 : " + Gamemanager.instance.saveManaged.playerStock[i].ToString();
                stockList[i].averagePriceText.text = "평단가 : " + Gamemanager.instance.saveManaged.stockAvg[i].ToString();
                stockList[i].shareCountText.text = "지분율 : " + (Gamemanager.instance.saveManaged.shareRatio[i]*100.0f).ToString("00");
            }
            else
            {
                stockList[i].stockHoldingText.text = "보유주식 : " + 0;
                stockList[i].averagePriceText.text = "평단가 : " + 0;
                stockList[i].shareCountText.text = "지분율 : " + 0;

            }
           

        }
    }
    public void CheckTradeCount()//텍스트 갱신
    {
       
        text_tradeCount.text = tradeCount.ToString();
        
        if (selected.stockName != -1)
        {
            text_totalCost.text = "합계 : "+(tradeCount * selected.stockRecentPrice).ToString();
            if (Gamemanager.instance.saveManaged.playerStock.ContainsKey(selected.stockName))
                text_ownStock.text = Gamemanager.instance.saveManaged.playerStock[selected.stockName].ToString();
            text_avgStock.text = Gamemanager.instance.saveManaged.stockAvg[selected.stockName].ToString();
            text_recStockPrice.text = selected.stockRecentPrice.ToString();
            text_stockRatio.text = "지분율 : "+(Gamemanager.instance.saveManaged.shareRatio[selected.stockName] * 100).ToString("00") + "%";
            text_shareSum.text = "지분금액 : "+Gamemanager.instance.saveManaged.shareSum[selected.stockName].ToString();
            text_dividends.text = "턴당 배당금 : " + (Gamemanager.instance.saveManaged.shareSum[selected.stockName] * 0.02f).ToString();
        }
        else
        {
            text_totalCost.text = "0";
            text_recStockPrice.text = "0";
            text_avgStock.text = "0";
            text_stockRatio.text = "지분율 : " + "0"+"%";
            text_shareSum.text = "지분금액 : " + "0";
            text_ownStock.text = "0";
        }


    }

    public void IncreaseCount()//매수 매도 개수 증가
    {
        if (selected.stockName == -1)
            return;

        if (countEvent == null)
        {
            countEvent = ClickUpdate();
            StartCoroutine(countEvent);
            isUp = true;
        }
        CheckTradeCount();
    }
    public void DecreaseCount()//매수 매도 개수 감소
    {
        if (selected.stockName == -1 || tradeCount <= 0)
            return;

        if (countEvent == null)
        {
            countEvent = ClickUpdate();
            StartCoroutine(countEvent);
            isUp = false;
        }
        CheckTradeCount();
    }
    public void UnClicked()//클릭해제(코루틴 해제)
    {
        if (countEvent != null)
        {
            StopCoroutine(countEvent);
            countEvent = null;
        }
        CheckTradeCount();
    }
    IEnumerator ClickUpdate()
    {
        float timer =0.1f;
        yield return null;
        while ((!isUp && tradeCount > 0) || (isUp && tradeCount < ConstManager.STOCKMAXIMUMCOUNT))
        {
            if (isUp)//증가케이스
            {
                if (tradeState)//구매일경우 가진금액 이상으로 올라갈수없음
                {
                    if (Gamemanager.instance.PlayerMoney >= ((tradeCount + 1) * selected.stockRecentPrice)
                        && selected.stockCount > tradeCount)
                    {
                        tradeCount++;
                        timer -= Time.deltaTime;
                    }
                }
                else//판매일경우 가진개수이상으로 올라갈수없음
                {
                    if (!Gamemanager.instance.saveManaged.playerStock.ContainsKey(selected.stockName))
                    {
                        Gamemanager.instance.saveManaged.playerStock.Add(selected.stockName, 0);
                    }
                    if (Gamemanager.instance.saveManaged.playerStock[selected.stockName] >= (tradeCount + 1))
                    {
                        tradeCount++;
                        timer -= Time.deltaTime;
                    }
                }

            }
            else
            {
                tradeCount--;
                timer -= Time.deltaTime;
            }
            CheckTradeCount();
            yield return new WaitForSeconds(timer);
        }
    }
    private void BuyConfirm()
    {
        if (!Gamemanager.instance.saveManaged.playerStock.ContainsKey(selected.stockName))
        {
            Gamemanager.instance.saveManaged.playerStock.Add(selected.stockName, 0);
        }
        if (!Gamemanager.instance.saveManaged.stockAvg.ContainsKey(selected.stockName))
        {
            Gamemanager.instance.saveManaged.stockAvg.Add(selected.stockName, 0);
        }
        int temp = Gamemanager.instance.saveManaged.stockAvg[selected.stockName] * Gamemanager.instance.saveManaged.playerStock[selected.stockName];
        Gamemanager.instance.PlayerMoney -= tradeCount * selected.stockRecentPrice;
        Gamemanager.instance.saveManaged.playerStock[selected.stockName] += tradeCount;
        //평단가 계산
        temp += tradeCount * selected.StockPriceArray[selected.StockPriceArray.Length - 1];
        Gamemanager.instance.saveManaged.stockAvg[selected.stockName] = temp / Gamemanager.instance.saveManaged.playerStock[selected.stockName];

        selected.stockCount -= tradeCount;
        Gamemanager.instance.saveManaged.shareRatio[selected.stockName] = Gamemanager.instance.saveManaged.playerStock[selected.stockName] /
            (Gamemanager.instance.saveManaged.playerStock[selected.stockName]
            + selected.stockCount * 1.0f);
        Gamemanager.instance.saveManaged.shareSum[selected.stockName] += tradeCount * selected.stockRecentPrice;//StockPriceArray[selected.StockPriceArray.Length - 1];
        Debug.Log(Gamemanager.instance.saveManaged.shareSum[selected.stockName]);
        tradeCount = 0;
        CheckTradeCount();
    }
    public void BuyStock()//주식 구매
    {
        if (selected.stockName == -1 || tradeCount <= 0)
            return;
        if (Gamemanager.instance.PlayerMoney >= (tradeCount * selected.stockRecentPrice))//구매가능금액 확인
        {
            UIManager.instance.confirmPanel.CreateUIConfirm(null, BuyConfirm, "구매확인", "정말구매하시겠습니까?");
        }
        else
            Debug.Log("구매금액부족");

    }
    private void SellConfirm()
    {
        int value;
        if (Gamemanager.instance.saveManaged.playerStock.TryGetValue(selected.stockName, out value))//판매개수가 존재하는가
        {
            value -= tradeCount;
            if (value <= 0)
            {
                value = 0;
                Gamemanager.instance.saveManaged.stockAvg[selected.stockName] = 0;
            }
            Gamemanager.instance.saveManaged.playerStock[selected.stockName] = value;
            Gamemanager.instance.PlayerMoney += tradeCount * selected.stockRecentPrice;
        }
        else
            Debug.Log("주식이 없습니다");
        selected.stockCount += tradeCount;
        Gamemanager.instance.saveManaged.shareRatio[selected.stockName] = Gamemanager.instance.saveManaged.playerStock[selected.stockName] /
            (Gamemanager.instance.saveManaged.playerStock[selected.stockName]
            + selected.stockCount * 1.0f); //지분율 계산
        Gamemanager.instance.saveManaged.shareSum[selected.stockName] -= tradeCount * selected.stockRecentPrice;
        if (Gamemanager.instance.saveManaged.shareSum[selected.stockName] < 0)
            Gamemanager.instance.saveManaged.shareSum[selected.stockName] = 0;
        //지분금액 계산

        tradeCount = 0;
        CheckTradeCount();
    }
    public void SellStock()//주식판매
    {
        if (selected.stockName == -1)
            return;

        UIManager.instance.confirmPanel.CreateUIConfirm(null, SellConfirm, "판매확인", "정말판매하시겠습니까?");
    }
    public int TradeCount
    {
        get
        {
            return tradeCount;
        }
    }
   
    public void SetStock(int _stockName)
    {

        //if (selected.stockName != _stockName)
        //{
      
            tradeCount = 0;
            selected = Gamemanager.instance.saveManaged.stockState[_stockName];
            if (!Gamemanager.instance.saveManaged.playerStock.ContainsKey(selected.stockName))
            {
                Gamemanager.instance.saveManaged.playerStock.Add(selected.stockName, 0);
            }
            if (!Gamemanager.instance.saveManaged.stockAvg.ContainsKey(selected.stockName))
            {
                Gamemanager.instance.saveManaged.stockAvg.Add(selected.stockName, 0);
            }
            if (!Gamemanager.instance.saveManaged.shareSum.ContainsKey(selected.stockName))
            {
                Gamemanager.instance.saveManaged.shareSum.Add(selected.stockName, 0);
            }
            Gamemanager.instance.saveManaged.shareRatio[selected.stockName] = Gamemanager.instance.saveManaged.playerStock[selected.stockName] /
            (Gamemanager.instance.saveManaged.playerStock[selected.stockName]
            + selected.stockCount * 1.0f);
            stockGraph.SetStart(selected.StockPriceArray);

        //}
        CheckTradeCount();
    }
}
