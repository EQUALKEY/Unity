using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCloseButton_Title : MonoBehaviour {
    public GameObject EC;
    public GameObject RankImage;

    void OnMouseDown() {
        PlayerPrefs.SetInt("WindowOn", 0);
        RankImage.SetActive(false);
        EC.transform.GetComponent<RankManager>().CloseRankBox();
        gameObject.SetActive(false);
    }
}
