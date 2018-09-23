using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_touch : MonoBehaviour {

    public GameObject EventContoller;

    private void OnMouseDown() // 선 하나를 클릭했다가 다른 선이 아닌 빈 화면을 눌렀을 때 초기화.
    {
        EventContoller.GetComponent<GamePlay>().Initiate();
    }
}
