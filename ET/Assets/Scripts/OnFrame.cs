using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFrame : MonoBehaviour {

    public GameObject OnEffect;
    public GameObject OnCircle;
    public GameObject EventController;
    public GameObject Tri;
    Vector3 BaseScale;

    
    Line_State.Lstate thisLine;

    float f=0.0f;

    void Start()
    {
        if(this.gameObject.name == "Invisible_height")
        {
            thisLine = Line_State.Lstate.Height;
        }    
        else if(this.gameObject.name == "Invisible_base")
        {
            thisLine = Line_State.Lstate.Base;
        }
        else if(this.gameObject.name == "Invisible_hypotenuse")
        {
            thisLine = Line_State.Lstate.Hypotenuse;
        }
        Debug.Log(thisLine);
    }

    public void OnMouseEnter() // 선 위에 마우스를 올리면 이펙트 활성화
    {
        OnEffect.SetActive(true);
    }

    public void OnMouseExit() // 선 위에 마우스를 올렸다가 선 위를 벗어날 때 이펙트 비활성화
    {
        OnEffect.SetActive(false);    
    }
    public void OnMouseDown() // 선을 눌렀을때 그 선에 해당되는 원을 활성화(0에서부터 커짐), 다시 자기를 눌렀을 때는 인식 x. (*) 추가해야될 사항 : hypo에서 hei클릭하면 사인함수발생.
    {
        if (Tri.GetComponent<Line_State>().GetLstate().ToString().Equals("idle"))
        {
            Tri.GetComponent<Line_State>().SetLstate(thisLine);
            OnCircle.SetActive(true);

            BaseScale = OnCircle.transform.localScale;
            OnCircle.transform.localScale = new Vector3(0f, 0f, 0f);

            f = 0f;
            StartCoroutine("createCircle");
        }
    }
    IEnumerator createCircle()
    {
        if (f <= 1.05f)
        {
            OnCircle.transform.localScale = BaseScale * f;
            f += 0.05f;
            yield return new WaitForSeconds(0.01f);

            StartCoroutine("createCircle");
        }
    }

}
