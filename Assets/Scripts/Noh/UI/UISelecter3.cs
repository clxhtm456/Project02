using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelecter3 : MonoBehaviour {
    private int defaultValue = -1;
    public bool mustSelect = true;
    private Button[] buttonList;
    private int selectResult;
    public int SelectResult
    {
        set {
            UIactiveable _button;
            if (selectResult != -1 || selectResult == value)
            {
                _button = buttonList[selectResult].GetComponent<UIactiveable>();
                _button.Active = false;
            }
            if (!mustSelect && selectResult == value)
            {
                selectResult = -1;
                return;
            }
            selectResult = value;
            _button = buttonList[selectResult].GetComponent<UIactiveable>();
            _button.Active = true;
            UIManager.instance.TopUI.ResetPanel();
        }
        get { return selectResult; }
    }
    // Use this for initialization
    //private void Awake()
    //{
        
    //}
    void Awake() {
        
        selectResult = -1;
        buttonList = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttonList.Length; i++)
        {
            int temp = i;
            buttonList[i].onClick.AddListener(delegate () { SelectorTrigger(temp); });
        }
        if (mustSelect)
            defaultValue = 0;
        if (defaultValue != -1)
            SelectorTrigger(defaultValue);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void SelectorTrigger(int _value)
    {
        if (selectResult != _value)
        { SelectResult = _value; }
    }
    public void ResetTrigger()
    {
        if (selectResult != -1 && buttonList != null)
        {
            UIactiveable _button;
            _button = buttonList[selectResult].GetComponent<UIactiveable>();
            _button.Active = false;
            selectResult = -1;
        }
        
    }
}
