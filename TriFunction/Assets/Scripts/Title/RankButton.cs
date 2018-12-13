using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankButton : MonoBehaviour {
    public GameObject EC;
    public GameObject RankWindow;
    public GameObject RankDataWindow;

    void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            PlayerPrefs.SetInt("WindowOn", 1);
            EC.GetComponent<RankManager>().GetRankInfo();
            RankWindow.SetActive(true);
            RankDataWindow.SetActive(true);
        }
    }
}