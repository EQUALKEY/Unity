using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankClose_play : MonoBehaviour {
    public GameObject EC;
    public GameObject GameoverRankBox;

    void OnMouseDown() {
        EC.GetComponent<RankManager>().CloseRankBox();
        GameoverRankBox.SetActive(true);
    }
}
