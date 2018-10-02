using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEdgeRange : MonoBehaviour
{

    public GameObject EC;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        if (!ec.isRotating)
        {
            switch (ec.Tstate)
            {
                case 0: // 활성화X
                    ec.BaseEffect.SetActive(true);
                    break;
                case 1: // Hypo
                    if (ec.isCo) ec.CoBowEffect.SetActive(true);
                    break;
                case 2: // Height
                    if (ec.isCo) ec.CoShieldEffect.SetActive(true);
                    break;
                case 3: // Base
                    ec.BaseDeleteEffect.SetActive(true);
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        ec.CoBowEffect.SetActive(false);
        ec.CoShieldEffect.SetActive(false);
        ec.BaseEffect.SetActive(false);
        ec.BaseDeleteEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        switch (ec.Tstate)
        {
            case 0: // 변활성화된게 없는경우
                ec.BaseActivated.SetActive(true);
                ec.BaseEffect.SetActive(false);
                ec.BaseDeleteEffect.SetActive(true);
                ec.Tstate = 3;
                break;
            case 1: // Hypo 활성화시
                if (ec.isCo)
                {
                    ec.CoBowEffect.SetActive(false);
                    ec.CoBow.SetActive(true);
                    ec.CoArrow.SetActive(true);
                    ec.isLaunching = true;
                    ec.isRotating = true;
                    ec.RotateInit();
                }
                break;
            case 2: // Height 활성화시
                if (ec.isCo)
                {
                    ec.CoShieldEffect.SetActive(false);
                    ec.CoShield.SetActive(true);
                    ec.isLaunching = true;
                    ec.isRotating = true;
                    ec.RotateInit();
                }
                break;
            case 3: // Base 활성화시
                ec.BaseActivated.SetActive(false);
                ec.BaseEffect.SetActive(true);
                ec.BaseDeleteEffect.SetActive(false);
                ec.Tstate = 0;
                break;
        }
    }

    void OnMouseUp()
    {
        ec.RotateFinish();
    }
}