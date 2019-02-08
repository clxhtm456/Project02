using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProfile : UIBase {
    public Text playerContext;
    public Text levelupButtonText;
    public GameObject stateButtonList;
    public GameObject[] stateDownButton;
    private int[] preStatus = new int[5];
    public override void CloseUI()
    {
        for (int i = 0; i < preStatus.Length; i++)
        {
            Gamemanager.instance.saveManaged.statePoint += preStatus[i];
            preStatus[i] = 0;
            stateDownButton[i].SetActive(false);
        }
        base.CloseUI();
    }
    public override void ResetPanel()
    {
        levelupButtonText.text = "레벨업(" + Gamemanager.instance.saveManaged.playerLevel * 10000 + ")";
        if (Gamemanager.instance.saveManaged.statePoint > 0)
            stateButtonList.SetActive(true);
        else
            stateButtonList.SetActive(false);
        playerContext.text=
            (": 플레이어 이름"+"\n"+
            ": "+Gamemanager.instance.saveManaged.playerLevel+"/100" + "\n\n" +
            ": " + Player.instance.playerState[0].ToString("000") + "/"+ preStatus[0].ToString("000") + "\n" +
            ": " + Player.instance.playerState[1].ToString("000") + "/" + preStatus[1].ToString("000") + "\n" +
            ": " + Player.instance.playerState[2].ToString("000") + "/" + preStatus[2].ToString("000") + "\n" +
            ": " + Player.instance.playerState[3].ToString("000") + "/" + preStatus[3].ToString("000") + "\n" +
            ": " + Player.instance.playerState[4].ToString("000") + "/" + preStatus[4].ToString("000") + "\n\n" +
            ": " + Gamemanager.instance.saveManaged.statePoint.ToString("00")
        );
        base.ResetPanel();
    }
    public void IncreaseState(int _temp)
    {
        if (Gamemanager.instance.saveManaged.statePoint <= 0)
            return;
        Gamemanager.instance.saveManaged.statePoint -= 1;
        preStatus[_temp] += 1;
        AudioManager.instance.PlayEffect("StatsArrow");
        stateDownButton[_temp].SetActive(true);
        ResetPanel();
    }
    public void DecreaseState(int _temp)
    {
        if (preStatus[_temp] <= 0)
            return;
        preStatus[_temp] -= 1;
        Gamemanager.instance.saveManaged.statePoint += 1;
        AudioManager.instance.PlayEffect("StatsArrow");
        if (preStatus[_temp] <= 0)
            stateDownButton[_temp].SetActive(false);
        ResetPanel();
    }
    public void ConfirmStatus()
    {
        for(int i = 0; i < preStatus.Length;i++)
        {
            Player.instance.playerState[i] += preStatus[i];
            preStatus[i] = 0;
            stateDownButton[i].SetActive(false);
        }
        AudioManager.instance.PlayEffect("Stats");
        ResetPanel();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LevelUpButton()
    {
        int levelupCost = Gamemanager.instance.saveManaged.playerLevel * 10000;
        if (Gamemanager.instance.PlayerMoney < levelupCost)
            return;

        Gamemanager.instance.PlayerMoney -= levelupCost;
        Gamemanager.instance.saveManaged.playerLevel += 1;
        Gamemanager.instance.saveManaged.statePoint += 2;

        ResetPanel();
    }
}
