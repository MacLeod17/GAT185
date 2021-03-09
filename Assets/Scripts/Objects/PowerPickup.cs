using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : Pickup
{
    [Range(1, 120)] public int colorChangeRate = 1;
    public Material material;

    private int currentFrame = 0;

    MeshRenderer objRenderer;

    private Material currentMaterial;

    private void Start()
    {
        objRenderer = GetComponent<MeshRenderer>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        Character player = other.gameObject.GetComponent<Character>();

        if (player != null)
        {
            GameSession.Instance.AddPoints((int)amount);

            SpawnObject(other);
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (material == null) return;

        currentFrame++;
        if (currentFrame == colorChangeRate)
        {
            currentFrame = 0;

            float r = Random.Range(0, 255);
            float g = Random.Range(0, 255);
            float b = Random.Range(0, 255);
            Debug.Log("Test");

            //objRenderer.material.color = new Color(r, g, b);
            objRenderer.material.color = new Color(r, g, b);

            //currentMaterial = new Material(Shader.Find("Diffuse"));
            //currentMaterial.color = new Color(r, g, b);
            //objRenderer.material = currentMaterial;
            //objRenderer.material.color = Color.Lerp(objRenderer.material.color, Color.red, 1);
            //prevColor = objRenderer.material.color;
        }
    }
}
