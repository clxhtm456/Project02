using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//0최하점평가
//1최고점평가
//2전체점수평가
public class UIGrading : UIBase {
    public UIGradStar[] uiGradStar;
    public Text[] gradingPerson;
    public Text[] gradingScore;
    private int[] randomIncounter = new int[3];
    private float[] scoreList = new float[5];
    /*등급별 멘트
     * 1 ~ 2 토탈점수 별 멘트
     * 10~12 유행점수 별 멘트0+1*10
     * 20~22 균형점수 별 멘트1+1*10
     * 30~32 실용점수 별 멘트
     * 40~42 디자인점수 별 멘트
     * 50~52 섬세함점수 별 멘트
    */
    // Use this for initialization
    void Start () {
    }
    public override void OpenUI()
    {
        base.OpenUI();
        scoreList[0] = CraftManager.instance.resultWeapon.ScoreOfPopular();
        scoreList[1] = CraftManager.instance.resultWeapon.ScoreOfBalance();
        scoreList[2] = CraftManager.instance.resultWeapon.ScoreOfUsefull();
        scoreList[3] = CraftManager.instance.resultWeapon.ScoreOfDesign();
        scoreList[4] = CraftManager.instance.resultWeapon.ScoreOfSense();
        int min = 0;
        int max = 0;
        for (int i = 0; i < scoreList.Length;i++)
        {
            if (scoreList[i] <= scoreList[min])
                min = i;
            if (scoreList[i] >= scoreList[max])
                max = i;
        }
        float minScore = min != 0?(scoreList [min]/ 10.0f): (scoreList[min] / 28.0f)*20.0f;
        float maxScore = max != 0 ? (scoreList[max] / 10.0f) : (scoreList[max] / 28.0f)*20.0f;
        float totalScore = CraftManager.instance.resultWeapon.TotalScore + scoreList[0];
        Debug.Log("min : " + min + "score : " + minScore);
        Debug.Log("max : " + max + "score : " + maxScore);

        uiGradStar[0].ScorePoint = ((int)maxScore / 2);
        uiGradStar[1].ScorePoint = ((int)minScore / 2);
        uiGradStar[2].ScorePoint = ((int)(CraftManager.instance.resultWeapon.TotalScore + scoreList[0]) / 10);

        if (maxScore < 8)
            maxScore = 1;
        else if (maxScore < 16)
            maxScore = 2;
        else
            maxScore = 3;

        if (minScore < 8)
            minScore = 1;
        else if (minScore < 16)
            minScore = 2;
        else
            minScore = 3;

        if (totalScore < 30)
            totalScore = 1;
        else if (totalScore < 70)
            totalScore = 2;
        else
            totalScore = 3;

        List<Dictionary<string, object>> tempList = new List<Dictionary<string, object>>();
        tempList = DataManager.instance.FindAllTable(DataManager.instance.gradingMentTable, "Key_list", "33" + (max + 1).ToString()+"001");
        tempList = tempList.FindAll(item => item["Key_Score"].ToString() == "33600"+maxScore);
        gradingScore[0].text = tempList[Random.Range(0, tempList.Count)]["Key_Com"].ToString();

        tempList = DataManager.instance.FindAllTable(DataManager.instance.gradingMentTable, "Key_list", "33" + (min + 1).ToString() + "001");
        tempList = tempList.FindAll(item => item["Key_Score"].ToString() == "33600" + minScore);
        gradingScore[1].text = tempList[Random.Range(0, tempList.Count)]["Key_Com"].ToString();
        
        gradingScore[2].text = DataManager.instance.FindRandomTable(DataManager.instance.gradingMent2Table, "Key_tier", "34100" + totalScore.ToString())["Key_com"].ToString();

        tempList = DataManager.instance.FindAllTable(DataManager.instance.contextTable, "Key_Grade", "30100" + (CraftManager.instance.resultWeapon.Rareity + 1).ToString());
        SetIncounterRandom(tempList.Count);
        for (int i = 0; i < gradingPerson.Length; i++)
        {
            gradingPerson[i].text = tempList[randomIncounter[i]]["Key_Grade_Panel"].ToString();
        }



    }
    void SetIncounterRandom(int _temp)
    {
        if (_temp < randomIncounter.Length)//무한루프방지
            return;
        for(int i = 0; i < randomIncounter.Length;i++)
        {
            randomIncounter[i] = Random.Range(0, _temp);
            for(int j = 0; j < i;j++)
            {
                if (randomIncounter[i] == randomIncounter[j])
                    i--;
            }
        }
    }
    public override void ResetPanel()
    {
    }

    // Update is called once per frame
    void Update () {
		
	}
}
