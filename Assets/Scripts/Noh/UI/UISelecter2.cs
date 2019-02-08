using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelecter2 : MonoBehaviour {
    private int defaultValue = -1;
    public bool mustSelect = true;
    public Button[] buttonList;
    private int selectResult = -1;
    public string buttonSound = null;
    public int SelectResult
    {
        set {
            UIactiveable _button;
            switch(mustSelect)
            {
                case true://반드시 선택해야하는경우 눌렀을때
                    if (selectResult != value)//같은걸 눌렀을경우 아무일도 일어나지않고
                    {
                        if (selectResult != -1)
                        {
                            _button = buttonList[selectResult].GetComponent<UIactiveable>();
                            _button.Active = false;//기존의선택을 취소함
                        }
                        selectResult = value;
                        _button = buttonList[selectResult].GetComponent<UIactiveable>();
                        _button.Active = true;
                    }
                    if (UIManager.instance.TopUI)
                        UIManager.instance.TopUI.ResetPanel();
                    break;
                case false://선택취소가 가능한경우 눌렀을때
                    if (selectResult != -1)//선택된게 있엇던경우
                    {
                        _button = buttonList[selectResult].GetComponent<UIactiveable>();
                        _button.Active = false;//기존의선택을 취소함
                    }
                    if (selectResult == value)//같은걸  선택취소
                    {
                        selectResult = -1;
                    }else//다른걸 누를경우  새로운 걸 선택
                    {
                        selectResult = value;
                        if (selectResult != -1)
                        {
                            _button = buttonList[selectResult].GetComponent<UIactiveable>();
                            _button.Active = true;
                        }
                    }
                    if (UIManager.instance.TopUI)
                        UIManager.instance.TopUI.ResetPanel();
                    break;
            }
        }
        get {
            if (mustSelect && selectResult == -1)
                return 0;
            return selectResult;
        }
    }
    public void OnEnable()
    {
        buttonList = GetComponentsInChildren<Button>();
        ResetTrigger();

        for (int i = 0; i < buttonList.Length; i++)
        {
            int temp = i;
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].onClick.AddListener(delegate () { SelectorTrigger(temp); });
        }
    }
    public void Awake()
    {
        buttonList = GetComponentsInChildren<Button>();
    }

    void Start() {
        
    }
	
	
    void SelectorTrigger(int _value)
    {
        if (buttonSound!= "")
        {
            AudioManager.instance.PlayEffect(buttonSound);
        }
        { SelectResult = _value; }
    }
    public void ResetTrigger()
    {
        if (mustSelect)//처음에 선택되어있는 트리거
        {
            SelectResult = 0;
        }
        else
        {
            SelectResult = -1;
        }

    }
}
