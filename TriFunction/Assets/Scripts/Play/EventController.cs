using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour {

    // GameOver창 전체 (부모), GameOver 배경, Clear 배경
    // 랭크버튼 (GameOver시 시작화면-재시작-랭크), 무한모드버튼 (Clear시 시작화면-재시작-무한모드)
    public GameObject GameOverWindow, GameOverBack, ClearBack, RankButton, InfinityModeButton;

    // 깨다
    public GameObject Character;
    private Animator Character_Animator;

    // 폭탄 오브젝트들
    public GameObject SinBoom, SecBoom, TanBoom, CosBoom, CosecBoom, CotanBoom;

    // 삼각형 오브젝트
    public GameObject Tri;
    
    // Edge 효과들 - Activated, Effect, DeleteEffect, Length
    public GameObject HypoActivated, HeightActivated, BaseActivated;
    public GameObject HypoEffect, HeightEffect, BaseEffect;
    public GameObject HypoDeleteEffect, HeightDeleteEffect, BaseDeleteEffect;
    public GameObject HypoLength, HeightLength, BaseLength;

    // Circle 4가지
    public GameObject HypoIdleCircle, HypoCoCircle, HeightCircle, BaseCircle;

    // 조준선 Effect
    public GameObject HypoIdleLineEffect, HypoCoLineEffect, BaseLineEffect, HeightLineEffect;

    // 무기들, 무기들 Prefab, 무기들 Effect
    public GameObject Spear, Bow, Arrow, Shield;
    public GameObject CoSpear, CoBow, CoArrow, CoShield;

    public GameObject SpearPrefab, BowPrefab, ArrowPrefab, ShieldPrefab;
    public GameObject CoSpearPrefab, CoBowPrefab, CoArrowPrefab, CoShieldPrefab;

    public GameObject SpearEffect, BowEffect, ArrowEffect, ShieldEffect;
    public GameObject CoSpearEffect, CoBowEffect, CoArrowEffect, CoShieldEffect;

    // 각도, 각도 Effect들
    public GameObject IdleAngleEffect, CoAngle, CoAngleEffect, CoAngleDeleteEffect;

    // 필살기
    public GameObject SkillButton;

    public bool SkillReady;      // 필살기 사용 가능여부 체크
    public int SkillGauge;       // 필살기 게이지, 0-20까지

    public bool isMonsterTypeOn; // 몬스터 타입이 켜져있는지 꺼져있는지 체크
    public bool isRotating;      // bool형태로 삼각형 회전 중인지 아닌지 체크
    public int isLaunching;      // 어떤 무기 발사 중인지. 0: 발사X, 1 ~ 6: sin, sec, tan, cos, cosec, cotan
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

    // 마우스 위치 추적
    public bool onHypo, onHeight, onBase, onCoAngle, onIdleAngle;

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
        isLaunching = 0;
        isRotating = false;
        UltimateGage = 0;
        Lifes = 3;
        Score = 0;

        Character_Animator = Character.GetComponent<Animator>();
        Character_Animator.SetInteger("Quebon_state", 0);

        SkillReady = false;
        SkillGauge = 0;

        StandardScale = Tri.transform.localScale;

        onHypo = false;
        onHeight = false;
        onBase = false;
        onCoAngle = false;
        onIdleAngle = false;

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

	void Update () {
        // 좌클릭하면 회전시작
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isRotating = true;
            RotateInit();
        }

        // 변, 각, 무기 Effect
        ClearWeaponEffect();
        if (!isRotating) {
            ClearTri();
            switch (Tstate) {
                case 0:
                    if (onHypo) HypoEffect.SetActive(true);
                    else if (onHeight && isCo) HeightEffect.SetActive(true);
                    else if (onBase && !isCo) BaseEffect.SetActive(true);
                    break;
                case 1:
                    if (onHypo) HypoDeleteEffect.SetActive(true);
                    else if (onHeight && !isCo) {
                        BowEffect.SetActive(true);
                        ArrowEffect.SetActive(true);
                        BaseLineEffect.SetActive(true);
                    }
                    else if (onBase && isCo) {
                        CoBowEffect.SetActive(true);
                        CoArrowEffect.SetActive(true);
                        HeightLineEffect.SetActive(true);
                    }
                    break;
                case 2:
                    if (onHeight) HeightDeleteEffect.SetActive(true);
                    else if (onHypo && isCo) {
                        CoSpearEffect.SetActive(true);
                        HypoCoLineEffect.SetActive(true);
                    }
                    else if (onBase && isCo) CoShieldEffect.SetActive(true);
                    break;
                case 3:
                    if (onBase) BaseDeleteEffect.SetActive(true);
                    else if (onHypo && !isCo) {
                        SpearEffect.SetActive(true);
                        HypoIdleLineEffect.SetActive(true);
                    }
                    else if (onHeight && !isCo) ShieldEffect.SetActive(true);
                    break;
            }
            if (onCoAngle) {
                if (isCo) CoAngleDeleteEffect.SetActive(true);
                else if (!isCo) CoAngleEffect.SetActive(true);
            }
            if (onIdleAngle && isCo) {
                CoAngleDeleteEffect.SetActive(true);
                IdleAngleEffect.SetActive(true);
            }
        }

        // 키보드'space' or CoAngle 클릭시
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Mouse0) && onCoAngle)) { isCo = !isCo; }

        // Co상태에서 IdleAngle 클릭시
        if (isCo && Input.GetKeyDown(KeyCode.Mouse0) && onIdleAngle) { isCo = false; }

        if (isCo) CoAngle.SetActive(true);
        else CoAngle.SetActive(false);

        // 키보드'w' or Hypo 클릭시
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Mouse0) && onHypo)) {
            switch (Tstate) {
                case 0:
                    Tstate = 1;
                    if (!isCo) MakeCircle(HypoIdleCircle);
                    else MakeCircle(HypoCoCircle);
                    break;
                case 1:
                    Tstate = 0;
                    HypoIdleCircle.SetActive(false);
                    HypoCoCircle.SetActive(false);
                    break;
                case 2:
                    if (isCo) {
                        if (Input.GetKeyDown(KeyCode.W)) {
                            StartCoroutine("Cosec");
                            RotateFinish();
                        } else {
                            CoSpear.SetActive(true);
                            isLaunching = 5;
                        }
                    }
                    break;
                case 3:
                    if (!isCo) {
                        if (Input.GetKeyDown(KeyCode.W)) {
                            StartCoroutine("Sec");
                            RotateFinish();
                        } else {
                            Spear.SetActive(true);
                            isLaunching = 2;
                        }
                    }
                    break;
            }
        }

        // 키보드'd' or Height 클릭시
        if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown(KeyCode.Mouse0) && onHeight)) {
            switch (Tstate)
            {
                case 0:
                    MakeCircle(HeightCircle);
                    Tstate = 2;
                    break;
                case 1:
                    if (!isCo) {
                        if (Input.GetKeyDown(KeyCode.D)) {
                            StartCoroutine("Sin");
                            RotateFinish();
                        } else {
                            Bow.SetActive(true);
                            Arrow.SetActive(true);
                            isLaunching = 1;
                        }
                    }
                    break;
                case 2:
                    Tstate = 0;
                    HeightCircle.SetActive(false);
                    break;
                case 3:
                    if (!isCo) {
                        if (Input.GetKeyDown(KeyCode.D)) {
                            StartCoroutine("Tan");
                            RotateFinish();
                        } else {
                            Shield.SetActive(true);
                            isLaunching = 3;
                        }
                    }
                    break;
            }
        }

        // 키보드'a' or Base 클릭시
        if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.Mouse0) && onBase)) {
            switch (Tstate)
            {
                case 0:
                    MakeCircle(BaseCircle);
                    Tstate = 3;
                    break;
                case 1:
                    if (isCo) {
                        if (Input.GetKeyDown(KeyCode.A)) {
                            StartCoroutine("Cos");
                            RotateFinish();
                        } else {
                            CoBow.SetActive(true);
                            CoArrow.SetActive(true);
                            isLaunching = 4;
                        }
                    }
                    break;
                case 2:
                    if (isCo) {
                        if (Input.GetKeyDown(KeyCode.A)) {
                            StartCoroutine("Cotan");
                            RotateFinish();
                        } else {
                            CoShield.SetActive(true);
                            isLaunching = 6;
                        }
                    }
                    break;
                case 3:
                    Tstate = 0;
                    BaseCircle.SetActive(false);
                    break;
            }
        }

        switch (Tstate) {
            case 1:
                HypoActivated.SetActive(true);
                HypoLength.SetActive(true);
                Tri.transform.localScale = StandardScale * 0.807f;
                break;
            case 2:
                HeightActivated.SetActive(true);
                HeightLength.SetActive(true);
                Tri.transform.localScale = StandardScale * 1.365f;
                break;
            case 3:
                BaseActivated.SetActive(true);
                BaseLength.SetActive(true);
                break;
        }

        // 마우스 떼면 회전종료
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isRotating = false;
            if (isLaunching != 0) {
                switch (isLaunching) {
                    case 1:
                        StartCoroutine("Sin");
                        break;
                    case 2:
                        StartCoroutine("Sec");
                        break;
                    case 3:
                        StartCoroutine("Tan");
                        break;
                    case 4:
                        StartCoroutine("Cos");
                        break;
                    case 5:
                        StartCoroutine("Cosec");
                        break;
                    case 6:
                        StartCoroutine("Cotan");
                        break;
                }
                RotateFinish();
            }
        }

        if (isRotating)
        {
            MousePresentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CoR;   // 마우스 현재위치는 깨다 기준 상대적 위치
            RotateAngle = Vector2.Angle(MouseStartPosition, MousePresentPosition);              // RotateAngle 계산
            // Vertor2.Angle은 항상 0<=Angle<180이므로 외적을 이용해서 반환
            if (Vector3.Cross(MouseStartPosition, MousePresentPosition).z < 0) RotateAngle = 360f - RotateAngle;
            // 회전 및 삼각형 중심 - 회전 중심 위치 조정
            Tri.transform.rotation = TriStartRotation * Quaternion.Euler(Vector3.forward * RotateAngle);
            // Hypo, Height, Base Length 같이 회전 (역방향)
            HypoLength.transform.rotation = Quaternion.Euler(-(Tri.transform.rotation * Vector3.forward));
            HeightLength.transform.rotation = Quaternion.Euler(-(Tri.transform.rotation * Vector3.forward));
            BaseLength.transform.rotation = Quaternion.Euler(-(Tri.transform.rotation * Vector3.forward));

            Tri.transform.position = Quaternion.Euler(Vector3.forward * RotateAngle) * TriStartPosition + CoR;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) { // 우클릭 초기화
            ClearTri();
        }
    }

    private void RotateFinish() {
        Tri.transform.localScale = StandardScale;
        isLaunching = 0;
        Tstate = 0;
        isCo = false;
        HypoIdleCircle.SetActive(false);
        HypoCoCircle.SetActive(false);
        HeightCircle.SetActive(false);
        BaseCircle.SetActive(false);
        ClearTri();
    }

    // 삼각형 내 모든걸 끔
    private void ClearTri() {
        ClearWeaponEffect();

        CoAngle.SetActive(false);
        CoAngleEffect.SetActive(false);
        CoAngleDeleteEffect.SetActive(false);
        IdleAngleEffect.SetActive(false);

        HypoActivated.SetActive(false);
        HeightActivated.SetActive(false);
        BaseActivated.SetActive(false);

        HypoLength.SetActive(false);
        HeightLength.SetActive(false);
        BaseLength.SetActive(false);

        HypoEffect.SetActive(false);
        HeightEffect.SetActive(false);
        BaseEffect.SetActive(false);

        HypoDeleteEffect.SetActive(false);
        HeightDeleteEffect.SetActive(false);
        BaseDeleteEffect.SetActive(false);

        Bow.SetActive(false);
        Arrow.SetActive(false);
        Spear.SetActive(false);
        Shield.SetActive(false);
        CoBow.SetActive(false);
        CoArrow.SetActive(false);
        CoSpear.SetActive(false);
        CoShield.SetActive(false);

        HypoIdleLineEffect.SetActive(false);
        HypoCoLineEffect.SetActive(false);
        HeightLineEffect.SetActive(false);
        BaseLineEffect.SetActive(false);
    }

    private void ClearWeaponEffect() {
        BowEffect.SetActive(false);
        ArrowEffect.SetActive(false);
        SpearEffect.SetActive(false);
        ShieldEffect.SetActive(false);
        CoBowEffect.SetActive(false);
        CoArrowEffect.SetActive(false);
        CoSpearEffect.SetActive(false);
        CoShieldEffect.SetActive(false);
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
                GameOver(false);
                GetComponent<AudioManager>().GameOverSound();
                break;
        }
    }

    public void GameOver(bool isCleared) // GameOver 시 해야될 일을 해주는 함수
    {                                    // isCleared면 Clear, 아니면 GameOver
        StopCoroutine("Timer");
        GameOverWindow.SetActive(true);
        GameOverWindow.transform.Translate(new Vector3(0f, 0f, 0.01f));
        if (isCleared)
        {
            GameOverBack.SetActive(false);
            ClearBack.SetActive(true);
            RankButton.SetActive(false);
            InfinityModeButton.SetActive(true);
        }
        else
        {
            GameOverBack.SetActive(true);
            ClearBack.SetActive(false);
            RankButton.SetActive(true);
            InfinityModeButton.SetActive(false);
        }
        if (!isMonsterTypeOn) PlayerPrefs.SetInt("isMonsterTypeOff", 1);
    }

    ///////////////////////////////////////////////////////////// 무기 발사 Coroutine
    IEnumerator Sin()
    {
        GameObject BowObject = Instantiate(BowPrefab, Bow.transform.position, Bow.transform.rotation);
        GameObject ArrowObject = Instantiate(ArrowPrefab, Arrow.transform.position, Arrow.transform.rotation);
        BowObject.transform.localScale *= 0.807f;
        ArrowObject.transform.localScale *= 0.807f;

        yield return new WaitForSeconds(1f);
        Destroy(BowObject);
        yield return new WaitForSeconds(1f);
        Destroy(ArrowObject);
    }

    IEnumerator Sec()
    {
        GameObject SpearObject = Instantiate(SpearPrefab, Spear.transform.position, Spear.transform.rotation);

        yield return new WaitForSeconds(2f);
        Destroy(SpearObject);
    }

    IEnumerator Tan()
    {
        GameObject ShieldObject = Instantiate(ShieldPrefab, Shield.transform.position, Shield.transform.rotation);

        yield return new WaitForSeconds(2f);
        Destroy(ShieldObject);
    }
    
    IEnumerator Cos()
    {
        GameObject CoBowObject = Instantiate(CoBowPrefab, CoBow.transform.position, CoBow.transform.rotation);
        GameObject CoArrowObject = Instantiate(CoArrowPrefab, CoArrow.transform.position, CoArrow.transform.rotation);
        CoBowObject.transform.localScale *= 0.807f;
        CoArrowObject.transform.localScale *= 0.807f;

        yield return new WaitForSeconds(1f);
        Destroy(CoBowObject);
        yield return new WaitForSeconds(1f);
        Destroy(CoArrowObject);
    }

    IEnumerator Cosec()
    {
        GameObject CoSpearObject = Instantiate(CoSpearPrefab, CoSpear.transform.position, CoSpear.transform.rotation);
        CoSpearObject.transform.localScale *= 1.365f;

        yield return new WaitForSeconds(2f);
        Destroy(CoSpearObject);
    }

    IEnumerator Cotan()
    {
        GameObject CoShieldObject = Instantiate(CoShieldPrefab, CoShield.transform.position, CoShield.transform.rotation);
        CoShieldObject.transform.localScale *= 1.365f;

        yield return new WaitForSeconds(2f);
        Destroy(CoShieldObject);
    }
}
