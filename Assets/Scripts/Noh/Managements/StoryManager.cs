using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StoryManager : Singleton<StoryManager>
{
    public UISpeakBlank speakBlank;
    public ProgressBar safeBar;
    public Text safeBarText;
    public UIDialog uiDialog;
    public GameObject deactiveBG;
    public GameObject fingerCursor;
    public GameObject[] tutorialButton;
    public List<GameObject> tempObject = new List<GameObject>();

    public GameObject badEnding;
    public GameObject happyEnding;

    public UINews news;
    private int popElementnTurn;//유행턴
    private int stockEventCount;

    private int angerGauge = 0;
    public void TurnCountEvent()
    {
        switch(Gamemanager.instance.saveManaged.gameTurn)
        {
            case 30:
                Gamemanager.instance.saveManaged.monsterPower.peopleNumber += 4000;
                NewsEvent("새로운 운석이 떨어졌습니다 새로운 몬스터들이 지구에 나타났습니다.");
                break;
            case 60:
                Gamemanager.instance.saveManaged.monsterPower.peopleNumber += 3000;
                Gamemanager.instance.saveManaged.monsterPower.heroNumber += 2;
                NewsEvent("새로운 몬스터들이 나타났습니다, 그중엔 돌연변이도 발견된 모양입니다.");
                break;
            case 90:
                Gamemanager.instance.saveManaged.monsterPower.peopleNumber += 4000;
                Gamemanager.instance.saveManaged.monsterPower.heroNumber += 4;
                NewsEvent("마지막 운석이 떨어졌습니다 새로운 몬스터들이 지구에 나타났습니다.");
                break;
        }
        if (Gamemanager.instance.saveManaged.monsterPower.peopleNumber <= 0)
        {
            happyEnding.SetActive(true);
            happyEnding.transform.SetAsLastSibling();
            uiDialog.OpenDialog("몬스터는 전멸했다.", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog("헌터들은 마지막까지 몬스터와 맞서 싸웠고 결국 마지막 몬스터 도 헌터의 손에 퇴치되었다.", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog("정체불명의 운석은 더이상 보이지않았고 인류에겐 평화가 찾아왔다..", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog("또한 헌터를 위한 무기를 제작하던 나 또한 세간의 관심을 받게되었고 몇번의 인터뷰와 함께 대스타가 되었다.", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog(null, null, () => { UIManager.instance.confirmPanel.CreateUIConfirm(null, () => {
                LoadingScene.LoadScene("StartScene");
                Destroy(gameObject);
                Destroy(SaveListManager.instance.gameObject);
                 }, "게임클리어", "타이틀로 돌아가시겠습니까?"); });
            
            //게임 승리
        }
        if (Gamemanager.instance.saveManaged.humanPower.peopleNumber <= 0)
        {
            badEnding.SetActive(true);
            badEnding.transform.SetAsLastSibling();
            uiDialog.OpenDialog("인류는 결국 전멸했다.", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog("헌터들은 마지막까지 싸웠으나 몰려드는 몬스터들의 군세를 막기는 역부족이었다.", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog("지구는 황폐화되었고 그 영향은 내 공방이라고 피해가지는 않았다.", Resources.Load<Sprite>("Portrait\\playerSprite"));
            uiDialog.OpenDialog(null, null,()=> { UIManager.instance.confirmPanel.CreateUIConfirm(null, ()=> {
                LoadingScene.LoadScene("StartScene");
                Destroy(gameObject);
                Destroy(SaveListManager.instance.gameObject);
            }, "게임오버", "타이틀로 돌아가시겠습니까?"); });
            //게임 패배
        }
    }
    public void HumanArmyEvent()
    {
        if(Gamemanager.instance.saveManaged.gameTurn%10 == 0)//10턴마다 발동
        {
            if(Gamemanager.instance.saveManaged.hazardRate > 500)
            {
                Gamemanager.instance.saveManaged.humanPower.heroNumber += 1;
                Gamemanager.instance.saveManaged.hazardRate = 0;
                NewsEvent("새로운 헌터가 나타났습니다.");
                return;
            }
            Gamemanager.instance.saveManaged.humanPower.peopleNumber += Gamemanager.instance.saveManaged.hazardRate;
            if (Gamemanager.instance.saveManaged.hazardRate > 0)
            {
                NewsEvent("신병 <color=#B9FF2B>" + Gamemanager.instance.saveManaged.hazardRate+"</color>명이 소집되었습니다.");
            }else
            {
                NewsEvent("병사들이 <color=#ff0000>" + Gamemanager.instance.saveManaged.hazardRate*-1.0f + "</color>명 탈영했다는 소식입니다.");
            }
        }
    }
    public void WarEncounter()
    {
        Gamemanager.instance.saveManaged.warCount--;
        if (Gamemanager.instance.saveManaged.warCount <= 0)
            isWar();
        else if(Gamemanager.instance.saveManaged.warCount < 5)
        {
            NewsEvent("전쟁이 가까워 지고 있습니다."+ "<color=#ff0000>" + Gamemanager.instance.saveManaged.warCount + "일"+"</color>" +"남았습니다");
            if (Gamemanager.instance.saveManaged.storyStep < 40)
                StoryScript();
        }
    }
    public void StockEvent()
    {
        if (stockEventCount <= 0)
        {
            int randStock = Random.Range(0, Gamemanager.instance.saveManaged.stockState.Count);
            if(Random.Range(0, 2)==0)
            {
                Gamemanager.instance.saveManaged.stockState[randStock].PriceEvent(Random.Range(1.2f, 2.5f), Random.Range(3, 5));
                NewsEvent(Gamemanager.instance.saveManaged.stockState[randStock].stockStringName+"기업의 주가가 오르고 있습니다.");
            }
            else
            {
                Gamemanager.instance.saveManaged.stockState[randStock].PriceEvent(Random.Range(0.5f, 0.7f), Random.Range(3, 5));
                NewsEvent(Gamemanager.instance.saveManaged.stockState[randStock].stockStringName + "기업의 주가가 떨어지고 있습니다.");
            }
            stockEventCount = Random.Range(2, 7);
        }
        else
            stockEventCount--;
    }
    public void isWar()
    {
        PowerLevel human = Gamemanager.instance.saveManaged.humanPower;
        PowerLevel monster = Gamemanager.instance.saveManaged.monsterPower;
        float humanDamage =  (human.totalPower(monster) / 100)- monster.heroNumber * 50;
        if (humanDamage < 0)
            humanDamage = 10;
        float monsterDamage = (monster.totalPower(human) / 100)- human.heroNumber * 50;
        if (monsterDamage < 0)
            monsterDamage = 10;
        Gamemanager.instance.saveManaged.monsterPower.peopleNumber -= (int)humanDamage;
        Gamemanager.instance.saveManaged.humanPower.peopleNumber -= (int)monsterDamage;
        if(humanDamage >= monsterDamage)
        {
            NewsEvent("인류가 전쟁에서 승리했습니다.");
            Gamemanager.instance.saveManaged.hazardRate += (int)humanDamage;
        }
        else
        {
            NewsEvent("인류가 전쟁에서 패배했습니다.");
            Gamemanager.instance.saveManaged.hazardRate -= (int)humanDamage;
        }
        Gamemanager.instance.saveManaged.warCount = Random.Range(5,15);
    }
    public void NewsEvent(string _message,UnityAction _action = null)
    {
        news.NewsAdd(_message);
        if (_action != null)
            _action();
    }
    public void PopularEvent()
    {
        PowerLevel human = Gamemanager.instance.saveManaged.humanPower;
        PowerLevel monster = Gamemanager.instance.saveManaged.monsterPower;
        float[] elementRequest = new float[5];
        float[] typeRequest = new float[5];
         elementRequest[0] = human.peopleNumber * (human.weaponList.FindAll(item => item.weaponElement == 0).Count) - 5 * monster.peopleNumber * (monster.weaponList.FindAll(item => item.Type == 1).Count);
         elementRequest[1] = human.peopleNumber * (human.weaponList.FindAll(item => item.weaponElement == 1).Count) - 5 * monster.peopleNumber * (monster.weaponList.FindAll(item => item.Type == 4).Count);
         elementRequest[2] = human.peopleNumber * (human.weaponList.FindAll(item => item.weaponElement == 2).Count) - 5 * monster.peopleNumber * (monster.weaponList.FindAll(item => item.Type == 0).Count);
         elementRequest[3] = human.peopleNumber * (human.weaponList.FindAll(item => item.weaponElement == 3).Count) - 5 * monster.peopleNumber * (monster.weaponList.FindAll(item => item.Type == 2).Count);
         elementRequest[4] = human.peopleNumber * (human.weaponList.FindAll(item => item.weaponElement == 4).Count) - 5 * monster.peopleNumber * (monster.weaponList.FindAll(item => item.Type == 3).Count);
        int min = 0;
        for(int i = 0; i < elementRequest.Length;i++)
        {
            if (elementRequest[min] > elementRequest[i])
                min = i;
        }
            switch(min)
            {
                case 0:
                    NewsEvent("<color=#ff0000>"+"화염"+"</color>"+ "속성무기 납품요청.", null);
                    break;
                case 1:
                    NewsEvent("<color=#00D8FF>" + "물" + "</color>" + "속성무기 납품요청.", null);
                    break;
                case 2:
                    NewsEvent("<color=#B600E5>" + "바람" + "</color>" + "속성무기 납품요청.", null);
                    break;
                case 3:
                    NewsEvent("<color=#E7C200>" + "대지" + "</color>" + "속성무기 납품요청.", null);
                    break;
                case 4:
                    NewsEvent("<color=#B9FF2B>" + "자연" + "</color>" + "속성무기 납품요청.", null);
                    break;
            }
        Gamemanager.instance.PopElement = min;

        typeRequest[0] = monster.peopleNumber * (monster.weaponList.FindAll(item => item.weaponElement == 0).Count) - 5 * human.peopleNumber * (human.weaponList.FindAll(item => item.Type == 1).Count);
        typeRequest[1] = monster.peopleNumber * (monster.weaponList.FindAll(item => item.weaponElement == 1).Count) - 5 * human.peopleNumber * (human.weaponList.FindAll(item => item.Type == 4).Count);
        typeRequest[2] = monster.peopleNumber * (monster.weaponList.FindAll(item => item.weaponElement == 2).Count) - 5 * human.peopleNumber * (human.weaponList.FindAll(item => item.Type == 0).Count);
        typeRequest[3] = monster.peopleNumber * (monster.weaponList.FindAll(item => item.weaponElement == 3).Count) - 5 * human.peopleNumber * (human.weaponList.FindAll(item => item.Type == 2).Count);
        typeRequest[4] = monster.peopleNumber * (monster.weaponList.FindAll(item => item.weaponElement == 4).Count) - 5 * human.peopleNumber * (human.weaponList.FindAll(item => item.Type == 3).Count);
        min = 0;
        for (int i = 0; i < elementRequest.Length; i++)
        {
            if (typeRequest[min] > elementRequest[i])
                min = i;
        }
        switch (min)
        {
            case 0:
                NewsEvent("공격형" + "무기 납품요청.", null);
                break;
            case 1:
                NewsEvent("내구형" + "무기 납품요청.", null);
                break;
            case 2:
                NewsEvent("무게형" + "무기 납품요청.", null);
                break;
            case 3:
                NewsEvent("밸런스형" + "무기 납품요청.", null);
                break;
            case 4:
                NewsEvent("마법형" + "무기 납품요청.", null);
                break;
        }
        Gamemanager.instance.PopType = min;
    }
    public void EventGenerator()
    {
        news.NewsClaer();
        PopularEvent();
        WarEncounter();
        WarNotice();
        StockEvent();
        HumanArmyEvent();
        TurnCountEvent();
    }
    public void WarNotice()
    {
        PowerLevel human = Gamemanager.instance.saveManaged.humanPower;
        PowerLevel monster = Gamemanager.instance.saveManaged.monsterPower;
        NewsEvent("현재 인류병력 은 총 <color=#D26B00>" + human.peopleNumber+"</color>명으로 집계되고있습니다.");
        if(human.heroNumber>0)
            NewsEvent("현재<color=#D26B00>" + human.heroNumber+ "</color>명의 헌터가 몬스터와 싸우고있습니다.");
        NewsEvent("현재 추정되는 몬스터의 숫자는 약<color=#D26B00>" + monster.peopleNumber + "</color>마리입니다.");
        if (monster.heroNumber > 0)
            NewsEvent("헌터와 비슷한 힘을가진 돌연변이가 출연중입니다. 그 수는 약<color=#D26B00>" + monster.heroNumber + "</color>마리 입니다.");
    }

    public void TextReset()
    {
        safeBarText.text = (
            "소지자금 : " + HazardRateString(HazardRatePart2()) +
            "\n일 수익 : " + HazardRateString(HazardRatePart3()) +
            "\n기업 자산 : " +
            "\n제작등급 : " + DataManager.instance.FindTextTable("Entry", (1008 + Gamemanager.instance.saveManaged.openRare).ToString())["Text"] +
            "\n종합 안정도 : " + HazardRateString(HazardRate())
            );
    }
    public float HazardRate()
    {
        float temp = (HazardRatePart1() + HazardRatePart2() + HazardRatePart3()) / 3.0f;
        
        return temp;


    }
    float HazardRatePart1()
    {
        float temp = ((Gamemanager.instance.saveManaged.gameTurn - (Gamemanager.instance.saveManaged.openRare) * 50) / 5.0f) * (13 - Gamemanager.instance.saveManaged.hazardCount) * 1.0f;

        return temp;
    }
    float HazardRatePart2()
    {
        int temp2 = 0;
        for (int i = 0; i < DataManager.instance.weaponTable.Count; i++)
        {
            if (int.Parse(DataManager.instance.weaponTable[i]["Class"].ToString()) == Gamemanager.instance.saveManaged.openRare)
            {
                temp2 = int.Parse(DataManager.instance.weaponTable[i]["Price_max"].ToString());
                break;
            }
        }
        if (temp2 == 0)
            return -1;//오류
        int max = temp2 * 10;
        int gamemoney = Gamemanager.instance.saveManaged.playerMoney.ToInt();
        if (gamemoney >= max)
            gamemoney = max;


        return 100 - 10 * gamemoney / temp2;
        //100- Mathf.Pow(gamemoney/temp2,2);
    }
    float HazardRatePart3()
    {
        int temp2 = 0;
        for (int i = 0; i < DataManager.instance.weaponTable.Count; i++)
        {
            if (int.Parse(DataManager.instance.weaponTable[i]["Class"].ToString()) == Gamemanager.instance.saveManaged.openRare)
            {
                temp2 = int.Parse(DataManager.instance.weaponTable[i]["Price_max"].ToString());
                break;
            }
        }
        if (temp2 == 0)
            return -1;//오류
        float max = temp2 * 1f;
        int gamemoney = Gamemanager.instance.turnPerMoney;
        if (gamemoney >= max)
            gamemoney = (int)max;
        return 100 - 10 * gamemoney / temp2;
    }
    string HazardRateString(float _temp)
    {
        if (_temp >= 70)
        {
            return "불안정";
        }
        else if (_temp >= 20)
        {
            return "보통";
        }
        else
        {
            return "안정";
        }
    }
    // Use this for initialization
    void Start()
    {
        stockEventCount = Random.Range(2, 7);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SortingOrder(GameObject _temp ,int _value)
    {
        _temp.GetComponent<Renderer>().sortingOrder = _value;
    }
    public void WrongBehavior()
    {
        deactiveBG.SetActive(false);
        Random rand = new Random();
        switch(angerGauge)
        {
            case 0:
            case 1:
            case 2:
                uiDialog.OpenDialog(DataManager.instance.FindTextTable("Entry", Random.Range(1050, 1053).ToString())["Text"].ToString(), Resources.Load<Sprite>("Portrait\\policeSprite"));
                break;
            case 3:
            case 4:
            case 5:
                uiDialog.OpenDialog(DataManager.instance.FindTextTable("Entry", Random.Range(1053, 1056).ToString())["Text"].ToString(), Resources.Load<Sprite>("Portrait\\policeSprite"));
                break;
            default:
                uiDialog.OpenDialog(DataManager.instance.FindTextTable("Entry", Random.Range(1056, 1059).ToString())["Text"].ToString(), Resources.Load<Sprite>("Portrait\\policeSprite"));
                break;
        }
        angerGauge++;
    }
    public void StoryScript()
    {
        switch (Gamemanager.instance.saveManaged.storyStep)
        {
            case 0:
                uiDialog.OpenDialog("환영합니다! 자세한것은 국가 기밀이지만 저는 당신을 돕기위해 상부에서 파견된 군인입니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("이미 아시겠지만 저희는 현재 몬스터와 고된 전쟁을 치루고 있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("이미 막대한 인명피해를 입었고 현재는 헌터들에게 인류의 희망을 모두 건 상태지요.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("지금 같은 시대에 당신처럼 헌터를 위한 근대식 무기를 제작할수 있는 인력이 남아있는건 인류에게 있어 큰 희망입니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("우선 국가에 봉사할수 있게끔 무기제작 시설을 간단하게 설명해드리겠습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("....??", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog("우선 화면에 보이는 화로를 클릭해보십시오.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("..!?", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog(null, null, () => StoryScript());
                break;

            case 1:
                deactiveBG.SetActive(true);
                SortingOrder(tutorialButton[0], 11);
                SortingOrder(tutorialButton[1], 11) ;
                SortingOrder(tutorialButton[2], 11);
                SortingOrder(tutorialButton[3], 8);
                fingerCursor.SetActive(true);
                fingerCursor.transform.localPosition = new Vector3(-222,-201);
                
                break;
            case 2:
                deactiveBG.SetActive(false);
                SortingOrder(tutorialButton[0], 0);
                SortingOrder(tutorialButton[1], 0);
                SortingOrder(tutorialButton[2], 0);
                SortingOrder(tutorialButton[3], 0);
                fingerCursor.SetActive(false);
                uiDialog.OpenDialog("....", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog("잘하셨습니다. 이 공방은 헌터들에게 지원할 무기를 제작할 장비들입니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("우선 장비제작에 사용할 화로와 모루, 망치를 선택해보십시오.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog(null, null, () => StoryScript());
                break;
            case 3:
                fingerCursor.SetActive(true);
                fingerCursor.transform.localPosition = new Vector3(547, -320);
                tempObject.Add(FindObjectOfType<UICreate>().transform.Find("Main/ToolField").gameObject);
                tempObject[0].GetComponent<Button>().onClick.AddListener(() => StoryScript());
                tempObject.Add(FindObjectOfType<UICreate>().transform.Find("Main/ExitButton").gameObject);
                tempObject[1].GetComponent<Button>().interactable = false;
                break;
            case 4:
                tempObject[0].GetComponent<Button>().onClick.RemoveAllListeners();
                fingerCursor.SetActive(false);
                uiDialog.OpenDialog("정부는 현재 기본적인 장비를 대여하고 있습니다 해금되어있는 화로,모루, 망치를 선택해주십시오", Resources.Load<Sprite>("Portrait\\policeSprite"));
                tempObject.Add(FindObjectOfType<UITool>().transform.Find("Main/CompleteButton").gameObject);
                tempObject.Add(FindObjectOfType<UITool>().gameObject);
                tempObject[2].GetComponent<Button>().onClick.AddListener(()=> {
                    if (
                        UIManager.instance.createPanel.ItemOptions[0].itemEntry != 0 &&
                        UIManager.instance.createPanel.ItemOptions[1].itemEntry != 0 &&
                        UIManager.instance.createPanel.ItemOptions[2].itemEntry != 0)
                    {
                        StoryScript();
                    }
                    else
                    {
                        uiDialog.OpenDialog("화로 모루 망치중 선택하지 않은 물품이 있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                        tempObject[3].GetComponent<UITool>().OpenUI();
                    }
                });//툴버튼 클릭시 다음으로 진행
                break;
            case 5:
                tempObject[2].GetComponent<Button>().onClick.RemoveAllListeners();
                uiDialog.OpenDialog("기본적인것은 다 준비가 된거같군요.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("....", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog("....8ㅅ8", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog("왼쪽에서는 무기의 기본적인 사양을 선택할수있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("준비가되셨으면 작업시작을 눌러주십시오.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                break;
            case 6:
                tempObject[1].GetComponent<Button>().interactable = true;
                uiDialog.OpenDialog("준비가 모두 끝났군요.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("화살표가 있는 작업게이지를 터치해주십시오.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog(null, null, () => {
                    fingerCursor.SetActive(true);
                    fingerCursor.transform.localPosition = new Vector3(119, 290);
                    deactiveBG.SetActive(true);
                });
                break;
            case 7:
                fingerCursor.SetActive(false);
                deactiveBG.SetActive(false);
                tempObject.Add(FindObjectOfType<UICraft>().transform.Find("Main/ExitButton").gameObject);
                tempObject[4].GetComponent<Button>().interactable = false;
                uiDialog.OpenDialog("이제 본격적으로 재료를 사용해 무기를 제작해보겠습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("우선 지원금을 먼저 드려야겠군요", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog(null, null, () => { Gamemanager.instance.PlayerMoney += ConstManager.STARTMONEY; }) ;
                UIManager.instance.turnOption.SetActive(true);
                UIManager.instance.alarm.SetActive(true);
                uiDialog.OpenDialog("여기서 무기를 제작할 재료를 선택해 보십시오.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("현재 수입루트가 없는 재료는 처음 구매할때 추가 요금이 들어갑니다", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("한번 수입루트가 생긴 재료는 이후 추가요금은 발생하지 않습니다만..", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("모든 재료는 기본적으로 요금이 들어갑니다만 최종정산은 무기가 완성될때 이루어집니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("대장장이님의 센스를 믿고 여기선 한번 맡겨보겠습니다. 왼쪽그래프에서는 무기의 상태를 알수있습니다", Resources.Load<Sprite>("Portrait\\policeSprite"));
                break;
            case 8:
                uiDialog.OpenDialog("헉..헉..", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog("퍽..퍽..", Resources.Load<Sprite>("Portrait\\playerSprite"));
                uiDialog.OpenDialog("참고로 작업도중에는 컴퓨터등으로 각종 다른업무를 볼수도 있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("그럼 이어서 다시한번 작업게이지를 눌러주십시오", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog(null, null, () => { fingerCursor.SetActive(true); });
                tempObject[4].GetComponent<Button>().interactable = true;
                break;
            case 9:
                fingerCursor.SetActive(false);
                uiDialog.OpenDialog("여기서는 손잡이가 될 재료들을 선택할수 있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("별거 아닌거 같지만 여기서 선택하는것들로 무기의 가치가 바뀔수있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("기본적인건 이전 주재료와 같으니 여기역시 맡기겠습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                tempObject.Add(FindObjectOfType<UICraftSub>().transform.Find("Main/ExitButton").gameObject);
                tempObject[5].GetComponent<Button>().interactable = false;
                break;
            case 10:
                tempObject[5].GetComponent<Button>().interactable = true;
                fingerCursor.SetActive(false);
                uiDialog.OpenDialog("여기서 대장장이님이 만드신 무기의 심사를 받을수 있습니다 이 피드백으로 다음에 더욱 훌륭한 무기를 만들어주십시오.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                tempObject.Add(FindObjectOfType<UIGrading>().transform.Find("Main/ExitButton").gameObject);
                tempObject[6].GetComponent<Button>().onClick.AddListener(() => StoryScript());
                break;
            case 11:
                fingerCursor.SetActive(true);
                fingerCursor.transform.localPosition = new Vector3(427, -367);
                uiDialog.OpenDialog("훌륭합니다! 해당무기는 일단 보관해보겠습니다 보관버튼을 눌러주십시오", Resources.Load<Sprite>("Portrait\\policeSprite"));
                tempObject.Add(FindObjectOfType<UIWeaponComp>().transform.Find("Main/Sell").gameObject);
                tempObject.Add(FindObjectOfType<UIWeaponComp>().transform.Find("Main/Post").gameObject);
                tempObject[7].GetComponent<Button>().interactable = false;
                tempObject[8].GetComponent<Button>().interactable = false;
                break;
            case 12:
                fingerCursor.SetActive(false);
                uiDialog.OpenDialog("제작한 무기는 컴퓨터를 통해 인벤토리에서 언제든 확인할수 있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("무기 판매를 선택하신다면 저희가 매입한 무기는 헌터에게 지급하겠습니다. 무기는 저희 인류를 위한 전쟁에 큰 힘이 될것입니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("그럼 제가 말씀드릴수 있는건 여기까지입니다. 모쪼록 몬스터와의 전쟁에서 승리할수 있기를.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog(null, null,()=> {
                    Gamemanager.instance.saveManaged.storyStep = 20;
                    UIManager.instance.turnOption.SetActive(true);
                    UIManager.instance.safeRate.SetActive(true);
                    UIManager.instance.profile.SetActive(true);
                    UIManager.instance.alarm.SetActive(true);
                    UIManager.instance.news.SetActive(true);
                    UIAlarm.instance.TextAlarm("정부군인은 떠나갔다");
                    int temp2 = SaveListManager.instance.playerdata.index;
                    SaveListManager.instance.GameSave(temp2);
                    UIAlarm.instance.TextAlarm("게임이 저장되었다..");
                    EventGenerator();//새이벤트 시작
                });
                tempObject[7].GetComponent<Button>().interactable = true;
                tempObject[8].GetComponent<Button>().interactable = true;
                break;
            case 21:
                uiDialog.OpenDialog("몬스터들의 움직임이 심상치않습니다.. 곧 전쟁이 벌어질지도 모르겠군요.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                if(Gamemanager.instance.saveManaged.humanPower.weaponList.Count > 0)
                    uiDialog.OpenDialog("하지만 대장장이님께서 제작한 무기들 덕분에 인류에게도 조금 희망이 보입니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                else
                    uiDialog.OpenDialog("하지만 어떻게 무기를 단 한개도 납품하지 않을수 있습니까..", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("5일 뒤 전쟁이 발발하면 많은 수의 병사들이 희생될 것입니다. 모든 병사가 전사한다면 이곳도 무사할수 없겠지요", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("허나 몬스터들도 아무피해가 없진 않을겁니다. 특히 저희의 전력인 헌터들은 홀로 많은 몬스터와 맞서며 아군을 지킬수있습니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("전쟁의 결과에 따라 신병들의 지원도 더욱 많아질수도 있겠지요. 이는 저희 인류의 전력에 큰 보탬이 됩니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog("5일까지 그럼 대장장이님도 다가오는 전쟁에 대비해주시기 바랍니다.", Resources.Load<Sprite>("Portrait\\policeSprite"));
                uiDialog.OpenDialog(null, null, () => {
                    Gamemanager.instance.saveManaged.storyStep = 40;
                });
                break;
        }
        Gamemanager.instance.saveManaged.storyStep++;
    }
}
