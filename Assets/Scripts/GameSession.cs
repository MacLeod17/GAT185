using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int HighScore { get; set; } = 0;

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI timerUI;

    public GameObject startScreen;
    public UnityEvent startSessionEvents;
    public GameObject gameOverScreen;

    public GameObject player;

    static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }

    float timer = 20.0f;

    public enum eState
    {
        StartSession,
        Session,
        EndSession,
        GameOver
    }

    public eState State { get; set; } = eState.StartSession;
    //public eState State { get; set; } = eState.Session;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        switch (State)
        {
            case eState.StartSession:
                timer = 20.0f;
                Score = 0;
                startSessionEvents?.Invoke();
                State = eState.Session;
                break;
            case eState.Session:
                break;
            case eState.GameOver:
                break;
            default:
                break;
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

    private void IncrementTimer()
    {
        timer -= Time.deltaTime;
        timerUI.text = string.Format("{0:d2}", (int)timer);
        if (timer <= 0)
        {
            State = eState.GameOver;
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
