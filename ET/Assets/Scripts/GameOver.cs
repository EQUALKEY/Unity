using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject EventController;
	public GameObject Enemy;

	public void OnMouseDown() {

        EventController.GetComponent<GamePlay>().ResetGame();
	}

    // Update is called once per frame

    public void Awake()
    {
        EventController.GetComponent<GamePlay>().isPlay = false;
    }
}
