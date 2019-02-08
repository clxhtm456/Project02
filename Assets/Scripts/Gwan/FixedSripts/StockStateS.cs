//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class StockStateS : MonoBehaviour {
    
    
//    Text stockHoldingText;
//    Text averagePriceText;
//    Text shareCountText;

//    private void Start()
//    {
//        stockHoldingText = transform.Find("StockHolding").GetComponentInChildren<Text>();
//        averagePriceText = transform.Find("AveragePrice").GetComponentInChildren<Text>();
//        shareCountText = transform.Find("ShareCount").GetComponentInChildren<Text>();

//    }

//    void Update()
//    {
        
//        stockHoldingText.text = string.Format("보유 주식 :{0}", PlayerInfo.possessionStock[CorporationManager.corKey]);
//        averagePriceText.text = string.Format("보유 주식 평단가\n{0}", PlayerInfo.averagePrice[CorporationManager.corKey]);
//        shareCountText.text = string.Format("지분율 : {0:P} ", PlayerInfo.possessionStock[CorporationManager.corKey]
//            / CorporationManager.corporationShare[CorporationManager.corKey]);

//    }
//}
