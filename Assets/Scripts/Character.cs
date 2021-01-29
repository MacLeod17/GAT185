using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [Range(0, 20)] public float speed = 1;
    [Range(0, 20)] public float jump = 1;
    [Range(-20, 20)] public float gravity = -9.8f;
    public Animator animator;

    CharacterController characterController;
    Vector3 inputDirection;
    Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool onGround = characterController.isGrounded;
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0;
        }

        inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.z = Input.GetAxis("Vertical");

        Quaternion cameraRotation = Camera.main.transform.rotation;
        Quaternion rotation = Quaternion.AngleAxis(cameraRotation.eulerAngles.y, Vector3.up);
        Vector3 direction = rotation * inputDirection;

        characterController.Move(direction * speed * Time.deltaTime);

        if (inputDirection.magnitude > 0.1f)
        {
            //transform.forward = inputDirection.normalized;
            Quaternion target = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 5.0f);
        }
        animator.SetFloat("Speed", inputDirection.magnitude);

        // Jump
        if (Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y += jump;
        }

        // Gravity Movement
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
