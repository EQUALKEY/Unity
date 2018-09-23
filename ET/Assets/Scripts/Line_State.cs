using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line_State : MonoBehaviour { // 현재 활성화 되어있는 선을 저장

    public enum Lstate
    {
        idle = 0,
        Base,
        Hypotenuse,
        Height
    }

    private Lstate L_state;

    void Start()
    {
        L_state = Lstate.idle;
    }

    public Lstate GetLstate() // 지금 선의 상태 반환
    {
        return L_state;
    }
    public void SetLstate(Lstate input_state) // 선을 input_state로 변경.
    {
        L_state = input_state;
    }
}
