using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporationStock : MonoBehaviour
{


    public string corName;

    public float price;
    public float corporationShare;

    public int corporationConfirm;

    void Awake()

    {
        price = Random.Range(1000, 10000);
        corporationShare = 100000; //일단 10만으로 정해놓음 

    }

    

}
