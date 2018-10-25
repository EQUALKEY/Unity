﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    // GameOver 오브젝트
    public GameObject GameOverWindow;

    // 깨다
    public GameObject Character;
    private Animator Character_Animator;

    // 폭탄 오브젝트들
    public GameObject SinBoom;
    public GameObject SecBoom;
    public GameObject TanBoom;
    public GameObject CosBoom;
    public GameObject CosecBoom;
    public GameObject CotanBoom;

    // 삼각형 오브젝트
    public GameObject Tri;
    
    // Edge 효과들 - Activated, Effect, DeleteEffect, Length
    public GameObject HypoActivated;
    public GameObject HeightActivated;
    public GameObject BaseActivated;

    public GameObject HypoEffect;
    public GameObject HeightEffect;
    public GameObject BaseEffect;

    public GameObject HypoDeleteEffect;
    public GameObject HeightDeleteEffect;
    public GameObject BaseDeleteEffect;

    public GameObject HypoLength;
    public GameObject HeightLength;
    public GameObject BaseLength;

    // Circle 4가지
    public GameObject HypoIdleCircle;
    public GameObject HypoCoCircle;
    public GameObject HeightCircle;
    public GameObject BaseCircle;

    // 조준선 Effect
    public GameObject HypoIdleLineEffect;
    public GameObject HypoCoLineEffect;
    public GameObject BaseLineEffect;
    public GameObject HeightLineEffect;

    // 무기들, 무기들 Effect
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

    // 필살기
    public GameObject SkillButton;

    public bool SkillReady;      // 필살기 사용 가능여부 체크
    public int SkillGauge;       // 필살기 게이지, 0-20까지

    public bool isMonsterTypeOn; // 몬스터 타입이 켜져있는지 꺼져있는지 체크
    public bool isRotating;      // bool형태로 삼각형 회전 중인지 아닌지 체크
    public bool isLaunching;     // 무기 발사 중인지 체크
    // int형태로 Tstate변수에 변활성화상태 저장
    public int Tstate;           // 활성화X = 0, Hypo = 1, Height = 2, Base = 3
    // bool형태로 isCo변수에 각도활성화상태 저장
    public bool isCo;            // 기본각 = false, Co각 = true;

    // 원 생성을 위한 변수들
    private float f;                        // 원 크기 조절을 위한 실수
    private Vector3 BaseScale;              // 초기 원 크기

    // 삼각형 회전을 위한 변수들
    private Vector3 TriStartPosition;       // Tri Start Position
    private Quaternion TriStartRotation;    // Tri Start Rotation
    private Quaternion HypoLengthStartRotation;  // HypoLength Start Rotation
    private Quaternion HeightLengthStartRotation;// HeightLength Start Rotation
    private Quaternion BaseLengthStartRotation;  // BaseLength Start Rotation
    private Vector3 MouseStartPosition;     // 마우스 위치 - 클릭한 순간
    private Vector3 MousePresentPosition;   // 마우스 위치 - 현재
    private Vector3 CoR;                    // Center of Rotation
    private float RotateAngle;              // 회전각도

    public Vector3 StandardScale;


    // 현재 시각
    public float current_Time;
    public Text TimeText;

    // 점수
    public int Score;
    public Text ScoreText;

    // 목숨
    public GameObject[] LifeOn = new GameObject[3];
    public GameObject[] LifeOff = new GameObject[3];
    public int Lifes;

    // 필살기
    public GameObject UltiBar;
    public GameObject UltiGage;
    public int UltimateGage;

    // 초기화
    void Awake () {
        CoR = new Vector3(0f, 0f, 0f);  // 깨다 위치
        Tstate = 0;
        isCo = false;
        isLaunching = false;
        isRotating = false;
        UltimateGage = 0;
        Lifes = 3;
        Score = 0;

        Character_Animator = Character.GetComponent<Animator>();
        Character_Animator.SetInteger("Quebon_state", 0);

        SkillReady = false;
        SkillGauge = 0;

        StandardScale = Tri.transform.localScale;

        if (PlayerPrefs.GetInt("isMonsterTypeOff") == 1) isMonsterTypeOn = false;
        else isMonsterTypeOn = true;

        StartTime();
	}
    
    // 회전 초기화
    public void RotateInit()
    {
        // TriRange 밖에서 마우스 누르면 회전 시작
        // 시작 시 삼각형 위치(깨다 기준), 회전 기록
        TriStartPosition = Tri.transform.position - CoR;
        TriStartRotation = Tri.transform.rotation;
        // 마우스 시작위치는 깨다 기준 상대적 위치
        MouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;
    }

    // 회전
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isCo)
            {
                CoAngle.SetActive(false);
                isCo = false;
            }
            else
            {
                CoAngle.SetActive(true);
                isCo = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isRotating = true;
            isLaunching = true;
            RotateFinish();
        }

        // 클릭하면 회전시작
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isRotating = true;
            RotateInit();
        }

        // 마우스 떼면 회전종료
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            RotateFinish();
        }

        if (isRotating)
        {
            MousePresentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;   // 마우스 현재위치는 깨다 기준 상대적 위치
            RotateAngle = Vector2.Angle(MouseStartPosition, MousePresentPosition);              // RotateAngle 계산
            // Vertor2.Angle은 항상 0<=Angle<180이므로 외적을 이용해서 반환
            if (Vector3.Cross(MouseStartPosition, MousePresentPosition).z < 0) RotateAngle = 360f - RotateAngle;
            // 회전 및 삼각형 중심 - 회전 중심 위치 조정
            Tri.transform.rotation = TriStartRotation * Quaternion.Euler(Vector3.forward * RotateAngle);
            HypoLength.transform.rotation = Quaternion.Euler(-(Tri.transform.rotation * Vector3.forward));
            HeightLength.transform.rotation = Quaternion.Euler(-(Tri.transform.rotation * Vector3.forward));
            BaseLength.transform.rotation = Quaternion.Euler(-(Tri.transform.rotation * Vector3.forward));

            Tri.transform.position = Quaternion.Euler(Vector3.forward * RotateAngle) * TriStartPosition + CoR;
        }
	}

    // 회전 종료, 무기 발사 시 전부 다 끄고 아니면 활성화변들과 원은 놔둠
    public void RotateFinish()
    {
        if (isRotating)
        {
            isRotating = false;
            Spear.SetActive(false);
            Bow.SetActive(false);
            Arrow.SetActive(false);
            Shield.SetActive(false);
            CoSpear.SetActive(false);
            CoBow.SetActive(false);
            CoArrow.SetActive(false);
            CoShield.SetActive(false);
            if (isLaunching)
            {
                Tri.transform.localScale = StandardScale;
                isLaunching = false;
                isCo = false;
                CoAngle.SetActive(false);

                HypoActivated.SetActive(false);
                HypoLength.SetActive(false);
                HypoIdleCircle.SetActive(false);
                HypoCoCircle.SetActive(false);
                HypoIdleLineEffect.SetActive(false);
                HypoCoLineEffect.SetActive(false);

                HeightActivated.SetActive(false);
                HeightLength.SetActive(false);
                HeightCircle.SetActive(false);
                HeightLineEffect.SetActive(false);

                BaseActivated.SetActive(false);
                BaseLength.SetActive(false);
                BaseCircle.SetActive(false);
                BaseLineEffect.SetActive(false);

                Tstate = 0;
            }
        }
    }

    // 원 생성, 생성할 원을 인자로 받아옴
    public void MakeCircle(GameObject Circle)
    {
        Circle.SetActive(true);
        Circle.transform.localScale = new Vector3(0f, 0f, 0f);
        f = 0f;
        StartCoroutine("createCircle", Circle);
    }

    IEnumerator createCircle(GameObject Circle)
    {
        if (f <= 1.05f)
        {
            Circle.transform.localScale = new Vector3(f, f, 1f);
            f += 0.1f;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine("createCircle", Circle);
        }
    }

    public void StartTime() // 시간 초기화 및 Timer()함수 실행
    {
        current_Time = 0f;
        StartCoroutine("Timer");
    }

    IEnumerator Timer() // 0.01초 단위로 시간을 측정
    {
        yield return new WaitForSeconds(0.01f);
        current_Time += 0.01f;
        TimeText.text = current_Time.ToString("##0.00") + " sec";
        StartCoroutine("Timer");
    }

    public void GetScore(int num)
    {
        Score += num;
        ScoreText.text = Score.ToString("0") + " 마리";
    }


    public void GetSkillGauge(int num)
    {
        if (!SkillReady) SkillGauge += num;
        if (SkillGauge >= 20)
        {
            SkillButton.SetActive(true);
            SkillReady = true;
            SkillGauge = 0;
        }
    }

    public void SetAnimationParameters(int NumOfAnimator, int state)
    {
        switch(NumOfAnimator) 
        {
            case 0: // Character_Animator , Quebon_state ( 0 : idle , 1 : attack , 2 : damaged )
                Character_Animator.SetInteger("Quebon_state", state);
                StopCoroutine("EndAnimation");
                StartCoroutine("EndAnimation", NumOfAnimator);
                break;
        }
    }

    IEnumerator EndAnimation(int NumOfAnimator)
    {
        if (NumOfAnimator == 0)
        {
            yield return new WaitForSeconds(1f);
            Character_Animator.SetInteger("Quebon_state", 0);
        }

    }

    public void LostLife() // Life를 잃는 것을 처리해주는 함수, 적이 몸에 닿을 시 실행
    {
        GetComponent<AudioManager>().DamagedSound();
        switch (Lifes)
        {
            case 3:  // 3개면 2개로
                LifeOn[2].SetActive(false);
                LifeOff[2].SetActive(true);
                Lifes--;
                break;
            case 2: // 2개면 1개로
                LifeOn[1].SetActive(false);
                LifeOff[1].SetActive(true);
                Lifes--;
                break;
            case 1: // 1개면 게임오버
                LifeOn[0].SetActive(false);
                LifeOff[0].SetActive(true);
                Lifes--;
                GameOver();
                GetComponent<AudioManager>().GameOverSound();
                break;
        }
    }

    public void GameOver() // GameOver 시 해야될 일을 해주는 함수
    {
        StopCoroutine("Timer");
        GameOverWindow.SetActive(true);
        GameOverWindow.transform.Translate(new Vector3(0f, 0f, 0.01f));
        if (!isMonsterTypeOn) PlayerPrefs.SetInt("isMonsterTypeOff", 1);
    }
}
