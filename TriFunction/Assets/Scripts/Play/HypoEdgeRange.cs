using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypoEdgeRange : MonoBehaviour {

    public GameObject EC;
    public GameObject HypoEdge_Range;

    private void OnMouseEnter()
    {
        HypoEdge_Range.SetActive(true);
        EC.GetComponent<EventController>().isOutofTriRange = false;
    }
    private void OnMouseExit()
    {
        HypoEdge_Range.SetActive(false);
        EC.GetComponent<EventController>().isOutofTriRange = true;
    }

}
