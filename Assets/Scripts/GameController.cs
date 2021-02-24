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
    public Transition transition;

    public AudioMixer audioMixer;

    public int highScore = 0;

    bool isPaused = false;
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

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        PlayerPrefs.SetInt("HighScore", highScore);

        Debug.Log(highScore);
    }

    public void SetHighScore(int score)
    {
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void OnLoadGameScene(string scene)
    {
        titleScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //transition.StartTransition(Color.black, 2, scene);

        SceneManager.LoadScene(scene);
    }

    public void OnLoadMenuScene(string scene)
    {
        titleScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnPause()
    {
        OnPauseScreen();
    }

    public void OnMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
    }

    public void OnMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }

    public void OnSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);
    }
}
