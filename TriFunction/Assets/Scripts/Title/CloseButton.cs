using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {
	public GameObject Target;

	void OnMouseDown() {
		Target.SetActive(false);
        PlayerPrefs.SetInt("WindowOn", 0);
	}
}
