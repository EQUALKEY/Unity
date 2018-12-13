using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCloseButton_Title : MonoBehaviour {
    public GameObject RankWindow;
    public GameObject RankDataWindow;

    void OnMouseDown() {
        PlayerPrefs.SetInt("WindowOn", 0);
        RankWindow.SetActive(false);
        RankDataWindow.SetActive(false);
    }
}
