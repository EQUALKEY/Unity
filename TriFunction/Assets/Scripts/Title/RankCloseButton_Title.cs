using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCloseButton_Title : MonoBehaviour {
    //// public GameObject EC;

    // 임시 RankText
    public GameObject RankImage;
    public GameObject RankText_tmp;

    void OnMouseDown() {
        RankImage.SetActive(false);
        RankText_tmp.SetActive(false);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("WindowOn", 0);
        //// EC.transform.GetComponent<RankManager>().CloseRankBox();
    }
}
