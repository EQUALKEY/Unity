using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Vector3 direction;
    
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Mode") == 0){
            if (GameObject.Find("EC").GetComponent<MakeEnemy>().StoryProgress == 4)
            {
                GameObject.Find("EC").GetComponent<MakeEnemy>().StoryProgress++;
                GameObject.Find("EC").GetComponent<MakeEnemy>().StoryManager();
            }


        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(new Vector3(1f, 0f, 0f) * Time.deltaTime *10f);
	}

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
