using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    // GameOver 오브젝트
    public GameObject GameOverWindow;

    // 폭탄 오브젝트들
    public GameObject SinBoom;
    public GameObject SecBoom;
    public GameObject TanBoom;
    public GameObject CosBoom;
    public GameObject CosecBoom;
    public GameObject CotanBoom;

    // 삼각형 오브젝트
    public GameObject Tri;
    
    // Edge 효과들 - Activated, Effect, DeleteEffect
    public GameObject HypoActivated;
    public GameObject HeightActivated;
    public GameObject BaseActivated;

    public GameObject HypoEffect;
    public GameObject HeightEffect;
    public GameObject BaseEffect;

    public GameObject HypoDeleteEffect;
    public GameObject HeightDeleteEffect;
    public GameObject BaseDeleteEffect;

    // Circle 4가지
    public GameObject HypoIdleCircle;
    public GameObject HypoCoCircle;
    public GameObject HeightCircle;
    public GameObject BaseCircle;

    // 무기들, 무기들 Effect
    public GameObject BaseLineEffect;
    public GameObject HypoLineEffect;
    public GameObject Spear;
    public GameObject Bow;
    public GameObject Arrow;
    public GameObject Shield;

    public GameObject CoSpear;
    public GameObject CoBow;
    public GameObject CoArrow;
    public GameObject CoShield;

    public GameObject SpearEffect;
    public GameObject BowEffect;
    public GameObject ArrowEffect;
    public GameObject ShieldEffect;

    public GameObject CoSpearEffect;
    public GameObject CoBowEffect;
    public GameObject CoArrowEffect;
    public GameObject CoShieldEffect;

    // 각도, 각도 Effect들
    public GameObject IdleAngleEffect;
    public GameObject CoAngle;
    public GameObject CoAngleEffect;
    public GameObject CoAngleDeleteEffect;

    public bool isRotatePosible; // 회전 가능 / 불가능 확인
    public bool isRotating;      // bool형태로 삼각형 회전 중인지 아닌지 체크
    // int형태로 Tstate변수에 변활성화상태 저장
    public int Tstate;           // 활성화X = 0, Hypo = 1, Height = 2, Base = 3
    // bool형태로 isCo변수에 각도활성화상태 저장
    public bool isCo;            // 기본각 = false, Co각 = true;

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
        isRotating = false;
        isRotatePosible = true;
	}

    void OnMouseDown() {
        if (isRotatePosible)
        {   // TriRange 밖에서 마우스 누르면 회전 시작
            // 시작 시 삼각형 위치(깨다 기준), 회전 기록
            TriStartPosition = Tri.GetComponent<Transform>().position - CoR;
            TriStartRotation = Tri.GetComponent<Transform>().rotation;
            // 마우스 시작위치는 깨다 기준 상대적 위치
            MouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;
            isRotating = true;
        }
    }
    
    void OnMouseUp() {
        isRotating = false;
        isRotatePosible = false;
    }
	/*
	void Update () {
        if (isRotating)
        {
            MousePresentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;   // 마우스 현재위치는 깨다 기준 상대적 위치
            RotateAngle = Vector2.Angle(MouseStartPosition, MousePresentPosition);              // RotateAngle 계산
            // Vertor2.Angle은 항상 0<=Angle<180이므로 외적을 이용해서 반환
            if (Vector3.Cross(MouseStartPosition, MousePresentPosition).z < 0) RotateAngle = 360f - RotateAngle;
            // 회전 및 삼각형 중심 - 회전 중심 위치 조정
            Tri.GetComponent<Transform>().rotation = TriStartRotation * Quaternion.Euler(Vector3.forward * RotateAngle);
            Tri.GetComponent<Transform>().position = Quaternion.Euler(Vector3.forward * RotateAngle) * TriStartPosition + CoR;
        }
	}*/
}
