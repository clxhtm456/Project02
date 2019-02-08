using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICGraph : MonoBehaviour {
    private RectTransform rect;
    private Image[] graphImage;
    private float sumScore;
    public float[] graphValue;
    public string[] graphColor;
    public Image cgPrefab;
    // Use this for initialization
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        int temp = graphValue.Length;
        graphImage = new Image[temp];
        for (int i =0; i < temp; i++)
        {
            graphImage[i] = Instantiate(cgPrefab,transform);//자식오브젝트로 생성
            graphImage[i].rectTransform.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.x);
            graphImage[i].type = Image.Type.Filled;
            graphImage[i].fillAmount = 0.0f;
            if(graphColor.Length <= i)
            {
                Color _color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                graphImage[i].color = _color;
            }
            else if(graphColor[i] != null)
            {

                graphImage[i].color = ConstManager.hexToColor(graphColor[i]);
            }
        }
    }
    void Start () {
        float fillamount = 0.0f;
        for (int i = 0; i < graphValue.Length; i++)
        {
            graphImage[i].fillAmount = 1.0f/ graphValue.Length;
        }
        for (int i = 0; i < graphValue.Length - 1; i++)
        {
            Quaternion qua = new Quaternion();
            qua.eulerAngles = new Vector3(0, 0, -360.0f * (fillamount + graphImage[i].fillAmount));
            fillamount += graphImage[i].fillAmount;
            graphImage[i + 1].rectTransform.rotation = qua;
        }

    }
	
	// Update is called once per frame
	void Update () {
        int temp = graphValue.Length;
        float fillamount = 0.0f;
        sumScore = 0;
        for (int i = 0; i < temp; i++)
        {
            sumScore += graphValue[i];
        }
        for (int i = 0; i < temp; i++)
        {
            graphImage[i].fillAmount = Mathf.Lerp(graphImage[i].fillAmount, (graphValue[i] * 1.0f / sumScore), Time.deltaTime*2.0f);
        }

        for (int i = 0; i < temp-1; i++)
        {
            Quaternion qua = new Quaternion();
            qua.eulerAngles = new Vector3(0, 0, -360.0f * (fillamount+graphImage[i].fillAmount));
            fillamount += graphImage[i].fillAmount;
            graphImage[i+1].rectTransform.rotation = qua;
        }
        
    }
}
