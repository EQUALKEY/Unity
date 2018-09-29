using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Title");
    }
}
