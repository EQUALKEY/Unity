using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAngleRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        ec.onCoAngleRange = true;
        if (!ec.isRotating)
        {
            if (ec.isCo) ec.CoAngleDeleteEffect.SetActive(true);
            else ec.CoAngleEffect.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        ec.onCoAngleRange = false;
        ec.CoAngleDeleteEffect.SetActive(false);
        ec.CoAngleEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (ec.isCo)
        {
            ec.CoAngle.SetActive(false);
            ec.CoAngleDeleteEffect.SetActive(false);
            ec.CoAngleEffect.SetActive(true);
            ec.isCo = false;
        }
        else
        {
            ec.CoAngleEffect.SetActive(false);
            ec.CoAngle.SetActive(true);
            ec.CoAngleDeleteEffect.SetActive(true);
            ec.isCo = true;
        }
    }
}
