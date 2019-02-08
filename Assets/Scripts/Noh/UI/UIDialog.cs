using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIDialog : MonoBehaviour {
    public Image[] dialogSprite;
    public Text dialogText;
    private string tempText;
    private float textCount;
    public float textSpeed;
    Queue<TextDialog> saveDialog = new Queue<TextDialog>();
    struct TextDialog
    {
        public Sprite unitSprite;
        public string dialogText;
        public UnityAction dialogEvent;
        public TextDialog(string m_dialogText, Sprite m_unitSprite, UnityAction endEvent)
        {
            unitSprite = m_unitSprite;
            dialogText = m_dialogText;
            dialogEvent = endEvent;
        }
    }

    public void OpenDialog(string dialogText,Sprite unitSprite,UnityAction endEvent = null)
    {
        TextDialog temp = new TextDialog(dialogText, unitSprite, endEvent);
        if (gameObject.activeInHierarchy == true)//만약 다이어로그 실행중이라면 예약후 리턴
        {
            saveDialog.Enqueue(temp);
            return;
        }
        gameObject.SetActive(true);
        OpenDialog(temp);
    }
    void OpenDialog(TextDialog textDialog)
    {
        textCount = 0.0f;
        if (textDialog.unitSprite != null)
        {
            if (dialogSprite[0].gameObject.activeInHierarchy == false)
            {
                if (dialogSprite[1].sprite != textDialog.unitSprite)
                {
                    dialogSprite[1].color = ConstManager.hexToColor("8C8C8C");
                    dialogSprite[0].gameObject.SetActive(true);
                    dialogSprite[0].sprite = textDialog.unitSprite;
                    dialogSprite[0].color = ConstManager.hexToColor("FFFFFF");
                }
            }
            else if (dialogSprite[1].gameObject.activeInHierarchy == false)
            {
                if (dialogSprite[0].sprite != textDialog.unitSprite)
                {
                    dialogSprite[0].color = ConstManager.hexToColor("8C8C8C");
                    dialogSprite[1].gameObject.SetActive(true);
                    dialogSprite[1].sprite = textDialog.unitSprite;
                    dialogSprite[1].color = ConstManager.hexToColor("FFFFFF");
                }
            }
            else//양쪽스프라이트 사용중에
            {
                if (dialogSprite[1].color == ConstManager.hexToColor("FFFFFF"))//오른쪽이 말하고있던중
                {
                    if (dialogSprite[1].sprite != textDialog.unitSprite)//새로운인물이 오른쪽에 있던 사람이 아닐경우
                    {
                        dialogSprite[1].color = ConstManager.hexToColor("8C8C8C");//새로운 인물은 왼쪽에 배치
                        dialogSprite[0].gameObject.SetActive(true);
                        dialogSprite[0].sprite = textDialog.unitSprite;
                        dialogSprite[0].color = ConstManager.hexToColor("FFFFFF");
                    }
                }
                else
                {
                    if (dialogSprite[0].sprite != textDialog.unitSprite)//새로운인물이 오른쪽에 있던 사람이 아닐경우
                    {
                        dialogSprite[0].color = ConstManager.hexToColor("8C8C8C");
                        dialogSprite[1].gameObject.SetActive(true);
                        dialogSprite[1].sprite = textDialog.unitSprite;
                        dialogSprite[1].color = ConstManager.hexToColor("FFFFFF");
                    }
                }

            }
        }
        if (textDialog.dialogText != null)
        {
            if (textDialog.dialogEvent != null)
                textDialog.dialogEvent();
            tempText = textDialog.dialogText;
        }
        else
        {
            if (textDialog.dialogEvent != null)
                textDialog.dialogEvent();
            NextDialog();
        }
        
    }
    void NextDialog()
    {
        textCount = 0.0f;
        if (saveDialog.Count > 0)
            OpenDialog(saveDialog.Dequeue());
        else
            EndDialog();
    }
    void EndDialog()
    {
        dialogSprite[0].gameObject.SetActive(false);
        dialogSprite[1].gameObject.SetActive(false);
        dialogSprite[0].sprite = null;
        dialogSprite[1].sprite = null;
        gameObject.SetActive(false);
        saveDialog.Clear();
    }
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeInHierarchy == false)
            return;
        if(Input.GetMouseButtonDown(0))
        {
            if (textCount < tempText.Length)
            {
                textCount = tempText.Length;
            }
            else
                NextDialog();
        }
             
        if (textCount < tempText.Length)
            textCount += Time.deltaTime* textSpeed;
        dialogText.text = tempText.Substring(0, (int)textCount);
	}
}
