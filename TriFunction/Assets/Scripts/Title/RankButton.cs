using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankButton : MonoBehaviour {
    // public GameObject EC;
    public GameObject RankImage;
    public GameObject CloseButton;
    public GameObject GameoverRankBox;

    ////
    public GameObject RankText_tmp;

    private RankManager RM;

    // private void Awake() {
        // RM = EC.GetComponent<RankManager>();
    // }

    void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            PlayerPrefs.SetInt("WindowOn", 1);
            GameoverRankBox.SetActive(false);
            // RM.MakeRankBox();
            RankText_tmp.SetActive(true);
            RankImage.SetActive(true);
            CloseButton.SetActive(true);
        }
    }
}