using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankBox : MonoBehaviour {

    public GameObject N, S, T, NN, B, L;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Set_RankBox(int num, int score, int time, string nickname, int badge, int level)
    {
        Set_Num_Text(num);
        Set_Text(S, score.ToString());
        Set_Text(T, time.ToString());
        Set_Text(NN, nickname);
        Set_Text(L, level.ToString());
    }

    void Set_Text(GameObject GO, string str)
    {
        GO.transform.GetChild(0).GetComponent<Text>().text = str;
    }
    void Set_Num_Text(int num)
    {
        N.transform.GetChild(num % 5).gameObject.SetActive(true);
        if (num < 100)
            N.transform.GetChild(5).GetComponent<Text>().text = num.ToString();
        else
            N.transform.GetChild(5).GetComponent<Text>().text = "99+";
    }
}
