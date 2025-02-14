using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvrPlayerSimulatorController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 2;
    private Vector3 camRotation;
    public Camera PlayerCamera;

    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;

    [Range(3, 20)]
    public int runSpeed = 5;

    void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {        
        if (Input.GetKey(KeyCode.Space))
        {

            transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

            camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

            PlayerCamera.transform.localEulerAngles = camRotation;
        }
    }

    private void Move()
    {
        // Get input from WASD or arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down arrows

        // Create a movement vector
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        var currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = runSpeed;
        }

            // Move the character
            characterController.Move(move * currentSpeed * Time.deltaTime);
    }
}
