using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankClose : MonoBehaviour {

    public GameObject EC;
    private RankManager RM;

    private void Awake()
    {
        RM = EC.GetComponent<RankManager>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        RM.Make_RankBox_Only_Mine();
    }
}
