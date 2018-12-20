using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() { ec = EC.GetComponent<EventController>(); }

    private void OnMouseEnter() { ec.onHeight = true; }

    private void OnMouseExit() { ec.onHeight = false; }
}
