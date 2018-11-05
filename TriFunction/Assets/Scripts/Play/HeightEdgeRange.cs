using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    private bool isBow, isShield;
    public GameObject Bow;
    public GameObject Arrow;
    public GameObject Shield;

    void Awake() {
        ec = EC.GetComponent<EventController>();
        isBow = false;
        isShield = false;
    }

    private void OnMouseEnter()
    {
        ec.onHeightRange = true;
        if (!ec.isRotating)
        {
            switch (ec.Tstate)
            {
                case 0:
                    ec.HeightEffect.SetActive(true);
                    if (!ec.isCo) ec.CoAngleEffect.SetActive(true);
                    break;
                case 1:
                    if (!ec.isCo)
                    {
                        ec.BowEffect.SetActive(true);
                        ec.BaseLineEffect.SetActive(true);
                    }
                    break;
                case 2:
                    ec.HeightDeleteEffect.SetActive(true);
                    break;
                case 3:
                    if (!ec.isCo) ec.ShieldEffect.SetActive(true);
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        ec.onHeightRange = false;
        ec.BowEffect.SetActive(false);
        ec.ShieldEffect.SetActive(false);
        ec.HeightDeleteEffect.SetActive(false);
        ec.HeightEffect.SetActive(false);
        ec.CoAngleEffect.SetActive(false);
        if (!ec.isLaunching) ec.BaseLineEffect.SetActive(false);
    }

    private void OnMouseDown()
    {
        switch (ec.Tstate)
        {
            case 0: // 변활성화된게 없는경우
                ec.Tri.transform.localScale = ec.Tri.transform.localScale * 1.365f;
                ec.HeightActivated.SetActive(true);
                ec.HeightLength.SetActive(true);
                ec.HeightEffect.SetActive(false);
                ec.HeightDeleteEffect.SetActive(true);
                ec.Tstate = 2;
                ec.isCo = true;
                ec.CoAngle.SetActive(true);
                ec.CoAngleEffect.SetActive(false);
                ec.MakeCircle(ec.HeightCircle);
                break;
            case 1: // Hypo 활성화시
                if(!ec.isCo)
                {
                    ec.BowEffect.SetActive(false);
                    ec.Bow.SetActive(true);
                    ec.Arrow.SetActive(true);
                    ec.isLaunching = true;
                    ec.isRotating = true;
                    ec.RotateInit();
                    isBow = true;
                }
                break;
            case 2: // Height 활성화시
                ec.Tri.transform.localScale = ec.StandardScale;
                ec.HeightActivated.SetActive(false);
                ec.HeightLength.SetActive(false);
                ec.HeightEffect.SetActive(true);
                ec.HeightDeleteEffect.SetActive(false);
                ec.HeightCircle.SetActive(false);
                ec.Tstate = 0;
                break;
            case 3: // Base 활성화시
                if(!ec.isCo)
                {
                    ec.ShieldEffect.SetActive(false);
                    ec.Shield.SetActive(true);
                    ec.isLaunching = true;
                    ec.isRotating = true;
                    ec.RotateInit();
                    isShield = true;
                }
                break;
        }
    }

    void OnMouseUp()
    {
        if (isBow)
        {
            StartCoroutine("Shoot_Bow");
            ec.SetAnimationParameters(0, 1);
            isBow = false;
        }

        if (isShield)
        {
            StartCoroutine("Keep_Shield");
            ec.SetAnimationParameters(0, 1);
            isShield = false;
        }

        ec.RotateFinish();
    }

    IEnumerator Shoot_Bow()
    {
        GameObject BowObject = Instantiate(Bow, ec.Bow.transform.position, ec.Bow.transform.rotation);
        GameObject ArrowObject = Instantiate(Arrow, ec.Arrow.transform.position, ec.Arrow.transform.rotation);
        BowObject.transform.localScale *= 0.807f;
        ArrowObject.transform.localScale *= 0.807f;

        yield return new WaitForSeconds(1f);
        Destroy(BowObject);
        yield return new WaitForSeconds(1f);
        Destroy(ArrowObject);
    }

    IEnumerator Keep_Shield()
    {
        GameObject ShieldObject = Instantiate(Shield, ec.Shield.transform.position, ec.Shield.transform.rotation);

        yield return new WaitForSeconds(2f);
        Destroy(ShieldObject);
    }
}
