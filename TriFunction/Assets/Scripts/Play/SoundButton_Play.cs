using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton_Play : MonoBehaviour {

    public GameObject SoundOn;
    public GameObject SoundOff;
    bool isOn;

    void Awake()
    {
        if (PlayerPrefs.GetInt("isSoundOn") == 1)
        {
            isOn = true;
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
        else
        {
            isOn = false;
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (isOn)
        {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
            isOn = false;
        }
        else
        {
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
            isOn = true;
        }
    }
}
