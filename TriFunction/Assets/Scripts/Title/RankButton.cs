using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankButton : MonoBehaviour {
    public GameObject RankImage;
    public GameObject CloseButton;
    public GameObject EC;
    private RankManager RM;

    private void Awake()
    {
        RM = EC.GetComponent<RankManager>();
    }
    void OnMouseDown()
    {
        RM.Make_RankBox_top5andMine();
        RM.RankBox_OnlyMine.SetActive(false);
        RankImage.SetActive(true);
        CloseButton.SetActive(true);
    }

}
