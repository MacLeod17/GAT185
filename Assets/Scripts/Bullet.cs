using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Range(0, 5)] public float lifespan = 2;
    [Range(1, 100)] public float speed = 10.0f;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    public void Fire(Vector3 forward)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(forward * speed, ForceMode.VelocityChange);
    }
}
