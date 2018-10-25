using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    /// <summary>
    /// [ 효과음 정리 ]
    ///  
    /// 화살 발사 소리
    /// 창 소리
    /// 몬스터가 맞는 소리
    /// 
    /// 깨다가 맞는 소리
    /// 
    /// 필살기 썼을 때 소리
    /// 
    /// BGM
    /// 
    /// 
    /// </summary>
	// Use this for initialization

    public AudioSource MonsterHited;

    public AudioSource Damaged;

    public AudioSource GameOver;

    public AudioSource BGM;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isSoundOn")==1)
        {
            SoundOn();
        }
        else
        {
            SoundOff();
        }
    }

    public void SoundOn()
    {
        AudioListener.volume = 1f;
    }
    public void SoundOff()
    {
        AudioListener.volume = 0f;
    }

    public void MonsterHitSound()
    {
        MonsterHited.Play();
    }

    public void DamagedSound()
    {
        Damaged.Play();
    }
    public void GameOverSound()
    {
        BGM.Stop();
        GameOver.Play();
    }
}
