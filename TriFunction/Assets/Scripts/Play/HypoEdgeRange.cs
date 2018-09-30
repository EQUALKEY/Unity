using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypoEdgeRange : MonoBehaviour {

    public GameObject EC;
    public GameObject HypoEffect;
    public GameObject HypoDeleteEffect;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        ec.isOutofTriRange = false;
        if (!ec.isClick)
        {
            if (ec.Tstate == 1)
            {
                HypoDeleteEffect.SetActive(true);
            }
            else
            {
                HypoEffect.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        ec.isOutofTriRange = true;
        HypoDeleteEffect.SetActive(false);
        if (ec.Tstate != 1) HypoEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        int Tstate = ec.Tstate;
        bool isCo = ec.isCo;
        if (Tstate == 0)
        { // 변활성화된게 없는경우
            HypoEffect.SetActive(true);
            ec.Tstate = 1;
        }
        else if (Tstate == 1)
        { // Hypo 활성화시
            HypoEffect.SetActive(false);
            HypoDeleteEffect.SetActive(false);
            ec.Tstate = 0;
        }
        else if (Tstate == 2)
        { // Height 활성화시

        }
        else if (Tstate == 3)
        { // Base 활성화시

        }
        else
        { // 예외시 ERROR
            Debug.Log("TriState out of range ERROR\n");
        }
    }
}