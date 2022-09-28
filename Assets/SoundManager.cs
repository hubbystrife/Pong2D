using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip uiBtn;
    public AudioClip ballBounce;
    public AudioClip goal;
    public AudioClip gameOver;
    public AudioClip PowerUp;
    private int i = 0;

    private AudioSource audio;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void UIClicksfx()
    {
        audio.PlayOneShot(uiBtn);
    }

    public void BallBouncesfx()
    {
        audio.PlayOneShot(ballBounce);
    }

    public void Goalsfx()
    {
        audio.PlayOneShot(goal);
    }

    public void GameOversfx()
    {
        audio.PlayOneShot(gameOver);
    }

    public void PowerUpsfx()
    {
        audio.PlayOneShot(PowerUp);
    }

    public void Mute()
    {
        i += 1;
        audio.mute = true;
        if (i % 2 == 0)
        {
            audio.mute = false;
        }
    }

}
