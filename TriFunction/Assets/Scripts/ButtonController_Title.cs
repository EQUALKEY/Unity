using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController_Title : MonoBehaviour {
    public GameObject[] HelpContents = new GameObject[10];
    public GameObject LeftButton;
    public GameObject RightButton;
    public int Page;

    public void GameClose_Title() {
        Application.OpenURL("https://www.quebon.tv/game/triFunction/exit");
    }

    public void StoryModeStart() {
        if (!PlayerPrefs.HasKey("isMonsterTypeOff")) PlayerPrefs.SetInt("isMonsterTypeOff", 0);
        PlayerPrefs.SetInt("Mode", 0);
        SceneManager.LoadScene("Play");
    }
    
    public void InfinityModeStart() {
        if (!PlayerPrefs.HasKey("isMonsterTypeOff")) PlayerPrefs.SetInt("isMonsterTypeOff", 0);
        PlayerPrefs.SetInt("Mode", 1);
        SceneManager.LoadScene("Play");
    }

    public void SetFirstPage() {
        Page = 0;
        HelpContents[Page].SetActive(true);
        for (int i = 1; i < 10; i++) HelpContents[i].SetActive(false);
        LeftButton.SetActive(false);
        RightButton.SetActive(true);
    }

    public void RightPage() {
        if (Page == 0) LeftButton.SetActive(true);
        HelpContents[Page].SetActive(false);
        Page++;
        HelpContents[Page].SetActive(true);
        if (Page == 9) RightButton.SetActive(false);
    }

    public void LeftPage() {
        if (Page == 9) RightButton.SetActive(true);
        HelpContents[Page].SetActive(false);
        Page--;
        HelpContents[Page].SetActive(true);
        if (Page == 0) LeftButton.SetActive(false);
    }
}
