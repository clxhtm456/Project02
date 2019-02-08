using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIChoiceTool : UIConfirm {
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
    override public void CreateUIConfirm(UIBase _root, UnityAction test, int _datatitle = -1,int _datacontext = -1)
    {
        if(_datatitle != -1)
            title.GetComponent<UITextInput>().TableKey = _datatitle;
        if (_datacontext != -1)
            context.GetComponent<UITextInput>().TableKey = _datacontext;
        yesButton.onClick.RemoveAllListeners();
        //초기화
        
        OpenUI();
        yesButton.onClick.AddListener(CloseUI);
        yesButton.onClick.AddListener(()=> { AudioManager.instance.PlayEffect("OK"); });
        if (test != null)
            yesButton.onClick.AddListener(() => { test(); });

        if (_root)
        {
            rootUI = _root;
            yesButton.onClick.AddListener(_root.CloseUI);
        }
        
    }

    override public void CreateUIConfirm(UIBase _root, UnityAction test, string _title, string _context)
    {
        title.text = _title;
        context.text = _context;
        yesButton.onClick.RemoveAllListeners();
        //초기화

        OpenUI();
        yesButton.onClick.AddListener(CloseUI);
        yesButton.onClick.AddListener(() => { AudioManager.instance.PlayEffect("OK"); });
        if (test != null)
            yesButton.onClick.AddListener(() => { test(); });
        if (_root)
        {
            rootUI = _root;
            yesButton.onClick.AddListener(_root.CloseUI);
        }

    }
    
}
