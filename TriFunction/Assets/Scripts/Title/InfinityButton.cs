using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfinityButton : MonoBehaviour
{
    void OnMouseDown()
    {
        PlayerPrefs.SetInt("isMonsterTypeOff", 0);
        PlayerPrefs.SetInt("Mode", 1);
        SceneManager.LoadScene("Play");
    }
}
