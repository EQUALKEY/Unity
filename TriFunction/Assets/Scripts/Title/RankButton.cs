using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankButton : MonoBehaviour {
    public GameObject EC;

    // 임시 랭크 (추가예정입니다)
    public GameObject RankText_tmp;
    public GameObject RankImage;
    public GameObject CloseButton;
    public GameObject RankWindow;

    private RankManager RM;

    private void Awake() {
        RM = EC.GetComponent<RankManager>();
    }

    void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            PlayerPrefs.SetInt("WindowOn", 1);
            RankText_tmp.SetActive(true);
            RankImage.SetActive(true);
            RankWindow.SetActive(false);
            CloseButton.SetActive(true);
        }
        //RM.MakeRankBox();
    }
}