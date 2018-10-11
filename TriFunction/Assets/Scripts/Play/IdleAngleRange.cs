using System.Collections;
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
        if (!ec.isRotating && ec.isCo && ec.Tstate == 0)
        {
            ec.CoAngleDeleteEffect.SetActive(true);
            ec.IdleAngleEffect.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        ec.CoAngleDeleteEffect.SetActive(false);
        ec.IdleAngleEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (ec.isCo && ec.Tstate == 0)
        {
            ec.IdleAngleEffect.SetActive(false);
            ec.CoAngleDeleteEffect.SetActive(false);
            ec.CoAngle.SetActive(false);
            ec.isCo = false;
        }
    }
}
