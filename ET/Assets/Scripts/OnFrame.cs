using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFrame : MonoBehaviour {

    public GameObject OnEffect;
    public GameObject OnCircle;
    public GameObject OnCircle_co;
    public GameObject EventController;
    public GameObject Tri;
    public GameObject Enemy;
    public GameObject GameOver;

    private Vector3 BaseScale;
    private string name;
    private Line_State.Lstate clickedLine;
    private Line_State.Lstate preLine;
    private Tri_State.Tstate thisTri;
    private float f;

    void Start() // name, clickedLine에 this 정보 저장
    {
        name = this.gameObject.name;
        if(name == "Invisible_height")
            clickedLine = Line_State.Lstate.Height;
        else if(name == "Invisible_base")
            clickedLine = Line_State.Lstate.Base;
        else if(name == "Invisible_hypotenuse")
            clickedLine = Line_State.Lstate.Hypotenuse;
        Debug.Log(clickedLine);
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
        preLine = Tri.GetComponent<Line_State>().GetLstate();

        // Line이 활성화되지 않은 경우 (Lstate = idle)에만 실행
        if (preLine == Line_State.Lstate.idle) {
            Tri.GetComponent<Line_State>().SetLstate(clickedLine); // Line 활성화

            // TriState가 idle인 경우 base, hypo를 클릭했을 때 Circle 생성
            if (thisTri == Tri_State.Tstate.idle && (name == "Invisible_base" || name == "Invisible_hypotenuse")) {
                CircleInit(OnCircle);    
                f = 0f;
                StartCoroutine("createCircle");
            }
            // TriState가 co인 경우 base가 height로 바뀌고 따라서 height, hypo를 클릭했을 때 Circle 생성
            else if (thisTri == Tri_State.Tstate.co && (name == "Invisible_height" || name == "Invisible_hypotenuse")) {
                CircleInit(OnCircle_co);    
                f = 0f;
                StartCoroutine("createCircle");
            }
        } else Attack(thisTri, preLine, clickedLine);
    }

    private void Attack(Tri_State.Tstate t, Line_State.Lstate pl, Line_State.Lstate cl) {
        if (t == Tri_State.Tstate.idle) {                                                              // 삼각형이 idle 상태인 경우
            if (pl == Line_State.Lstate.Base) {                                                        // Base가 활성화 시
                if (cl == Line_State.Lstate.Hypotenuse) Enemy.GetComponent<RandomAttack>().Anwser(1);  // idle 상태에서 Base - Hypo 순서는 sec, index는 1
                else if (cl == Line_State.Lstate.Height) Enemy.GetComponent<RandomAttack>().Anwser(2); // idle 상태에서 Base - Height 순서는 tan, index는 2
                else GameOver.SetActive(true);                                                         // idle 상태에서 Base - Base 순서는 GameOver
            } else if (pl == Line_State.Lstate.Hypotenuse) {                                                 // Hypo가 활성화 시
                if (cl == Line_State.Lstate.Height) Enemy.GetComponent<RandomAttack>().Anwser(0);      // idle 상태에서 Hypo - Height 순서는 sin, index는 0
                else GameOver.SetActive(true);                                                         // idle 상태에서 Hypo 다음에 Height 안누르면 GamaOver
            } else if (pl == Line_State.Lstate.Height)                                                 // Height가 활성화시
                GameOver.SetActive(true);                                                              // GameOver
        }
        else if (thisTri == Tri_State.Tstate.co) { // 삼각형이 co 상태인 경우
            if (pl == Line_State.Lstate.Base) GameOver.SetActive(true);                                // Base가 활성화시
            else if (pl == Line_State.Lstate.Hypotenuse) {                                             // GameOver
                if (cl == Line_State.Lstate.Base) Enemy.GetComponent<RandomAttack>().Anwser(3);      // co 상태에서 Hypo - Base 순서는 cos, index는 3
                else GameOver.SetActive(true);                                                         // co 상태에서 Hypo 다음에 Base 안누르면 GameOver
            } else if (pl == Line_State.Lstate.Height) {                                               // Height가 활성화시
                if (cl == Line_State.Lstate.Base) Enemy.GetComponent<RandomAttack>().Anwser(5);        // co 상태에서 Height - Base 순서는 cotan, index는 5
                else if (cl == Line_State.Lstate.Hypotenuse) Enemy.GetComponent<RandomAttack>().Anwser(4);   // co 상태에서 Height - Hypo 순서는 cosec, index는 4
                else GameOver.SetActive(true);                                                         // co 상태에서 Height - Height 순서는 GameOver
            }
        }
    }

    IEnumerator createCircle() // Circle 생성
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
