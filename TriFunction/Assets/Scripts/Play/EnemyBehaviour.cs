﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    public float Velocity;
    private Vector3 Direction3d;
    private Vector2 Direction;

	// Use this for initialization
	void Awake () {
        EC = GameObject.Find("EC"); // Prefab이다 보니까 public에 못넣음. 직접 찾아야 되더라. 다른방법 찾으면 Find 없애자.
        ec = EC.GetComponent<EventController>();
        Direction3d = new Vector3(0f,0f,0f) - transform.position;
        Direction = (new Vector2(Direction3d.x,Direction3d.y )).normalized;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Direction * Velocity * Time.deltaTime);
        if (Vector3.Distance(new Vector3(0f, 0f, 0f), transform.position) < 2f)
        {
            ec.SetAnimationParameters(0, 2);
            ec.LostLife();
            Destroy(this.gameObject);
        }
	}

    public void DoDie()
    {
        ec.GetScore(1);
        Velocity = 0f;
        GetComponent<Animator>().SetInteger("Monster_state", 1);
        StartCoroutine("DoDestroy");
    }

    IEnumerator DoDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}