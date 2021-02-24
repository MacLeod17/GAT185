using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public RawImage image;
    public float time;
    public Color color;
    public bool startOnAwake;

    Color startColor;

    void Start()
    {
        if (startOnAwake)
        {
            StartTransition(color, time);
        }
    }

    public void StartTransition(Color color, float time, string sceneName = "")
    {
        this.color = color;
        this.time = time;
        startColor = image.color;

        StartCoroutine(TransitionRoutine(this.time, sceneName));
    }

    IEnumerator TransitionRoutine(float timer, string sceneName)
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float t = timer / time;
            image.color = Color.Lerp(color, startColor, t);

            yield return null;
        }

        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }

        yield return null;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Wait 1");
        yield return new WaitForSeconds(time);
        Debug.Log("Wait 2");

        yield return null;
    }

    IEnumerator Timer(float time)
    {
        Debug.Log("Start");
        while (time > 0)
        {
            time -= Time.deltaTime;
            Debug.Log(time);
            yield return null;
        }
    }
}
