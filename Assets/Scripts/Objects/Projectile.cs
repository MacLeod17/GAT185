using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Range(0, 5)] public float lifespan = 2;
    [Range(1, 100)] public float speed = 10.0f;
    [Range(-180, 180)] public float angle = 0;
    public bool destroyOnHit = false;
    public GameObject destroyFX;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    private void OnDestroy()
    {
        if (destroyFX != null)
        {
            Instantiate(destroyFX, transform.position, transform.rotation);
        }
    }

    public void Fire(Vector3 forward)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();

        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        rigidBody.AddForce(forward * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
