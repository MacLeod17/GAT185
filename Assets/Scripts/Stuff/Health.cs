using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float healthMax;
    public float decay;
    public Slider slider;
    public GameObject destroySpawnObject;
    public UnityEvent deathEvent;
    public bool destroyOnDeath = false;

    public float health;
    public bool isDead { get; set; } = false;
    float timer = 1.0f;

    void Start()
    {
        health = healthMax;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (Game.Instance != null)
        {
            if (timer <= 0 && Game.Instance.State == Game.eState.Game)
            {
                Game.Instance.AddPoints(5);
                timer = 1.0f;
            }
        }

        if (slider != null)
        {
            if (Game.Instance.State == Game.eState.Game)
            {
                AddHealth(-Time.deltaTime * decay);
                slider.value = health / healthMax;
            }
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            if (deathEvent != null) deathEvent.Invoke();
            if (destroySpawnObject != null)
            {
                Instantiate(destroySpawnObject, transform.position, Quaternion.identity);
            }
            if (destroyOnDeath) Destroy(gameObject);
        }
    }

    public void AddHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, healthMax);
    }
}
