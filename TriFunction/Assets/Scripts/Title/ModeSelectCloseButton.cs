using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectCloseButton : MonoBehaviour {
    public GameObject Target;
    public GameObject GameCloseButton;

    void OnMouseDown()
    {
        Target.SetActive(false);
        GameCloseButton.SetActive(true);
        PlayerPrefs.SetInt("WindowOn", 0);
        gameObject.SetActive(false);
    }
}
