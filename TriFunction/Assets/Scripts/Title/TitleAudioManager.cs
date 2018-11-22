using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudioManager : MonoBehaviour {

    private void Awake()
    {
    }

    public void SoundOn()
    {
        AudioListener.volume = 1f;
    }
    public void SoundOff()
    {
        AudioListener.volume = 0f;
    }
}
