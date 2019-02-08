//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BuyAndSellTab : MonoBehaviour {

//    public GameObject buyUI;
//    public GameObject sellUI;
//    public GameObject[] uiPanel;
//    public static int buySellConfimrCount;

//  private void OnEnable()
//    {
//        for (int i = 0; i < uiPanel.Length; i++)
//        {
//            uiPanel[i].gameObject.SetActive(false);
//        }
//    }
    
//    public void BuyTab()

//    {
//        buySellConfimrCount = 1;
//        uiPanel[0].gameObject.SetActive(true);

//        buyUI.gameObject.GetComponent<CountCheck>().count = 0;
      
     
       
//    }
//    public void SellTab()

//    {
//        buySellConfimrCount = 2;
//        sellUI.gameObject.GetComponent<CountCheck>().count = 0;
      
//        uiPanel[1].gameObject.SetActive(true);
      
//    }
//}
