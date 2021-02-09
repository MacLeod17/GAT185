using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [Range(0, 20)] public float speed = 1;
    [Range(0, 20)] public float jump = 1;
    [Range(-20, 20)] public float gravity = -9.8f;
    public Animator animator;
    public Weapon weapon;

    CharacterController characterController;
    bool onGround = false;
    Vector3 inputDirection;
    Vector3 velocity;
    Health health;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (Game.Instance != null && Game.Instance.State == Game.eState.StartGame)
        {
            health.health = health.healthMax;
        }

        onGround = characterController.isGrounded;
        if (onGround && velocity.y < 0)
        {
            velocity.y = 0;
        }

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
        // Animator
        animator.SetFloat("Speed", inputDirection.magnitude);
        animator.SetBool("OnGround", onGround);
        animator.SetFloat("VelocityY", velocity.y);

        // Gravity Movement
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (health.health <= 0)
        {
            inputDirection = Vector3.zero;
            animator.SetFloat("Speed", inputDirection.magnitude);
            animator.SetTrigger("Death");
        }
    }

    public void OnFire()
    {
        if (!animator.GetBool("Death"))// && Game.Instance.State == Game.eState.Game)
        {
            weapon.Fire(transform.forward);
        }
    }

    public void OnJump()
    {
        // Jump
        if (onGround && !animator.GetBool("Death"))// && Game.Instance.State == Game.eState.Game)
        {
            velocity.y += jump;
        }
    }

    public void OnMove(InputValue input)
    {
        if (!animator.GetBool("Death"))// && Game.Instance.State == Game.eState.Game)
        {
            Vector2 v2 = input.Get<Vector2>();
            inputDirection = Vector3.zero;
            inputDirection.x = v2.x;
            inputDirection.z = v2.y;
        }
    }

    public void OnPunch()
    {
        if (!animator.GetBool("Death"))// && Game.Instance.State == Game.eState.Game)
        {
            animator.SetTrigger("Punch");
        }
    }

    public void OnThrow()
    {
        if (!animator.GetBool("Death"))// && Game.Instance.State == Game.eState.Game)
        {
            animator.SetTrigger("Throw");
        }
    }
}
