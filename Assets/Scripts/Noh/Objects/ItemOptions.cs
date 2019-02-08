using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ItemOptions{
    public int iconEntry;
    public int itemEntry;
    public string itemName;
    public int itemPrice;
    public string itemContext;
    public int rareity;
    public float[] parameter;
    public Sprite LoadIcon()
    {
        Sprite itemIcon;
        itemIcon = Resources.Load<Sprite>("Icon\\" + iconEntry.ToString());
        return itemIcon;
    }
}
