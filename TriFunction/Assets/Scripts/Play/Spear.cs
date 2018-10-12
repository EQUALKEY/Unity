using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    Vector3 direction;
    bool isStraight;
    private void Awake()
    {
        isStraight = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        StartCoroutine("Spear_Move");
    }
    // Update is called once per frame
    void Update()
    {
        if (isStraight)
        {
            transform.Translate(new Vector3(0f, 1f, 0f) * Time.deltaTime * 10f);
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, 1f) * 10f);
        }
    }

    IEnumerator Spear_Move()
    {
        yield return new WaitForSeconds(0.5f);
        isStraight = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == transform.tag &&
            collision.gameObject != null &&
            collision.gameObject.transform.parent != null &&
            collision.gameObject.transform.parent.gameObject.name == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DoDie();
            isStraight = false;
        }
    }
}
