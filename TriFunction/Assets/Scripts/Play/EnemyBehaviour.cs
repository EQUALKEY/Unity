using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    public GameObject Combo;

    public float Velocity;
    private Vector3 Direction3d;
    private Vector2 Direction;

    private float PushVelocity;
    private bool isPushing;

    // Use this for initialization
    void Awake () {
        EC = GameObject.Find("EC"); // Prefab이다 보니까 public에 못넣음. 직접 찾아야 되더라. 다른방법 찾으면 Find 없애자.
        Combo = GameObject.Find("Combo");
        ec = EC.GetComponent<EventController>();
        Direction3d = new Vector3(0f,0f,0f) - transform.position;
        Direction = (new Vector2(Direction3d.x,Direction3d.y )).normalized;
        PushVelocity = 30f;
        isPushing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.magnitude > 4f) isPushing = false;
        if (!isPushing) transform.Translate(Direction * Velocity * Time.deltaTime);
        else transform.Translate(-Direction * PushVelocity * Time.deltaTime);

        if (Vector3.Distance(new Vector3(0f, 0f, 0f), transform.position) < 1.6f) {
            ec.SetAnimationParameters(0, 2);
            ec.LostLife();
            Destroy(this.gameObject);
        }

        if (ec.isMonsterTypeOn) this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void PushBack() {
        if(transform.position.magnitude < 4f) isPushing = true;
    }

    public void DoDie(bool isSkill = false) {
        EC.GetComponent<AudioManager>().MonsterHitSound();
        if (!isSkill) ec.GetSkillGauge(1);
        ec.combo++;
        ec.GetScore(1,transform.position,transform.rotation);
        Velocity = 0f;
        GetComponent<Animator>().SetInteger("Monster_state", 1);
        StartCoroutine("DoDestroy");
    }

    IEnumerator DoDestroy() {
        Destroy(GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject.transform.GetChild(0).gameObject);
        Destroy(this.gameObject);
    }
}
