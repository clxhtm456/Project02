using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionCost : MonoBehaviour {

    public ProductionFactory factory;
    public ProductionPatent patent;
    int initialCost;
    int profit;

    private void Update()
    {
        if (factory.isSelected && patent.isSelected)
        {
            GetComponentInChildren<Button>().enabled = true;
            CalInitialCost();
            CalProfit();
        }
        else
        {
            GetComponentInChildren<Button>().enabled = false;
        }
    
    }
    public void CalInitialCost()
    {
        initialCost = patent.itemPrice * 4;
        transform.Find("CostImagePanel").Find("InitialCost").GetComponent<Text>().text
            = DataManager.instance.FindTextTable("Entry", "204004")["Text"] + string.Format("{0:#,###}", initialCost);
    }
    public void CalProfit()
    {
       
        profit =(int)( (patent.itemPrice * patent.tier) * (1 + (patent.tier / 10f)) *factory.earningRate);
        transform.Find("CostImagePanel").Find("Profit").GetComponent<Text>().text
            = DataManager.instance.FindTextTable("Entry", "204005")["Text"] + string.Format("{0:#,###}", profit);
    }
}
