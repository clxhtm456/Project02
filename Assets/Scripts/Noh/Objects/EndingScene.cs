using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour {
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Color color = image.color;
        color.a += Time.deltaTime*0.5f;
        image.color = color;

    }
}
