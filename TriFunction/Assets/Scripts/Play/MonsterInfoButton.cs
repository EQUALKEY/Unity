using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfoButton : MonoBehaviour {

    public GameObject MonsterInfo;
    bool isInfoOn;

    void Awake()
    {
        isInfoOn = true;
    }

    void OnMouseDown()
    {
        if (isInfoOn)
        {
            MonsterInfo.SetActive(false);
            isInfoOn = false;
        }
        else
        {
            MonsterInfo.SetActive(true);
            isInfoOn = true;
        }
    }
}
