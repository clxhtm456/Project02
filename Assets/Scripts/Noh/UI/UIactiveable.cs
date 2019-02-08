using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIactiveable : MonoBehaviour
{
    public Sprite activeImage;
    public Sprite disactiveImage;
    public string activeColor;
    public string disactiveColor;
    public int activeSize;
    public int disactiveSize;
    private Text _text;
    private Image image;
    private bool active;
    public bool Active
    {
        set
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }

            active = value;

            if (image && activeImage == null||disactiveImage==null)
            {
                image.enabled = false;
            }

            if (active)
            {
                if (disactiveImage==null)
                {
                    image.enabled = true;
                }
                if(image)
                    image.sprite = activeImage;
                if (_text != null)
                {
                    if (activeColor != "")
                        _text.color = ConstManager.hexToColor(activeColor);
                    if (activeSize != 0)
                        _text.fontSize = activeSize;
                }
            }
            else
            {
                if (activeImage == null)
                { 
                image.enabled = true;
                }
                image.sprite = disactiveImage;
                if (_text != null)
                {
                    if (activeColor != "")
                        _text.color = ConstManager.hexToColor(disactiveColor);
                    if (disactiveSize != 0)
                        _text.fontSize = disactiveSize;
                }
            }
        }
        get { return active; }
    }

    void Start()
    {
        image = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
    }

}
