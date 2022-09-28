using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Panel List")]
    public GameObject MainPanel;
    public GameObject HTPPanel;
    public GameObject TimerPanel;
    public GameObject BallPanel;
    public GameObject Splash;
    // Start is called before the first frame update
    void Start()
    {
        Splash.SetActive(true);
        StartCoroutine("DelayMainPanel");
        MainPanel.SetActive(false);
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        BallPanel.SetActive(false);
        SoundManager.instance.UIClicksfx();
    }

    public IEnumerator DelayMainPanel()
    {
        yield return new WaitForSeconds(3);
        Splash.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void SinglePlayerButton()
    {
        GameData.instance.isSinglePlayer = true;
        MainPanel.SetActive(false);
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClicksfx();
    }
    public void MultiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        MainPanel.SetActive(false);
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClicksfx();
    }
    public void SetTimerButton(float Timer)
    {
        GameData.instance.gameTimer = Timer;
        TimerPanel.SetActive(false);
        BallPanel.SetActive(true);
        SoundManager.instance.UIClicksfx();
    }

    public void SetBallButton(int ball)
    {
        GameData.instance.Ball = ball;
        BallPanel.SetActive(false);
        HTPPanel.SetActive(true);
        SoundManager.instance.UIClicksfx();
    }

    public void BackButton()
    {
        MainPanel.SetActive(true);
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        BallPanel.SetActive(false);
        SoundManager.instance.UIClicksfx();
    }

    public void SoundButton()
    {
        SoundManager.instance.Mute();
    }

    public void Ball1Btn()
    {
        SoundManager.instance.Mute();
    }

    public void Ball2Btn()
    {
        SoundManager.instance.Mute();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClicksfx();
    }
    public void ExitGame()
    {
        Application.Quit();
        SoundManager.instance.UIClicksfx();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
