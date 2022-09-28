using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static  MainMenuManager instance;

    [Header ("GameSetting")]
    public float p1score;
    public float p2score;
    public float timer;
    public int ball;
    public bool isOver;
    public bool goldenGoal;
    public bool isSpawnPowerUp;
    public GameObject ballSpawned;

    [Header ("Prefab")]
    public GameObject[] BallPrefab;
    public GameObject[] PowerUp;

    [Header ("Panels")]
    public GameObject GoldenGoal;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    [Header("InGame UI")]
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI player1ScoreTxt;
    public TextMeshProUGUI player2ScoreTxt;

    [Header ("Game Over UI")]
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject youWin;
    public GameObject youLose;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);

        player2WinUI.SetActive(false);
        player1WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        GoldenGoal.SetActive(false);
        
        timer = GameData.instance.gameTimer;
        ball = GameData.instance.Ball;
        isOver = false;
        goldenGoal=false;
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        player1ScoreTxt.text = p1score.ToString();
        player2ScoreTxt.text = p2score.ToString();
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timer/60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if(seconds % 20 == 0 && !isSpawnPowerUp)
            {
                StartCoroutine("SpawnPowerUp");
            }

            if(timer <= 0f && !isOver)
            {
                timerTxt.text = "00:00";
                if (p1score == p2score)
                {
                    if(!goldenGoal)
                    {
                        goldenGoal = true;
                        goldengoal();

                        Ball[] ball = FindObjectsOfType<Ball>();
                        for( int i=0;i<ball.Length;i++)
                        {
                            Destroy(ball[i].gameObject);
                        }

                        SpawnBall();
                    }
                }
                else
                {
                    GameOver();
                }
            }
        }
    }

    public IEnumerator SpawnPowerUp()
    {
        isSpawnPowerUp = true;
        Debug.Log("Power Up");
        int rand = Random.Range(0,PowerUp.Length);
        Instantiate(PowerUp[rand], new Vector3(Random.Range(186f, 195f), Random.Range(228f, 236f), 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isSpawnPowerUp = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        SoundManager.instance.UIClicksfx();
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.UIClicksfx();
    }

    public void BackToMenu()
    {
        SoundManager.instance.UIClicksfx();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

    }
    public void Restart()
    {
        SoundManager.instance.UIClicksfx();
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");

    }
    public void SpawnBall()
    {
        Debug.Log("Muncul Bola");
        StartCoroutine("DelaySpawn");
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        if(ballSpawned == null)
        {
            ballSpawned = Instantiate(BallPrefab[ball], new Vector3(190f, 232f, 0), Quaternion.identity);
        }
    }
    
    public void goldengoal()
    {
        Debug.Log("GoldenGoal");
        StartCoroutine("StartGoldenGoal");
    }

    private IEnumerator StartGoldenGoal()
    {
        GoldenGoal.SetActive(true);
        yield return new WaitForSeconds(4);
        GoldenGoal.SetActive(false);
    }
    public void GameOver()
    {
        SoundManager.instance.GameOversfx();
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0;


        GameOverPanel.SetActive(true);
    

        if(!GameData.instance.isSinglePlayer)
        {
            if(p1score > p2score)
            {
                player1WinUI.SetActive(true);
            }
            if(p1score < p2score)
            {
                player2WinUI.SetActive(true);
            }
        }
        else
        {
            if(p1score > p2score)
            {
                youWin.SetActive(true);
            }
            if(p1score < p2score)
            {
                youWin.SetActive(true);
            }
        }
    }
    
    
}
