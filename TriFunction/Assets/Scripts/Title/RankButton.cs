using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankButton : MonoBehaviour {
    public GameObject RankImage;
    public GameObject CloseButton;

    void OnMouseDown()
    {
        RankImage.SetActive(true);
        CloseButton.SetActive(true);
    }

}
