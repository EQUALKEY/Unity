using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankButton : MonoBehaviour {
    public GameObject EC;
    public GameObject RankImage;
    public GameObject CloseButton;
    public GameObject GameoverRankBox;

    private RankManager RM;

    private void Awake() {
        RM = EC.GetComponent<RankManager>();
    }

    void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            PlayerPrefs.SetInt("WindowOn", 1);
            GameoverRankBox.SetActive(false);
            RM.MakeRankBox();
            RankImage.SetActive(true);
            CloseButton.SetActive(true);
        }
        RM.MakeRankBox();
    }
}
