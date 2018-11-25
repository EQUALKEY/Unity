using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour {
	public GameObject HelpWindow;
    public GameObject CloseButton;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject[] Pages = new GameObject[10];

    public int HelpPage;

    void Init()
    {
        HelpPage = 0;
        HelpWindow.SetActive(true);
        CloseButton.SetActive(true);
        LeftButton.SetActive(false);
        RightButton.SetActive(true);
        Pages[0].SetActive(true);
        for (int i = 1; i < 10; i++) Pages[i].SetActive(false);
    }

    public void NextPage()
    {
        if (HelpPage == 0)
        {
            LeftButton.SetActive(true);
        }
        else if (HelpPage == 8)
        {
            RightButton.SetActive(false);
        }
        Pages[HelpPage++].SetActive(false);
        Pages[HelpPage].SetActive(true);
    }

    public void PrevPage()
    {
        if (HelpPage == 1)
        {
            LeftButton.SetActive(false);
        }
        else if (HelpPage == 9)
        {
            RightButton.SetActive(true);
        }
        Pages[HelpPage--].SetActive(false);
        Pages[HelpPage].SetActive(true);
    }

	void OnMouseDown() {
        if (PlayerPrefs.GetInt("WindowOn") == 0) {
            PlayerPrefs.SetInt("WindowOn", 1);
            Init();
        }
	}
}