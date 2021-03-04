using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    public override void OnTriggerEnter(Collider other)
    {
        Character player = other.gameObject.GetComponent<Character>();
        Movement movement = other.gameObject.GetComponent<Movement>();

        if (player != null)
        {
            player.speed += amount;
            Mathf.Clamp(player.speed, 0, 20);

            SpawnObject(other);
            Destroy(gameObject);
        }
        else if (movement != null)
        {
            movement.speedMax += amount;
            movement.accelerationMax += (amount / 2.0f);
            Mathf.Clamp(movement.speedMax, 1, 25);
            Mathf.Clamp(movement.accelerationMax, 1, 25);

            SpawnObject(other);
            Destroy(gameObject);
        }
    }
}
