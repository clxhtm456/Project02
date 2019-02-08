using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<Dictionary<string, object>> textTable;
    public List<Dictionary<string, object>> toolTable;
    public List<Dictionary<string, object>> metalTable;
    public List<Dictionary<string, object>> subresourceTable;
    public List<Dictionary<string, object>> manacubeTable;
    public List<Dictionary<string, object>> weaponTable;
    public List<Dictionary<string, object>> contextTable;
    public List<Dictionary<string, object>> context1Table;
    public List<Dictionary<string, object>> context2Table;
    public List<Dictionary<string, object>> gradingMentTable;
    public List<Dictionary<string, object>> gradingMent2Table;
    public List<Dictionary<string, object>> gradingPersonTable;
    public List<Dictionary<string, object>> factoryTable;
    // Use this for initialization
    new void Awake()
    {
        LoadingTextTable();
        LoadingToolTable();
        LoadingMetalTable();
        LoadingSubresourceTable();
        LoadingManacubeTable();
        LoadingWeaponTable();
        LoadingContextTable();
        LoadingContext1Table();
        LoadingContext2Table();
        LoadingGradingMentTable();
        LoadingGradingMent2Table();
        LoadingGradingPersonTable();
        LoadingFactoryTable();
    }
    public void LoadingTextTable()
    {
        try
        {
            textTable = CSVReader.Read("texttable");
        }
        catch
        {
            Debug.Log("texttable 읽기실패");
        }
    }
    public void LoadingToolTable()
    {
        try
        {
            toolTable = CSVReader.Read("tooltable");
            for (int i = 0; i < toolTable.Count; i++)
            {
                toolTable[i]["Key_Name"] = FindTextTable("Entry", toolTable[i]["Key_Name"].ToString())["Text"].ToString();
                toolTable[i]["Key_Define"] = FindTextTable("Entry", toolTable[i]["Key_Define"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("tooltable 읽기실패");
        }
    }
    public void LoadingMetalTable()
    {
        try
        {
            metalTable = CSVReader.Read("metaltable");
            for (int i = 0; i < metalTable.Count; i++)
            {
                metalTable[i]["Key_Name"] = FindTextTable("Entry", metalTable[i]["Key_Name"].ToString())["Text"].ToString();
                metalTable[i]["Key_Define"] = FindTextTable("Entry", metalTable[i]["Key_Define"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("metaltable 읽기실패");
        }
    }
    public void LoadingSubresourceTable()
    {
        try
        {
            subresourceTable = CSVReader.Read("subresourceTable");
            for (int i = 0; i < subresourceTable.Count; i++)
            {
                subresourceTable[i]["Key_Name"] = FindTextTable("Entry", subresourceTable[i]["Key_Name"].ToString())["Text"].ToString();
                subresourceTable[i]["Key_Define"] = FindTextTable("Entry", subresourceTable[i]["Key_Define"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("subresourceTable 읽기실패");
        }
    }
    public void LoadingManacubeTable()
    {
        try
        {
            manacubeTable = CSVReader.Read("manacubetable");
            for (int i = 0; i < manacubeTable.Count; i++)
            {
                manacubeTable[i]["Key_Name"] = FindTextTable("Entry", manacubeTable[i]["Key_Name"].ToString())["Text"].ToString();
                manacubeTable[i]["Key_Define"] = FindTextTable("Entry", manacubeTable[i]["Key_Define"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("manacubeTable 읽기실패");
        }
    }

    public void LoadingWeaponTable()
    {
        try
        {
            weaponTable = CSVReader.Read("weaponTable");
        }
        catch
        {
            Debug.Log("weaponTable 읽기실패");
        }
    }
    public void LoadingContextTable()
    {
        try
        {
            contextTable = CSVReader.Read("weaponContext");
            for (int i = 0; i < contextTable.Count; i++)
            {
                contextTable[i]["Key_Name"] = FindTextTable("Entry", contextTable[i]["Key_Name"].ToString())["Text"].ToString();
                contextTable[i]["Key_Grade_Panel"] = FindTextTable("Entry", contextTable[i]["Key_Grade_Panel"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("weaponContext 읽기실패");
        }
    }
    public void LoadingContext1Table()
    {
        try
        {
            context1Table = CSVReader.Read("weaponContext2");
            for (int i = 0; i < context1Table.Count; i++)
            {
                context1Table[i]["Key_define"] = FindTextTable("Entry", context1Table[i]["Key_define"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("weaponContext 읽기실패");
        }
    }
    public void LoadingContext2Table()
    {
        try
        {
            context2Table = CSVReader.Read("weaponContext3");
            for (int i = 0; i < context2Table.Count; i++)
            {
                context2Table[i]["Key_Name"] = FindTextTable("Entry", context2Table[i]["Key_Name"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("weaponContext 읽기실패");
        }
    }
    public void LoadingGradingMentTable()
    {
        try
        {
            gradingMentTable = CSVReader.Read("GradingMent");
            for (int i = 0; i < gradingMentTable.Count; i++)
            {
                gradingMentTable[i]["Key_Com"] = FindTextTable("Entry", gradingMentTable[i]["Key_Com"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("gradingMentTable 읽기실패");
        }
    }
    public void LoadingGradingMent2Table()
    {
        try
        {
            gradingMent2Table = CSVReader.Read("GradingMent2");
            for (int i = 0; i < gradingMent2Table.Count; i++)
            {
                gradingMent2Table[i]["Key_com"] = FindTextTable("Entry", gradingMent2Table[i]["Key_com"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("gradingMent2Table 읽기실패");
        }
    }
    public void LoadingGradingPersonTable()
    {
        try
        {
            gradingPersonTable = CSVReader.Read("GradingPerson");
            for (int i = 0; i < gradingPersonTable.Count; i++)
            {
                gradingPersonTable[i]["Text"] = FindTextTable("Entry", gradingPersonTable[i]["Text"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("gradingPersonTable 읽기실패");
        }
    }
    public void LoadingFactoryTable()
    {
        try
        {
            factoryTable = CSVReader.Read("factorytable");
            for (int i = 0; i < factoryTable.Count; i++)
            {
                factoryTable[i]["Key_Name"] = FindTextTable("Entry", factoryTable[i]["Key_Name"].ToString())["Text"].ToString();
                factoryTable[i]["Key_Define"] = FindTextTable("Entry", factoryTable[i]["Key_Define"].ToString())["Text"].ToString();
            }
        }
        catch
        {
            Debug.Log("factorytable 읽기실패");
        }
    }
    public Dictionary<string, object> FindTextTable(string _table, string _value)
    {
        //Entry
        //Text
        //TextLoc1
        //TextLoc2 
               return textTable.Find(item => item[_table].ToString() == _value);
    }
    public Dictionary<string, object> FindToolTable(string _table, string _value)
    {
        //Key 
        //Categorize  
        //Key_Name 
        //Score_Min01 
        //Score_Max01 
        //Probability01   
        //Score_Min02 
        //Score_Max02 
        //Probability02 
        //CancelABanPrice 
        //Price 
        //Key_Define
        return toolTable.Find(item => item[_table].ToString() == _value);
    }

    public Dictionary<string, object> FindMetalTable(string _table, string _value)
    {
        //Key
        //Key_Name
        //Class
        //Hardness
        //Variability
        //Durability
        //Mass
        //Mana
        //TotalForm
        //Key_StrongPoint 
        //Key_WeakPoint 
        //Price   
        //Key_Define
        return metalTable.Find(item => item[_table].ToString() == _value);
    }

    public Dictionary<string, object> FindSubresouceTable(string _table, string _value)
    {
        //Key
        //Type
        //Class
        //Key_Name
        //Completion
        //Sensitive
        //CancelABanPrice
        //Price
        //Key_Define
        return subresourceTable.Find(item => item[_table].ToString() == _value);
    }
    public Dictionary<string, object> FindManacubeTable(string _table, string _value)
    {
        //Key
        //Key_Name
        //Class
        //Key_Option
        //Key_SpecialAbility
        //Attack_Form
        //Deign_Form
        //Mass_Form
        //Durability_Form
        //Mana_Form
        //Key_Properties
        //Price
        //Key_Define

        return manacubeTable.Find(item => item[_table].ToString() == _value);
    }

    public Dictionary<string, object> FindWeaponTable(string _table, string _value)
    {
        //Class
        //Attack_min
        //Attack_max
        //Speed_min
        //Speed_max
        //Dura_min
        //Dura_max
        //Magic_min
        //Magic_max
        //Option_min
        //Option_max
        //Special_min
        //Special_max
        //Price_min
        //Price_max
        return weaponTable.Find(item => item[_table].ToString() == _value);
    }
    public Dictionary<string, object> FindContextTable(string _table, string _value)
    {
        //Class
        //Text
        return contextTable.Find(item => item[_table].ToString() == _value);
    }
    public Dictionary<string, object> FindContext1Table(string _table, string _value)
    {
        //Class
        //Text
        return context1Table.Find(item => item[_table].ToString() == _value);
    }
    public Dictionary<string, object> FindContext2Table(string _table, string _value)
    {
        //Class
        //Text
        return context2Table.Find(item => item[_table].ToString() == _value);
    }
    public Dictionary<string, object> FindTable(List<Dictionary<string, object>> _list, string _table, string _value)
    {
        try
        {
            return _list.Find(item => item[_table].ToString() == _value);
        }catch
        {
            return null;
        }
    }
    public List<Dictionary<string, object>> FindAllTable(List<Dictionary<string, object>> _list, string _table, int max, int min)
    {
        try
        {
            return _list.FindAll(item => int.Parse(item[_table].ToString()) >= min && int.Parse(item[_table].ToString()) <= max);
        }
        catch
        {
            return null;
        }
    }
    public List<Dictionary<string, object>> FindAllTable(List<Dictionary<string, object>> _list, string _table, string _value)
    {
        try
        {
            return _list.FindAll(item => item[_table].ToString() == _value);
        }
        catch
        {
            return null;
        }
    }
    public Dictionary<string, object> FindRandomTable(List<Dictionary<string, object>> _list, string _table, string _value)
    {
        try
        {
            List<Dictionary<string, object>> _temp = _list.FindAll(item => item[_table].ToString() == _value);
            return _temp[Random.Range(0, _temp.Count)];
        }catch
        {
            return null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
