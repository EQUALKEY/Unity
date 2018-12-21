using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController_Play : MonoBehaviour {
    
    private EventController ec;
    public GameObject GameOverRankBox;
    public GameObject GameCloseButton;
    public GameObject RankWindow;
    public GameObject RankDataWindow;
    public GameObject Triangle;
    public GameObject CollisionCircle;
    public GameObject Character;

    void Awake() { ec = transform.GetComponent<EventController>(); }

    public void GameClose_Play() {
        Application.OpenURL("https://www.quebon.tv/game/triFunction/exit");
    }

    public void MonsterTypeOn() {
        PlayerPrefs.SetInt("isMonsterTypeOn", 1);
        ec.isMonsterInfoOn = true;
    }

    public void MonsterTypeOff() {
        PlayerPrefs.SetInt("isMonsterTypeOn", 0);
        ec.isMonsterInfoOn = false;
    }

    public void Totitle() {
        SceneManager.LoadScene("Title");
    }

    public void Restart() {
        SceneManager.LoadScene("Play");
    }

    public void InfinityMode() {
        PlayerPrefs.SetInt("Mode", 1);
        SceneManager.LoadScene("Play");
    }

    public void RankOn() {
        ec.GetComponent<RankManager>().GetRankInfo();
    }
}
