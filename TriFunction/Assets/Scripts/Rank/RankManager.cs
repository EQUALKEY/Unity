using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{

    public const string gameName = "TriFunc";

    public GameObject EC;

    public GameObject GameOverRankBox;
    public GameObject MyRankData;
    public GameObject RankWindow;
    public GameObject RankDataWindow;
    public GameObject[] RankBoxTop5 = new GameObject[5];
    public GameObject MyRankBoxWithTop5;
    public Text WaitPlz;
    public Text MyWaitPlz;

    // UserData 저장용 구조체
    struct UserData
    {
        public string host;
        public string userid;
        public string nickname;
        public string token;
        public UserData(string host, string userid, string nickname, string token) {
            this.host = host;
            this.userid = userid;
            this.nickname = nickname;
            this.token = token;
        } 
    }

    public void SetUserData(string data)
    {
        UserJsonData = data;
        // Debug.Log("Set: " + UserJsonData);

        user = JsonUtility.FromJson<UserData>(UserJsonData);
    }

    // UserData 받아올 JSON과 구조체
    public string UserJsonData;
    UserData user = new UserData();

    // 시작하면서 UserData 받아오고 저장
    void Start() {
        LoadData();
    }

    void LoadData()
    {
        Application.ExternalCall("SetUserData");
        // Debug.Log("Get: " + UserJsonData);

        // JSON Parsing
        user = JsonUtility.FromJson<UserData>(UserJsonData);
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
    struct RankData
    {
        public User user;
        public int rank;        // 등수
        public int score;       // 점수
        public float time;     // 시간
        public string nickname; // 닉네임
        public int level;       // 레벨 (깨봉홈페이지 레벨)
    }

    // 상위 5등과 자신의 RankData 저장할 구조체
    RankData[] Top5 = new RankData[5];

    RankData MyRank;

    // DB에 정보 전송, 점수-시간-userid 를 보낸다
    public void PutAndGetRankInfo(int score, float time)
    {
        MyWaitPlz.text = "로딩 중...";
        if (string.IsNullOrEmpty(user.token))
        {
            // Debug.Log("token is empty.");
            LoadData();

            //not authorized
            return;
        }

        StartCoroutine(PutAndGetRanking(user.token, score, time));

        // 점수는 (int) score, 시간은 (int) time, userid는 (string) user.userid 에 저장되어있다
        // 이 값들을 DB로 보내 저장한다.
    }

    // DB에서 Top5와 자신의 정보 받아온다.
    // token은 (string) user.token에 저장되어 있다.
    // 받아오는 데이터는 각각의 등수, 점수, 시간, 닉네임, 레벨
    public void GetRankInfo()
    {
        WaitPlz.text = "로딩 중...";
        if (string.IsNullOrEmpty(user.token))
        {
            // Debug.Log("token is empty.");
            LoadData();
            //not authorized
            return;
        }
        StartCoroutine(GetRanking(user.token));
    }

    private IEnumerator GetRanking(string token)
    {
        string url = user.host + "/user/v1/games/" + gameName;

        using (UnityWebRequest w = UnityWebRequest.Get(url))
        {
            w.SetRequestHeader("Authorization", "Bearer " + token);
            yield return w.SendWebRequest();

            if (w.isHttpError || w.isNetworkError)
            {
                //TODO handle error
            }
            else
            {
                // Debug.Log(w.downloadHandler.text);
                //success
                Ranking r = JsonUtility.FromJson<Ranking>(w.downloadHandler.text);

                MyRank = r.my;
                MyRank.nickname = r.my.user.nickname;
                MyRank.level = r.my.user.badges.winner.level;

                WaitPlz.text = "";
                if (RankDataWindow.activeSelf) RankDataWindow.transform.localPosition = new Vector3();
                else RankDataWindow.SetActive(true);
                // Debug.Log("my rank=" + MyRank.rank);

                int size = Math.Min(r.ranking.Count, 5);
                int i = 0;
                for (i = 0; i < size; i++)
                {
                    Top5[i] = r.ranking[i];
                    Top5[i].nickname = r.ranking[i].user.nickname;
                    Top5[i].level = r.ranking[i].user.badges.winner.level;

                    // Debug.Log(i + ": rank=" + Top5[i].rank + ", score=" + Top5[i].score);
                }
                if (i < 5)
                {
                    for (int j = i; j < 5; j++)
                    {
                        //TODO don't show empty data
                        Top5[j] = new RankData();
                    }
                }
                for (i = 0; i < 5; i++)
                    RankBoxTop5[i].GetComponent<RankBox>().SetRankBox(Top5[i].rank, Top5[i].score, Top5[i].time, Top5[i].nickname, Top5[i].level, false);

                if (MyRank.rank <= 5)
                    MyRankBoxWithTop5.SetActive(false);
                else
                {
                    MyRankBoxWithTop5.GetComponent<RankBox>().SetRankBox(MyRank.rank, MyRank.score, MyRank.time, MyRank.nickname, MyRank.level, false);
                    MyRankBoxWithTop5.SetActive(true);
                }
                if (PlayerPrefs.GetInt("Mode") == 1) GameOverRankBox.GetComponent<RankBox>().SetRankBox(MyRank.rank, MyRank.score, MyRank.time, MyRank.nickname, MyRank.level, false);
            }
        }
    }

    private IEnumerator PutAndGetRanking(string token, int score, float time)
    {
        string url = user.host + "/user/v1/games/" + gameName + "/users/" + user.userid;
        string data = "{\"score\":" + score + ",\"time\":" + time + "}";

        using (UnityWebRequest w = UnityWebRequest.Put(url, data))
        {
            w.SetRequestHeader("Authorization", "Bearer " + token);
            w.SetRequestHeader("Content-Type", "application/json");
            // Debug.Log(url + "\n\n" + data);
            yield return w.SendWebRequest();

            if (w.isHttpError || w.isNetworkError)
            {
                //TODO handle error
            }
            else
            {
                //sucess
                MyRank = JsonUtility.FromJson<RankData>(w.downloadHandler.text);

                // Debug.Log(w.downloadHandler.text);
                // Debug.Log("my rank=" + MyRank.rank);

                // Debug.Log(w.downloadHandler.text);
                //success
                RankData r = JsonUtility.FromJson<RankData>(w.downloadHandler.text);
                
                MyRank.nickname = r.user.nickname;
                MyRank.level = r.user.badges.winner.level;
                
                if (MyRank.score <= score) EC.transform.GetComponent<EventController>().GameOverRankBox.GetComponent<RankBox>().SetRankBox(MyRank.rank, score, time, MyRank.nickname, MyRank.level, false);
                else EC.transform.GetComponent<EventController>().GameOverRankBox.GetComponent<RankBox>().SetRankBox(0, score, time, MyRank.nickname, MyRank.level, true);
                MyWaitPlz.text = "";
                MyRankData.SetActive(true);
            }
        }
    }
}