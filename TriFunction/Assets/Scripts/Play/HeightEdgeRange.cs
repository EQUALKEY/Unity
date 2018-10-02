using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightEdgeRange : MonoBehaviour {

    public GameObject EC;
    private EventController ec;

    void Awake() {
        ec = EC.GetComponent<EventController>();
    }

    private void OnMouseEnter()
    {
        int Tstate = ec.Tstate;
        if (!ec.isRotating)
        {
            switch(Tstate)
            {
                case 0:
                    ec.HeightEffect.SetActive(true);
                    break;
                case 1:
                    ec.BowEffect.SetActive(true);
                    break;
                case 2:
                    ec.HeightDeleteEffect.SetActive(true);
                    break;
                case 3:
                    ec.Shield.SetActive(true);
                    break;
            }
        }
    }

    private void OnMouseExit()
    {
        if(!ec.isRotating) { 
            ec.HeightDeleteEffect.SetActive(false);
            ec.HeightEffect.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        int Tstate = ec.Tstate;
        bool isCo = ec.isCo;
        switch (Tstate)
        {
            case 0: // 변활성화된게 없는경우
                ec.HeightActivated.SetActive(true);
                ec.HeightEffect.SetActive(false);
                ec.HeightDeleteEffect.SetActive(true);
                ec.Tstate = 2;
                break;
            case 1: // Hypo 활성화시
                ec.BowEffect.SetActive(false);
                ec.Bow.SetActive(true);
                ec.Arrow.SetActive(true);
                ec.isRotating = true;
                break;
            case 2: // Height 활성화시
                ec.HeightActivated.SetActive(false);
                ec.HeightEffect.SetActive(true);
                ec.HeightDeleteEffect.SetActive(false);
                ec.Tstate = 0;
                break;
            case 3: // Base 활성화시
                ec.ShieldEffect.SetActive(false);
                ec.Shield.SetActive(true);
                ec.isRotating = true;
                break;
        }
    }
}
