using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject optionsScreen;
    public GameObject pauseScreen;

    public AudioMixer audioMixer;

    bool isPaused;
    float timeScale;

    static GameController instance = null;
    public GameController Instance { 
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        titleScreen.SetActive(true);
    }

    public void OnLoadGameScene(string scene)
    {
        SceneManager.LoadScene(scene);
        titleScreen.SetActive(false);
    }

    public void OnLoadMenuScene(string scene)
    {
        titleScreen.SetActive(true);
        SceneManager.LoadScene(scene);
    }

    public void OnTitleScreen()
    {
        titleScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void OnOptionsScreen()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void OnPauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = timeScale;
        }
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    }

    public void OnPause()
    {
        OnPauseScreen();
    }
}
