using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboNum : MonoBehaviour {

    private void Awake()
    {

        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
