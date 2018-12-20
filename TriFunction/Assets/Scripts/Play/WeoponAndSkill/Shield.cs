using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == transform.tag &&
            collision.gameObject != null &&
            collision.gameObject.transform.parent != null &&
            collision.gameObject.transform.parent.gameObject.name == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DoDie();
            Destroy(this.gameObject);
        }
    }
}
