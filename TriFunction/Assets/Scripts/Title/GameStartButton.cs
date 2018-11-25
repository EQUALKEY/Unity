using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour {
	public GameObject ModeSelect;

	void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            PlayerPrefs.SetInt("WindowOn", 1);
            ModeSelect.SetActive(true);
        }
	}
}
