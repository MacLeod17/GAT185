using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    public float speed = 20;
    public float turnRate = 180;

    Rigidbody rb;
    ParticleSystem ps;
    Vector2 inputDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        rb.AddTorque(Vector3.up * inputDirection.x * turnRate);
        rb.AddRelativeForce(Vector3.forward * speed * inputDirection.y);

        var main = ps.main;
        main.startLifetime = inputDirection.y;
    }

    public void OnMove(InputValue input)
    {
        inputDirection = input.Get<Vector2>();
    }
}
