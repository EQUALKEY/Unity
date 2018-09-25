using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    public GameObject Tri;

	// Use this for initialization
	void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        Tri.transform.Translate(new Vector3(0f, 0f, -0.01f));
    }
}
