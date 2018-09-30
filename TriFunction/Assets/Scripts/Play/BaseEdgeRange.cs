using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEdgeRange : MonoBehaviour {

    public GameObject EC;
    public GameObject BaseEffect;
    public GameObject BaseDeleteEffect;
    private EventController ec;

    void Awake()
    {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        ec.isOutofTriRange = false;
        if (ec.Tstate == 3)
        {
            BaseDeleteEffect.SetActive(true);
        }
        else
        {
            BaseEffect.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        ec.isOutofTriRange = true;
        if (ec.Tstate == 3)
        {
            BaseDeleteEffect.SetActive(false);
        }
        else
        {
            BaseEffect.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        int Tstate = ec.Tstate;
        bool isCo = ec.isCo;
        if (Tstate == 0)
        { // 변활성화된게 없는경우
            BaseEffect.SetActive(true);
            ec.Tstate = 3;
        }
        else if (Tstate == 1)
        { // Hypo 활성화시

        }
        else if (Tstate == 2)
        { // Height 활성화시

        }
        else if (Tstate == 3)
        { // Base 활성화시
            BaseEffect.SetActive(false);
            BaseDeleteEffect.SetActive(false);
            ec.Tstate = 0;
        }
        else
        { // 예외시 ERROR
            Debug.Log("TriState out of range ERROR\n");
        }
    }
}
