using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankButton_Play : MonoBehaviour {
    public GameObject EC;
    public GameObject RankWindow;
    public GameObject RankDataWindow;
    public GameObject GameOverRankBox;

    void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("WindowOn") == 0)
        {
            PlayerPrefs.SetInt("WindowOn", 1);
            GameOverRankBox.SetActive(false);
            EC.GetComponent<RankManager>().GetRankInfo();
            RankWindow.SetActive(true);
            RankDataWindow.SetActive(true);
        }
    }
}