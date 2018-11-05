using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
    void OnMouseDown()
    {
        if (this.gameObject.tag == "Finish") PlayerPrefs.SetInt("Mode", 1);
        SceneManager.LoadScene("Play");
    }
}
