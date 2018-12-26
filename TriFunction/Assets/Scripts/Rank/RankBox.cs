using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankBox : MonoBehaviour {

    // 등수, 점수, 시간, 닉네임, 레벨
    public GameObject N, S, T, NN, L;
    public GameObject NumBackGroundPar;

    public void SetRankBox(int num, int score, float time, string nickname, int level, bool isGameoverRankBox)
    {
        for (int i = 0; i < NumBackGroundPar.transform.childCount; i++) NumBackGroundPar.transform.GetChild(i).gameObject.SetActive(false);
        NumBackGroundPar.transform.GetChild(num % 5).gameObject.SetActive(true);
        if (isGameoverRankBox) N.SetActive(false);
        else
        {
            if (num < 100) SetText(N, num.ToString());
            else SetText(N, "99+");
            N.SetActive(true);
        }

        if (nickname == "")
        {
            SetText(N, "");
            SetText(S, "");
            SetText(T, "");
            SetText(NN, "");
            SetText(L, "");
        } else {
            SetText(S, score.ToString() + "점");
            SetText(T, time.ToString("##0.00") + "초");
            SetText(NN, nickname);
            SetText(L, level.ToString());
        }
    }

    void SetText(GameObject GO, string str) {
        GO.transform.GetComponent<Text>().text = str;
    }
}
