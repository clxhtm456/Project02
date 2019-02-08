using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlarm : Singleton<UIAlarm> {
    public Text prefabs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TextAlarm(string _text)
    {
        Text temp = Instantiate(prefabs);
        temp.transform.SetParent(transform);
        temp.text = "-"+_text;
        Text[] list = gameObject.GetComponentsInChildren<Text>();
        if (list.Length >= 9)
            Destroy(list[0].gameObject);
    }
}
