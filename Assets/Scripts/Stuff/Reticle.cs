using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;
        transform.position = Input.mousePosition;
    }
}
