using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StockData
{
    public Queue<int> stockPrice = new Queue<int>();//이전가격
    
    public int[] StockPriceArray//총가격배열
    {
        get
        {
            int[] tempArray = new int[stockPrice.Count + 1];
            tempArray[stockPrice.Count] = stockRecentPrice;
            stockPrice.ToArray().CopyTo(tempArray, 0);
            return tempArray;
        }
    }
    public string stockStringName;//주식 한글이름
    public int stockName;//(==key) 
    public int stockCount;//발행주식수
    public int stockRecentPrice;//현재가격

    private int eventTrigger;//상승이벤트트리거
    public int goalPrice;//기본상승 목표가격
    private int trendTimeCount; //기본상승폭 지속시간
    private int newsgoalPrice;//뉴스상승목표가격
    private int newsTimeCount;//뉴스 상승폭 지속시간
    public float EventRate(int _trigger)
    {
        switch (_trigger)
        {
            case 11111:
                return 2.0f;
            case 22222:
                return 1.5f;
            case 33333:
                return 1.2f;
            case 44444:
                return 1.1f;
            default:
                return 1.2f;
        }
    }
    public void TurnEvent()
    {
        if (newsTimeCount > 0)
        {
            if (newsTimeCount == 1)
            {
                SetPrice(newsgoalPrice);
                newsTimeCount--;
            }
            else
            {
                int goalPerPoint = (newsgoalPrice - stockRecentPrice) / newsTimeCount--;
                int limit = (int)(stockRecentPrice * 0.3f);
                float ratio = 50 * (1 - (goalPerPoint / (float)limit));
                if (Random.Range(0, 100) < ratio)
                {
                    //감소
                    SetPrice(stockRecentPrice - (int)(goalPerPoint * ((ratio + 50) * 0.01f)));
                }
                else
                {
                    //증가
                    SetPrice(stockRecentPrice + (int)(goalPerPoint * ((ratio + 50) * 0.01f)));
                }
            }
        }
        else
        {
            if (trendTimeCount <= 0)
            {
                EventStart(EventRate(eventTrigger));
            }
            int goalPerPoint = (goalPrice - stockRecentPrice) / trendTimeCount--;
            int limit = (int)(stockRecentPrice * 0.3f);
            if (goalPerPoint >= limit)
                goalPerPoint = limit;
            float ratio = 50 * (1 - (goalPerPoint / (float)limit));
            if (Random.Range(0, 100) < ratio)
            {
                //감소
                SetPrice(stockRecentPrice - (int)(goalPerPoint * ((ratio + 50) * 0.01f)));
            }
            else
            {
                //증가
                SetPrice(stockRecentPrice + (int)(goalPerPoint * ((ratio + 50) * 0.01f)));
            }
        }
    }
    public void PriceEvent(float _pwr, int _turn = 3)
    {
        newsgoalPrice = (int)(stockRecentPrice * _pwr);
        newsTimeCount = _turn;
    }
    public void EventStart(float _pwr,int _turn = 15)
    {
        goalPrice = (int)(stockRecentPrice * _pwr);
        trendTimeCount = _turn;
    }
    public int EventTrigger
    {
        get { return eventTrigger; }
        set { eventTrigger = value; }
    }
    public void SetPrice(int _price)
    {
        
        stockPrice.Enqueue(stockRecentPrice);
        //Debug.Log(stockName+" 상승폭 : "+ EventRate(eventTrigger) +"배"+ "가격" + stockRecentPrice);
        if (stockPrice.Count > ConstManager.STOCKPRICEMAX-1)
            stockPrice.Dequeue();
        stockRecentPrice = _price;
        
    }
}
