using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
    public GameObject Tri;
    public GameObject CircleCollision;
    public GameObject Character;

    // 임시 랭크
    public GameObject RankWindow;

    void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            Tri.SetActive(true);
            CircleCollision.SetActive(true);
            Character.SetActive(false);
            RankWindow.SetActive(false);
            if (gameObject.tag == "Finish") PlayerPrefs.SetInt("Mode", 1);
            SceneManager.LoadScene("Play");
        }
    }
}
