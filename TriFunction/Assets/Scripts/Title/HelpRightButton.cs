using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpRightButton : MonoBehaviour {
    public GameObject Help;
    private HelpButton h;

    void Start()
    {
        h = Help.GetComponent<HelpButton>();
    }

    void OnMouseDown()
    {
        h.NextPage();
    }
}
