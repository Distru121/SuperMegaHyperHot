using System.Collections;
using System.Collections.Generic;
using Unity.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController characterController;
    public Transform mainCamera;
    public Animator animator;

    public float walkSpeed = 6.0f;
    public float runSpeed = 12.0f;
    public float mouseSensitivity = 60.0f;
    public float xRotation = 0;


    private void Update()
    {
        MovePlayer();
        CameraMotion();
        //Interact();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 move = mainCamera.right * moveX + mainCamera.forward * moveZ;
        move.y = 0;

        float velocityMagnitude = move.magnitude;
        animator.SetFloat("Speed", velocityMagnitude);
        animator.SetBool("isRunning", isRunning);

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



    //the components storing logic
    InteractableComponent[] storedComponents = null;
    public TMP_Text uiList;

    void Interact()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            storedComponents = TryGettingAllComponentsAtMousePos();
            string uiListOfComponents = "";

            if (storedComponents != null)
            {
                foreach (InteractableComponent component in storedComponents)
                {
                    print(component.name);
                    uiListOfComponents += component.name + "\n";
                }
            }
            else
                print("No component selected.");

            uiList.text = uiListOfComponents;
        }
    }
    InteractableComponent[] TryGettingAllComponentsAtMousePos()
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (Input.GetMouseButtonDown(0))
                print(hit.collider.name);
        }

        //if u already have a stored component and clicking something that has not, it gains that component
        if(hit.collider.gameObject.GetComponent<InteractableComponent>() == null && storedComponents != null)
        {
            foreach(InteractableComponent component in storedComponents)
            {
                component.CloneTo(hit.collider.gameObject);
                Destroy(component);
            }
            return null;
        }

        return hit.collider.gameObject.GetComponents<InteractableComponent>();
    }
}
