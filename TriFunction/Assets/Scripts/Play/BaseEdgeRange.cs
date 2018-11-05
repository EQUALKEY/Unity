using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    private bool isCoBow, isCoShield;
    public GameObject CoBow;
    public GameObject CoArrow;
    public GameObject CoShield;

    void Awake() {
        ec = EC.GetComponent<EventController>();
        isCoBow = false;
        isCoShield = false;
    }

    private void OnMouseEnter()
    {
        ec.onBaseRange = true;
        if (!ec.isRotating)
        {
            switch (ec.Tstate)
            {
                case 0: // 활성화X
                    ec.BaseEffect.SetActive(true);
                    if (ec.isCo)
                    {
                        ec.IdleAngleEffect.SetActive(true);
                        ec.CoAngleDeleteEffect.SetActive(true);
                    }
                    break;
                case 1: // Hypo
                    if (ec.isCo)
                    {
                        ec.CoBowEffect.SetActive(true);
                        ec.HeightLineEffect.SetActive(true);
                    }
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
        ec.onBaseRange = false;
        ec.CoBowEffect.SetActive(false);
        ec.CoShieldEffect.SetActive(false);
        ec.BaseEffect.SetActive(false);
        ec.BaseDeleteEffect.SetActive(false);
        ec.IdleAngleEffect.SetActive(false);
        ec.CoAngleDeleteEffect.SetActive(false);
        if (!ec.isLaunching) ec.HeightLineEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        switch (ec.Tstate)
        {
            case 0: // 변활성화된게 없는경우
                ec.BaseActivated.SetActive(true);
                ec.BaseLength.SetActive(true);
                ec.BaseEffect.SetActive(false);
                ec.BaseDeleteEffect.SetActive(true);
                ec.Tstate = 3;
                ec.isCo = false;
                ec.CoAngle.SetActive(false);
                ec.CoAngleDeleteEffect.SetActive(false);
                ec.IdleAngleEffect.SetActive(false);
                ec.MakeCircle(ec.BaseCircle);
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
                    isCoBow = true;
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
                    isCoShield = true;
                }
                break;
            case 3: // Base 활성화시
                ec.BaseActivated.SetActive(false);
                ec.BaseLength.SetActive(false);
                ec.BaseEffect.SetActive(true);
                ec.BaseDeleteEffect.SetActive(false);
                ec.BaseCircle.SetActive(false);
                ec.Tstate = 0;
                break;
        }
    }

    void OnMouseUp()
    {
        if (isCoBow)
        {
            StartCoroutine("Shoot_CoBow");
            ec.SetAnimationParameters(0, 1);
            isCoBow = false;
        }

        if (isCoShield)
        {
            StartCoroutine("Keep_CoShield");
            ec.SetAnimationParameters(0, 1);
            isCoShield = false;
        }

        ec.RotateFinish();
    }

    IEnumerator Shoot_CoBow()
    {
        GameObject CoBowObject = Instantiate(CoBow, ec.CoBow.transform.position, ec.CoBow.transform.rotation);
        GameObject CoArrowObject = Instantiate(CoArrow, ec.CoArrow.transform.position, ec.CoArrow.transform.rotation);
        CoBowObject.transform.localScale *= 0.807f;
        CoArrowObject.transform.localScale *= 0.807f;

        yield return new WaitForSeconds(1f);
        Destroy(CoBowObject);
        yield return new WaitForSeconds(1f);
        Destroy(CoArrowObject);
    }

    IEnumerator Keep_CoShield()
    {
        GameObject CoShieldObject = Instantiate(CoShield, ec.CoShield.transform.position, ec.CoShield.transform.rotation);
        CoShieldObject.transform.localScale *= 1.365f;

        yield return new WaitForSeconds(2f);
        Destroy(CoShieldObject);
    }
}