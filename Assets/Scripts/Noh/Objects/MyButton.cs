using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour{
    public GameObject outLine;
    public UIBase buttonObject;
    private bool buttonActive = true;
    private bool action;
	// Use this for initialization
	void Start () {
    }
    public bool ButtonActive
    {
        get { return buttonActive; }
        set
        {
            //outLine = GameObject.Find("OutLine").gameObject;
            //outLine.SetActive(value);
            buttonActive = value;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(action)
        {
            if(buttonObject == null || buttonObject.gameObject.activeInHierarchy == false)
            {
                Player.instance.ActionObject = null;
                action = false;
            }
        }
	}
    public void ButtonAction()
    {
        if (UIManager.instance.TopUI != null || !buttonActive)
            return;
        if(Gamemanager.instance.saveManaged.storyStep < 20)//듀토리얼 도중
        {
            if (GetComponentInChildren<Renderer>().sortingOrder >= 11)//올바른 지시
            {
                StoryManager.instance.StoryScript();
            }
            else//틀린지시
            {
                StoryManager.instance.WrongBehavior();
                return;
            }
        }
        
        if (buttonObject)
        {
            buttonObject.OpenUI();
        }
        action = true;
    }
}
