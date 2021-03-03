using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShip : MonoBehaviour
{
    public EnemyShipData enemyShipData;

    Rigidbody rb;
    GameObject targetGameObject;
    Weapon weapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
        targetGameObject = GameObject.FindGameObjectWithTag(enemyShipData.targetTag);
    }

    void Update()
    {
        Vector3 direction = targetGameObject.transform.position - transform.position;
        Vector3 cross = Vector3.Cross(transform.forward, direction.normalized);

        rb.AddTorque(Vector3.up * cross.y * enemyShipData.turnRate);
        rb.AddRelativeForce(Vector3.forward * enemyShipData.speed);

        if (direction.magnitude < enemyShipData.fireRange)
        {
            weapon.Fire(transform.forward);
        }
    }


}
