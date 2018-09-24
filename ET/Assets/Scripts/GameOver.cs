using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public GameObject EventController;
	public GameObject Enemy;

	public void OnMouseDown() {
		Enemy.SetActive(false);
		Enemy.GetComponent<RandomAttack>().Stop();
		EventController.GetComponent<GamePlay>().ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
