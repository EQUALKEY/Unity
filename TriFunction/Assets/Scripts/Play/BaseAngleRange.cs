using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAngleRange : MonoBehaviour {
    
    public GameObject CoAngle;
    public GameObject CoAngleDeleteEffect;
    public GameObject EC;
    private EventController ec;

    void Awake()
    {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        ec.isOutofTriRange = false;
        if (ec.isCo)
        {
            CoAngleDeleteEffect.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        ec.isOutofTriRange = true;
        CoAngleDeleteEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (ec.isCo)
        {
            CoAngle.SetActive(false);
            CoAngleDeleteEffect.SetActive(false);
            ec.isCo = false;
        }
    }
}
