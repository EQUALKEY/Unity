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
        if (!ec.isRotating)
        {
            switch(ec.Tstate)
            {
                case 0: // 활성화X
                    ec.HypoEffect.SetActive(true);
                    break;
                case 1: // Hypo
                    ec.HypoDeleteEffect.SetActive(true);
                    break;
                case 2: // Height
                    if (ec.isCo)
                    {
                        ec.CoSpearEffect.SetActive(true);
                        ec.HypoCoLineEffect.SetActive(true);
                    }
                    break;
                case 3: // Base
                    if (!ec.isCo)
                    {
                        ec.SpearEffect.SetActive(true);
                        ec.HypoIdleLineEffect.SetActive(true);
                    }
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        ec.SpearEffect.SetActive(false);
        ec.CoSpearEffect.SetActive(false);
        ec.HypoEffect.SetActive(false);
        ec.HypoDeleteEffect.SetActive(false);
        if (!ec.isLaunching)
        {
            ec.HypoIdleLineEffect.SetActive(false);
            ec.HypoCoLineEffect.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        switch(ec.Tstate)
        {
            case 0: // 변활성화된게 없는경우
                ec.HypoActivated.SetActive(true);
                ec.HypoEffect.SetActive(false);
                ec.HypoDeleteEffect.SetActive(true);
                ec.Tstate = 1;
                if (ec.isCo) ec.MakeCircle(ec.HypoCoCircle);
                else ec.MakeCircle(ec.HypoIdleCircle);
                break;
            case 1: // Hypo 활성화시
                ec.HypoActivated.SetActive(false);
                ec.HypoEffect.SetActive(true);
                ec.HypoDeleteEffect.SetActive(false);
                ec.HypoCoCircle.SetActive(false);
                ec.HypoIdleCircle.SetActive(false);
                ec.Tstate = 0;
                break;
            case 2: // Height 활성화시
                if(ec.isCo)
                {
                    ec.CoSpearEffect.SetActive(false);
                    ec.CoSpear.SetActive(true);
                    ec.isRotating = true;
                    ec.isLaunching = true;
                    ec.RotateInit();
                }
                break;
            case 3: // Base 활성화시
                if(!ec.isCo)
                {
                    ec.SpearEffect.SetActive(false);
                    ec.Spear.SetActive(true);
                    ec.isRotating = true;
                    ec.isLaunching = true;
                    ec.RotateInit();
                }
                break;
        }
    }

    void OnMouseUp()
    {
        ec.RotateFinish();
    }
}