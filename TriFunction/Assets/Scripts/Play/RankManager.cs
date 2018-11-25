using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using System;

public class RankManager : MonoBehaviour {

    public const string gameName = "TriFunc";

    public GameObject EC;
    
    public GameObject GameoverRankBox;
    public GameObject RankWindow;
    public GameObject RankImage;
    public GameObject RankCloseButton;
    public GameObject[] RankBoxTop5 = new GameObject[5];
    public GameObject MyRankBoxWithTop5;

    // JavaScript 함수 import
    [DllImport("__Internal")]
    private static extern string GetUserData();
    
    // UserData 저장용 구조체
    struct UserData {
        public string host;
        public string userId;
        public string nickname;
        public string token;
    }

    // UserData 받아올 JSON과 구조체
    public string UserJsonData;
    UserData user = new UserData();

    // 시작하면서 UserData 받아오고 저장
    void Start() {
        UserJsonData = GetUserData();

        // JSON Parsing
        user = JsonUtility.FromJson<UserData>(UserJsonData);
    }

    struct Badge
    {
        public int level;
    }

    struct User
    {
        public string userId;
        public string nickname;
        public Dictionary<string, Badge> badges;
    }

    struct Ranking
    {
        public RankData my;
        public List<RankData> ranking;
    }

    // RankData 저장할 구조체
    struct RankData {
        public User user;
        public int rank;        // 등수
        public int score;       // 점수
        public int time;        // 시간
        public string nickname; // 닉네임
        public int level;       // 레벨 (깨봉홈페이지 레벨)
    }

    // 상위 5등과 자신의 RankData 저장할 구조체
    RankData[] Top5 = new RankData[5];

    RankData MyRank;

    // DB에 정보 전송, 점수-시간-userid 를 보낸다
    public void PushRankInfo(int score, int time) {
        if (string.IsNullOrEmpty(user.token))
        {
            //not authorized
            return;
        }

        PutRanking(user.token, score, time);

        // 점수는 (int) score, 시간은 (int) time, userid는 (string) user.userid 에 저장되어있다
        // 이 값들을 DB로 보내 저장한다.
    }

    // DB에서 Top5와 자신의 정보 받아온다.
    // token은 (string) user.token에 저장되어 있다.
    // 받아오는 데이터는 각각의 등수, 점수, 시간, 닉네임, 레벨
    private void GetRankInfo() {
        if (string.IsNullOrEmpty(user.token)) {
            //not authorized
            return;
        }
        GetRanking(user.token);
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

    private IEnumerator GetRanking(string token)
    {
        string url = user.host + "/user/v1/games/" + gameName;

        using (UnityWebRequest w = UnityWebRequest.Get(url))
        {
            w.SetRequestHeader("Authorization", "Bearer " + token);
            yield return w.SendWebRequest();

            if (w.isHttpError || w.isNetworkError) {
                //TODO handle error
            }
            else{
                //success
                Ranking r = JsonUtility.FromJson<Ranking>(w.downloadHandler.text);

                MyRank = r.my;
                MyRank.nickname = r.my.user.nickname;
                if (r.my.user.badges.ContainsKey("winner")) {
                    MyRank.level = r.my.user.badges["winner"].level;
                }

                int size = Math.Min(r.ranking.Count, 5);
                int i = 0;
                for (i = 0; i < size; i++) {
                    Top5[i] = r.ranking[i];
                    Top5[i].nickname = r.ranking[i].user.nickname;
                    if (r.my.user.badges.ContainsKey("winner"))
                    {
                        Top5[i].level = r.ranking[i].user.badges["winner"].level;
                    }
                }
                if (i < 5) {
                    for (int j = i; j < 5; j++) {
                        //TODO don't show empty data
                        Top5[j] = new RankData();
                    }
                }
            }
        }
    }

    private IEnumerator PutRanking(string token, int score, int time)
    {
        string url = user.host + "/user/v1/games/" + gameName + "/users/" + user.userId;
        string data = "{\"score\":" + score + ",\"time\":" + time + "}";

        using (UnityWebRequest w = UnityWebRequest.Put(url, data))
        {
            w.SetRequestHeader("Authorization", "Bearer " + token);
            yield return w.SendWebRequest();

            if (w.isHttpError || w.isNetworkError)
            {
                //TODO handle error
            }
            else
            {
                //sucess
            }
        }
    }
}
