using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCloseButton_Title : MonoBehaviour {
    public GameObject EC;

    void OnMouseDown() {
        EC.transform.GetComponent<RankManager>().CloseRankBox();
    }
}
