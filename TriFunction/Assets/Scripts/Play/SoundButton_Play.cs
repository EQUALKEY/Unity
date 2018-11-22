using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton_Play : MonoBehaviour {

    public GameObject SoundOn;
    public GameObject SoundOff;
    public GameObject EC;
    private AudioManager AM;
    bool isOn;

    void Awake() {
        AM = EC.GetComponent<AudioManager>();

        if (PlayerPrefs.GetInt("isSoundOn") == 1) {
            isOn = true;
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        } else {
            isOn = false;
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
        }
    }

    void OnMouseDown() {
        if (isOn) {
            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
            PlayerPrefs.SetInt("isSoundOn", 0);
            isOn = false;
            AM.SoundOff();
        } else {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
            PlayerPrefs.SetInt("isSoundOn", 1);
            isOn = true;
            AM.SoundOn();
        }
    }
}
