//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CountCheck : MonoBehaviour
//{

//    Text countText;

//    public int count;

//    int countVariation2s;
//    int countVariation5s;

//    float clickTimer;
//    float plusTimer;


//    bool btnClicked;
//    bool isExceedCount;

//    private void OnDisable()
//    {
//        count = 0;
//        countText.text = string.Format("{0}주", count);
//    }

//    void Start()
//    {
//        countText = transform.Find("UIPanel").transform.Find("CountCheck").transform.Find("CountText").GetComponent<Text>();
//    }


//    void Update()
//    {
//        if (CorporationManager.corChanged)  //회사바뀌면 카운트UI 0으로 초기화
//        {
//            count = 0;
//        }

//        ExceedCount();
//        CountVariation();

//        countText.text = string.Format("{0}주", count);

//    }

//    public void UpBtnDown()

//    {
//        if (!isExceedCount)
//        {
//            CorporationManager.corChanged = false;
//            btnClicked = true;
//            count += 1;
//            plusTimer = Time.deltaTime;
//        }
//    }

//    public void DownBtnDown()

//    {
//        btnClicked = true;
//        if (count > 0)
//        {
//            count -= 1;

//            plusTimer = -Time.deltaTime;
//        }
//    }

//    public void BtnUp()
//    {
//        btnClicked = false;
//        clickTimer = 0;
//    }



//    void CountVariation()
//    {

//        if (btnClicked && !isExceedCount)
//        {
//            clickTimer += plusTimer;

//            count += (int)clickTimer;

//            if (count <= 0)
//            {
//                count = 0;
//            }

//        }
//    }

//    public void ExceedCount()
//    {
//        float exceedCount = PlayerInfo.possessionMoney / PriceAWeek.price;


//        if (BuyAndSellTab.buySellConfimrCount == 1 && exceedCount <= count + 1)
//        {
//            count = (int)exceedCount;
//            isExceedCount = true;
//        }
//        else if (BuyAndSellTab.buySellConfimrCount == 2 && PlayerInfo.possessionStock[CorporationManager.corKey] <= count)
//        {
//            isExceedCount = true;
//        }
//        else
//        {
//            isExceedCount = false;
//        }
//    }



//    /*void CountVariation()


//    {

//        if (btnClicked && !isExceedCount)
//        {

//            clickTimer += Time.deltaTime;

//            if (clickTimer > 2.0f)
//            {
//                count += countVariation2s;
//            }
//            if (clickTimer > 5.0f)
//            {
//                count += countVariation5s;
//            }

//            else
//            {
//                initialTimer += Time.deltaTime;

//                if (initialTimer > 0.5f)
//                {
//                    count += countVariation2s;
//                    initialTimer = 0;
//                }

//            }

//            if (count <= 0)
//            {
//                count = 0;
//            }


//        }

//    }

//    */
//}
