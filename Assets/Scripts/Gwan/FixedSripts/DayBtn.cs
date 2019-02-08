//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI.Extensions;

//public class DayBtn : MonoBehaviour
//{
//    public GameObject corporations;

//    public StockGraph stockGraph;

//    public static int month = 1;
//    public static int day = 1;

//    public const int week = 7;
//    public const int aMonth = 30;

  
//    public static bool dayChanged;
//    //   float monthDayRate = 1 / month;
//    //public static bool excessDayRate;

//    public void NextDay()

//    {
//        day += 1;
//       StockGraph. graphDay += 1;
//        if (day > aMonth)
//        {
//            month += 1;
//            day = 1;
//        }
//        dayChanged = true;

//    }
///*
//    public void AddGraph(float price, int corConfirm) //month는 weekadd 함수(이함수)를 복붙해서 값만 바꿔서 해보기

//    {
//        float weekDayRate = 1 / (float)week;
//        float dayRate = (float)day * weekDayRate;

//        float priceRate = price / 10000;

//        if (dayRate > 1)
//        {

//            for (int j = 0; j < stockGraph.stockDayInf[corConfirm].weekList.Count; j++)
//            {
//                stockGraph.stockDayInf[corConfirm].weekList[j] -= new Vector2((weekDayRate), 0);
//            }
//            stockGraph.stockDayInf[corConfirm].weekList.RemoveAt(0);

//            dayRate = 1;

//        }
//        stockGraph.point = new Vector2(dayRate, priceRate);
//        stockGraph.stockDayInf[corConfirm].weekList.Add(stockGraph.point);

//    }*/

//    //public void AddGraph(float price, int corConfirm) //month는 weekadd 함수(이함수)를 복붙해서 값만 바꿔서 해보기

//    //{
//    //    float weekDayRate = 1 / (float)week;
//    //    float dayRate = (float)graphDay * weekDayRate;

//    //    float priceRate = price / 10000;

//    //    stockGraph.point = new Vector2(dayRate, priceRate);
//    //    stockGraph.stockDayInf[corConfirm].weekList.Add(stockGraph.point);
        
//    //    if (dayRate > 1)
//    //    {

//    //        for (int j = 0; j < stockGraph.stockDayInf[corConfirm].weekList.Count-1; j++)
//    //        {
//    //            Vector2 changeList = stockGraph.stockDayInf[corConfirm].weekList[j];
//    //            changeList.y = stockGraph.stockDayInf[corConfirm].weekList[j + 1].y;
//    //            stockGraph.stockDayInf[corConfirm].weekList[j] = changeList;
                
//    //        }
//    //         stockGraph.stockDayInf[corConfirm].weekList.RemoveAt(stockGraph.stockDayInf[corConfirm].weekList.Count-1);
//    //            //리스트의 마지막번째것 제거

//    //    }
    

//    //}
//}
