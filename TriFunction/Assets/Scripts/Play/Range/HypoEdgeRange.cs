using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypoEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() { ec = EC.GetComponent<EventController>(); }

    private void OnMouseEnter() { ec.onHypo = true; }

    private void OnMouseExit() { ec.onHypo = false; }
}