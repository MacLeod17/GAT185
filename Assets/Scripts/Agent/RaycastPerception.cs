using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPerception : Perception
{
    public Transform raycastTransform;
    [Range(1, 50)] public float distance = 1;
    [Range(0, 180)] public float angle = 10;
    [Min(2)] public int numRaycast = 2;

    public override GameObject[] GetGameObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>();

        float angleOffset = (angle * 2.0f) / (numRaycast - 1);

        for (int i = 0; i < numRaycast; i++)
        {
            float rayDistance = distance;

            Quaternion rotation = Quaternion.AngleAxis(-angle + (angleOffset * i), Vector3.up);
            Vector3 forward = rotation * raycastTransform.forward;

            Ray ray = new Ray(raycastTransform.position, forward);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, rayDistance))
            {
                rayDistance = raycastHit.distance;
                gameObjects.Add(raycastHit.collider.gameObject);
            }
            Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        }

        return gameObjects.ToArray();
    }
}
