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
    }

    void OnMouseDown()
    {
        if(ec.isMonsterTypeOn)
        {
            TypeOn.SetActive(true);
            TypeOff.SetActive(false);
            ec.isMonsterTypeOn = false;
        }
        else
        {
            TypeOn.SetActive(false);
            TypeOff.SetActive(true);
            ec.isMonsterTypeOn = true;
        }
    }
}
