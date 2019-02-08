using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIConfirm : UIBase {
    public Button yesButton;
    public Button noButton;
    public Image icon;
    public Text title;
    public Text context;
    public override void OpenUI()
    {
        if (gameObject.activeInHierarchy == false)
            gameObject.active = true;
        recttransform.SetAsLastSibling();
        rootUI = UIManager.instance.TopUI;
        UIManager.instance.TopUI = this;
        ResetPanel();
    }
    public override void CloseUI()
    {
        if (gameObject.activeInHierarchy == true)
            gameObject.SetActive(false);
        UIManager.instance.TopUI = rootUI;
        if (rootUI)
        {
            rootUI.ResetPanel();
        }
    }
    virtual public void CreateUIConfirm(UIBase _root, UnityAction test, int _datatitle = -1,int _datacontext = -1)
    {
        if(_datatitle != -1)
            title.GetComponent<UITextInput>().TableKey = _datatitle;
        if (_datacontext != -1)
            context.GetComponent<UITextInput>().TableKey = _datacontext;
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        //초기화
        
        OpenUI();
        yesButton.onClick.AddListener(CloseUI);
        yesButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("OK"); });
        if (test != null)
            yesButton.onClick.AddListener(() => { test(); });
        
        noButton.onClick.AddListener(CloseUI);
        noButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("Cancel"); });
        if (_root)
        {
            rootUI = _root;
            yesButton.onClick.AddListener(_root.CloseUI);
        }
        
    }

    virtual public void CreateUIConfirm(UIBase _root, UnityAction test, string _title, string _context)
    {
        title.text = _title;
        context.text = _context;
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        //초기화

        OpenUI();
        yesButton.onClick.AddListener(CloseUI);
        yesButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("OK"); });
        if (test != null)
            yesButton.onClick.AddListener(() => { test(); });
        
        noButton.onClick.AddListener(CloseUI);
        noButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("Cancel"); });
        if (_root)
        {
            rootUI = _root;
            yesButton.onClick.AddListener(_root.CloseUI);
        }

    }

    virtual public void CreateUIConfirm(UIBase _root, UnityAction test1, UnityAction test2, string _title, string _context)//취소 성공 모두 이벤트를갖는경우
    {
        title.text = _title;
        context.text = _context;
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        //초기화

        OpenUI();
        yesButton.onClick.AddListener(CloseUI);
        yesButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("OK"); });
        if (test1 != null)
            yesButton.onClick.AddListener(() => { test1(); });
        if (test2 != null)
            noButton.onClick.AddListener(() => { test2(); });
        
        noButton.onClick.AddListener(CloseUI);
        noButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("Cancel"); });

    }
    public void CancelByMoney()
    {
        title.text = "구매불가";
        context.text = "돈이 부족합니다";
        AudioManager.instance.PlayEffect("NoMoney");
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        //초기화

        OpenUI();
        yesButton.onClick.AddListener(CloseUI);
        yesButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("OK"); });
        noButton.onClick.AddListener(CloseUI);
        noButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("Cancel"); });

    }

}
