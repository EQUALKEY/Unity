using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAngleRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() { ec = EC.GetComponent<EventController>(); }

    private void OnMouseEnter() { ec.onCoAngle = true; }

    private void OnMouseExit() { ec.onCoAngle = false; }
}