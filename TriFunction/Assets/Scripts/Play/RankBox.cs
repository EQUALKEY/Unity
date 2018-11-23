using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankBox : MonoBehaviour {

    // 등수, 점수, 시간, 닉네임, 레벨
    public GameObject N, S, T, NN, L;
    public GameObject NumBackGroundPar;
    
    public void SetRankBox(int num, int score, int time, string nickname, int level) {
        NumBackGroundPar.transform.GetChild(num % 5).gameObject.SetActive(true);
        if (num < 100) SetText(N, num.ToString());
        else SetText(N, "99+");
        SetText(S, score.ToString());
        SetText(T, time.ToString());
        SetText(NN, nickname);
        SetText(L, level.ToString());
    }

    void SetText(GameObject GO, string str) {
        GO.transform.GetComponent<Text>().text = str;
    }
}
