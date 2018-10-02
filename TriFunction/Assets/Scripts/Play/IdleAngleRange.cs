﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAngleRange : MonoBehaviour {
    
    public GameObject EC;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        if (!ec.isRotating)
        {
            if (ec.isCo)
            {
                ec.CoAngleDeleteEffect.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        ec.CoAngleDeleteEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (ec.isCo)
        {
            ec.CoAngle.SetActive(false);
            ec.CoAngleDeleteEffect.SetActive(false);
            ec.isCo = false;
        }
    }
}