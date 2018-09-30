using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightEdgeRange : MonoBehaviour {

    public GameObject EC;
    public GameObject HeightEdge_Range;

    private void OnMouseEnter()
    {
        HeightEdge_Range.SetActive(true);
        EC.GetComponent<EventController>().isOutofTriRange = false;
    }
    private void OnMouseExit()
    {
        HeightEdge_Range.SetActive(false);
        EC.GetComponent<EventController>().isOutofTriRange = true;
    }

}
