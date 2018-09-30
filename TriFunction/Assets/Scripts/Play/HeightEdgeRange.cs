using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightEdgeRange : MonoBehaviour {

    public GameObject EC;
    public GameObject HeightEffect;
    public GameObject HeightDeleteEffect;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        ec.isOutofTriRange = false;
        if (!ec.isClick)
        {
            if (ec.Tstate == 2)
            {
                HeightDeleteEffect.SetActive(true);
            }
            else
            {
                HeightEffect.SetActive(true);
            }
        }
    }

    private void OnMouseExit()
    {
        ec.isOutofTriRange = true;
        HeightDeleteEffect.SetActive(false);
        if (ec.Tstate != 2) HeightEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        int Tstate = ec.Tstate;
        bool isCo = ec.isCo;
        if (Tstate == 0)
        { // 변활성화된게 없는경우
            HeightEffect.SetActive(true);
            ec.Tstate = 2;
        }
        else if (Tstate == 1)
        { // Hypo 활성화시

        }
        else if (Tstate == 2)
        { // Height 활성화시
            HeightEffect.SetActive(false);
            HeightDeleteEffect.SetActive(false);
            ec.Tstate = 0;
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
