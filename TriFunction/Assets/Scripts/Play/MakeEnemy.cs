using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeEnemy : MonoBehaviour {

    // sin, sec, tan, cos, cosec, cotan
    public GameObject[] Enemy = new GameObject[6];          
    public GameObject[] EnemyTypeInfo = new GameObject[6];

    // 몬스터 부모 gameObject
    public GameObject EnemyParent;

    // EventController
    public GameObject EC;
    private EventController ec;

    // StoryMode Text
    public GameObject TextBox;
    public Text TextBox_text;
   
    // StoryMode Bubble
    public GameObject SpeechBubble;
    public Text SpeechBubble_text;

    // StoryMode 공격 받으면 활성화, 이걸로 Text, Bubble 내용이 조금 바뀜
    public bool isAttacked;

    // StoryMode 진행
    public int StoryProgress;

    string[] textArr = {
        "어느 날 깨다는 깨봉으로 열심히 삼각함수를 공부를 하였습니다. \n공부를 마친 깨다는 잠시 쉬기 위해 TV를 켰는데 \n마침 중세시대의 기사들이 나오는 영화가 나오고 있었습니다. \n지난 방학 때 로마로 여행을 갔다 온 깨다는 콜로세움의 검투사들의 전투가 멋있어 보였습니다. \n한창 영화를 보던 깨다는 눈꺼풀이 점차 무거워 지며 참을 수 없는 졸음에 빠집니다."
        ,"정신을 차린 깨다는 콜로세움 경기장 안에 서있었습니다. \n그리고 바로 눈 앞에는 험상궂게 생기고 온몸에 흉터가 있는 검투사가 한 명 서있었습니다. \n깨다를 노려보며 그 검투사는 입을 열었습니다."
        ,"신참! 정신 똑바로 차려라! \n싸울 검투사가 부족해 지원을 받았더니\n이렇게 흐리멍텅한 사람을 보내주다니…. 나 원 참…"
        ,"이 삼각형을 이용해서 공격과 방어를 할 수 있다.\n 삼각함수는 알고 있겠지? \n잘 모르겠으면 깨봉tv에서 공부하고 와라. "
        ,"멋진 화살이군.\n이번에는 활로 슬라임을 조준해서 쏘아보거라.\n처음이니까 2마리부터다."
        ,"이번에는 sec다. 멋진 창을 날릴 수 있지"
        ,"이번에는 tan다. 든든한 방패로 적을 막을 수 있지"
        ,"좀 실력이 늘었나? 그렇다면 한번 테스트 해보지. 10마리다."
        ,"이번에는 co를 이용해 보지.\n기준각의 대각을 누르면 co각이 활성화 된다.\ncos이 co각의 sin인건 알고 있겠지? cos 2마리다."
        ,"이번에는 cosec다."
        ,"드디어 마지막 몬스터로군 cotan다."
        ,"co몬스터 10마리만 처치해봐라"
        ,"이제 좀 쓸만해졌는걸? \n이제 연습은 끝났다. 20마리를 잡아봐라"
        ,"마지막 시험이다. 더 강한 놈들을 준비해놨지."
        ,"훌륭하군! 처음에 경솔했던 내 발언은 잊어주게.\n너는 이미 훌륭한 전사다. 이제 너는 콜로세움의 수 많은 전사들과 경쟁할 수 있다.\n누가 더 훌륭한 전사인지 겨루어보거라"
        ,"한 눈에 봐도 싸움을 해본 적이 없는 것 같으니\n내가 가르쳐주겠다.\n정신 똑바로 챙기고 들어! 니 목숨이 걸린 일이다."
        ,"sin의 개념을 알고 있다면\n삼각형의 변을 눌러서 sin을 표현해봐라."
    };

    /// StoryMode는
    /// StoryLevelState 변수로 컨트롤
    /// 0: Infinity Mode
    /// 1: sin만, velocity = 0.5 rezentime = 4, 2마리
    /// 2: sec만, velocity = 0.5 rezentime = 4, 2마리
    /// 3: tan만, velocity = 0.5 rezentime = 4, 2마리
    /// 4: sin tan sec, velocity = 0.5 rezentime = 4, 10마리
    /// 5: cos만, velocity = 0.5 rezentime = 4, 2마리
    /// 6: cosec만, velocity = 0.5 rezentime = 4, 2마리 
    /// 7: cotan만, velocity = 0.5 rezentime = 4, 2마리
    /// 8: co각들, velocity = 0.5 rezentime = 4, 10마리
    /// 9: 각 6가지 전부 나옴, velocity = 0.6 rezentime = 4, 20마리
    /// 10: 각 6가지 전부 나옴, velocity = 0.8 rezentime = 3.5, 20마리
    /// Clear!

    /// InfinityMode는
    /// InfinityLevelState 변수로 컨트롤

    public int InfinityLevelState;  // 0 ~ 8
    private int StoryLevelState;    // 0 ~ 10
    private bool IsStoryMode;       // StoryMode인지 체크
    private int CreatedMonsterCnt;  // 생성한 몬스터 수 Count
    private float MonsterVelocity;  // 생성할 몬스터 속도
    private float Rezentime;        // 몬스터 리젠간격

    // StoryMode, InfinityMode Level 기준 몬스터 수
    private static readonly int[] StoryModeMonsterCnt = { 2, 4, 6, 16, 18, 20, 22, 32, 52, 72, 99999999 };
    private static readonly int[] InfinityModeMonsterCnt = { 4, 20, 60, 100, 160, 250, 400, 600, 99999999 };
    
    // StoryMode Level Control, 0 ~ 7은 속도-리젠간격 같고, 8, 9만 다름 => 3가지
    private static readonly float[] StoryMonsterVelocity = { 0.7f, 0.8f, 0.9f };
    private static readonly float[] StoryRezentime = { 4f, 3.5f, 3f };

    // InfinityMode Level Control, 0 ~ 8
    private static readonly float[] InfinityMonsterVelocity = { 0.6f, 0.8f, 1.0f, 1.2f, 1.4f, 1.5f, 1.5f, 1.5f, 1.5f };
    private static readonly float[] InfinityRezentime = { 4f, 3.5f, 3.0f, 2.5f, 2.0f, 1.8f, 1.5f, 1.3f, 1.0f };

    // 초기화
    private void Awake() {
        ec = EC.GetComponent<EventController>();

        InfinityLevelState = 0;
        CreatedMonsterCnt = 0;

        // StoryMode
        if (PlayerPrefs.GetInt("Mode") == 0) {
            IsStoryMode = true;
            StoryLevelState = 1;
            ec.isPlay = false;
            StoryProgress = 0;
            StoryManager();
        } else { // InfinityMode
            IsStoryMode = false;
            StoryLevelState = 0;
            ec.isPlay = true;
            Make_Enemy();
        }
    }

    // 몬스터 생성 시작
    void Make_Enemy() {
        StartCoroutine("CreateEnemyController");
    }

    // 몬스터 생성 관리
    IEnumerator CreateEnemyController() {
        // 몬스터 속도, 리젠시간 설정
        if (IsStoryMode) {
            MonsterVelocity = StoryMonsterVelocity[StoryLevelState - 8 < 0 ? 0 : StoryLevelState - 8];
            Rezentime = StoryRezentime[StoryLevelState - 8 < 0 ? 0 : StoryLevelState - 8];
        } else {
            MonsterVelocity = InfinityMonsterVelocity[InfinityLevelState];
            Rezentime = InfinityRezentime[InfinityLevelState];
        }

        // 몬스터 생성되는 종류 설정
        // sin, sec, tan, cos, cosec, cotan
        int EnemyType = Random.Range(0, 6);

        // 스토리모드인 경우 생성되는 몬스터 종류 관리
        if (IsStoryMode) {
            switch (StoryLevelState) { // 1 ~ 9 단계
                case 1: // sin
                    EnemyType = 0;
                    break;
                case 2: // sec
                    EnemyType = 1;
                    break;
                case 3: // tan
                    EnemyType = 2;
                    break;
                case 4: // sin, sec, tan
                    EnemyType = Random.Range(0, 3);
                    break;
                case 5: // cos
                    EnemyType = 3;
                    break;
                case 6: // cosec
                    EnemyType = 4;
                    break;
                case 7: // cotan
                    EnemyType = 5;
                    break;
                case 8: // cos, cosec, cotan
                    EnemyType = Random.Range(3, 6);
                    break;
                // 9, 10의 경우 6가지 모두 나온다
                // 0은 InfinityMode
            }
        }

        // 종류, 속도, 리젠간격에 맞게 몬스터 생성
        CreateEnemy(EnemyType, MonsterVelocity);
        yield return new WaitForSeconds(Rezentime);

        // Life = 0, StoryMode끝, StoryMode에서 멈춰야되는 부분의 경우 몬스터 생성 중지
        bool NeedStop = false;
        if (ec.Lifes == 0 || StoryLevelState == 11) NeedStop = true; // Life = 0, StoryMode끝
        if (IsStoryMode) {
            for (int i = 0; i < 9; i++) {
                if (CreatedMonsterCnt == StoryModeMonsterCnt[i]) { // StoryMode에서 멈춰야되는 부분들
                    NeedStop = true;
                    break;
                }
            }
        }
        if (!NeedStop) StartCoroutine("CreateEnemyController");
    }

    // EnemyType이랑 velocity넣으면 몬스터 만듦
    void CreateEnemy(int EnemyType, float velocity) {
        CreatedMonsterCnt++;

        // StoryMode인 경우
        if (IsStoryMode) {
            int i = 0;
            while (CreatedMonsterCnt >= StoryModeMonsterCnt[i]) i++;
            StoryLevelState = i + 1;
        } else { // InfinityMode인 경우
            int i = 0;
            while (CreatedMonsterCnt >= InfinityModeMonsterCnt[i]) i++;
            StoryLevelState = i + 1;
        }

        // 360도 방향에서 랜덤 생성
        float PositionEdge = Random.Range(-1f * Mathf.PI, Mathf.PI);
        GameObject newEnemy = Instantiate(Enemy[EnemyType], new Vector3(10f * Mathf.Cos(PositionEdge), 10f * Mathf.Sin(PositionEdge)), new Quaternion(0f,0f,0f,1f), EnemyParent.transform);
        Instantiate(EnemyTypeInfo[EnemyType], newEnemy.transform);   // 몬스터 Type정보도 같이 생성 (껐다켰다함)
        newEnemy.GetComponent<EnemyBehaviour>().Velocity = velocity; // 생성된 몬스터 속도 설정
    }

    // 스토리모드 진행
    private void Update() {
        // 게임 멈춘 상태에서 마우스 클릭하면 스토리모드 진행
        if (Input.GetKeyDown(KeyCode.Mouse0) && !ec.isPlay && EnemyParent.transform.childCount == 0) {
            if (StoryProgress == 2)
            {
                StoryProgress = -2;
                Start_SpeechBubble(15);
            }else if(StoryProgress == -2)
            {
                StoryProgress = 3;
                StoryManager();
            }
            else if(StoryProgress == 3)
            {
                StoryProgress = -3;
                Start_SpeechBubble(16);

            }
            else if(StoryProgress == -3)
            {
                StoryProgress = 4;
                StoryManager();
            }
            else
            {
                StoryProgress++;
                StoryManager();
            }
        }
    }

    // 스토리모드 실행, StoryProgress 변수로 Story 어느정도 진행했는지 파악
    public void StoryManager() {
        switch (StoryProgress) {
            case 0:
                Start_TextBox(0);
                break;
            case 1:
                Start_TextBox(1);
                break;
            case 2:
                Stop_TextBox();
                Start_SpeechBubble(2);
                break;
            case 3:
                Start_SpeechBubble(3);
                break;
            case 4: // sin 쏘면 case 5로 이동
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                break;
            case 5:
                ec.isPlay = false;
                Start_SpeechBubble(4);
                break;
            case 6: // storyLevelState 1
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 7:
                Start_SpeechBubble(5);
                ec.isPlay = false;
                break;
            case 8: // storyLevelState 2
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 9:
                Start_SpeechBubble(6);
                ec.isPlay = false;
                break;
            case 10: // storyLevelState 3
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 11:
                Start_SpeechBubble(7);
                ec.isPlay = false;
                break;
            case 12: // storyLevelState 4
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 13:
                Start_SpeechBubble(8);
                ec.isPlay = false;
                break;
            case 14: // storyLevelState 5
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 15:
                Start_SpeechBubble(9);
                ec.isPlay = false;
                break;
            case 16: // storyLevelState 6
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 17:
                Start_SpeechBubble(10);
                ec.isPlay = false;
                break;
            case 18: // storyLevelState 7
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 19:
                Start_SpeechBubble(11);
                ec.isPlay = false;
                break;
            case 20: // storyLevelState 8
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 21:
                Start_SpeechBubble(12);
                ec.isPlay = false;
                break;
            case 22: // storyLevelState 9
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 23:
                Start_SpeechBubble(13);
                ec.isPlay = false;
                break;
            case 24: // storyLevelState 10
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 25:
                Start_SpeechBubble(14);
                ec.isPlay = false;
                break;
            case 26:
                Stop_SpeechBubble();
                ec.GameOver(true);
                break;

        }
    }

    // TextBox + num번째 Text 띄우기
    void Start_TextBox(int num) {
        TextBox.SetActive(true);
        TextBox_text.text = textArr[num];
    }
    
    // TextBox 지우기
    void Stop_TextBox() {
        TextBox.SetActive(false);
    }

    // Bublle + num번째 Text 띄우기
    void Start_SpeechBubble(int num) {
        SpeechBubble.SetActive(true);
        SpeechBubble_text.text = textArr[num];
    }

    // Bubble 지우기
    void Stop_SpeechBubble() {
        SpeechBubble.SetActive(false);
    }
}
