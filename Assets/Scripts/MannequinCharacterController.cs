using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinCharacterController : MonoBehaviour
{
    public bool useCharacterForward = false;
    public float turnSpeed = 10f;

    public KeyCode sprintKeyboard = KeyCode.LeftShift;
    public KeyCode sprintJoystick = KeyCode.JoystickButton2;

    float turnSpeedMultiplier;
    float speed = 0f;
    float direction = 0f;
    bool isSprinting = false;
    Animator anim;
    Vector3 targetDirection;
    Vector2 input;
    Quaternion freeRotation;
    Camera mainCamera;
    float velocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        speed = useCharacterForward ? Mathf.Abs(input.x) + input.y : Mathf.Abs(input.x) + Mathf.Abs(input.y);
        speed = Mathf.Clamp(speed, 0f, 1f);
        speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);

        anim.SetFloat("Speed", speed);

        direction = (input.y < 0f && useCharacterForward) ? input.y : 0f;
        anim.SetFloat("Direction", direction);

        isSprinting = (Input.GetKey(sprintJoystick) || Input.GetKey(sprintKeyboard) && input != Vector2.zero && direction >= 0f);
        anim.SetBool("IsSprinting", isSprinting);
    }
}
