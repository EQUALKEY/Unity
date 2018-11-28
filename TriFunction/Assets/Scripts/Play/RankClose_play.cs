using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankClose_play : MonoBehaviour {
    public GameObject EC;
    public GameObject GameoverRankBox;
    public GameObject RankImage;

    void OnMouseDown() {
        PlayerPrefs.SetInt("WindowOn", 0);
        EC.GetComponent<RankManager>().CloseRankBox();
        GameoverRankBox.SetActive(true);
        RankImage.SetActive(false);
        gameObject.SetActive(false);
    }
}
