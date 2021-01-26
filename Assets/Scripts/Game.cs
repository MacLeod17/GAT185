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

    static Game instance = null;

    float timer = 60.0f;

    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    //public eState State { get; set; } = eState.Title;
    public eState State { get; set; } = eState.StartGame;

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
                break;
            case eState.StartGame:
                timer = 60.0f;
                Score = 0;
                scoreUI.text = string.Format("{0:D4}", Score);
                music?.Play();
                startScreen.SetActive(false);
                gameOverScreen.SetActive(false);
                State = eState.Game;
                break;
            case eState.Game:
                timer -= Time.deltaTime;
                timerUI.text = string.Format("{0:d2}", (int)timer);
                if (timer <= 0)
                {
                    music?.Stop();
                    State = eState.GameOver;
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
        if (Score > HighScore) HighScore = Score;
        scoreUI.text = string.Format("{0:D4}", Score);
        highScoreUI.text = string.Format("{0:D4}", HighScore);
    }

    public void StartGame()
    {
        State = eState.StartGame;
    }
}
