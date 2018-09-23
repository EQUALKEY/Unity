using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFrame : MonoBehaviour {

    public GameObject OnEffect;
    public GameObject OnCircle;
    public GameObject OnCircle_co;
    public GameObject EventController;
    public GameObject Tri;

    private Vector3 BaseScale;
    private string name;
    private Line_State.Lstate thisLine;
    private Tri_State.Tstate thisTri;
    private float f;

    void Start()
    {
        name = this.gameObject.name;
        if(name == "Invisible_height")
            thisLine = Line_State.Lstate.Height;
        else if(name == "Invisible_base")
            thisLine = Line_State.Lstate.Base;
        else if(name == "Invisible_hypotenuse")
            thisLine = Line_State.Lstate.Hypotenuse;
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

    private void CircleInit(GameObject Circle) { // OnCircle과 OnCircle_co 생성 전 활성화 및 초기화
        Circle.SetActive(true);
        BaseScale = Circle.transform.localScale;
        Circle.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void OnMouseDown() // 선을 눌렀을때 그 선에 해당되는 원을 활성화(0에서부터 커짐), 다시 자기를 눌렀을 때는 인식 x.
                              //(*) 추가해야될 사항 : hypo에서 hei클릭하면 사인함수발생.
    {
        thisTri = Tri.GetComponent<Tri_State>().GetTstate();

        // Line이 활성화되지 않은 경우 (Lstate = idle)에만 실행
        if (Tri.GetComponent<Line_State>().GetLstate() == Line_State.Lstate.idle)
        {
            Tri.GetComponent<Line_State>().SetLstate(thisLine); // Line 활성화

            // TriState가 idle인 경우 base, hypo를 클릭했을 때 Circle 생성
            if (thisTri == Tri_State.Tstate.idle && (name == "Invisible_base" || name == "Invisible_hypotenuse"))
            {
                CircleInit(OnCircle);    
                f = 0f;
                StartCoroutine("createCircle");
            }
            // TriState가 co인 경우 base가 height로 바뀌고 따라서 height, hypo를 클릭했을 때 Circle 생성
            else if(thisTri == Tri_State.Tstate.co && (name == "Invisible_height" || name == "Invisible_hypotenuse"))
            {
                CircleInit(OnCircle_co);    
                f = 0f;
                StartCoroutine("createCircle");
            }
        }
    }
    IEnumerator createCircle()
    {
        if (f <= 1.05f)
        {
            // TriState에 따라 OnCircle 또는 OnCircle_co를 만듦
            if (thisTri == Tri_State.Tstate.idle) OnCircle.transform.localScale = BaseScale * f;
            else if (thisTri == Tri_State.Tstate.co) OnCircle_co.transform.localScale = BaseScale * f;
            f += 0.1f;
            yield return new WaitForSeconds(0.01f);

            StartCoroutine("createCircle");
        }
    }

}
