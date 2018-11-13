using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo : MonoBehaviour {

    public int num;
    int numline;
    private void Awake()
    {
        num = GameObject.Find("EC").GetComponent<EventController>().combo;
        numline = (int)Mathf.Log10(num);

        switch (numline)
        {
            case 0:
                Transform newnum = Instantiate(transform.GetChild(num), transform.position, transform.rotation);
                newnum.gameObject.SetActive(true);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }

        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
