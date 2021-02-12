using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Range(0, 3)] public float fireRate = 0.5f;
    [Range(0, 100)] public float angle = 10.0f;

    public int ammo = 100;
    public Projectile projectile;
    public Transform emitTransform;

    float fireTimer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public bool Fire(Vector3 position, Vector3 direction)
    {
        if (fireTimer >= fireRate)// && ammo > 0)
        {
            fireTimer = 0;
            //ammo--;

            Projectile b = Instantiate(projectile, position, Quaternion.identity);
            b.GetComponent<Projectile>().Fire(direction);

            return true;
        }

        return false;
    }

    public bool Fire(Vector3 direction)
    {
        if (fireTimer >= fireRate)// && ammo > 0)
        {
            fireTimer = 0;
            //ammo--;

            Projectile b = Instantiate(projectile, emitTransform.position, emitTransform.rotation);
            b.GetComponent<Projectile>().Fire(direction);

            return true;
        }

        return false;
    }
}
