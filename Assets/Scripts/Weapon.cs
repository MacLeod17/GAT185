using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bullet;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Bullet b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<Bullet>().Fire(ray.direction);
        }
    }
}
