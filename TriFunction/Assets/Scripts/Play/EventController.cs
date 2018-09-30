using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public GameObject Tri;
    public bool isOutofTriRange;
    // int형태로 Tstate변수에 변활성화상태 저장
    // 활성화X = 0, Hypo = 1, Height = 2, Base = 3
    public int Tstate;
    // bool형태로 isCo변수에 각도활성화상태 저장
    // 기본각 = false, Co각 = true;
    public bool isCo;
    // bool형태로 삼각형 회전 중인지 아닌지 체크
    public bool isClick;

    private Vector3 TriStartPosition;       // Tri Start Position
    private Quaternion TriStartRotation;    // Tri Start Rocation
    private Vector3 MouseStartPosition;     // 마우스 위치 - 클릭한 순간
    private Vector3 MousePresentPosition;   // 마우스 위치 - 현재
    private Vector3 CoR;                    // Center of Rotation
    private float RotateAngle;              // 회전각도

    void Awake () {
        CoR = new Vector3(0f, -0.15f, 0f);  // 깨다 위치
        Tstate = 0;
        isCo = false;
        isClick = false;
        isOutofTriRange = true;
	}

    void OnMouseDown() {
        if (isOutofTriRange)
        {   // TriRange 밖에서 마우스 누르면 회전 시작
            // 시작 시 삼각형 위치(깨다 기준), 회전 기록
            TriStartPosition = Tri.GetComponent<Transform>().position - CoR;
            TriStartRotation = Tri.GetComponent<Transform>().rotation;
            // 마우스 시작위치는 깨다 기준 상대적 위치
            MouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;
            isClick = true;
        }
    }
    
    void OnMouseUp() {
        isClick = false;
    }
	
	void Update () {
        if (isClick)
        {
            MousePresentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;   // 마우스 현재위치는 깨다 기준 상대적 위치
            RotateAngle = Vector2.Angle(MouseStartPosition, MousePresentPosition);              // RotateAngle 계산
            // Vertor2.Angle은 항상 0<=Angle<180이므로 외적을 이용해서 반환
            if (Vector3.Cross(MouseStartPosition, MousePresentPosition).z < 0) RotateAngle = 360f - RotateAngle;
            // 회전 및 삼각형 중심 - 회전 중심 위치 조정
            Tri.GetComponent<Transform>().rotation = TriStartRotation * Quaternion.Euler(Vector3.forward * RotateAngle);
            Tri.GetComponent<Transform>().position = Quaternion.Euler(Vector3.forward * RotateAngle) * TriStartPosition + CoR;
        }
	}
}
