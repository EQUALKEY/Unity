using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour {
	public GameObject SoundOn;
	public GameObject SoundOff;

	bool isOn;

	void Awake() {
		PlayerPrefs.SetInt("isSoundOn", 1);
		isOn = true;
	}

	void OnMouseDown() {
		if(isOn) {
			PlayerPrefs.SetInt("isSoundOn", 0);
			SoundOff.SetActive(true);
			SoundOn.SetActive(false);
			isOn = false;
		} else {
			PlayerPrefs.SetInt("isSoundOn", 1);
			SoundOff.SetActive(false);
			SoundOn.SetActive(true);
			isOn = true;
		}
	}
}
