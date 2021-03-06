﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

public class RankManager : MonoBehaviour {

    public const string gameName = "TriFunc";

    public GameObject EC;
    
    public GameObject GameoverRankBox;
    public GameObject RankWindow;
    public GameObject RankImage;
    public GameObject RankCloseButton;
    public GameObject[] RankBoxTop5 = new GameObject[5];
    public GameObject MyRankBoxWithTop5;

    //// public Text js;

    // JavaScript 함수 import
    [DllImport("__Internal")]
    private static extern string GetUserData();
    
    // UserData 저장용 구조체
    struct UserData {
        public string host;
        public string userid;
        public string nickname;
        public string token;
    }

    public void SetUserData(string data) {
        UserJsonData = data;
        Debug.Log("Set: " + UserJsonData);

        user = JsonUtility.FromJson<UserData>(UserJsonData);
    }

    // UserData 받아올 JSON과 구조체
    public string UserJsonData;
    UserData user = new UserData();

    // 시작하면서 UserData 받아오고 저장
    void Start() {
        //test data
        //user.host = "https://dev-api.quebon.tv";
        //user.userid = "1068183666556929";
        //user.nickname = "테스트닉네임";
        //user.token = "eyJhbGciOiJIUzI1NiJ9.eyJleHAiOjE1NDMxMzg3MDQsInR5cGUiOiJJTkRWIiwiaWQiOiIxMDY4MTgzNjY2NTU2OTI5Iiwic2Vzc2lvbklkIjoiYTRlMTE1MTItMWExNS00MjM5LTllMDYtNTdiYTBkNzE2ZTE0IiwiYXV0aExldmVsIjo5LCJyb2xlcyI6W3sibmFtZSI6InByZW1pdW1fdXNlciIsInBlcm1pc3Npb25zIjpbIlBSRU1JVU1fVVNFUiJdfV0sInN1YnNjcmlwdGlvbiI6eyJzdWJzY3JpcHRpb25JZCI6IjEyNjg5MjI1OTU1NzQ3ODciLCJlbmREYXRlIjoiMjAxOS0wNS0yMSIsImFjdGl2ZSI6dHJ1ZX0sInJlYWRPbmx5IjpmYWxzZSwiaWF0IjoxNTQzMTE3MTA0fQ.L7s4O-Nskr4Q3YWnAn9Yj3uPe7XH3y6ceyAPeEVFsMY";
        LoadData();
    }

    void LoadData() {
        Application.ExternalCall("SetUserData");
        //UserJsonData = GetUserData();
        Debug.Log("Get: " + UserJsonData);

        // JSON Parsing
        user = JsonUtility.FromJson<UserData>(UserJsonData);
    }

    void Awake() {
        PlayerPrefs.SetInt("WindowOn", 0);
    }

    [System.Serializable]
    struct Badges
    {
        public Badge winner;
    }

    [System.Serializable]
    struct Badge
    {
        public int level;
    }

    [System.Serializable]
    struct User
    {
        public string userId;
        public string nickname;
        public Badges badges;
    }

    [System.Serializable]
    struct Ranking
    {
        public RankData my;
        public List<RankData> ranking;
    }

    // RankData 저장할 구조체
    [System.Serializable]
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
            Debug.Log("token is empty.");
            LoadData();

            //not authorized
            return;
        }

        StartCoroutine(PutRanking(user.token, score, time));

        // 점수는 (int) score, 시간은 (int) time, userid는 (string) user.userid 에 저장되어있다
        // 이 값들을 DB로 보내 저장한다.
    }

    // DB에서 Top5와 자신의 정보 받아온다.
    // token은 (string) user.token에 저장되어 있다.
    // 받아오는 데이터는 각각의 등수, 점수, 시간, 닉네임, 레벨
    private void GetRankInfo() {
        if (string.IsNullOrEmpty(user.token)) {
            Debug.Log("token is empty.");
            LoadData();
            //not authorized
            return;
        }
        StartCoroutine(GetRanking(user.token));
    }

    // Clear나 Gameover 화면 중앙에 랭크 띄우는 함수
    public void MakeGameoverRankBox() {
        GetRankInfo();
        GameoverRankBox.SetActive(true);
        GameoverRankBox.GetComponent<RankBox>().SetRankBox(MyRank.rank, MyRank.score, MyRank.time, MyRank.nickname, MyRank.level);
    }

    // RankButton 눌러서 랭킹창 띄우는 함수
    public void MakeRankBox() {
        GetRankInfo();
        GameoverRankBox.SetActive(false);
        RankImage.SetActive(true);
        RankCloseButton.SetActive(true);
        RankWindow.SetActive(true);
        for (int i = 0; i < 5; i++)
            RankBoxTop5[i].GetComponent<RankBox>().SetRankBox(Top5[i].rank, Top5[i].score, Top5[i].time, Top5[i].nickname, Top5[i].level);

        if (MyRank.rank <= 5)
            MyRankBoxWithTop5.SetActive(false);
        else {
            MyRankBoxWithTop5.SetActive(true);
            MyRankBoxWithTop5.GetComponent<RankBox>().SetRankBox(MyRank.rank, MyRank.score, MyRank.time, MyRank.nickname, MyRank.level);
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
                Debug.Log(w.downloadHandler.text);
                //success
                Ranking r = JsonUtility.FromJson<Ranking>(w.downloadHandler.text);

                MyRank = r.my;
                MyRank.nickname = r.my.user.nickname;
                MyRank.level = r.my.user.badges.winner.level;

                Debug.Log("my rank=" + MyRank.rank);

                int size = Math.Min(r.ranking.Count, 5);
                int i = 0;
                for (i = 0; i < size; i++) {
                    Top5[i] = r.ranking[i];
                    Top5[i].nickname = r.ranking[i].user.nickname;
                    Top5[i].level = r.ranking[i].user.badges.winner.level;

                    Debug.Log(i + ": rank=" + Top5[i].rank + ", score=" + Top5[i].score);
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
        string url = user.host + "/user/v1/games/" + gameName + "/users/" + user.userid;
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
