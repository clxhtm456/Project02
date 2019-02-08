using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PowerLevel
{
    public int peopleNumber;
    public List<Weapon> weaponList;
    public int heroNumber;
    public float totalPower(PowerLevel _enemy)
    {
        float temp = 0;
        if (temp < 0)
            temp = 0;
        temp += (peopleNumber/5) * (1+weaponList.FindAll(item => item.weaponElement == 0).Count)- 5*_enemy.peopleNumber/5 * (_enemy.weaponList.FindAll(item => item.Type == 1).Count);
        temp += (peopleNumber/5) * (1+weaponList.FindAll(item => item.weaponElement == 1).Count) - 5*_enemy.peopleNumber/5 * (_enemy.weaponList.FindAll(item => item.Type == 4).Count);
        temp += (peopleNumber/5) * (1+weaponList.FindAll(item => item.weaponElement == 2).Count) - 5*_enemy.peopleNumber/5 * (_enemy.weaponList.FindAll(item => item.Type == 0).Count);
        temp += (peopleNumber/5) * (1+weaponList.FindAll(item => item.weaponElement == 3).Count) - 5*_enemy.peopleNumber/5 * ( _enemy.weaponList.FindAll(item => item.Type == 2).Count);
        temp += (peopleNumber/5) * (1+weaponList.FindAll(item => item.weaponElement == 4).Count) - 5*_enemy.peopleNumber/5 * (_enemy.weaponList.FindAll(item => item.Type == 3).Count);
        temp += peopleNumber * weaponList.Count;
        temp += (50 * heroNumber);
        for (int i = 0; i < weaponList.Count;i++)
            temp += (5*heroNumber * ((weaponList[i].Tier) * 100+weaponList[i].TotalScore));
        return temp;
    }
}
