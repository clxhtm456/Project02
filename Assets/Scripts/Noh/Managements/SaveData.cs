using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public bool newGame = true;
    public int index;
    public int openRare;//해금등급
    public int storyStep;//스토리 진행정도
    public PowerLevel humanPower;//스토리요소 인류 전력
    public PowerLevel monsterPower;//스토리요소 몬스터 전력
    public int hazardRate;//안정도요소(높으면 인류병력이 참전함)
    public int warCount = 8;
    public GameMoney playerMoney = new GameMoney();
    public bool canWork = true;
    public int gameTurn;
    public int hazardCount;//해금아이템개수
    public int statePoint;//플레이어 스탯포인트
    public int playerLevel;//플레이어 레벨
    public Dictionary<int, int> playerStock = new Dictionary<int, int>(); //보유주식<이름,개수>
    public Dictionary<int, int> stockAvg = new Dictionary<int, int>(); //평단가<주식이름,평단가>
    public Dictionary<int, float> shareRatio = new Dictionary<int, float>(); //보유주식지분율<이름,지분율>
    public Dictionary<int, int> shareSum = new Dictionary<int, int>(); //보유주식지분금액<이름,지분금액>
    public List<StockData> stockState = new List<StockData>();//주식 현황(미구매)
    public List<int> ownTool = new List<int>(); //보유도구<entry>
    public List<int> ownSubr = new List<int>();//보유 서브항목<entry>
    public List<Weapon> ownWeapon = new List<Weapon>();//보유무기(인벤토리)
    public List<Royalty> ownRoyalty = new List<Royalty>();//보유특허<무기,활성화상태>
    public List<Factory> ownFactory = new List<Factory>();//보유공장
    public Dictionary<int, Royalty> activeRoyalty = new Dictionary<int, Royalty>();//활성화특허<공장,특허정보>
}
