//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PriceAWeek : MonoBehaviour
//{
//    Text priceText;

//    public GameObject corporations;

//    static CorporationStock[] corporationStock;
    
//    public static float price;


//    private void Start()
//    {
//        priceText = GetComponent<Text>();
//        corporationStock = corporations.GetComponentsInChildren<CorporationStock>();
//    }

//    private void OnDisable()
//    {
//        price = 0;
//        priceText.text = string.Format("현재 주당 가격 : {0}", price);
//    }
//    private void Update()
//    {
//        priceText.text = string.Format("현재 주당 가격 : {0}", price);
//    }
    
//    public static void SetPriceAweek()
//    {
//        price = corporationStock[CorporationManager.corKey].price;
//    }

   

//}
