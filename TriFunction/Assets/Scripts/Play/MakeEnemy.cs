using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeEnemy : MonoBehaviour {

    public GameObject[] Enemy = new GameObject[6];      // sin,sec,tan,cos,cosec,cotan
    public GameObject[] EnemyTypeInfo = new GameObject[6];  // sin,sec,tan,cos,cosec,cotan
    public GameObject EnemyParent;

    public GameObject EC;
    private EventController ec;

    public GameObject TextBox;
    public Text TextBox_text;
   
    public GameObject SpeechBubble;
    public Text SpeechBubble_text;
    public bool isAttacked;

    string[] textArr = 
    {
        "어느 날 깨다는 깨봉으로 열심히 삼각함수를 공부를 하였습니다. 공부를 마친 깨다는 잠시 쉬기 위해 TV를 켰는데 마침 중세시대의 기사들이 나오는 영화를 하고있었습니다. 지난 방학 때 로마로 여행을 갔다 온 깨다는 콜로세움의 검투사들의 전투가 멋있어 보였습니다. 한창 영화를 보던 깨다는 눈꺼풀이 점차 무거워 지며 참을 수 없는 졸음에 빠집니다."
        ,"정신을 차린 깨다는 콜로세움 경기장 안에 서있었습니다. 그리고 바로 눈 앞에는 험상궂게 생기고 온몸에 흉터가 있는 검투사가 한 명 서있었습니다. 깨다를 노려보며 그 검투사는 입을 열었습니다."
        ,"신참! 정신 똑바로 차려라! 싸울 검투사가 부족해 지원을 받았더니 이렇게 흐리멍텅한 사람을 보내주다니…. 나 원 참… 한 눈에 봐도 싸움을 해본 적이 없는 것 같으니 내가 가르쳐주겠다. 정신 똑바로 챙기고 들어! 니 목숨이 걸린 일이다."
        ,"이 삼각형을 이용해서 공격과 방어를 할 수 있다. 삼각함수는 알고 있겠지? 잘 모르겠으면 깨봉tv에서 공부하고 와라. sin의 개념을 알고 있다면 삼각형의 변을 눌러서 sin을 표현해봐라."
        ,"멋진 화살이군. 이번에는 활로 슬라임을 조준해서 쏘아보거라. 처음이니까 2마리부터다."
        ,"이번에는 sec다. 멋진 창을 날릴 수 있지"
        ,"이번에는 tan다. 든든한 방패로 적을 막을 수 있지"
        ,"좀 실력이 늘었나? 그렇다면 한번 테스트 해보지. 10마리다."
        ,"이번에는 co를 이용해 보지. 기준각의 대각을 누르면 co각이 활성화 된다. cos이 co각의 sin인건 알고 있겠지? cos 2마리다."
        ,"이번에는 cosec다."
        ,"드디어 마지막 몬스터로군 cotan다."
        ,"co몬스터 10마리만 처치해봐라"
        ,"이제 좀 쓸만해졌는걸? 이제 연습은 끝났다. 20마리를 잡아봐라"
        ,"마지막 시험이다. 더 강한 놈들을 준비해놨지."
        ,"훌륭하군! 처음에 경솔했던 내 발언은 잊어주게. 너는 이미 훌륭한 전사다. 이제 너는 콜로세움의 수 많은 전사들과 경쟁할 수 있다. 누가 더 훌륭한 전사인지 겨루어보거라"
    };

    public int StoryProgress;

    /// <summary>
    /// 자 이제 레벨디자인을 한번 해봅시다. 전창민 군께서 velocity = 1, rezentime = 3 인 상황에서 100마리 넘게 잡았으므로 더올려도 문제없다고 판단됩니다.
    /// 그래서 어찌 할꺼냐 하면은 처음에는 좀 쉽게 해줍니다.
    /// 레벨로 구분
    /// Level 1 - velocity = 0.6 rezentime = 4 입니다. 이거 지루합니다. 그래서 4마리까지만 합시다. 
    /// Level 2 - velocity = 0.8 rezentime = 3.5 입니다. 이거 좀 할만 합니다. 그래서 16마리까지 합시다.
    /// Level 3 - velocity = 1.0 rezentime = 3 (test 할 때 난이도) 살짝 어려워 집니다. 따라서 40마리까지 ㄱㄱ
    /// level 4 - velocity = 1.1 (속도 확빨라지면 개어렵습니다.) rezentime = 2.8 로 갑니다. 60마리까지 ㄱㄱ
    /// level 5 - velocity = 1.2 rezentime = 2.5 입니다. 최고난이도입니다. 
    /// 
    /// </summary>
    /// 

    /// <storymode>
    /// storyLevelState 변수로 컨트롤
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
    /// 
    /// </story>

    public int LevelState; // 1,2,3,4,5,6,7
    private int storyLevelState; // 0 ~ 10
    private int CreatedMonsterCnt;
    private bool isStoryMode;

    private float Monster_velocity;
    private float rezentime;

    private void Awake()
    {
        ec = EC.GetComponent<EventController>();

        if (PlayerPrefs.GetInt("Mode") == 0)
        {
            isStoryMode = true;
            storyLevelState = 1;
            ec.isPlay = false;
            StoryProgress = 0;
            StoryManager();
            
        }
        else
        {
            isStoryMode = false;
            storyLevelState = 0;
            ec.isPlay = true;
        }

        LevelState = 1;
        CreatedMonsterCnt = 0;

        if(!isStoryMode) Make_Enemy();
    }

    void Make_Enemy() 
    {
        StartCoroutine("Create_Enemy_Controller");
    }

    IEnumerator Create_Enemy_Controller() // Create_Enemy 관리해줌.
    {
        if (isStoryMode)
        {
            switch (storyLevelState)
            {
                case 9:
                    Monster_velocity = 0.6f;
                    rezentime = 4f;
                    break;
                case 10:
                    Monster_velocity = 0.8f;
                    rezentime = 3.5f;
                    break;
                default:
                    Monster_velocity = 0.5f;
                    rezentime = 4f;
                    break;
            }
        }
        else
        {
            switch (LevelState)
            {
                case 1:
                    Monster_velocity = 0.6f;
                    rezentime = 4f;
                    break;
                case 2:
                    Monster_velocity = 0.8f;
                    rezentime = 3.5f;
                    break;
                case 3:
                    Monster_velocity = 1.0f;
                    rezentime = 3.0f;
                    break;
                case 4:
                    Monster_velocity = 1.2f;
                    rezentime = 2.5f;
                    break;
                case 5:
                    Monster_velocity = 1.4f;
                    rezentime = 2.0f;
                    break;
                case 6:
                    Monster_velocity = 1.5f;
                    rezentime = 1.8f;
                    break;
                case 7:
                    Monster_velocity = 1.5f;
                    rezentime = 1.5f;
                    break;
                case 8:
                    Monster_velocity = 1.5f;
                    rezentime = 1.3f;
                    break;
                case 9:
                    Monster_velocity = 1.5f;
                    rezentime = 1.0f;
                    break;
            }
        }

        int EnemyType = Random.Range(0, 6);

        if (isStoryMode)
        {
            switch (storyLevelState)
            {
                case 1:
                    EnemyType = 0;
                    break;
                case 2:
                    EnemyType = 1;
                    break;
                case 3:
                    EnemyType = 2;
                    break;
                case 4:
                    EnemyType = Random.Range(0, 3);
                    break;
                case 5:
                    EnemyType = 3;
                    break;
                case 6:
                    EnemyType = 4;
                    break;
                case 7:
                    EnemyType = 5;
                    break;
                case 8:
                    EnemyType = Random.Range(3, 6);
                    break;
            }
        }

        Create_Enemy(EnemyType,Monster_velocity);
        yield return new WaitForSeconds(rezentime);
        if (ec.Lifes != 0 || storyLevelState == 11)
        {
            if(!isStoryMode || (CreatedMonsterCnt!=2 && CreatedMonsterCnt!= 4 && CreatedMonsterCnt != 6 && CreatedMonsterCnt != 16 && CreatedMonsterCnt != 18
                && CreatedMonsterCnt != 20 && CreatedMonsterCnt != 22 && CreatedMonsterCnt != 32 && CreatedMonsterCnt != 52 && CreatedMonsterCnt != 72))
                StartCoroutine("Create_Enemy_Controller");
        }
    }

    void Create_Enemy(int EnemyType, float velocity) // EnemyType이랑 velocity넣으면 적 만들어줌.
    {
        CreatedMonsterCnt++;
        if (isStoryMode)
        {
            if (CreatedMonsterCnt < 2) storyLevelState = 1;
            else if (CreatedMonsterCnt < 4) storyLevelState = 2;
            else if (CreatedMonsterCnt < 6) storyLevelState = 3;
            else if (CreatedMonsterCnt < 16) storyLevelState = 4;
            else if (CreatedMonsterCnt < 18) storyLevelState = 5;
            else if (CreatedMonsterCnt < 20) storyLevelState = 6;
            else if (CreatedMonsterCnt < 22) storyLevelState = 7;
            else if (CreatedMonsterCnt < 32) storyLevelState = 8;
            else if (CreatedMonsterCnt < 52) storyLevelState = 9;
            else if (CreatedMonsterCnt < 72) storyLevelState = 10;
            else
            {
                storyLevelState = 11;
            }

        }
        else
        {
            if (CreatedMonsterCnt < 4)
            {
                LevelState = 1;
            }
            else if (CreatedMonsterCnt < 20)
            {
                LevelState = 2;
            }
            else if (CreatedMonsterCnt < 60)
            {
                LevelState = 3;
            }
            else if (CreatedMonsterCnt < 100)
            {
                LevelState = 4;
            }
            else if (CreatedMonsterCnt < 160)
            {
                LevelState = 5;
            }
            else if (CreatedMonsterCnt < 250)
            {
                LevelState = 6;
            }
            else if (CreatedMonsterCnt < 400)
            {
                LevelState = 7;
            }
            else if (CreatedMonsterCnt < 600)
            {
                LevelState = 8;
            }
            else LevelState = 9;
        }
        float PositionEdge = Random.Range(-1f * Mathf.PI, Mathf.PI); // 360도 방향에서 랜덤 생성
        GameObject newEnemy = Instantiate(Enemy[EnemyType], new Vector3(10f * Mathf.Cos(PositionEdge), 10f * Mathf.Sin(PositionEdge)), new Quaternion(0f,0f,0f,1f), EnemyParent.transform);
        Instantiate(EnemyTypeInfo[EnemyType], newEnemy.transform);
        
        newEnemy.GetComponent<EnemyBehaviour>().Velocity = velocity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !ec.isPlay)
        {
            StoryProgress++;
            StoryManager();
        }
    }

    public void StoryManager()
    {
        switch (StoryProgress)
        {
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
            case 4:// storyLevelState 1 시작
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                break;
            case 5:
                ec.isPlay = false;
                Start_SpeechBubble(4);
                break;
            case 6:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 7:
                Start_SpeechBubble(5);
                ec.isPlay = false;
                break;
            case 8:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 9:
                Start_SpeechBubble(6);
                ec.isPlay = false;
                break;
            case 10:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 11:
                Start_SpeechBubble(7);
                ec.isPlay = false;
                break;
            case 12:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 13:
                Start_SpeechBubble(8);
                ec.isPlay = false;
                break;
            case 14:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 15:
                Start_SpeechBubble(9);
                ec.isPlay = false;
                break;
            case 16:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 17:
                Start_SpeechBubble(10);
                ec.isPlay = false;
                break;
            case 18:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 19:
                Start_SpeechBubble(11);
                ec.isPlay = false;
                break;
            case 20:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 21:
                Start_SpeechBubble(12);
                ec.isPlay = false;
                break;
            case 22:
                Stop_SpeechBubble();
                ec.isPlay = true;
                isAttacked = false;
                Make_Enemy();
                break;
            case 23:
                Start_SpeechBubble(13);
                ec.isPlay = false;
                break;
            case 24:
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
    void Start_TextBox(int num)
    {
        TextBox.SetActive(true);
        TextBox_text.text = textArr[num];
    }

    void Stop_TextBox()
    {
        TextBox.SetActive(false);
    }

    void Start_SpeechBubble(int num)
    {
        SpeechBubble.SetActive(true);
        SpeechBubble_text.text = textArr[num];
    }

    void Stop_SpeechBubble()
    {
        SpeechBubble.SetActive(false);
    }
}
