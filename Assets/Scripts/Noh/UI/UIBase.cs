using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class UIBase : MonoBehaviour {
    protected RectTransform recttransform;
    protected UIBase rootUI = null;
    // Use this for initialization
    private void Awake()
    {
        recttransform = GetComponent<RectTransform>();
    }
    void Start () {
	}
    virtual public void ResetPanel()
    {

    }
   
    protected void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update () {
		
	}
    virtual public void OpenUI()//UI를 아래로 정렬후 활성화
    {
        gameObject.SetActive(true);
        recttransform.SetAsLastSibling();
        rootUI = UIManager.instance.TopUI;
        UIManager.instance.TopUI = this;
        
        ResetPanel();
    }
    virtual public void CloseUI()
    {
        if (gameObject.activeInHierarchy == true)
            gameObject.SetActive(false);
        UIManager.instance.TopUI = rootUI;

        if (rootUI)
        {
            rootUI.ResetPanel();
        }
    }
    virtual public void CreateConfirm()
    {
        UIManager.instance.confirmPanel.CreateUIConfirm(this, null);
    }
}
