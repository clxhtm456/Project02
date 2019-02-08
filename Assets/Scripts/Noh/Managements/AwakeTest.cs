using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (ConstManager.RandomEvent(20))
            Debug.Log("20퍼센트");
        if (ConstManager.RandomEvent(50))
            Debug.Log("50퍼센트");
        if (ConstManager.RandomEvent(100))
            Debug.Log("100퍼센트");
        if (ConstManager.RandomEvent(1))
            Debug.Log("1퍼센트");


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void Awake()
    {
        Debug.Log(gameObject.name + "Awake");
    }
}
