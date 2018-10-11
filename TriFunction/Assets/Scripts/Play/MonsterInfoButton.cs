using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfoButton : MonoBehaviour {

    public GameObject MonsterInfo;
    public GameObject MonsterInfoUpButton;
    public GameObject MonsterInfoDownButton;

    void OnMouseDown()
    {
        if(gameObject.name == "monster_infoUpButton")
        {
            MonsterInfo.SetActive(true);
            MonsterInfoDownButton.SetActive(true);
            MonsterInfoUpButton.SetActive(false);
        }
        else
        {
            MonsterInfo.SetActive(false);
            MonsterInfoUpButton.SetActive(true);
            MonsterInfoDownButton.SetActive(false);
        }
    }
}
