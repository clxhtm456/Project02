using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : Singleton<CraftManager> {
    private int workStep;//작업단계
    private GameMoney workTotalCost = new GameMoney();
    public Weapon resultWeapon = new Weapon();
    

    public enum ROYALSTATE
    {
        DISACTIVE,//비활성
        ACTIVE,//활성
        USING,//공장돌리는중
        EXHAUSTED//공장다돌림(순전히 레시피)
    };
    public GameMoney WorkTotalCost
    {
        set { workTotalCost = value; }
        get { return workTotalCost; }
    }
    public void ResetManager()//한턴에 작업은 한번만 가능하게
    {
        resultWeapon = null;
        //for (int i = 0; i < toolList.Length; i++)
        //    toolList[i] = null;
        workTotalCost = new GameMoney();
        Gamemanager.instance.saveManaged.canWork = false;
        Player.instance.Working = false;
        UIManager.instance.progressBar.gameObject.SetActive(false);
        WorkStep = 0;//작업단계 1단계
        UIManager.instance.progressBar.StopRate = ConstManager.FIRSTWORKLIMIT;
        UIManager.instance.ShutAllDown();
    }
    public void WorkNextStep()
    {
        UIManager.instance.confirmPanel.CreateUIConfirm(null, WorkNextStepConfirm,StopWorkConfirm, "작업 계속하기", "작업을 속행하시겠습니까");
    }
    public void WorkNextStepConfirm()
    {
        switch (workStep)
        {
            case 0:
                {
                    UIManager.instance.metalChoicePanel.OpenUI();
                }
                break;
            case 1:
                {
                    UIManager.instance.subChoicePanel.OpenUI();
                }
                break;
            case 2:
                {
                    Gamemanager.instance.PlayerMoney -= workTotalCost;
                    Gamemanager.instance.saveManaged.ownWeapon.Add(resultWeapon);
                    UIManager.instance.gradingPanel.OpenUI();
                }
                break;
        }
        if (Gamemanager.instance.saveManaged.storyStep < 20)//듀토리얼 도중
            StoryManager.instance.StoryScript();
    }
    public void StopWorkConfirm()
    {
        ResetManager();
        Gamemanager.instance.saveManaged.canWork = true;
    }
    public void GotoWork()//작업구문
    {
        Player.instance.Working = true;
        UIManager.instance.progressBar.gameObject.SetActive(true);
        WorkStep = 0;//작업단계 1단계
        UIManager.instance.progressBar.StopRate = ConstManager.FIRSTWORKLIMIT;
    }
    public void GotoNext(float _limit)
    {
        workStep+=1;
        UIManager.instance.progressBar.StopRate = _limit;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int WorkStep
    {
        get { return workStep; }
        set
        {
            workStep = value;
        }
    }
    public void WeapontoInven()
    {
        if (resultWeapon == null)
            return;
        UIManager.instance.confirmPanel.CreateUIConfirm(null, WeapontoInvenConfirm, "무기 보관", resultWeapon.itemName + "을 보관후 인벤토리로 이동하시겠습니까?");
    }
    public void WeapontoInvenConfirm()
    {
        ResetManager();
        UIManager.instance.ShutAllDown();
        UIManager.instance.inventoryPanel.OpenUI();
        UIManager.instance.inventoryPanel.inventorySlot.SelectResult = Gamemanager.instance.saveManaged.ownWeapon.Count-1;
        if (Gamemanager.instance.saveManaged.storyStep < 20)
            StoryManager.instance.StoryScript();
    }
    public void SellWeapon()
    {
        if (resultWeapon == null)
            return;
        UIManager.instance.confirmPanel.CreateUIConfirm(UIManager.instance.TopUI, SellWeaponConfirm, "무기 판매", resultWeapon.ItemPrice + "\\에 판매하시겠습니까?");
    }
    public void SellWeaponConfirm()
    {
        resultWeapon.SellItem();
        ResetManager();
    }
    public void RegRoyalty()
    {
        if (resultWeapon == null)
            return;
        UIManager.instance.regRoyalPanel.OpenUI();
        UIManager.instance.regRoyalPanel.RegWeapon(resultWeapon);
    }
    //public void RegRoyaltyConfirm()
    //{
    //    Royalty royalty = new Royalty();
    //    royalty.weaponData = resultWeapon;
    //    royalty.royalState = (int)ROYALSTATE.DISACTIVE;
    //    Gamemanager.instance.saveManaged.ownRoyalty.Add(Gamemanager.instance.saveManaged.ownRoyalty.Count, royalty);
    //    ResetManager();
    //    UIManager.instance.ShutAllDown();
    //}
}
