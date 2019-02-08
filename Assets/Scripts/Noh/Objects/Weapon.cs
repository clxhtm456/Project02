using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Weapon
{
    public int iconEntry;
    public string itemName;

    public string itemContext;
    public string contextName;
    private float toolScore = -1;
    private float additionalScore = -1;
    private float totalScore = -1;
    private float popScore = -1;


    public ItemOptions[] toolList;
    public ItemOptions[] resourceMetal;
    public ItemOptions[] resourceItem;
    int type = 0;
    int rareity = 0;
    int additionalWork = -1;
    public Weapon()
    {
        toolList = new ItemOptions[3];
        for (int i = 0; i < toolList.Length; i++)
            toolList[i].parameter = new float[6];
        resourceMetal = new ItemOptions[3];
        for (int i = 0; i < resourceMetal.Length; i++)
            resourceMetal[i].parameter = new float[6];
        resourceItem = new ItemOptions[3];
        for (int i = 0; i < resourceItem.Length; i++)
            resourceItem[i].parameter = new float[6];
        type = 0;
        rareity = 0;
        additionalWork = -1;
    }
    public void SellItem()
    {
        Gamemanager.instance.PlayerMoney += ItemPrice;
        Gamemanager.instance.saveManaged.ownWeapon.Remove(this);
        Gamemanager.instance.saveManaged.humanPower.weaponList.Add(this);//무기 정규군 지원
    }
    public int weaponElement
    {
        get
        {
            for (int i = 0; i < resourceItem[2].parameter.Length; i++)
            {
                if (resourceItem[2].parameter[i] == 1.0f)
                    return i;
            }
            return 0;
        }
    }
    public int Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Rareity
    {
        get { return rareity; }
        set { rareity = value; }
    }
    public int AdditionalWork
    {
        get { return additionalWork; }
        set { additionalWork = value; }
    }
    public float ToolScore
    {
        get { return toolScore; }
        set { toolScore = value; }
    }
    public float AdditionalScore
    {
        get { return additionalScore; }
        set { additionalScore = value; }
    }
    public int AttackPower
    {
        get
        {
            int temp = 0;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            temp = (int)WeaponCalc(
                int.Parse(weapontable["Attack_max"].ToString()),
                int.Parse(weapontable["Attack_min"].ToString())
                , TotalScore + popScore, 100.0f);
            return temp;
        }
    }
    public float AttackSpeed
    {
        get
        {
            float temp = 0.0f;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            float weaponinfo_min;
            float weaponinfo_max;
            float.TryParse(weapontable["Speed_max"].ToString(), out weaponinfo_max);
            float.TryParse(weapontable["Speed_min"].ToString(), out weaponinfo_min);
            temp = WeaponCalc(
               weaponinfo_max,
                weaponinfo_min,
                TotalScore + popScore, 100.0f);
            temp = Mathf.Round(temp * 10.0f) * 0.1f;
            return temp;
        }
    }
    public float Durability
    {
        get
        {
            float temp = 0.0f;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            float weaponinfo_min;
            float weaponinfo_max;
            float.TryParse(weapontable["Dura_max"].ToString(), out weaponinfo_max);
            float.TryParse(weapontable["Dura_min"].ToString(), out weaponinfo_min);
            temp = WeaponCalc(
               weaponinfo_max,
                weaponinfo_min,
                TotalScore + popScore, 100.0f);
            temp = Mathf.Round(temp * 10.0f) * 0.1f;
            return temp;
        }
    }
    public float MagicPower
    {
        get
        {
            float temp = 0.0f;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            temp = WeaponCalc(
                int.Parse(weapontable["Magic_max"].ToString()),
                int.Parse(weapontable["Magic_min"].ToString())
                , TotalScore + popScore, 100.0f);
            temp = Mathf.Round(temp * 10.0f) * 0.1f;
            return temp;
        }
    }
    public int OptionCount
    {
        get
        {
            int temp = 0;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            temp = (int)WeaponCalc(
                int.Parse(weapontable["Option_max"].ToString()),
                int.Parse(weapontable["Option_min"].ToString())
                , TotalScore + popScore, 100.0f);
            return temp;
        }
    }
    public int Special
    {
        get
        {
            int temp = 0;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            temp = (int)WeaponCalc(
                int.Parse(weapontable["Special_max"].ToString()),
                int.Parse(weapontable["Special_min"].ToString())
                , TotalScore + popScore, 100.0f);
            return temp;
        }
    }
    public int ItemPrice
    {
        get
        {
            int temp = 0;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            float weaponinfo_min;
            float weaponinfo_max;
            float.TryParse(weapontable["Price_max"].ToString(), out weaponinfo_max);
            float.TryParse(weapontable["Price_min"].ToString(), out weaponinfo_min);
            temp = (int)WeaponCalc(
               weaponinfo_max,
                weaponinfo_min,
                TotalScore + ScoreOfPopular(), 100.0f);
            return temp;
        }
    }
    public GameMoney ItemPriceData
    {
        get
        {
            int temp = 0;
            Dictionary<string, object> weapontable = DataManager.instance.FindWeaponTable("Class", Rareity.ToString());

            float weaponinfo_min;
            float weaponinfo_max;
            float.TryParse(weapontable["Price_max"].ToString(), out weaponinfo_max);
            float.TryParse(weapontable["Price_min"].ToString(), out weaponinfo_min);
            temp = (int)WeaponCalc(
               weaponinfo_max,
                weaponinfo_min,
                TotalScore + ScoreOfPopular(), 100.0f);
            GameMoney temp2 = new GameMoney(temp);
            return temp2;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Sprite LoadIcon()
    {
        Sprite itemIcon;
        itemIcon = Resources.Load<Sprite>("Icon\\" + iconEntry.ToString());
        return itemIcon;
    }
    float WeaponCalc(float _max, float _min, float _value)
    {

        if (_value > 1.0f)
            _value = 1.0f;
        if (_value < -1.0f)
            _value = -1.0f;
        float temp = (_max * (1 + _value) + _min * (1 - _value)) * 0.5f;
        return temp;
    }
    float WeaponCalc(float _max, float _min, float _value, float _valmax)
    {
        _value = _value / _valmax;
        if (_value > 1.0f)
            _value = 1.0f;
        if (_value < 0.0f)
            _value = 0.0f;
        return (_min + ((_max - _min) * _value));
    }
    public float ScoreOfUsefull()
    {
        float sum_Har = Player.instance.playerState[0];
        float sum_Var = Player.instance.playerState[1];
        float sum_Dur = Player.instance.playerState[2];
        float sum_Mas = Player.instance.playerState[3];
        float sum_Man = Player.instance.playerState[4];
        float usefull = 0.0f;
        for (int i = 0; i < resourceMetal.Length; i++)//금속재료 확인
            if (resourceMetal[i].itemEntry != 0)
            {
                sum_Har += resourceMetal[i].parameter[0];
                sum_Var += resourceMetal[i].parameter[1];
                sum_Dur += resourceMetal[i].parameter[2];
                sum_Mas += resourceMetal[i].parameter[3];
                sum_Man += resourceMetal[i].parameter[4];
            }
        usefull = WeaponCalc(ConstManager.USEFULLH_MAX, ConstManager.USEFULLH_MIN, sum_Har) +
            WeaponCalc(ConstManager.USEFULLV_MAX, ConstManager.USEFULLV_MIN, sum_Var) +
             WeaponCalc(ConstManager.USEFULLD_MAX, ConstManager.USEFULLD_MIN, sum_Dur) +
              WeaponCalc(ConstManager.USEFULLM_MAX, ConstManager.USEFULLM_MIN, sum_Mas) +
             WeaponCalc(ConstManager.USEFULLA_MAX, ConstManager.USEFULLA_MIN, sum_Man);
        if (resourceItem[0].itemEntry != 0)
            usefull += WeaponCalc(ConstManager.WOODUSEFULL_MAX, ConstManager.WOODUSEFULL_MIN, resourceItem[0].parameter[0]);
        if (usefull >= 10.0f)
            usefull = 10.0f;
        //Debug.Log("실용성 점수 : " + usefull);
        return usefull;
    }

    public float ScoreOfDesign()
    {
        float sum_Har = Player.instance.playerState[0];
        float sum_Var = Player.instance.playerState[1];
        float sum_Dur = Player.instance.playerState[2];
        float sum_Mas = Player.instance.playerState[3];
        float sum_Man = Player.instance.playerState[4];
        float design = 0.0f;
        for (int i = 0; i < resourceMetal.Length; i++)//금속재료 확인
            if (resourceMetal[i].itemEntry != 0)
            {
                sum_Har += resourceMetal[i].parameter[0];
                sum_Var += resourceMetal[i].parameter[1];
                sum_Dur += resourceMetal[i].parameter[2];
                sum_Mas += resourceMetal[i].parameter[3];
                sum_Man += resourceMetal[i].parameter[4];
            }
        design = WeaponCalc(ConstManager.DESIGNH_MAX, ConstManager.DESIGNH_MIN, sum_Har) +
            WeaponCalc(ConstManager.DESIGNV_MAX, ConstManager.DESIGNV_MIN, sum_Var) +
             WeaponCalc(ConstManager.DESIGND_MAX, ConstManager.DESIGND_MIN, sum_Dur) +
              WeaponCalc(ConstManager.DESIGNM_MAX, ConstManager.DESIGNM_MIN, sum_Mas) +
             WeaponCalc(ConstManager.DESIGNA_MAX, ConstManager.DESIGNA_MIN, sum_Man);

        if (resourceItem[0].itemEntry != 0)
            design += WeaponCalc(ConstManager.WOODDESIGN_MAX, ConstManager.WOODDESIGN_MIN, resourceItem[0].parameter[1]);
        if (design >= 10.0f)
            design = 10.0f;
        //Debug.Log("디자인 점수 : " + design);
        return design;
    }

    public float ScoreOfSense()
    {
        float sum_Har = Player.instance.playerState[0];
        float sum_Var = Player.instance.playerState[1];
        float sum_Dur = Player.instance.playerState[2];
        float sum_Mas = Player.instance.playerState[3];
        float sum_Man = Player.instance.playerState[4];
        float sense = 0.0f;
        for (int i = 0; i < resourceMetal.Length; i++)//금속재료 확인
            if (resourceMetal[i].itemEntry != 0)
            {
                sum_Har += resourceMetal[i].parameter[0];
                sum_Var += resourceMetal[i].parameter[1];
                sum_Dur += resourceMetal[i].parameter[2];
                sum_Mas += resourceMetal[i].parameter[3];
                sum_Man += resourceMetal[i].parameter[4];
            }
        sense = WeaponCalc(ConstManager.SENSEH_MAX, ConstManager.SENSEH_MIN, sum_Har) +
            WeaponCalc(ConstManager.SENSEV_MAX, ConstManager.SENSEV_MIN, sum_Var) +
             WeaponCalc(ConstManager.SENSED_MAX, ConstManager.SENSED_MIN, sum_Dur) +
              WeaponCalc(ConstManager.SENSEM_MAX, ConstManager.SENSEM_MIN, sum_Mas) +
             WeaponCalc(ConstManager.SENSEA_MAX, ConstManager.SENSEA_MIN, sum_Man);


        if (resourceItem[1].itemEntry != 0)
            sense += WeaponCalc(ConstManager.LEASENSE_MAX, ConstManager.LEASENSE_MIN, resourceItem[1].parameter[1]);
        if (sense >= 10.0f)
            sense = 10.0f;
        //Debug.Log("섬세함 점수 : " + sense);
        return sense;
    }

    public float ScoreOfBalance()
    {
        float sum_Har = Player.instance.playerState[0];
        float sum_Var = Player.instance.playerState[1];
        float sum_Dur = Player.instance.playerState[2];
        float sum_Mas = Player.instance.playerState[3];
        float sum_Man = Player.instance.playerState[4];
        for (int i = 0; i < resourceMetal.Length; i++)//금속재료 확인
            if (resourceMetal[i].itemEntry != 0)
            {
                sum_Har += resourceMetal[i].parameter[0];
                sum_Var += resourceMetal[i].parameter[1];
                sum_Dur += resourceMetal[i].parameter[2];
                sum_Mas += resourceMetal[i].parameter[3];
                sum_Man += resourceMetal[i].parameter[4];
            }
        float avg = (sum_Har + sum_Var + sum_Dur + sum_Mas + sum_Man) / 5.0f;
        float mo = (
        (sum_Har - avg) * (sum_Har - avg) +
            (sum_Var - avg) * (sum_Var - avg) +
            (sum_Dur - avg) * (sum_Dur - avg) +
            (sum_Mas - avg) * (sum_Mas - avg) +
            (sum_Man - avg) * (sum_Man - avg)) / 4.0f;
        float balanceScore = (1 - Mathf.Sqrt(mo)) * 10.0f;


        if (resourceItem[1].itemEntry != 0)
            balanceScore += WeaponCalc(ConstManager.LEAVALANCE_MAX, ConstManager.LEAVALANCE_MIN, resourceItem[1].parameter[0]);
        if (balanceScore >= 10.0f)
            balanceScore = 10.0f;
        //Debug.Log("균형점수 : " + balanceScore);
        return balanceScore;
    }
    public float ScoreOfTool()
    {
        float temp = 0.0f;
        for (int i = 0; i < toolList.Length; i++)
        {
            int randVal = Random.Range(0, 100);
            temp +=
                (randVal <= toolList[i].parameter[0] ?
                Random.Range(toolList[i].parameter[1], toolList[i].parameter[2]) :
                Random.Range(toolList[i].parameter[3], toolList[i].parameter[4])
                );
        }
        temp /= 3.0f;
        if (temp >= 25.0f)
            temp = 25.0f;
        //Debug.Log("도구 점수 : " + temp);
        return temp;
    }
    public float ScoreOfAddition()
    {
        float temp = 0.0f;
        if (additionalWork == 0 || additionalWork == 2)
        {
            temp += 3.0f;
        }
        if (additionalWork == 1 || additionalWork == 2)
        {
            if (ConstManager.RandomEvent(50))//50퍼센트확률로
                temp += 10.0f;
            else
                temp -= 5.0f;
        }
        //Debug.Log("추가 작업 : " + temp);
        return temp;
    }
    public float ScoreOfPopular()
    {
        float temp = 0.0f;
        if (resourceItem[2].itemEntry != 0)
            temp += (resourceItem[2].parameter[Gamemanager.instance.PopElement] == 1.0f) ? 5.0f : 0.0f;
        temp += Gamemanager.instance.PopType == Type ? 5.0f : 0.0f;
        if (temp >= 10.0f)
            temp += 8.0f;
        if (resourceItem[2].itemEntry != 0)
            temp += 10.0f * resourceItem[2].parameter[Type];
        if (temp >= 28.0f)
            temp = 28.0f;
        //Debug.Log("유행 점수 : " + temp);
        return temp;
    }
    public float TotalScore
    {
        get
        {
            if (totalScore == -1)
            {
                float temp = 0.0f;
                if (ToolScore == -1)
                    ToolScore = ScoreOfTool();
                if (AdditionalScore == -1)
                    AdditionalScore = ScoreOfAddition();
                if (popScore == -1)
                    popScore = ScoreOfPopular();
                temp +=
                    ScoreOfUsefull() +
                    ScoreOfDesign() +
                    ScoreOfSense() +
                    ScoreOfBalance() +
                    ToolScore +
                    AdditionalScore;

                itemContext += DataManager.instance.FindRandomTable(DataManager.instance.contextTable, "Key_Grade", "30100"+(Rareity+1).ToString())["Key_Name"].ToString() + " ";
                itemContext += DataManager.instance.FindRandomTable(DataManager.instance.context1Table, "Key_tier", "31100" + ((temp + ScoreOfPopular() - 1 < 0) ? 0 : (int)((temp + ScoreOfPopular() - 1) / 20.0f)+1).ToString())["Key_define"].ToString() + " ";
                itemContext += contextName;
                totalScore = temp;
                //Debug.Log("유행제외 토탈 점수 : " + temp);
                iconEntry = type * 10 + (int)(totalScore / 20.0f);
                return temp;
            }
            else
                return totalScore;
        }
    }
    public int Tier
    {
        get
        {
            int temp;

            if (TotalScore >= 90)
                temp = 10;
            else
                temp = ((int)TotalScore / 10) + 1;
            return temp;
        }
    }
}
