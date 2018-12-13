using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryButton : MonoBehaviour {
    void OnMouseDown() {
        PlayerPrefs.SetInt("WindowOn", 0);
        PlayerPrefs.SetInt("isMonsterTypeOff", 0);
        PlayerPrefs.SetInt("Mode", 0);
        SceneManager.LoadScene("Play");
    }
}
