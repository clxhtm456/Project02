using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelecter : MonoBehaviour
{
    public bool mustSelect = true;
    public Button[] buttonList;
    private int selectResult = -1;
    public string buttonSound;
    public int SelectResult
    {
        set
        {
            Color _color;
            switch (mustSelect)
            {
                case true://반드시 선택해야하는경우 눌렀을때
                    if (selectResult != value)//같은걸 눌렀을경우 아무일도 일어나지않고 그외의 경우만 트리거
                    {
                        if (selectResult != -1)
                        {
                            _color = buttonList[selectResult].image.color;
                            _color.r += 0.2f;
                            _color.g += 0.2f;
                            _color.b += 0.2f;
                            buttonList[selectResult].image.color = _color;//기존선택 취소
                        }
                        selectResult = value;
                        _color = buttonList[selectResult].image.color;
                        _color.r -= 0.2f;
                        _color.g -= 0.2f;
                        _color.b -= 0.2f;
                        buttonList[selectResult].image.color = _color;
                    }
                    if (UIManager.instance.TopUI)
                        UIManager.instance.TopUI.ResetPanel();
                    break;
                case false://선택취소가 가능한경우 눌렀을때
                    if (selectResult != -1)//선택된게 있엇던경우
                    {
                        _color = buttonList[selectResult].image.color;
                        _color.r += 0.2f;
                        _color.g += 0.2f;
                        _color.b += 0.2f;
                        buttonList[selectResult].image.color = _color;//기존선택 취소
                    }
                    if (selectResult == value)//같은걸  선택취소
                    {
                        selectResult = -1;
                    }
                    else//다른걸 누를경우  새로운 걸 선택
                    {
                        selectResult = value;
                        if (selectResult != -1)
                        {
                            _color = buttonList[selectResult].image.color;
                            _color.r -= 0.2f;
                            _color.g -= 0.2f;
                            _color.b -= 0.2f;
                            buttonList[selectResult].image.color = _color;
                        }
                    }
                    if (UIManager.instance.TopUI)
                        UIManager.instance.TopUI.ResetPanel();
                    break;
            }
        }
        get
        {
            if (mustSelect && selectResult == -1)
                return 0;
            return selectResult;
        }
    }
    public void OnEnable()
    {
        buttonList = GetComponentsInChildren<Button>();
        ResetTrigger();

        selectResult = -1;

        for (int i = 0; i < buttonList.Length; i++)
        {
            int temp = i;
            buttonList[i].onClick.RemoveAllListeners();
            buttonList[i].onClick.AddListener(delegate () { SelectorTrigger(temp); });
        }
    }
    // Use this for initialization
    public void Awake()
    {
        buttonList = GetComponentsInChildren<Button>();
        if (mustSelect)
            SelectResult = 0;
        else
            selectResult = -1;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SelectorTrigger(int _value)
    {
        if (buttonSound != "")
        {
            AudioManager.instance.PlayEffect(buttonSound);
        }
        SelectResult = _value;
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
