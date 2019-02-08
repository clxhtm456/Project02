using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorporateName : MonoBehaviour {

    Text corpoateName;

    private void Awake()
    {
        corpoateName = GetComponent<Text>();

    }
    private void OnDisable()
    {
        corpoateName.text = "";
    }


    public void SetCorporateName(Text name) //이건 납두고 나중에 데이터베이스사용하면 다시설정

    {
        corpoateName.text = name.text;

    }

}
