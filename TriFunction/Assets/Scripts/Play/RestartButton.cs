using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
    void OnMouseDown()
    {
        SceneManager.LoadScene("Play");
    }
}
