using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellBtn : MonoBehaviour
{



    public GameObject tradeBtn;
    public GameObject stateBtn;
    Color activeColor;
    Color disactiveColor;
    private void Awake()
    {
        activeColor = new Color(63 / 255f, 143 / 255f, 253 / 255f); 
        disactiveColor = new Color(94 / 255f, 94 / 255f, 94 / 255f); 

    }
    public void TradeBtnClicked()
    {
        
        //tradeBtn.GetComponentInChildren<Text>().fontSize = 25;
        tradeBtn.GetComponentInChildren<Text>().color = Color.white;
    
        //stateBtn.GetComponentInChildren<Text>().fontSize = 21;
        stateBtn.GetComponentInChildren<Text>().color = disactiveColor;

    }
    public void StateBtnClicked()
    {
        
       // stateBtn.GetComponentInChildren<Text>().fontSize = 25;
        stateBtn.GetComponentInChildren<Text>().color = Color.white;
       
       // tradeBtn.GetComponentInChildren<Text>().fontSize = 21;
        tradeBtn.GetComponentInChildren<Text>().color = disactiveColor;

    }
}
