using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstManager {
    public const int STARTMONEY = 50000;
    //주식관련
    public const int STOCKPRICEMAX = 21;//주식가격표시날짜 개수
    public const int STOCKMAXIMUMCOUNT = 500;
    
    public const float BIGHIT = 2f; //주식상승폭~
    public const float HIT = 1.5f;
    public const float KEEP= 1.2f;
    public const float FAIL = 1.1f; //~주식상승폭
    //제조관련
    public const float FIRSTWORKLIMIT = 20.0f;//작업준비 퍼센트율
    public const float SECONDWORKLIMIT = 70.0f;//메인작업 퍼센트율
    public const float THIRDWORKLIMIT = 99.0f;//마무리작업 퍼센트율
    public const float WORKSPEED = 5.0f;//작업 스피드
    public const float ICONSPEED = 50.0f;//아이콘 이동 스피드
    public const float ADDWORKCOST = 1.1f;//추가작업에 따른 자금소모증가

    public const float USEFULLH_MAX = 0.0f;
    public const float USEFULLH_MIN = 0.0f;
    public const float USEFULLV_MAX = 0.0f;
    public const float USEFULLV_MIN = 0.0f;
    public const float USEFULLD_MAX = 3.0f;
    public const float USEFULLD_MIN = -1.0f;
    public const float USEFULLM_MAX = 3.0f;
    public const float USEFULLM_MIN = -2.0f;
    public const float USEFULLA_MAX = 4.0f;
    public const float USEFULLA_MIN = 0.0f;

    public const float LEAVALANCE_MAX = 3.0f;
    public const float LEAVALANCE_MIN = -3.0f;
    public const float LEASENSE_MAX = 3.0f;
    public const float LEASENSE_MIN = -3.0f;

    public const float WOODUSEFULL_MAX = 3.0f;
    public const float WOODUSEFULL_MIN = -3.0f;
    public const float WOODDESIGN_MAX = 3.0f;
    public const float WOODDESIGN_MIN = -3.0f;

    public const float DESIGNH_MAX = -1.0f;
    public const float DESIGNH_MIN = 3.0f;
    public const float DESIGNV_MAX = 5.0f;
    public const float DESIGNV_MIN = -2.0f;
    public const float DESIGND_MAX = 2.0f;
    public const float DESIGND_MIN = 0.0f;
    public const float DESIGNM_MAX = 0.0f;
    public const float DESIGNM_MIN = 0.0f;
    public const float DESIGNA_MAX = 0.0f;
    public const float DESIGNA_MIN = 0.0f;

    public const float SENSEH_MAX = -1.0f;
    public const float SENSEH_MIN = 1.0f;
    public const float SENSEV_MAX = 7.0f;
    public const float SENSEV_MIN = -2.0f;
    public const float SENSED_MAX = 2.0f;
    public const float SENSED_MIN = 0.0f;
    public const float SENSEM_MAX = 0.0f;
    public const float SENSEM_MIN = 0.0f;
    public const float SENSEA_MAX = 0.0f;
    public const float SENSEA_MIN = 0.0f;

    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
    public static bool RandomEvent(float _percent)
    {
        if (_percent >= 100)
            return true;
        float max = 100;
        max /= _percent;
        if (Random.Range(0, max) <= 1)
            return true;
        else
            return false;
    }

}

