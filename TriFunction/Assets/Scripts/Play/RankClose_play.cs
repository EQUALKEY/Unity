using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankClose_play : MonoBehaviour {
    //// public GameObject EC;
    //// public GameObject GameoverRankBox;

    // 임시 Rank
    public GameObject RankImage;
    public GameObject RankText_tmp;

    void OnMouseDown() {
        //// EC.GetComponent<RankManager>().CloseRankBox();
        //// GameoverRankBox.SetActive(true);
        PlayerPrefs.SetInt("WindowOn", 0);
        RankImage.SetActive(false);
        RankText_tmp.SetActive(false);
        gameObject.SetActive(false);
    }
}
