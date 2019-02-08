using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpObject : MonoBehaviour {
    public int level;
    public GameObject[] levelingObject;
	// Use this for initialization
    public int Level
    {
        set
        {
            level = value;
            SettingLevel();
        }
    }
	void Start () {
        SettingLevel();
    }

    void SettingLevel()
    {
        for (int i = 0; i < levelingObject.Length; i++)
            if (i+1 == level)
                levelingObject[i].SetActive(true);
            else
                levelingObject[i].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
