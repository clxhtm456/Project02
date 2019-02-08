using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockManager : Singleton<StockManager>
{
    public enum STOCKSTATE//주식 성공 현황
    {
        BIGSUCCESS = 11111,
        SUCCESS = 22222,
        NORMAL = 33333,
        FAIL = 44444
    }
    public void NewStockEvent()//첫게임 시작시 주식이벤트 랜덤 적용
    {
        //랜덤 숫자섞기
        int[] eventTrigger = {11111,22222,22222,33333,33333,33333,33333,44444,44444,44444 };
        int[] tempList = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int idx, old;
        for(int i = 0; i<tempList.Length;i++)
        {
            idx = Random.Range(0, 9);
            old = tempList[i];
            tempList[i] = tempList[idx];
            tempList[idx] = old;
        }
        for (int i = 0; i < tempList.Length; i++)//이벤트 대입&& 처음 그래프 그리기
        {
            Gamemanager.instance.saveManaged.stockState[tempList[i]].EventTrigger = eventTrigger[i];
            Gamemanager.instance.saveManaged.stockState[tempList[i]].stockRecentPrice = (int)(Gamemanager.instance.saveManaged.stockState[tempList[i]].stockRecentPrice/Gamemanager.instance.saveManaged.stockState[tempList[i]].EventRate(eventTrigger[i]));
            for(int j = 0; j < 21; j++)
                Gamemanager.instance.saveManaged.stockState[tempList[i]].TurnEvent();

        }
    }
    public void CalcStockPrice()
    {
        for (int i = 0; i < Gamemanager.instance.saveManaged.stockState.Count; i++)
        {
            Gamemanager.instance.saveManaged.stockState[i].TurnEvent();
        }
    }
        //public void CalcProbability() //확률계산
        //{
        //    float probability = Random.Range(0.0f, 100.0f); //상승폭확률 85%:3이하변동 | 10%:10이하변동 | 3%:20이하변동 | 2: 30이하변동
        //    if (probability < 2)
        //    {
        //        calPercent = 0.3f;
        //        calPercent2 = 0.2f;
        //        print("2프로확률");
        //    }
        //    else if (probability < 5)
        //    {
        //        calPercent = 0.2f;
        //        calPercent2 = 0.1f;
        //        print("3프로확률");
        //    }
        //    else if (probability < 15)
        //    {
        //        calPercent = 0.1f;
        //        calPercent2 = 0.03f;
        //        print("10프로확률");
        //    }
        //    else
        //    {
        //        calPercent = 0.03f;
        //        calPercent2 = 0;
        //        print("85프로확률");
        //    }
        //}
        //public void SetPlusMinus(int index)
        //{
        //    int IorDprobability = Random.Range(0, 2); //상승인지 하락인지 정하기위한 랜덤변수

        //    percent = Random.Range(calPercent2, calPercent);


        //    if (IorDprobability == 1) //감소
        //    {
        //        percent = -(percent);
        //        print("감소");
        //        stockList[index].decreaseCount++;

        //        if (stockList[index].increaseCount != 0)
        //        {
        //            stockList[index].increaseCount = 0;
        //        }

        //    }

        //    else
        //    {
        //        print("성장");
        //        stockList[index].increaseCount++;
        //        if (stockList[index].decreaseCount != 0)
        //        {
        //            stockList[index].decreaseCount = 0;
        //        }

        //    }
        //    if (stockList[index].increaseCount >= 5 || stockList[index].decreaseCount >= 5)
        //    {
        //        stockList[index].isTrendTime = true;
        //    }

        //}
    }
