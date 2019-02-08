//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UI.Extensions;

//public struct CorStockInf
//{
//    public List<Vector2> weekList;
//    public List<Vector2> monthList;
//}


//public class StockGraph : MonoBehaviour
//{
//    public GameObject graph;
//    UILineRenderer lineRenderer;
//    public CorStockInf[] stockDayInf;

//    public Vector2[] initiatePoint;
//    public Vector2 point;
    
//    public static int graphDay = 1;

//    private void Update()
//    {
//        if (graph.gameObject.activeInHierarchy == false)
//        {
//            initiatePoint = new Vector2[0];
//            lineRenderer.Points = initiatePoint; //나중에 봐야함 좀이상함
//        }
//    }
//    void Start()
//    {
//        lineRenderer = graph.GetComponentInChildren<UILineRenderer>();
//    }
 
//    void Awake()
//    {
   
//        stockDayInf = new CorStockInf[10];
//        for (int i = 0; i < 10; i++)
//        {
//            stockDayInf[i].weekList = new List<Vector2>() ; //{ new Vector2(0, 0) } 초기 0,0을 넣으려면 추가
//            stockDayInf[i].monthList = new List<Vector2>();
//        }

//    }


//    public void SetCorStockGraph()

//    {
//        lineRenderer.Points = stockDayInf[CorporationManager.corKey].weekList.ToArray();
     
//    }


//    public void AddGraph(float price, int corConfirm) //month는 weekadd 함수(이함수)를 복붙해서 값만 바꿔서 해보기

//    {
//        float weekDayRate = 1 / (float)DayBtn.week;
//        float dayRate = (float)graphDay * weekDayRate;

//        float priceRate = price / 10000;

//        point = new Vector2(dayRate, priceRate);
//        stockDayInf[corConfirm].weekList.Add(point);
     

//        if (dayRate > 1)
//        {

//            for (int j = 0; j < stockDayInf[corConfirm].weekList.Count - 1; j++)
//            {
//                Vector2 changeList = stockDayInf[corConfirm].weekList[j];
//                changeList.y = stockDayInf[corConfirm].weekList[j + 1].y;
//                stockDayInf[corConfirm].weekList[j] = changeList;

//            }
//           stockDayInf[corConfirm].weekList.RemoveAt(stockDayInf[corConfirm].weekList.Count - 1);
//            //리스트의 마지막번째것 제거

//        }


//    }
//}
