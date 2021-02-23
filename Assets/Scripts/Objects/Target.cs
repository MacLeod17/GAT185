using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int[] points;
    public Material[] material;
    public GameObject destroyGameObject;

    int targetIndex;

    private void Start()
    {
        targetIndex = Random.Range(0, points.Length);
        GetComponent<Renderer>().material = material[targetIndex];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject, 1);
            AudioManager.Instance.PlayAudio(0);

            // Add Score to Game
            Game.Instance.AddPoints(points[targetIndex]);
            if (destroyGameObject != null) Destroy(destroyGameObject);
        }
    }
}
