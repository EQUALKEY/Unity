using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEdgeRange : MonoBehaviour {

    public GameObject EC;
    public GameObject BaseEdge_Range;

    private void OnMouseEnter()
    {
        BaseEdge_Range.SetActive(true);
        EC.GetComponent<EventController>().isOutofTriRange = false;
    }
    private void OnMouseExit()
    {
        BaseEdge_Range.SetActive(false) ;
        EC.GetComponent<EventController>().isOutofTriRange = true;
    }
}
