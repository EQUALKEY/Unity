using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour {

    public GameObject EC;

    public GameObject RankBoxPref;
    public GameObject RankBoxOnlyMine;
    public GameObject RankWindow;
    public GameObject[] RankBoxTop5 = new GameObject[5];
    public GameObject RankBoxMineWithTop5;

    struct rank_str {
        public int rank_num;
        public int rank_Score;
        public int rank_Time;
        public string rank_Nickname;
        public int rank_Badge;
        public int rank_Level;
    }

    rank_str[] top5 = new rank_str[5];
    rank_str myrank;
    
    private void Awake()
    {
        Get_Rank_Info();
        myrank.rank_num = 1;
        myrank.rank_Score = 100;
        myrank.rank_Time = 100;
        myrank.rank_Nickname = "Hello";
        myrank.rank_Badge = 0;
        myrank.rank_Level = 1;
    }

    public void Push_Rank_Info()
    {
        //DB에 정보 전송
    }
    void Get_Rank_Info()
    {
        //top5 에 정보를 저장
        //myrank에 정보를 저장
    }

    public void Make_RankBox_Only_Mine()
    {
        RankBoxOnlyMine.SetActive(true);
        Get_Rank_Info();
        RankBoxOnlyMine.GetComponent<RankBox>().Set_RankBox(myrank.rank_num, myrank.rank_Score, myrank.rank_Time, myrank.rank_Nickname, myrank.rank_Badge, myrank.rank_Level);
    }

    public void Make_RankBox_top5andMine()
    {

        Get_Rank_Info();
        RankWindow.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            RankBoxTop5[i].GetComponent<RankBox>().Set_RankBox(top5[i].rank_num, top5[i].rank_Score, top5[i].rank_Time, top5[i].rank_Nickname, top5[i].rank_Badge, top5[i].rank_Level);
        }
        if (myrank.rank_num <= 5)
            RankBoxMineWithTop5.SetActive(false);
        else
        {
            RankBoxMineWithTop5.SetActive(true);
            RankBoxMineWithTop5.GetComponent<RankBox>().Set_RankBox(myrank.rank_num, myrank.rank_Score, myrank.rank_Time, myrank.rank_Nickname, myrank.rank_Badge, myrank.rank_Level);
        }
    }
}
