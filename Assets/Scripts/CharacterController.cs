using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController characterController;
    public Transform mainCamera;
    public Animator animator;

    public float walkSpeed = 6.0f;
    public float runSpeed = 12.0f;
    public float mouseSensitivity = 100.0f;
    public float xRotation = 0;

    private void Update()
    {
        MovePlayer();
        CameraMotion();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? walkSpeed : runSpeed;
        Vector3 move = mainCamera.right * moveX + mainCamera.forward * moveZ;
        move.y = 0;

        float velocityMagnitude = move.magnitude;
        animator.SetFloat("Speed", velocityMagnitude);

        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    void CameraMotion()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
