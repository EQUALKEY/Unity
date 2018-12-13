using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankClose_play : MonoBehaviour {
    public GameObject GameOverRankBox;
    public GameObject RankWindow;
    public GameObject RankDataWindow;

    void OnMouseDown() {
        PlayerPrefs.SetInt("WindowOn", 0);
        GameOverRankBox.SetActive(true);
        RankWindow.SetActive(false);
        RankDataWindow.SetActive(false);
    }
}
