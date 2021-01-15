using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0.1f;
    public Bullet bullet;

    int ammo = 100;
    float fireTimer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Bullet b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<Bullet>().Fire(ray.direction);
        }*/
    }

    public bool Fire(Vector3 position, Vector3 direction)
    {
        if (fireTimer >= fireRate && ammo > 0)
        {
            fireTimer = 0;
            ammo--;

            Bullet b = Instantiate(bullet, position, Quaternion.identity);
            b.GetComponent<Bullet>().Fire(direction);

            return true;
        }

        return false;
    }
}
