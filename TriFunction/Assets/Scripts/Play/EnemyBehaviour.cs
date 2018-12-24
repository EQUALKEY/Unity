using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private GameObject EC;
    private EventController ec;
    private MakeEnemy me;

    private GameObject Combo;
    private int newScore;

    public float Velocity;
    private Vector3 Direction3d;
    private Vector2 Direction;

    private float PushVelocity;
    private bool isPushing;
    private static readonly string AttackedMessage = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! \n";
    private static readonly string NotAttackeMessage = "잘했다! ";

    private bool isDying;

    void Awake () {
        // Prefab이라 EC, Combo find
        EC = GameObject.Find("EC");
        Combo = GameObject.Find("Combo");
        ec = EC.GetComponent<EventController>();
        me = EC.GetComponent<MakeEnemy>();
        isDying = false;
        
        // 움직이는 방향
        Direction3d = new Vector3(0f, 0f, 0f) - transform.position;
        Direction = (new Vector2(Direction3d.x, Direction3d.y)).normalized;
        
        // 몬스터에 공격받아서 다른 몬스터들 미는 넉백 속도
        PushVelocity = 30f;
        isPushing = false;
	}
	
	void Update () {
        // Life 까일 때 밀어냄
        if (transform.position.magnitude > 4f) isPushing = false;
        if (!isPushing) transform.Translate(Direction * Velocity * Time.deltaTime);
        else transform.Translate(-Direction * PushVelocity * Time.deltaTime);

        // Life가 깎이는 경우
        if (Vector3.Distance(new Vector3(0f, 0f, 0f), transform.position) < 1.6f) {
            // 깨다 공격받는 애니메이션
            ec.SetAnimationParameters(0, 2);
            ec.LostLife();
            
            // 스토리모드에서 몬스터에 라이프가 깎인경우
            if (PlayerPrefs.GetInt("Mode") == 0) {
                me.isAttacked = true; // 이후에 나타나는 텍스트 바뀜
                ec.KillMonsters++;    // Kill수에 넣어야함 (스토리진행)

                // StoryMode에서 Story단계가 넘어가는 경우
                if (((me.StoryProgress == 12 || me.StoryProgress == 20) && ec.KillMonsters == 10) || 
                    ((me.StoryProgress == 22 || me.StoryProgress == 24) && ec.KillMonsters == 20) ||
                    ((me.StoryProgress == 6 || me.StoryProgress == 8 || me.StoryProgress == 10 || me.StoryProgress == 14 || me.StoryProgress == 16 || me.StoryProgress == 18) && ec.KillMonsters == 2))
                    ToNextStory();
            }

            Destroy(gameObject);
        }

        // 몬스터정보 띄울지말지
        if (ec.isMonsterInfoOn) gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // 무기로 몬스터 맞춘경우 실행
    public void DoDie(bool isSkill = false) {
        if (!isDying) {
            isDying = true;
            // 소리
            EC.GetComponent<AudioManager>().MonsterHitSound();

            // 필살기 실행중이 아닌경우 필살기게이지++
            if (!isSkill) ec.GetSkillGauge(1);

            // 콤보++
            ec.combo++;

            // 점수++
            newScore = (100 + ec.combo - 1) * (me.InfinityLevelState + 1) * (me.InfinityLevelState + 1);
            ec.GetScore(newScore, transform.position, transform.rotation);

            // 맞으면 멈추고 죽는 애니메이션
            Velocity = 0f;
            GetComponent<Animator>().SetInteger("Monster_state", 1);

            // 죽는 애니메이션 보여주고 0.5초 뒤에 사라짐
            StartCoroutine("DoDestroy");

            // StoryMode에서 Story단계가 넘어가는 경우
            if (PlayerPrefs.GetInt("Mode") == 0 &&
                ((me.StoryProgress == 12 || me.StoryProgress == 20) && ec.KillMonsters == 10) ||
                ((me.StoryProgress == 22 || me.StoryProgress == 24) && ec.KillMonsters == 20) ||
                ((me.StoryProgress == 6 || me.StoryProgress == 8 || me.StoryProgress == 10 || me.StoryProgress == 14 || me.StoryProgress == 16 || me.StoryProgress == 18) && ec.KillMonsters == 2))
                ToNextStory();
        }
    }

    // StoryMode에서 다음 Story로
    private void ToNextStory() {
        ec.KillMonsters = 0;
        me.StoryProgress++;
        me.StoryManager();
        if (me.StoryProgress != 25)
        {
            if (me.isAttacked) me.SpeechBubble_text.text = AttackedMessage + me.SpeechBubble_text.text;
            else me.SpeechBubble_text.text = NotAttackeMessage + me.SpeechBubble_text.text;
        }
    }

    // 0.5초 후에 Destroy
    IEnumerator DoDestroy() {
        Destroy(GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // 몬스터한테 공격받았을 때 다른 몬스터 밀쳐냄
    public void PushBack() {
        if (transform.position.magnitude < 4f) isPushing = true;
    }
}
