using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StockState : MonoBehaviour
{[System.NonSerialized]
    public Text corporateName;
    [System.NonSerialized]
    public Text stockHoldingText;
    [System.NonSerialized]
    public Text averagePriceText;
    [System.NonSerialized]
    public Text shareCountText;

    private void Awake()
    {
        corporateName = transform.Find("Name").GetComponent<Text>();
        stockHoldingText = transform.Find("StockHolding").GetComponent<Text>();
        averagePriceText = transform.Find("AveragePrice").GetComponent<Text>();
        shareCountText = transform.Find("ShareCount").GetComponent<Text>();
    }

    void Update()
    {
        //corporateName.text = gameObject.name;
        //stockHoldingText.text = string.Format("보유 주식 :{0}", Gamemanager.instance.playerStock[corporationKey]);
        //averagePriceText.text = string.Format("보유 주식 평단가\n{0}", Gamemanager.instance.stockAvg[corporationKey]);
        //shareCountText.text = string.Format("지분율 : {0:P} ", Gamemanager.instance.shareRatio[corporationKey]);
         
    }
}
