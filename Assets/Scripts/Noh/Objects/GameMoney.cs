using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameMoney{
    int copper;
    int silver;
    int gold;
    public int ToInt()
    {
        int temp = gold * 1000000;
        temp += silver * 1000;
        temp += copper;
        return temp;
    }
    public GameMoney()
    {
        copper = 0;
        silver = 0;
        gold = 0;
    }
    public GameMoney(int value)
    {
        if (value >= 0)
            copper = value;
        else
            copper = 0;
        if (copper >= 1000)
        {
            silver += copper / 1000;
            copper = copper % 1000;
        }
        if (silver >= 1000)
        {
            gold += silver / 1000;
            silver = silver % 1000;
        }
    }
    public GameMoney(GameMoney value)
    {
        copper = value.copper;
        silver = value.silver;
        gold = value.gold;
    }

    public int Copper
    {
        set
        {
            if (value > 0)
                copper = value;
            else if (value < 0)
            {
                Silver -= 1;
                copper = 1000 + value;
            }
            else
            {
                copper = 0;
            }
            if (copper >= 1000)
            {
                silver += copper / 1000;
                copper = copper % 1000;
            }
            if (silver >= 1000)
            {
                gold += silver / 1000;
                silver = silver % 1000;
            }
        }
        get
        {
            return copper;
        }
    }
    public int Silver
    {
        set
        {

            if (value > 0)
                silver = value;
            else if (value < 0)
            {
                Gold -= 1;
                silver = 1000 + value;
            }
            else
            {
                silver = 0;
            }
            if (silver >= 1000)
            {
                gold += silver / 1000;
                silver = silver % 1000;
            }
        }
        get
        {
            return silver;
        }
    }
    public int Gold
    {
        set
        {
            if (value > 0)
                gold = value;
            else if (value < 0)
            {
                gold = 0;
                silver = 0;
                copper = 0;
            }
            else
            {
                gold = 0;
            }
        }
        get
        {
            return gold;
        }
    }
    public GameMoney Zero()
    {
        GameMoney temp = new GameMoney(0);
        return temp;
    }
    public void ShowMoney()
    {
        Debug.Log("gold : " + Gold + "silver : " + Silver + "copper : " + Copper);
    }

    public static GameMoney operator +(GameMoney v1, int v2)
    {
        GameMoney temp = new GameMoney(v1);
        temp.Copper += v2;
        return temp;
    }
    public static GameMoney operator +(GameMoney v1, GameMoney v2)
    {
        GameMoney temp = new GameMoney(v1);
        temp.Copper += v2.ToInt();
        return temp;
    }
    public static GameMoney operator -(GameMoney v1, int v2)
    {
        GameMoney temp = new GameMoney(v1);
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (goldVal > 0)
        {
            temp.Gold -= goldVal;
        }
        if (silverVal > 0)
        {
            temp.Silver -= silverVal;
        }
        if(copperVal > 0)
        {
            temp.Copper -= copperVal;
        }

        return temp;
    }
    public static GameMoney operator -(GameMoney v1, GameMoney v2)
    {
        GameMoney temp = new GameMoney(v1);
        if (v2.Gold> 0)
        {
            temp.Gold -= v2.gold;
        }
        if (v2.Silver > 0)
        {
            temp.Silver -= v2.silver;
        }
        if (v2.Copper > 0)
        {
            temp.Copper -= v2.copper;
        }

        return temp;
    }

    public static bool operator >(GameMoney v1, int v2)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return true;
        else if (v1.Gold < goldVal)
            return false;
        else
        {
            if (v1.Silver > silverVal)
                return true;
            else if (v1.Silver < silverVal)
                return false;
            else
            {
                if (v1.Copper > copperVal)
                    return true;
                else if (v1.Copper < copperVal)
                    return false;
                else
                    return false;
            }
        }
    }
    public static bool operator >(GameMoney v1, GameMoney v2)
    {
        if (v1.Gold > v2.Gold)
            return true;
        else if (v1.Gold < v2.Gold)
            return false;
        else
        {
            if (v1.Silver > v2.Silver)
                return true;
            else if (v1.Silver < v2.Silver)
                return false;
            else
            {
                if (v1.Copper > v2.Copper)
                    return true;
                else if (v1.Copper < v2.Copper)
                    return false;
                else
                    return false;
            }
        }
    }
    public static bool operator <(GameMoney v1, GameMoney v2)
    {
        if (v1.Gold > v2.Gold)
            return false;
        else if (v1.Gold < v2.Gold)
            return true;
        else
        {
            if (v1.Silver > v2.Silver)
                return false;
            else if (v1.Silver < v2.Silver)
                return true;
            else
            {
                if (v1.Copper > v2.Copper)
                    return false;
                else if (v1.Copper < v2.Copper)
                    return true;
                else
                    return false;
            }
        }
    }
    public static bool operator >=(GameMoney v1, GameMoney v2)
    {
        if (v1.Gold > v2.Gold)
            return true;
        else if (v1.Gold < v2.Gold)
            return false;
        else
        {
            if (v1.Silver > v2.Silver)
                return true;
            else if (v1.Silver < v2.Silver)
                return false;
            else
            {
                if (v1.Copper > v2.Copper)
                    return true;
                else if (v1.Copper < v2.Copper)
                    return false;
                else
                    return true;
            }
        }
    }
    public static bool operator <=(GameMoney v1, GameMoney v2)
    {
        if (v1.Gold > v2.Gold)
            return false;
        else if (v1.Gold < v2.Gold)
            return true;
        else
        {
            if (v1.Silver > v2.Silver)
                return false;
            else if (v1.Silver < v2.Silver)
                return true;
            else
            {
                if (v1.Copper > v2.Copper)
                    return false;
                else if (v1.Copper < v2.Copper)
                    return true;
                else
                    return true;
            }
        }
    }
    public override string ToString()
    {
        string temp = "";
        if (gold > 0)
        {
            while(gold>1000)
            {
                if(temp == "")
                    temp += ((gold / 1000)).ToString();
                else
                    temp += ((gold / 1000)).ToString("000");
                temp += ",";
                gold %= 1000;
            }
            if (temp == "")
                temp += gold.ToString();
            else
                temp += gold.ToString("000");
            temp += ",";
            temp += silver.ToString("000");
            temp += ",";
            temp += copper.ToString("000");
        }
        else
        {
            if (silver > 0)
            {
                temp += silver.ToString();
                temp += ",";
                temp += copper.ToString("000");
            }else
            {
                if (copper > 0)
                    temp += copper.ToString();
                else
                    temp = "0";
            }
        }

        return temp;
    }
    public static bool operator <(GameMoney v1, int v2)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return false;
        else if (v1.Gold < goldVal)
            return true;
        else
        {
            if (v1.Silver > silverVal)
                return false;
            else if (v1.Silver < silverVal)
                return true;
            else
            {
                if (v1.Copper > copperVal)
                    return false;
                else if (v1.Copper < copperVal)
                    return true;
                else
                    return false;
            }
        }
    }
    public static bool operator >(int v2,GameMoney v1)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return false;
        else if (v1.Gold < goldVal)
            return true;
        else
        {
            if (v1.Silver > silverVal)
                return false;
            else if (v1.Silver < silverVal)
                return true;
            else
            {
                if (v1.Copper > copperVal)
                    return false;
                else if (v1.Copper < copperVal)
                    return true;
                else
                    return false;
            }
        }
    }
    public static bool operator <(int v2, GameMoney v1)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return true;
        else if (v1.Gold < goldVal)
            return false;
        else
        {
            if (v1.Silver > silverVal)
                return true;
            else if (v1.Silver < silverVal)
                return false;
            else
            {
                if (v1.Copper > copperVal)
                    return true;
                else if (v1.Copper < copperVal)
                    return false;
                else
                    return false;
            }
        }
    }

    public static bool operator >=(GameMoney v1, int v2)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return true;
        else if (v1.Gold < goldVal)
            return false;
        else
        {
            if (v1.Silver > silverVal)
                return true;
            else if (v1.Silver < silverVal)
                return false;
            else
            {
                if (v1.Copper > copperVal)
                    return true;
                else if (v1.Copper < copperVal)
                    return false;
                else
                    return true;
            }
        }
    }
    public static bool operator <=(GameMoney v1, int v2)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return false;
        else if (v1.Gold < goldVal)
            return true;
        else
        {
            if (v1.Silver > silverVal)
                return false;
            else if (v1.Silver < silverVal)
                return true;
            else
            {
                if (v1.Copper > copperVal)
                    return false;
                else if (v1.Copper < copperVal)
                    return true;
                else
                    return true;
            }
        }
    }
    public static bool operator >=(int v2, GameMoney v1)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return false;
        else if (v1.Gold < goldVal)
            return true;
        else
        {
            if (v1.Silver > silverVal)
                return false;
            else if (v1.Silver < silverVal)
                return true;
            else
            {
                if (v1.Copper > copperVal)
                    return false;
                else if (v1.Copper < copperVal)
                    return true;
                else
                    return true;
            }
        }
    }
    public static bool operator <=(int v2, GameMoney v1)
    {
        int copperVal = v2 % 1000;
        int temp2 = (v2 / 1000);
        int silverVal = temp2 % 1000;
        int goldVal = (temp2 / 1000) % 1000;
        if (v1.Gold > goldVal)
            return true;
        else if (v1.Gold < goldVal)
            return false;
        else
        {
            if (v1.Silver > silverVal)
                return true;
            else if (v1.Silver < silverVal)
                return false;
            else
            {
                if (v1.Copper > copperVal)
                    return true;
                else if (v1.Copper < copperVal)
                    return false;
                else
                    return true;
            }
        }
    }
}
