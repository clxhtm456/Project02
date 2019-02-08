using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwinkleObject : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Color color;
    private bool switchB = true;
    // Use this for initialization
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        color = spriteRenderer.color;
        if(switchB)
        {
            color.a += Time.deltaTime;
            if (color.a >= 1.0f)
                switchB = false;
        }else
        {
            color.a -= Time.deltaTime;
            if (color.a <= 0.2f)
                switchB = true;
        }
        spriteRenderer.color = color;

    }
}
