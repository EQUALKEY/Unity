using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NormalButton : MonoBehaviour {
	void OnMouseDown() {
		PlayerPrefs.SetInt("Mode", 1);
		SceneManager.LoadScene("Play");
	}
}
