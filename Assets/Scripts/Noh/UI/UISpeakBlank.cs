using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpeakBlank : MonoBehaviour {
    private Text text;
    private float timer = 0.0f;
    void Movefunc()
    {
        Vector3 repPos = Player.instance.transform.position;
        Vector3 pos = Camera.main.WorldToScreenPoint(repPos);
        transform.position = pos;
    }
    // Use this for initialization
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }
    void Start () {
		
	}

    private void LateUpdate()
    {
        Movefunc();
    }

    // Update is called once per frame
    void Update () {
        if (!gameObject.activeInHierarchy)
            return;
        if (timer < Time.deltaTime)
        {
            gameObject.SetActive(false);
        }
        else
            timer -= Time.deltaTime;
	}
    public void PlayerSpeak(string _text,float _timer)
    {
        timer = _timer;
        text.text = _text;
        gameObject.SetActive(true);
    }
    public void PlayerSpeak(int _textEntry, float _timer)
    {
        timer = _timer;
        text.text = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == _textEntry)["Text"].ToString();
        gameObject.SetActive(true);
    }
}
