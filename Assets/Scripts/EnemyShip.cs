using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShip : MonoBehaviour
{
    public float speed = 5;
    public float turnRate = 18;
    public string targetTag;

    Rigidbody rb;
    GameObject targetGameObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetGameObject = GameObject.FindGameObjectWithTag(targetTag);
    }

    void Update()
    {
        Vector3 direction = targetGameObject.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);

        Quaternion rotation = Quaternion.LookRotation(direction);

        //rb.AddTorque(rotation.eulerAngles);
        //rb.AddTorque(Vector3.up * turnRate * angle);
        //rb.AddRelativeForce(Vector3.forward * speed);

        rb.AddForce(direction.normalized * speed);
    }
}
