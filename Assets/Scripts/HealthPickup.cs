using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float amount = 10;
    public GameObject spawnObject;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(amount);
            if (spawnObject != null)
            {
                GameObject gameObject = Instantiate(spawnObject, other.transform.position, other.transform.rotation, other.transform);
                Destroy(gameObject, 2);
            }

            Destroy(gameObject);
        }
    }
}
