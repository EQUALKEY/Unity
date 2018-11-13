﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

    public GameObject EC;
    private EventController ec;
    float f;

    void Awake()
    {
        f = 0.3f;
        this.gameObject.transform.localScale = new Vector3(f, f, 1f);
        ec = EC.GetComponent<EventController>();
        ec.SkillReady = false;
        ec.UltiStar.SetActive(false);
    }

    void Update()
    {
        if(f<4.5f)
        {
            ec.SkillButton.SetActive(false);
            ec.SkillReady = false;
            f += 0.04f;
            this.gameObject.transform.localScale = new Vector3(f, f, 1f);
        } else
        {
            f = 0.3f;
            this.gameObject.transform.localScale = new Vector3(f, f, 1f);
            this.gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null &&
            collision.transform.parent.name == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DoDie(true);
        }
    }
}