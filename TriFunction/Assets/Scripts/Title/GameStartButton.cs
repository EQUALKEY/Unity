using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : MonoBehaviour {
	public GameObject ModeSelect;

	void OnMouseDown() {
		ModeSelect.SetActive(true);
	}
}
