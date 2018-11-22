using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour {

    public GameObject EC;

    public GameObject RankBox_Pref;
    public GameObject RankBox_OnlyMine;
    public GameObject RankWindow;
    public GameObject[] RankBox_top5 = new GameObject[5];
    public GameObject RankBox_MineWithtop5;

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
    // Use this for initialization
    private void Awake()
    {
        Get_Rank_Info();
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        RankBox_OnlyMine.SetActive(true);
        Get_Rank_Info();
        RankBox_OnlyMine.GetComponent<RankBox>().Set_RankBox(myrank.rank_num, myrank.rank_Score, myrank.rank_Time, myrank.rank_Nickname, myrank.rank_Badge, myrank.rank_Level);
    }

    public void Make_RankBox_top5andMine()
    {

        Get_Rank_Info();
        RankWindow.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            RankBox_top5[i].GetComponent<RankBox>().Set_RankBox(top5[i].rank_num, top5[i].rank_Score, top5[i].rank_Time, top5[i].rank_Nickname, top5[i].rank_Badge, top5[i].rank_Level);
        }
        if (myrank.rank_num <= 5)
            RankBox_MineWithtop5.SetActive(false);
        else
        {
            RankBox_MineWithtop5.SetActive(true);
            RankBox_MineWithtop5.GetComponent<RankBox>().Set_RankBox(myrank.rank_num, myrank.rank_Score, myrank.rank_Time, myrank.rank_Nickname, myrank.rank_Badge, myrank.rank_Level);
        }
    }
}
