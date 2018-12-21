using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {
    void Awake() {
        StartCoroutine("vib");
    }

    IEnumerator vib() {
        for (int i = 0; i < 20; i++) {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x + 0.003f, scale.y + 0.003f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 20; i++) {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x - 0.003f, scale.y - 0.003f, 0);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("vib");
    }
}
