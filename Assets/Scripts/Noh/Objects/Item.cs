using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Item : MonoBehaviour
{
    public Text textName;
    public Text textPrice;
    public Text textContext;
    public Button buyButton;
    public string iconSound;
    public UIShop root;
    public Image itemIcon;
    public Sprite iconSprite;
    private bool clickable = true;
    protected bool isBought;
    public ItemOptions options;
    private int banPrice;

    public bool Clickable
    {
        set
        {
            clickable = value;
            buyButton.interactable = value;
        }
        get { return clickable; }
    }
    public int BanPrice
    {
        set
        {
            banPrice = value;
        }
        get { return banPrice; }
    }
    public int ItemEntry
    {
        set { options.itemEntry = value; }
        get { return options.itemEntry; }
    }
    public string ItemName
    {
        set { options.itemName = value; }
        get { return options.itemName; }
    }
    public string ItemContext
    {
        set { options.itemContext = value; }
        get { return options.itemContext; }
    }
    public int ItemPrice
    {
        set { options.itemPrice = value; }
        get { return options.itemPrice; }
    }
    public int IconEntry
    {
        set { options.iconEntry = value; }
        get { return options.iconEntry; }
    }
    public bool IsBought
    {
        set
        {
            isBought = value;
            UIactiveable temp = GetComponent<UIactiveable>();
            if (temp != null)
                temp.Active = value;

        }
        get { return isBought; }

    }
    private void Awake()
    {
        buyButton = GetComponent<Button>();
    }
    public Sprite LoadIcon()
    {
        Sprite itemIcon;
        if (options.iconEntry != 0)
            itemIcon = Resources.Load<Sprite>("Icon\\" + options.iconEntry.ToString());
        else
            itemIcon = Resources.Load<Sprite>("Icon\\icon_item_anvil_silver");
        return itemIcon;
    }

}
