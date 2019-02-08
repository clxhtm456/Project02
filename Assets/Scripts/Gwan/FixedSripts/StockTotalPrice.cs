//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class StockTotalPrice : MonoBehaviour
//{
//    public CountCheck countCheck;

//    public Text totalPriceText;
    
//    public  float totalPrice;

//    private void OnDisable()
//    {
//        totalPrice = 0;
//        totalPriceText.text = string.Format("총 금액 : {0}", totalPrice);
//    }
//    private void Update()
//    {
//        totalPrice = PriceAWeek.price * countCheck.count;
//        totalPriceText.text = string.Format("총 금액 : {0}", totalPrice);
//    }
//}
