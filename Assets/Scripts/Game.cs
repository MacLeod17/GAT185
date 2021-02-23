using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int HighScore { get; set; } = 0;

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI timerUI;

    public GameObject startScreen;
    public GameObject gameOverScreen;
    public AudioSource music;

    public GameObject player;

    static Game instance = null;

    float timer = 20.0f;

    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    public eState State { get; set; } = eState.Title;
    //public eState State { get; set; } = eState.StartGame;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        switch (State)
        {
            case eState.Title:
                startScreen.SetActive(true);
                gameOverScreen.SetActive(false);
                break;
            case eState.StartGame:
                timer = 20.0f;
                Score = 0;
                scoreUI.text = string.Format("{0:D4}", Score);
                music?.Play();
                startScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                State = eState.Game;
                break;
            case eState.Game:
                //IncrementTimer();
                if (CheckDeath())
                {
                    State = eState.GameOver;
                    music?.Stop();
                }
                break;
            case eState.GameOver:
                gameOverScreen.SetActive(true);
                break;
            default:
                break;
        }        
    }

    public static Game Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddPoints(int points)
    {
        Score += points;
        scoreUI.text = string.Format("{0:D4}", Score);

        //SetHighScore();
    }

    private void SetHighScore()
    {
        if (Score > HighScore) HighScore = Score;
        highScoreUI.text = string.Format("{0:D4}", HighScore);
    }

    public void StartGame()
    {
        State = eState.StartGame;
    }

    private void IncrementTimer()
    {
        timer -= Time.deltaTime;
        timerUI.text = string.Format("{0:d2}", (int)timer);
        if (timer <= 0)
        {
            State = eState.GameOver;
            music?.Stop();
        }
    }

    private bool CheckDeath()
    {
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            if (playerHealth.health <= 0)
            {
                return true;
            }
        }
        return false;
    }
}
