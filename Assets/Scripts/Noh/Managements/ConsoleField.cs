using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleField : MonoBehaviour {
    InputField inputField;
    // Use this for initialization
    private void Awake()
    {
        inputField = GetComponent<InputField>();
    }
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SendBroadMessage()
    {
        try
        {
            GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in gos)
            {
                if (go && go.transform.parent == null)
                {
                    go.gameObject.BroadcastMessage(inputField.text, null,SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
            inputField.text = null;
            gameObject.SetActive(false);
        }
        inputField.text = null;
        gameObject.SetActive(false);
    }
}
