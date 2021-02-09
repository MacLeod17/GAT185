using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float decay;
    public Slider slider;

    public float health;
    float timer = 1.0f;

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && Game.Instance.State == Game.eState.Game)
        {
            Game.Instance.AddPoints(5);
            timer = 1.0f;
        }

        if (Game.Instance.State == Game.eState.Game)
        {
            AddHealth(-Time.deltaTime * decay);
            slider.value = health / healthMax;
        }
    }

    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, healthMax);
    }
}
