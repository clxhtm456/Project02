using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cgraph : MonoBehaviour {
    private Image image;
    public Image second;
    // Use this for initialization
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion qua = new Quaternion();
        qua.eulerAngles = new Vector3(0,0, -360.0f * image.fillAmount);

        second.rectTransform.rotation = qua;
    }
}
