using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemy : MonoBehaviour {

    public GameObject[] Enemy = new GameObject[6];      // sin,sec,tan,cos,cosec,cotan
    public GameObject[] EnemyTypeInfo = new GameObject[6];  // sin,sec,tan,cos,cosec,cotan
    public GameObject EnemyParent;

    public GameObject EC;
    private EventController ec;

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

    private int LevelState; // 1,2,3,4,5
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
        }
        else
        {
            isStoryMode = false;
            storyLevelState = 0;
        }

        LevelState = 1;
        CreatedMonsterCnt = 0;

        Make_Enemy();
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
                    Monster_velocity = 1.1f;
                    rezentime = 2.8f;
                    break;
                case 5:
                    Monster_velocity = 1.2f;
                    rezentime = 2.5f;
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
        if (ec.Lifes != 0 || storyLevelState == 11) StartCoroutine("Create_Enemy_Controller");
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
                ec.GameOver(true);
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
            else
            {
                LevelState = 5;
            }
        }
        float PositionEdge = Random.Range(-1f * Mathf.PI, Mathf.PI); // 360도 방향에서 랜덤 생성
        GameObject newEnemy = Instantiate(Enemy[EnemyType], new Vector3(10f * Mathf.Cos(PositionEdge), 10f * Mathf.Sin(PositionEdge)), new Quaternion(0f,0f,0f,1f), EnemyParent.transform);
        Instantiate(EnemyTypeInfo[EnemyType], newEnemy.transform);
        
        newEnemy.GetComponent<EnemyBehaviour>().Velocity = velocity;
    }
}
