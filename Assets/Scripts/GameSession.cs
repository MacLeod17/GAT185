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

    public Slider slider;

    public UnityEvent startSessionEvents;
    public GameObject gameOverScreen;

    GameObject player;
    Health playerHealth;

    static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }

    float timer = 40.0f;

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

    private void Start()
    {
        
    }

    private void Update()
    {
        switch (State)
        {
            case eState.StartSession:
                if (gameOverScreen != null) gameOverScreen.SetActive(false);
                timer = 40.0f;
                Score = 0;
                if (player != null)
                {
                    Destroy(player);
                    player = null;
                }
                GameController.Instance.transition.StartTransition(Color.clear, 1);
                EventManager.Instance.TriggerEvent("StartSession");
                startSessionEvents?.Invoke();
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    playerHealth = player.GetComponent<Health>();
                    playerHealth.slider = slider;
                }
                State = eState.Session;
                break;
            case eState.Session:
                CheckDeath();
                break;
            case eState.EndSession:
                State = eState.GameOver;
                break;
            case eState.GameOver:
                if (gameOverScreen != null) gameOverScreen.SetActive(true);
                break;
            default:
                break;
        }        
    }

    public void AddPoints(int points)
    {
        Score += points;
        if (scoreUI != null) scoreUI.text = string.Format("{0:D4}", Score);

        //SetHighScore();
    }

    public void StartSession()
    {
        State = eState.StartSession;
    }

    private void SetHighScore()
    {
        if (Score > HighScore) HighScore = Score;
        if (highScoreUI != null) highScoreUI.text = string.Format("{0:D4}", HighScore);
    }

    private void IncrementTimer()
    {
        timer -= Time.deltaTime;
        if (timerUI != null) timerUI.text = string.Format("{0:d2}", (int)timer);
        if (timer <= 0)
        {
            State = eState.EndSession;
        }
    }

    private void CheckDeath()
    {
        if (playerHealth != null)
        {
            if (playerHealth.health <= 0)
            {
                State = eState.EndSession;
            }
        }
    }
}
