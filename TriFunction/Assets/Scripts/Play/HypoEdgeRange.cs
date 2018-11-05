using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypoEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    private bool isSpear, isCoSpear;
    public GameObject Spear;
    public GameObject CoSpear;

    void Awake() {
        ec = EC.GetComponent<EventController>();
        isSpear = false;
        isCoSpear = false;
    }

    private void OnMouseEnter()
    {
        ec.onHypoRange = true;
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
        ec.onHypoRange = false;
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
                ec.Tri.transform.localScale = ec.Tri.transform.localScale * 0.807f;
                ec.HypoActivated.SetActive(true);
                ec.HypoLength.SetActive(true);
                ec.HypoEffect.SetActive(false);
                ec.HypoDeleteEffect.SetActive(true);
                ec.Tstate = 1;
                if (ec.isCo) ec.MakeCircle(ec.HypoCoCircle);
                else ec.MakeCircle(ec.HypoIdleCircle);
                break;
            case 1: // Hypo 활성화시
                ec.Tri.transform.localScale = ec.StandardScale;
                ec.HypoActivated.SetActive(false);
                ec.HypoLength.SetActive(false);
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
                    isCoSpear = true;
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
                    isSpear = true;
                }
                break;
        }
    }

    void OnMouseUp()
    {
        if (isSpear)
        {
            StartCoroutine("Shoot_Spear");
            ec.SetAnimationParameters(0, 1);
            isSpear = false;
        }

        if (isCoSpear)
        {
            StartCoroutine("Shoot_CoSpear");
            ec.SetAnimationParameters(0, 1);
            isCoSpear = false;
        }

        ec.RotateFinish();
    }

    IEnumerator Shoot_Spear()
    {
        GameObject SpearObject = Instantiate(Spear, ec.Spear.transform.position, ec.Spear.transform.rotation);
        
        yield return new WaitForSeconds(2f);
        Destroy(SpearObject);
    }

    IEnumerator Shoot_CoSpear()
    {
        GameObject CoSpearObject = Instantiate(CoSpear, ec.CoSpear.transform.position, ec.CoSpear.transform.rotation);
        CoSpearObject.transform.localScale *= 1.365f;

        yield return new WaitForSeconds(2f);
        Destroy(CoSpearObject);
    }
}