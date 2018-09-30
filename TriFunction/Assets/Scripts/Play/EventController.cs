using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

    public bool isOutofTriRange;
    public GameObject Tri;


    Vector3 standardPosition;
    Vector3 curScreenSpace;
    Vector3 scrSpace;
    Vector3 offset;
    Vector3 curPosition;
    float standardRotation;
    float standardw;
    // Use this for initialization
    void Start () {
        isOutofTriRange = true;
	}
	
	// Update is called once per frame
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
