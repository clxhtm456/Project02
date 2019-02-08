using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gamemanager : Singleton<Gamemanager>
{
    public EasyTween fadeout;
    public bool gameOver = false;
    // System.DateTime ex = new System.DateTime(2018, 05, 1);
    public SaveData saveManaged = new SaveData();
    public int playerStartMoney;
    public int turnPerMoney;
    private int popElement;
    private int popType;
    public int PopElement
    {
        set { popElement = value % 5; }//속성값은 5가지만을 가짐
        get { return popElement; }
    }
    public int PopType
    {
        set { popType = value % 5; }//타입값은 5가지만을 가짐
        get { return popType; }
    }

    //private GameObject activeButton;
    //public GameObject ActiveButton
    //{
    //    get { return activeButton; }
    //    set { activeButton = value; }
    //}
    //저장해야하는 변수목록
    //주식항목
    void SaveDataConnect()
    {
        if (SaveListManager.instance)
        {
            saveManaged = SaveListManager.instance.playerdata;
            if (saveManaged.newGame == true)
            {
                saveManaged.newGame = false;
                PlayNewGame();
            }
        }
        if(saveManaged.storyStep >= 20)
        {
            UIManager.instance.turnOption.SetActive(true);
            UIManager.instance.safeRate.SetActive(true);
            UIManager.instance.profile.SetActive(true);
            UIManager.instance.alarm.SetActive(true);
            UIManager.instance.news.SetActive(true);
            StoryManager.instance.EventGenerator();//새이벤트 시작
        }
        StoryManager.instance.safeBar.recentValue = StoryManager.instance.HazardRate();//안정도갱신
        StoryManager.instance.TextReset();
        UIManager.instance.CheckMoney();
        UIManager.instance.turnPanel.text = GameTurn.ToString("00");
    }
    void PlayNewGame()
    {
        Debug.Log("새게임 시작");
        StockManager.instance.NewStockEvent();
        saveManaged.canWork = true;
        StoryManager.instance.StoryScript();
    }
    public void Start()
    {
        //print(ex.AddDays(-4).Day);
        Screen.SetResolution(1920, 1080, true);
        Application.runInBackground = true;
        SaveDataConnect();
        ResetTurnPerMoney();
        //if (SaveListManager.instance.recentSave == (int)SaveListManager.SaveState.NEWPLAY)
        //{
        //    PlayNewGame();
        //}
        //PlayNewGame();
        
        fadeout.OpenCloseObjectAnimation();

    }
    public GameMoney PlayerMoney
    {
        get
        {
            return saveManaged.playerMoney;
        }
        set
        {
            if (value > saveManaged.playerMoney)
                AudioManager.instance.PlayEffect("ItemSell");
            saveManaged.playerMoney = value;
            //saveManaged.playerMoney.ShowMoney();
            UIManager.instance.CheckMoney();
        }
    }
    public Dictionary<int, int> PlayerStock
    {
        get
        {
            return saveManaged.playerStock;
        }
        set
        {
            saveManaged.playerStock = value;
        }
    }
    public Dictionary<int, int> StockAvg
    {
        get
        {
            return saveManaged.stockAvg;
        }
        set
        {
            saveManaged.stockAvg = value;
        }
    }
    public Dictionary<int, float> ShareRatio
    {
        get
        {
            return saveManaged.shareRatio;
        }
        set
        {
            saveManaged.shareRatio = value;
        }
    }
    public Dictionary<int, int> ShareSum
    {
        get
        {
            return saveManaged.shareSum;
        }
        set
        {
            saveManaged.shareSum = value;
        }
    }
    public List<int> OwnTool
    {
        get
        {
            return saveManaged.ownTool;
        }
        set
        {
            saveManaged.ownTool = value;
        }
    }
    public List<int> OwnSubr
    {
        get
        {
            return saveManaged.ownSubr;
        }
        set
        {
            saveManaged.ownSubr = value;
        }
    }
    public List<Weapon> ownWeapon
    {
        get
        {
            return saveManaged.ownWeapon;
        }
        set
        {
            saveManaged.ownWeapon = value;
        }
    }

    public int GameTurn
    {
        get
        {
            return (saveManaged.gameTurn %30)+1;
        }
        set
        {
            saveManaged.gameTurn = value;
        }
    }
    public void NextTurn()//턴종료
    {
        if (Player.instance.Working)//작업중에는 턴종료불가
        {
            return;
        }
        fadeout.OpenCloseObjectAnimation();
        Player.instance.AnimationState = 4;//애니메이션
        StockManager.instance.CalcStockPrice();//주식가격변동
        //플레이어 행동 해제
        Player.instance.ActionObject = null;
        StoryManager.instance.speakBlank.PlayerSpeak("ZZZ", 1.8f);


        saveManaged.canWork = true;
        //턴증가
        saveManaged.gameTurn++;
        UIManager.instance.turnPanel.text = GameTurn.ToString("00");
        TurnPerMoneyCheck();
        //안정도계산
        StoryManager.instance.safeBar.recentValue = StoryManager.instance.HazardRate();
        StoryManager.instance.TextReset();
        //Player.instance.SpeakBlank.PlayerSpeak("턴종료", 3);
        StoryManager.instance.EventGenerator();//새이벤트 시작
        Debug.Log("인류 파워 : "+saveManaged.humanPower.totalPower(saveManaged.monsterPower));
        Debug.Log("몬스터 파워 : " + saveManaged.monsterPower.totalPower(saveManaged.humanPower));
        AutoSaveManager();//자동저장

    }
    public void ResetTurnPerMoney()//턴당골드 갱신
    {
        turnPerMoney = 0;
        int temp = saveManaged.ownRoyalty.Count;
        for (int i = 0; i < temp; i++)
        {
            if (saveManaged.ownRoyalty[i].royalState == (int)(CraftManager.ROYALSTATE.ACTIVE))
            {
                turnPerMoney += saveManaged.ownRoyalty[i].RoyaltyPrice();//무기의 4퍼센트가격
            }
        }
        UIManager.instance.CheckTurnMoney();
    }
    void TurnPerMoneyCheck()//턴당골드 추가
    {
        ResetTurnPerMoney();
        PlayerMoney += turnPerMoney;
    }


    // Use this for initialization
    //void CalcLocalMouse()
    //   {
    //       uiMousePos = Input.mousePosition;
    //       uiMousePos.x -= Screen.width * 0.5f;
    //       uiMousePos.x *= (uiCanvas.referenceResolution.x / Screen.width);
    //       uiMousePos.y -= Screen.height * 0.5f;
    //       uiMousePos.y *= (uiCanvas.referenceResolution.y / Screen.height);
    //   }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.instance.TopUI != null)
                UIManager.instance.TopUI.CloseUI();
            else
                UIManager.instance.confirmPanel.CreateUIConfirm(null, ApplicationExitConfirm, "게임 종료", "게임을 종료하시겠습니까?");
        }
        if (gameOver)
            return;
        UIButtonCheck();
        //PlayerMoneyCheck();
    }
    void ApplicationExitConfirm()
    {
        Application.Quit();
    }
    void UIButtonCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D[] rayHit;
            rayHit = Physics2D.RaycastAll(ray.origin, ray.direction);
            if (rayHit.Length > 0 && rayHit[0] && StoryManager.instance.uiDialog.gameObject.activeInHierarchy == false && UIManager.instance.TopUI == null && (rayHit[0].transform.tag == "Button" || rayHit[0].transform.tag == "WorkButton"))
            {
                Player.instance.ActionObject = rayHit[0].transform.gameObject;
            }
        }
    }
    public void GameSaveManager()
    {
        if (saveManaged.storyStep < 20)
        {
            UIManager.instance.confirmPanel.CreateUIConfirm(null, null, "저장실패", "듀토리얼이 끝나기전까지는 저장할수없습니다.");
            return;
        }
        try
        {
            int temp2 = SaveListManager.instance.playerdata.index;
            SaveListManager.instance.GameSave(temp2);
            UIManager.instance.confirmPanel.CreateUIConfirm(null, null, "게임저장", "게임이 저장되었습니다.");
        }
        catch
        {
            Debug.Log("세이브실패");//ui필요
        }
    }
    public void GameSaveManager(int _val)
    {
        SaveListManager.instance.GameSave(_val);
        SaveListManager.instance.StateSave();
    }
    public void GameLoadManager(int _val)
    {
        SaveListManager.instance.StartGame(_val);
        Destroy(gameObject);
    }
    public void AutoSaveManager()
    {
        SaveListManager.instance.AutoGameSave();
    }
}
