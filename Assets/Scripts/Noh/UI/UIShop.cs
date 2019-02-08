using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour {
    public GameObject itemPrefab;
    public List<Item> itemList = new List<Item>();
    public List<Dictionary<string,object>> dbItemList = new List<Dictionary<string, object>>();
    protected RectTransform recttransform;
    public Item resultItem;//결과값 아이템

    public void SelectItem(Item _value)
    {
        resultItem = _value;
        recttransform = _value.GetComponent<RectTransform>();
        recttransform.SetAsFirstSibling();
    }
}
