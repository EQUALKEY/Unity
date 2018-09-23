using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri_State : MonoBehaviour { // 삼각형이 q를 누르면 왼쪽, e를 누르면 오른쪽으로 회전함


	public enum Tstate{
		idle=0,
		tleft,
		tright
	}

    private Tstate Tristate;
    // Use this for initialization
    void Start () {
		Tristate = Tstate.idle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void turn_left(){
        if (Tristate == Tstate.idle)
        {
            Tristate = Tstate.tleft;
        }
        else if (Tristate == Tstate.tleft)
        {
            Tristate = Tstate.tright;
        }
        else if (Tristate == Tstate.tright)
        {
            Tristate = Tstate.idle;
        }
        SetRotation();
    }

	public void turn_right(){
		if(Tristate==Tstate.idle){
			Tristate=Tstate.tright;
		}
		else if(Tristate==Tstate.tleft){
			Tristate=Tstate.idle;
		}else if(Tristate==Tstate.tright){
			Tristate=Tstate.tleft;
        }
        SetRotation();
    }

    private void SetRotation() // rotation 값 설정
    {
        if(Tristate == Tstate.idle)
        {
            transform.SetPositionAndRotation(new Vector3(-5.1f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        }
        else if(Tristate == Tstate.tleft)
        {
            transform.SetPositionAndRotation(new Vector3(-5.1f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            transform.Rotate(new Vector3(0f, 0f, 153f));
        }
        else if(Tristate == Tstate.tright)
        {
            transform.SetPositionAndRotation(new Vector3(-5.1f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            transform.Rotate(new Vector3(0f, 0f, -90f));
        }
    }

    public Tstate GetTstate() // Tristate 값을 반환
    {
        return Tristate;
    }
}
