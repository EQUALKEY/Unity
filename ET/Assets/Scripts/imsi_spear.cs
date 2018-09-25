using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imsi_spear : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        StartCoroutine("spearanimation");
	}

    IEnumerator spearanimation()
    {

        for (int i = 0; i < 11; i++)
        {
            transform.Rotate(new Vector3(0f, 0f, -2.5f));
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < 25; i++)
        {
            transform.Translate(new Vector3(0.3f, 0f, 0f));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
