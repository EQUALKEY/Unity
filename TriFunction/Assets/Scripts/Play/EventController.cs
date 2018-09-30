using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public GameObject Tri;
    public bool isOutofTriRange;
    // int형태로 Tstate변수에 변활성화상태 저장
    public int Tstate; // 활성화X = 0, Hypo = 1, Height = 2, Base = 3
    // bool형태로 isCo변수에 각도활성화상태 저장
    public bool isCo; // 기본각 = false, Co각 = true;

    private Vector3 standardPosition;
    private Vector3 curScreenSpace;
    private Vector3 scrSpace;
    private Vector3 offset;
    private Vector3 curPosition;
    private float standardRotation;
    private float standardw;

    void Awake () {
        Tstate = 0;
        isCo = false;
        isOutofTriRange = true;
	}
	
	void Update () {
        if (isOutofTriRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                scrSpace = Camera.main.WorldToScreenPoint(transform.position);

                curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
                standardPosition =  Camera.main.ScreenToWorldPoint(curScreenSpace) ;
                standardRotation = Tri.transform.rotation.z;
                standardw = Tri.transform.rotation.w;
            }
            if (Input.GetMouseButton(0)){
                curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
                curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) ;

                Debug.Log(Mathf.Atan2(curPosition.y - Tri.transform.position.y, curPosition.x - Tri.transform.position.x) 
                    + "   "+ Mathf.Atan2(standardPosition.y - Tri.transform.position.y, standardPosition.x - Tri.transform.position.x));


                Tri.transform.rotation = new Quaternion (0f,0f,Mathf.Atan2(curPosition.y-Tri.transform.position.y,curPosition.x-Tri.transform.position.x)
                    -Mathf.Atan2(standardPosition.y-Tri.transform.position.y,standardPosition.x-Tri.transform.position.x) + standardRotation,standardw);

            }
        }
	}
}
