using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

    Vector3 direction;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, 1f, 0f) * Time.deltaTime * 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == transform.tag)
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DoDie();
            Destroy(this.gameObject);
        }
    }
}
