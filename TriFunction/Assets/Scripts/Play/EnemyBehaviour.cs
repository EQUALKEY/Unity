using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject EC;
    private EventController ec;
    private MakeEnemy me;

    public GameObject Combo;
    private int newScore;

    public float Velocity;
    private Vector3 Direction3d;
    private Vector2 Direction;

    private float PushVelocity;
    private bool isPushing;
    
    // Use this for initialization
    void Awake () {
        EC = GameObject.Find("EC"); // Prefab이다 보니까 public에 못넣음. 직접 찾아야 되더라. 다른방법 찾으면 Find 없애자.
        Combo = GameObject.Find("Combo");
        ec = EC.GetComponent<EventController>();
        me = EC.GetComponent<MakeEnemy>();
        Direction3d = new Vector3(0f,0f,0f) - transform.position;
        Direction = (new Vector2(Direction3d.x,Direction3d.y )).normalized;
        PushVelocity = 30f;
        isPushing = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.magnitude > 4f) isPushing = false;
        if (!isPushing) transform.Translate(Direction * Velocity * Time.deltaTime);
        else transform.Translate(-Direction * PushVelocity * Time.deltaTime);

        if (Vector3.Distance(new Vector3(0f, 0f, 0f), transform.position) < 1.6f) {
            ec.SetAnimationParameters(0, 2);
            ec.LostLife();
            if (PlayerPrefs.GetInt("Mode") == 0)
            {
                me.isAttacked = true;
                newScore = (100 + ec.combo-1) * me.LevelState * me.LevelState;
                ec.GetScore(newScore, transform.position, transform.rotation);
                if (me.StoryProgress == 6 || me.StoryProgress == 8 || me.StoryProgress == 10 || me.StoryProgress == 12 || me.StoryProgress == 14 || me.StoryProgress == 16 || me.StoryProgress == 18
                    || me.StoryProgress == 20 || me.StoryProgress == 22 || me.StoryProgress == 24 )
                {
                    if (me.StoryProgress==12 || me.StoryProgress==20)
                    {
                        if (ec.KillMosters == 10)
                        {
                            ec.KillMosters = 0;
                            me.StoryProgress++;
                            me.StoryManager();
                            me.SpeechBubble_text.text = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! " + me.SpeechBubble_text.text;
                        }
                    }
                    else if ( me.StoryProgress == 22 || me.StoryProgress==24 )
                    {
                        if(ec.KillMosters == 20)
                        {
                            ec.KillMosters = 0;
                            me.StoryProgress++;
                            me.StoryManager();
                            me.SpeechBubble_text.text = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! " + me.SpeechBubble_text.text;
                        }
                    }
                    else
                    {
                        if (ec.KillMosters == 2)
                        {
                            ec.KillMosters = 0;
                            me.StoryProgress++;
                            me.StoryManager();
                            me.SpeechBubble_text.text = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! " + me.SpeechBubble_text.text;
                        }
                    }
                }
            }
            Destroy(this.gameObject);
        }

        if (ec.isMonsterTypeOn) this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void PushBack() {
        if(transform.position.magnitude < 4f) isPushing = true;
    }

    public void DoDie(bool isSkill = false) {
        EC.GetComponent<AudioManager>().MonsterHitSound();
        if (!isSkill) ec.GetSkillGauge(1);
        ec.combo++;
        newScore = (100 + ec.combo-1) * me.LevelState * me.LevelState;
        ec.GetScore(newScore,transform.position,transform.rotation);
        Velocity = 0f;
        GetComponent<Animator>().SetInteger("Monster_state", 1);

        if (PlayerPrefs.GetInt("Mode") == 0)
        {
            if (me.StoryProgress == 6 || me.StoryProgress == 8 || me.StoryProgress == 10 || me.StoryProgress == 12 || me.StoryProgress == 14 || me.StoryProgress == 16 || me.StoryProgress == 18
                   || me.StoryProgress == 20 || me.StoryProgress == 22 || me.StoryProgress == 24 || me.StoryProgress == 26 || me.StoryProgress == 28)
            {
                if (me.StoryProgress == 12 || me.StoryProgress == 20)
                {
                    if (ec.KillMosters == 10)
                    {
                        ec.KillMosters = 0;
                        me.StoryProgress++;
                        me.StoryManager();
                        if(me.isAttacked)
                            me.SpeechBubble_text.text = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! " + me.SpeechBubble_text.text;
                        else
                            me.SpeechBubble_text.text = "잘했다! " + me.SpeechBubble_text.text;
                    }
                }
                else if (me.StoryProgress == 22 || me.StoryProgress == 24)
                {
                    if (ec.KillMosters == 20)
                    {
                        ec.KillMosters = 0;
                        me.StoryProgress++;
                        me.StoryManager();
                        if (me.isAttacked)
                            me.SpeechBubble_text.text = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! " + me.SpeechBubble_text.text;
                        else
                            me.SpeechBubble_text.text = "잘했다! " + me.SpeechBubble_text.text;
                    }
                }
                else
                {
                    if (ec.KillMosters == 2)
                    {
                        ec.KillMosters = 0;
                        me.StoryProgress++;
                        me.StoryManager();
                        if (me.isAttacked)
                            me.SpeechBubble_text.text = "좀 더 집중하지 않으면 여기서 살아남을 수 없다! " + me.SpeechBubble_text.text;
                        else
                            me.SpeechBubble_text.text = "잘했다! " + me.SpeechBubble_text.text;
                    }
                }
            }
        }

        StartCoroutine("DoDestroy");
    }

    IEnumerator DoDestroy() {
        Destroy(GetComponent<CircleCollider2D>());
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject.transform.GetChild(0).gameObject);
        Destroy(this.gameObject);
    }
}
