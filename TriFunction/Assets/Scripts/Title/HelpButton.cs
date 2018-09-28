using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour {
	public GameObject HelpWindow;

	void OnMouseDown() {
		HelpWindow.SetActive(true);
	}
}
