using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UIGraph : MonoBehaviour
{
    private Dot[] dotList;
    private int dotCount;
    private bool start;
    private int graphLength;
    RectTransform recttransform;
    RectTransform rectTransform2;
    public float speed;
    private float maxHeight;
    public int[] dotValue;
    private UILineRenderer uilineRenderer;


    private void Awake()
    {
        dotList = GetComponentsInChildren<Dot>();
        dotValue = new int[dotList.Length];
        maxHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y;
    }
    void Start()
    {
        if (speed <= 0)
            speed = 1.0f;
    }
    public void ResetGraph()
    {
        for (int i = 0; i < dotList.Length; i++)
        {
            uilineRenderer = dotList[i].GetComponent<UILineRenderer>();
            Vector2[] tempvecter = new Vector2[2];

            for (int j = 0; j < 2; j++)
            {
                tempvecter[j] = new Vector3(0, 0);
            }
            uilineRenderer.Points = tempvecter;
        }
        dotCount = 0;
        CalcValue();
        start = false;
    }


    void Update()
    {
        if (dotCount < graphLength - 1)
            DrawLine();
    }
    public void SetStart(int[] _list = null)
    {//초기화
        graphLength = _list.Length;
        for (int i = 0; i < _list.Length; i++)
        {
            uilineRenderer = dotList[i].GetComponent<UILineRenderer>();
            Vector2[] tempvecter = new Vector2[2];

            for (int j = 0; j < 2; j++)
            {
                tempvecter[j] = new Vector3(0, 0);
            }
            uilineRenderer.Points = tempvecter;
        }
        dotCount = 0;
        if (_list != null)
        {
            //dotValue = _list;
            int min = _list[0];
            int max = _list[0];
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i] <= min)
                    min = _list[i];
                if (_list[i] >= max)
                    max = _list[i];
            }
            for (int i = 0; i < _list.Length; i++)
            {

                recttransform = dotList[i].GetComponent<RectTransform>();
                Vector2 pos;
                ////높이계산
                pos = recttransform.transform.localPosition;
                float temp;

                if (min == max)
                    temp = 0;
                else
                    temp = ((float)(_list[i] - min) / (max - min)); //최대 최소 비율

                pos.y = temp * maxHeight - maxHeight * 0.5f;
                recttransform.transform.localPosition = pos;

            }
            

            CalcValue();

            start = true;
        }
    }
    void CalcValue()
    {

        uilineRenderer = dotList[dotCount].GetComponent<UILineRenderer>();
        Vector2[] tempvecter = new Vector2[2];

        for (int i = 0; i < 2; i++)
        {
            tempvecter[i] = new Vector3(0, 0);
        }
        uilineRenderer.Points = tempvecter;
        if (dotCount < dotList.Length - 1)
        {
            recttransform = dotList[dotCount + 1].transform.parent.GetComponent<RectTransform>();
            rectTransform2 = dotList[dotCount].transform.parent.GetComponent<RectTransform>();
        }
        //그래프 각도 세팅
    }
    void DrawLine()
    {
        if (!start)
            return;
        var destination = new Vector3(recttransform.anchoredPosition.x - rectTransform2.anchoredPosition.x, dotList[dotCount + 1].transform.localPosition.y - dotList[dotCount].transform.localPosition.y);

        uilineRenderer.Points[1] = Vector2.MoveTowards(uilineRenderer.Points[1], destination, speed);
        uilineRenderer.SetAllDirty();


        if (Vector3.Distance(uilineRenderer.Points[1], destination) <= 0.1f)
        {
            dotCount++;
            CalcValue();
        }

    }
}
