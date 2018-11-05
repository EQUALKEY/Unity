using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() { ec = EC.GetComponent<EventController>(); }

    private void OnMouseEnter() { ec.onBase = true; }

    private void OnMouseExit() { ec.onBase = false; }
}