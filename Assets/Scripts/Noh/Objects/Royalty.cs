using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Royalty{

    public Weapon weaponData;
    public int royalState;

    public int RoyaltyPrice()
    {
        return (int)(weaponData.ItemPrice * 0.04f);
    }
    public void ActiveRoyal(int _temp)
    {
        royalState = _temp;
    }
}
