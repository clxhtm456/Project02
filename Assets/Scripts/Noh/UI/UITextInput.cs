using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextInput : MonoBehaviour {
    Text mtext;
    public int tableKey;
    public string tableKey2;
    public string additionalText;
    public int TableKey
    {
        set { tableKey = value;
            SetTableKey();
        }
    }
    public string TableKey2
    {
        set
        {
            tableKey2 = value;
            SetTableKey();
        }
    }
    public string MText
    {
        set { mtext.text = value; }
    }
	// Use this for initialization
	void Awake () {
        mtext = GetComponent<Text>();
        SetTableKey();
    }
    void SetTableKey()
    {
        try
        {

            object result;
            object result2;
            result = DataManager.instance.textTable.Find(item => int.Parse(item["Entry"].ToString()) == tableKey)["Text"];

            if (tableKey2 != "")
            {
                result2 = DataManager.instance.textTable.Find(item => (item["Entry"].ToString()) == tableKey2)["Text"];
            }
            else
                result2 = "";

            mtext.text = (result.ToString() + (additionalText)+result2.ToString());
        }
        catch(System.Exception e)
        {
            Debug.Log("찾지못함");
        }
    }
 
}
