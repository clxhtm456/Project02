using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGradStar : MonoBehaviour
{
    public int scorePoint = 0;
    public UIactiveable[] buttonList;
    public int ScorePoint
    {
        set
        {
            Debug.Log(value);
            scorePoint = value;
            for (int i =0; i< buttonList.Length;i++)
            {
                Color col = new Color(1,1,1);
                if (i < scorePoint/2)
                {
                    buttonList[i].Active = true;
                }else if(scorePoint%2==1 && i == scorePoint / 2)
                {
                    buttonList[i].Active = true;
                    col.a = 0.5f;
                }
                else
                    buttonList[i].Active = false;
                buttonList[i].GetComponent<Image>().color = col;
            }
            if (UIManager.instance.TopUI != null)
                UIManager.instance.TopUI.ResetPanel();
        }
        get
        {
            return scorePoint;
        }
    }
    public void Awake()
    {
        buttonList = GetComponentsInChildren<UIactiveable>();
    }

    void Start()
    {
    }
}
