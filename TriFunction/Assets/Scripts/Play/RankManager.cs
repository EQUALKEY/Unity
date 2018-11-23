using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour {

    public GameObject EC;
    
    public GameObject GameoverRankBox;
    public GameObject RankWindow;
    public GameObject RankImage;
    public GameObject RankCloseButton;
    public GameObject[] RankBoxTop5 = new GameObject[5];
    public GameObject MyRankBoxWithTop5;

    // RankData 저장할 구조체
    struct RankData {
        public int Rank;        // 등수
        public int Score;       // 점수
        public int Time;        // 시간
        public string Nickname; // 닉네임
        public int Level;       // 레벨 (깨봉홈페이지 레벨)
    }

    // 상위 5등과 자신의 RankData 저장할 구조체
    RankData[] Top5 = new RankData[5];
    RankData MyRank;

    // DB에 정보 전송
    public void PushRankInfo(int Rank, int Time, string Nickname) {
        // 이 세 값을 DB로 보낸다.
        // Rank, Time, Nickname을 DB로보내고 저장한다.
    }

    // DB에서 Top5와 자신의 정보 받아온다.
    // 받아오는 데이터는 각각의 등수, 점수, 시간, 닉네임, 레벨
    private void GetRankInfo() {
        // RankData MyRank, Top5[5]; 에서
        // MyRank에는 플레이어의 정보 저장
        // Top5[5]에는 상위 5명 정보 저장

        //////////////////////
        //
        // ????
        //
        ///////////////////////

        /* 받아온 값들 대입
        MyRank.Rank = 
        MyRank.Score = 
        MyRank.Time = 
        MyRank.Nickname = 
        MyRank.Level = 

        Top5[0].Rank = 
        Top5[0].Score = 
        Top5[0].Time = 
        Top5[0].Nickname = 
        Top5[0].Level = 

        Top5[1].Rank = 
        Top5[1].Score = 
        Top5[1].Time = 
        Top5[1].Nickname = 
        Top5[1].Level = 

        ...

        Top5[4].Rank = 
        Top5[4].Score = 
        Top5[4].Time = 
        Top5[4].Nickname = 
        Top5[4].Level = 
        */
    }

    // Clear나 Gameover 화면 중앙에 랭크 띄우는 함수
    public void MakeGameoverRankBox() {
        GetRankInfo();
        GameoverRankBox.SetActive(true);
        GameoverRankBox.GetComponent<RankBox>().SetRankBox(MyRank.Rank, MyRank.Score, MyRank.Time, MyRank.Nickname, MyRank.Level);
    }

    // RankButton 눌러서 랭킹창 띄우는 함수
    public void MakeRankBox() {
        GetRankInfo();
        GameoverRankBox.SetActive(false);
        RankImage.SetActive(true);
        RankCloseButton.SetActive(true);
        RankWindow.SetActive(true);
        for (int i = 0; i < 5; i++)
            RankBoxTop5[i].GetComponent<RankBox>().SetRankBox(Top5[i].Rank, Top5[i].Score, Top5[i].Time, Top5[i].Nickname, Top5[i].Level);

        if (MyRank.Rank <= 5)
            MyRankBoxWithTop5.SetActive(false);
        else {
            MyRankBoxWithTop5.SetActive(true);
            MyRankBoxWithTop5.GetComponent<RankBox>().SetRankBox(MyRank.Rank, MyRank.Score, MyRank.Time, MyRank.Nickname, MyRank.Level);
        }
    }

    // RankBox 끔. GameoverRankBox는 안켜줌 (Title에서는 필요없는 기능)
    public void CloseRankBox() {
        RankWindow.SetActive(false);
        RankImage.SetActive(false);
        RankCloseButton.SetActive(false);
    }
}
