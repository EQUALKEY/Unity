using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyButton : MonoBehaviour {
	void OnMouseDown() {
		PlayerPrefs.SetInt("Mode", 0);
		SceneManager.LoadScene("Play");
	}
}
