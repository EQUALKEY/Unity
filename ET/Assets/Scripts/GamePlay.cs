using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour { // 게임의 전반적인 부분을 관리하는 이벤트컨트롤러라 생각하면 됨.

	public GameObject Tri;              // 삼각형
	public GameObject Tri_withoutWeapon;
    public GameObject Enemy;            // Enemy (sin, sec, tan ... 컨트롤)
    public GameObject GameOver;         // GameOver시 나타나고 EventController의 ResetGame 실행

    public GameObject Circle_b;     // idle 상태 base Circle
    public GameObject Circle_h;     // idle 상태 hypo Circle
    public GameObject Circle_cob;   // co 상태 base Circle (height)
    public GameObject Circle_coh;   // co 상태 hypo Circle

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () { // q 키 입력 인식.
		if(Input.GetKeyDown(KeyCode.Q)){
            Tri.GetComponent<Tri_State>().turn();
		}
	}
    
    public void Initiate() // 초기화.(원 , L_state)
    {
        Circle_b.SetActive(false);
        Circle_h.SetActive(false);
        Circle_cob.SetActive(false);
        Circle_coh.SetActive(false);
        Tri.GetComponent<Line_State>().SetLstate(Line_State.Lstate.idle);
    }

    public void ResetGame() { // 게임 다시 시작
        Enemy.SetActive(true);
        Enemy.GetComponent<RandomAttack>().Awake();
        Initiate();
		GameOver.SetActive(false);
    }
}
