using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTurnEnd : MonoBehaviour
{
    public GameObject outLine;
    private bool buttonActive = true;
    private bool action;
    // Use this for initialization
    void Start()
    {

    }
    public bool ButtonActive
    {
        get { return buttonActive; }
        set
        {
            outLine.SetActive(value);
            buttonActive = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ButtonAction()
    {
        if (UIManager.instance.TopUI != null || !buttonActive)
            return;
        Gamemanager.instance.NextTurn();
        action = true;
    }
}
