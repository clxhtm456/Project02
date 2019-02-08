using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGraphBar : MonoBehaviour {
    public Dot[] dotList;
    private int dotCount;
    private bool start;
    RectTransform recttransform;
    public float speed;
    private float maxHeight;
    public int[] dotValue = new int[ConstManager.STOCKPRICEMAX];
    // Use this for initialization
    private void Awake()
    {
        dotList = GetComponentsInChildren<Dot>();
        maxHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y;
    }
    void Start () {
        if (speed <= 0)
            speed = 1.0f;
        SetStart();

    }
    public void ResetGraph()
    {
        dotCount = 0;
            for (int i = 0; i < dotList.Length; i++)
            {
                recttransform = dotList[i].GetComponent<RectTransform>();
                Vector2 pos = recttransform.sizeDelta;
                pos.x = 0.0f;
                recttransform.sizeDelta = pos;
                pos = recttransform.transform.localScale;
                pos.x = 0.0f;
                recttransform.transform.localScale = pos;
            }
            start = false;
    }

    // Update is called once per frame
    void Update () {
        if (dotCount < dotList.Length - 1) ;
            //DrawLine();
    }
    public void SetStart(int[] _list = null)
    {//초기화
        dotCount = 0;
        if (_list != null)
        {
            dotValue = _list;
            int min = dotValue[0];
            int max = dotValue[0];
            for (int i = 0; i < dotValue.Length; i++)
            {
                if (dotValue[i] <= min)
                    min = dotValue[i];
                if (dotValue[i] >= max)
                    max = dotValue[i];
            }
            for (int i = 0; i < dotList.Length; i++)
            {
                recttransform = dotList[i].GetComponent<RectTransform>();
                Vector2 pos = recttransform.sizeDelta;
                pos.x = 0.0f;
                recttransform.sizeDelta = pos;
                pos = recttransform.transform.localScale;
                pos.x = 0.0f;
                recttransform.transform.localScale = pos;
                ////높이계산
                pos = recttransform.transform.localPosition;
                float temp = ((float)(dotValue[i] - min) / (max - min));
                pos.y = temp * maxHeight- maxHeight*0.5f;

                recttransform.transform.localPosition = pos;

            }


            CalcValue();
            start = true;
        }
    }
    void CalcValue()
    {
        if (dotCount >= dotList.Length-1)//예외처리
            return;
        recttransform = dotList[dotCount].GetComponent<RectTransform>();
        float distance = Vector2.Distance(dotList[dotCount].transform.position, dotList[dotCount + 1].transform.position);
        Vector2 pos = recttransform.sizeDelta;
        //다음지점좌표를 찾아 그 길이만큼 그래프 크기 세팅
        pos.x = distance;
        recttransform.sizeDelta = pos;
        float Angle = Mathf.Atan2(dotList[dotCount].transform.position.y - dotList[dotCount+1].transform.position.y,
            dotList[dotCount].transform.position.x - dotList[dotCount+1].transform.position.x);
        float lAngle = 180 + Angle * 180 / 3.14159274f;
        Vector3 rot = dotList[dotCount].transform.localEulerAngles;
        rot.z = lAngle;
        dotList[dotCount].transform.localEulerAngles = rot;
        //그래프 각도 세팅
    }
    void DrawLine(GameObject _tempdot)
    {
        if (!start)
            return;

        _tempdot.transform.localPosition = Vector3.MoveTowards(_tempdot.transform.localPosition, dotList[dotCount + 1].transform.localPosition, Time.deltaTime * speed);
       
        //pos.x += Time.deltaTime * speed;
        //dotList[dotCount].transform.localScale = pos;
        ////그래프 그리기
        //if(pos.x >= 1.0f)//그래프 길이 도달시 다음그래프로
        //{
        //    dotCount++;
        //    CalcValue();
        //}
    }
}
