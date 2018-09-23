using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri_State : MonoBehaviour { // 삼각형이 q를 누르면 왼쪽, e를 누르면 오른쪽으로 회전함

    public GameObject EventContoller;
	public enum Tstate{
		idle=0,
        co
	}

    private Tstate TriState; // 삼각형 회전 상태 저장
    // Use this for initialization
    void Start () {
		TriState = Tstate.idle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void turn(){
        if (TriState == Tstate.idle)
        {
            TriState = Tstate.co;
        }
        else if (TriState == Tstate.co)
        {
            TriState = Tstate.idle;
        }
        SetRotation();
    }

    private void SetRotation() // rotation 값 설정
    {
        EventContoller.GetComponent<GamePlay>().Initiate();
        if(TriState == Tstate.idle)
        {
            transform.SetPositionAndRotation(new Vector3(-5.1f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        }
        else if(TriState == Tstate.co)
        {
            transform.SetPositionAndRotation(new Vector3(-5.1f, -1.2f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            transform.Rotate(new Vector3(0f, 0f, 90f));
        }
    }

    public Tstate GetTstate() // TriState 값을 반환
    {
        return TriState;
    }

    public void SetTstate(Tstate input_state) // 선을 input_state로 변경.
    {
        TriState = input_state;
    }
}
