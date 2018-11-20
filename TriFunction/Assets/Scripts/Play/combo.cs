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
                Transform newnum = Instantiate(transform.GetChild(num), transform.position+ new Vector3(0f,0.5f,0f), transform.rotation);
                newnum.gameObject.SetActive(true);
                Transform combotext = Instantiate(transform.GetChild(10), transform.position + new Vector3(0.9f, 0.4f, 0f), transform.rotation);
                combotext.gameObject.SetActive(true);
                break;
            case 1:
                Transform newnum1_0 = Instantiate(transform.GetChild(num/10), transform.position + new Vector3(-0.2f, 0.5f, 0f), transform.rotation);
                newnum1_0.gameObject.SetActive(true);
                Transform newnum1_1 = Instantiate(transform.GetChild(num%10), transform.position + new Vector3(0.2f, 0.5f, 0f), transform.rotation);
                newnum1_1.gameObject.SetActive(true);
                Transform combotext1 = Instantiate(transform.GetChild(10), transform.position + new Vector3(0.7f, 0.4f, 0f), transform.rotation);
                combotext1.gameObject.SetActive(true);
                break;
            case 2:
                Transform newnum2_0 = Instantiate(transform.GetChild(num / 100), transform.position + new Vector3(-0.4f, 0.5f, 0f), transform.rotation);
                newnum2_0.gameObject.SetActive(true);
                Transform newnum2_1 = Instantiate(transform.GetChild((num / 10)%10), transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
                newnum2_1.gameObject.SetActive(true);
                Transform newnum2_2 = Instantiate(transform.GetChild(num % 10), transform.position + new Vector3(0.4f, 0.5f, 0f), transform.rotation);
                newnum2_2.gameObject.SetActive(true);
                Transform combotext2 = Instantiate(transform.GetChild(10), transform.position + new Vector3(0.9f, 0.4f, 0f), transform.rotation);
                combotext2.gameObject.SetActive(true);
                break;
            case 3:
                Transform newnum3_0 = Instantiate(transform.GetChild(num / 1000), transform.position + new Vector3(-0.6f, 0.5f, 0f), transform.rotation);
                newnum3_0.gameObject.SetActive(true);
                Transform newnum3_1 = Instantiate(transform.GetChild((num / 100) % 10), transform.position + new Vector3(-0.2f, 0.5f, 0f), transform.rotation);
                newnum3_1.gameObject.SetActive(true);
                Transform newnum3_2 = Instantiate(transform.GetChild((num /10)% 10), transform.position + new Vector3(0.2f, 0.5f, 0f), transform.rotation);
                newnum3_2.gameObject.SetActive(true);
                Transform newnum3_3 = Instantiate(transform.GetChild(num % 10), transform.position + new Vector3(0.6f, 0.5f, 0f), transform.rotation);
                newnum3_3.gameObject.SetActive(true);
                Transform combotext3 = Instantiate(transform.GetChild(10), transform.position + new Vector3(1.1f, 0.4f, 0f), transform.rotation);
                combotext3.gameObject.SetActive(true);
                break;
        }

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
