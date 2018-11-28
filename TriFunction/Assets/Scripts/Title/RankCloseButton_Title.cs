using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCloseButton_Title : MonoBehaviour {
    // public GameObject EC;
    public GameObject RankImage;

    ////
    public GameObject RankText_tmp;

    void OnMouseDown() {
        PlayerPrefs.SetInt("WindowOn", 0);
        RankImage.SetActive(false);
        // EC.transform.GetComponent<RankManager>().CloseRankBox();
        RankText_tmp.SetActive(false);
        gameObject.SetActive(false);
    }
}
