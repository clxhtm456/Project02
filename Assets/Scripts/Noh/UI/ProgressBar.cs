using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public float maxValue;
    public float recentValue;
    private float workSpeed;
    public bool stop;
    public Image progress;
    public Text progressRate;
    private Button button;
    private float stopRate;//멈춰야하는 값
    public float StopRate
    {
        set { stopRate = value;
            stop = false;
            button.interactable = false;
            if(GetComponent<UIactiveable>())
                GetComponent<UIactiveable>().Active = false;
        }
    }
    public bool Stop
    {
        get { return stop; }
        set
        {
            if (value != stop)
            {
                stop = value;
                if (stop == true)
                {
                    AudioManager.instance.PlayEffect("percentSound");
                    if (Gamemanager.instance.saveManaged.storyStep < 10)//듀토리얼 도중
                        StoryManager.instance.StoryScript();
                }
            }
        }
    }
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    // Use this for initialization
    void Start () {
        workSpeed = ConstManager.WORKSPEED;
    }
    private void OnDisable()
    {
        recentValue = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        
        if (recentValue >= maxValue)
            return;
        if (!stop)
            recentValue += workSpeed * Time.deltaTime;
        float rate = (recentValue / maxValue);
        Vector3 val = progress.transform.localScale;
        val.x = 1-rate;
        progress.transform.localScale = val;
        if(progressRate != null)
            progressRate.text = ((int)(rate*100.0f)).ToString()+"%";
        if (((int)(rate * 100.0f)) >= stopRate && button)
        {
            Stop = true;
            
            button.interactable = true;
            GetComponent<UIactiveable>().Active = true;
        }
	}
}
