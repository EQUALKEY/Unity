using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypoEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        int Tstate = ec.Tstate;
        bool isCo = ec.isCo;
        if (!ec.isRotating)
        {
            switch(Tstate)
            {
                case 0: // 활성화X
                    ec.HypoEffect.SetActive(true);
                    break;
                case 1: // Hypo
                    ec.HypoDeleteEffect.SetActive(true);
                    break;
                case 2: // Height
                    if(isCo)
                    {
                        ec.isRotatePosible = true;
                        ec.CoSpearEffect.SetActive(true);
                    }
                    break;
                case 3: // Base
                    ec.isRotatePosible = true;
                    ec.SpearEffect.SetActive(true);
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        if (!ec.isRotating)
        {
            ec.HypoEffect.SetActive(false);
            ec.HypoDeleteEffect.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        int Tstate = ec.Tstate;
        bool isCo = ec.isCo;
        switch(Tstate)
        {
            case 0: // 변활성화된게 없는경우
                ec.HypoActivated.SetActive(true);
                ec.HypoEffect.SetActive(false);
                ec.HypoDeleteEffect.SetActive(true);
                ec.Tstate = 1;
                break;
            case 1: // Hypo 활성화시
                ec.HypoActivated.SetActive(false);
                ec.HypoEffect.SetActive(true);
                ec.HypoDeleteEffect.SetActive(false);
                ec.Tstate = 0;
                break;
            case 2: // Height 활성화시
                if(isCo)
                {
                    ec.CoSpearEffect.SetActive(false);
                    ec.CoSpear.SetActive(true);
                    ec.isRotating = true;
                }
                break;
            case 3: // Base 활성화시
                ec.SpearEffect.SetActive(false);
                ec.Spear.SetActive(true);
                ec.isRotating = true;
                break;
        }
    }
}