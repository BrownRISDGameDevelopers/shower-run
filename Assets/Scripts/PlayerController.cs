using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1f;

    public Transform playerBody;

    float xRotation = 0f;

    Vector2 mousemovement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mousemovement = Mouse.current.delta.ReadValue();
        
        float mouseX = mousemovement.x * mouseSensitivity; //* Time.deltaTime;
        float mouseY = mousemovement.y * mouseSensitivity; //* Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}