using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTypeButton : MonoBehaviour {

    public GameObject TypeOn;
    public GameObject TypeOff;
    public GameObject EC;
    private EventController ec;

    void Awake()
    {
        ec = EC.GetComponent<EventController>();
        if (PlayerPrefs.GetInt("isMonsterTypeOn") == 1) {
            TypeOn.SetActive(false);
            TypeOff.SetActive(true);
        } else {
            TypeOn.SetActive(true);
            TypeOff.SetActive(false);
        }
    }

    void OnMouseDown() {
        if(ec.isMonsterInfoOn) {
            PlayerPrefs.SetInt("isMonsterTypeOn", 0);
            ec.isMonsterInfoOn = false;
            TypeOn.SetActive(true);
            TypeOff.SetActive(false);
        } else {
            PlayerPrefs.SetInt("isMonsterTypeOn", 1);
            ec.isMonsterInfoOn = true;
            TypeOn.SetActive(false);
            TypeOff.SetActive(true);
        }
    }
}
