using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    Vector3 direction;
    
    private void Awake() {
        // 스토리모드 4번째 StoryProgress에서 sin쏘면 다음단계로 넘어간다
        if (PlayerPrefs.GetInt("Mode") == 0 && GameObject.Find("EC").GetComponent<MakeEnemy>().StoryProgress == 4) {
                GameObject.Find("EC").GetComponent<MakeEnemy>().StoryProgress++;
                GameObject.Find("EC").GetComponent<MakeEnemy>().StoryManager();
        }
    }

    // 화살 날아감
    void Update () {
        transform.Translate(new Vector3(1f, 0f, 0f) * Time.deltaTime *10f);
	}

    // sin몬스터에 부딪힌 경우 sin몬스터 공격하고 화살이 사라진다
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == transform.tag &&
            collision.gameObject != null &&
            collision.gameObject.transform.parent != null &&
            collision.gameObject.transform.parent.gameObject.name == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DoDie();
            Destroy(gameObject);
        }
    }
}
