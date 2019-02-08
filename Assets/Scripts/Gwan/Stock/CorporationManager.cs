//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CorporationManager : MonoBehaviour
//{

//    public GameObject corporations;
    
//    CorporationStock[] corporationStock;
//    StockGraph stockGraph;
//    public float[] price;
//    public static float[] corporationShare;
//    public static int corporationCount;
//    public static int corKey;
//    public static bool corChanged;

//    void OnEnable()
//    {
//        corporationStock = corporations.GetComponentsInChildren<CorporationStock>();
//        corporationCount = corporations.transform.childCount;
//    }

//    void Start()

//    {
//        stockGraph = GetComponent<StockGraph>();
//        price = new float[corporationCount];
//        corporationShare = new float[corporationCount];

//        for (int i = 0; i < corporationCount; i++)
//        {
//            price[i] = corporationStock[i].price;
//            corporationShare[i] = corporationStock[i].corporationShare;

//            stockGraph.AddGraph(price[i], corporationStock[i].corporationConfirm);
//        }


//    }



//    private void Update()
//    {
//        corporationStock[corKey].corporationShare = corporationShare[corKey];

//        if (DayBtn.dayChanged)
//        {
//            for (int i = 0; i < corporationCount; i++)
//            {
//                corporationStock[i].price = Random.Range(1000, 10000);
//                price[i] = corporationStock[i].price;

//                stockGraph.AddGraph(price[i], corporationStock[i].corporationConfirm);

//            }

//            DayBtn.dayChanged = false;
//        }

//    }


//    public void CorKey(int key)
//    {
//        corKey = key;
//        UiManager.corBtnSet = true;
//        corChanged = true;
//        PriceAWeek.SetPriceAweek();
//        stockGraph.SetCorStockGraph();
//    }


//}
