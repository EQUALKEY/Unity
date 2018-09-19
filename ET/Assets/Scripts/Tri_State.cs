using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tri_State : MonoBehaviour {

	private int Tri_state;

	enum state{
		idle,
		tleft,
		tright
	}

	// Use this for initialization
	void Start () {
		Tri_State = state.idle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void trun_left(){
		if(Tri_state=state.idle){
			Tri_state=state.tleft;
			transform.Rotate();
		}
		else if(Tri_state=state.tleft){
			Tri_state=state.tright;
			transform.Rotate();
		}else if(Tri_state=state.tright){
			Tri_state=state.idle;
			transform.Rotate();
		}
	}

	public void turn_right(){
		if(Tri_state=state.idle){
			Tri_state=state.tright;
			transform.Rotate();
		}
		else if(Tri_state=state.tleft){
			Tri_state=state.idle;
			transform.Rotate();
		}else if(Tri_state=state.tright){
			Tri_state=state.tleft;
			transform.Rotate();
		}
	}
}
