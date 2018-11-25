using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleButton : MonoBehaviour
{
    void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            SceneManager.LoadScene("Title");
        }
    }
}
