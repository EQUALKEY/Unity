using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour { // 게임의 전반적인 부분을 관리하는 이벤트컨트롤러라 생각하면 됨.

	public GameObject Tri;
	public GameObject Tri_withoutWeapon;


    public GameObject Circle_b;
    public GameObject Circle_h;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () { // q,e 키 입력 인식.
		if(Input.GetKeyDown(KeyCode.Q)){
            Tri.GetComponent<Tri_State>().turn_left();
		}
		if(Input.GetKeyDown(KeyCode.E)){
            Tri.GetComponent<Tri_State>().turn_right();
        }
	}
    
    public void Initiate() // 초기화.(원 , L_state)
    {
        Circle_b.SetActive(false);
        Circle_h.SetActive(false);
        Tri.GetComponent<Line_State>().SetLstate(Line_State.Lstate.idle);
    }

}
