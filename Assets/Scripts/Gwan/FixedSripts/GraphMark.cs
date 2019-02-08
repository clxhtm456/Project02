//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI.Extensions;
//public class GraphMark : MonoBehaviour
//{
//    static UILineRenderer graphLine;
//    public GameObject corporationManager;
//    public static StockGraph stockGraph;
//    public Vector2[] initiatePoint;

//    void Start()
//    {
//        graphLine = GetComponent<UILineRenderer>();
//        stockGraph = corporationManager.GetComponent<StockGraph>();
//    }

//    private void OnDisable()
//    {
//        initiatePoint = new Vector2[0];
//        graphLine.Points = initiatePoint; //나중에 봐야함 좀이상함
//    }

 
//    public static void SetCorStockGraph()

//    {
//        graphLine.Points = stockGraph.stockDayInf[CorporationManager.corKey].weekList.ToArray();
//    }

    
//}
