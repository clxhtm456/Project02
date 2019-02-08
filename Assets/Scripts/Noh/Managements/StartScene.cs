using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    public EasyTween titleUI; 
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (titleUI.GetComponent<VideoPlayer>().time != titleUI.GetComponent<VideoPlayer>().clip.length)
        {
            if(Input.GetMouseButtonDown(0))
                titleUI.GetComponent<VideoPlayer>().time = titleUI.GetComponent<VideoPlayer>().clip.length;
        }else if(titleUI.IsObjectOpened() == false)
        {
            titleUI.OpenCloseObjectAnimation();
        }
	}
    public void StartGame(GameObject uiNewgame)
    {
        int temp = SaveListManager.instance.LoadingSaveList();
        Debug.Log(temp);
        if(temp >= 2)
        {
            uiNewgame.SetActive(true);
        }else
        {
            SaveListManager.instance.NewGame(temp);
        }
    }
}
