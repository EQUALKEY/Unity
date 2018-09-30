using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAngleRange : MonoBehaviour {

    public GameObject CoAngle;
    public GameObject CoAngleEffect;
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
        else
        {
            CoAngleEffect.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        ec.isOutofTriRange = true;
        CoAngleDeleteEffect.SetActive(false);
        CoAngleEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        if(ec.isCo)
        {
            CoAngle.SetActive(false);
            CoAngleDeleteEffect.SetActive(false);
            ec.isCo = false;
        } else
        {
            CoAngle.SetActive(true);
            ec.isCo = true;
        }
    }
}
