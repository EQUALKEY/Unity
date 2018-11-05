using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAngleRange : MonoBehaviour {
    
    public GameObject EC;
    private EventController ec;

    void Awake() { ec = EC.GetComponent<EventController>(); }

    private void OnMouseEnter() { ec.onIdleAngle = true; }

    private void OnMouseExit() { ec.onIdleAngle = false; }
}