using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UINews : MonoBehaviour {
    private GameObject[] newsList = new GameObject[2];
    public GameObject newsDetailPrefab;
    public GameObject newsDetailMenu;
    public List<string> newsMess = new List<string>();
    private int newsCount = 0;
    public float nextTimer;
    private float mNextTimer;
    private float size;
    bool scrollUp = false;

    // Use this for initialization
    private void Awake()
    {
        newsList[0] = EffectManager.instance.MakeEffect("NewsPrefab");
        newsList[0].transform.SetParent(transform);
        newsList[0].transform.localPosition = Vector3.zero;
        newsList[1] = EffectManager.instance.MakeEffect("NewsPrefab");
        newsList[1].transform.SetParent(transform);
        Vector2 pos = newsList[0].transform.position;
        size = newsList[0].GetComponent<RectTransform>().rect.height;
        pos.y -= size;
        newsList[1].transform.position = pos;
        if (newsMess.Count > 0)
        {
            newsList[0].GetComponent<Text>().text = newsMess[(newsCount++ % newsMess.Count)];
            newsList[1].GetComponent<Text>().text = newsMess[(newsCount++ % newsMess.Count)];
        }

        for(int i =0;i<newsMess.Count;i++)
        {
            GameObject text = Instantiate(newsDetailPrefab);
            text.transform.SetParent(newsDetailMenu.transform);
            text.GetComponent<Text>().text = newsMess[i];
        }
    }
    public void NewsAdd(string _newsMess)
    {
        newsMess.Add(_newsMess);
        if (newsMess.Count < 3)
        {
            newsList[newsMess.Count - 1].GetComponent<Text>().text = _newsMess;
        }
        GameObject text = Instantiate(newsDetailPrefab);
        text.transform.SetParent(newsDetailMenu.transform);
        text.GetComponent<Text>().text = _newsMess;
    }
    public void NewsClaer()
    {
        newsList[0].GetComponent<Text>().text = null;
        newsList[1].GetComponent<Text>().text = null;
        int temp = newsDetailMenu.transform.childCount;
        for (int i = 0; i < temp; i++)
            Destroy(newsDetailMenu.transform.GetChild(i).gameObject);
        newsMess.Clear();
        newsCount = 0;
    }
	// Update is called once per frame
	void Update () {
        switch(scrollUp)
        {
            case true:
                for(int i = 0; i < 2; i++)
                {
                    newsList[i].transform.Translate(Vector2.up * Time.deltaTime * 30.0f);
                    if (newsList[i].transform.localPosition.y >= size)
                    {
                        Vector2 pos = newsList[(i + 1) % 2].transform.position;
                        pos.y -= size;
                        newsList[i].transform.position = pos;
                        if(newsMess.Count > 0)
                            newsList[i].GetComponent<Text>().text = newsMess[(newsCount++%newsMess.Count)];
                        scrollUp = false;
                    }
                }
                break;
            case false:
                if (mNextTimer < Time.deltaTime)
                {
                    scrollUp = true;
                    mNextTimer = nextTimer;
                }
                else
                    mNextTimer -= Time.deltaTime;
                break;
        }
        
	}
}
