using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    public float boostMultiplier = 1.5f;  // Factor to increase speed when Shift is held

    // References to the wheel Transforms
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    private void Start()
    {
        if (GameVariables.city1Finished || GameVariables.city2Finished || GameVariables.city3Finished)
        {
            NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            GameVariables.Load(transform);
            if (agent != null)
            {
                agent.enabled = true;
            }
        }
    }

    void Update()
    {
        if (!GameVariables.stopControls)
        {
            // Determine the effective speed based on Shift key input
            float currentSpeed = moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed *= boostMultiplier;
            }

            // Capture input
            float moveX = 0f;
            float moveZ = 0f;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveZ = 1f;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveZ = -1f;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -1f;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveX = 1f;
            }

            // Get the camera's forward and right direction, ignoring the Y axis (flattened)
            Vector3 forward = cameraTransform.forward;
            forward.y = 0f;
            forward.Normalize();

            Vector3 right = cameraTransform.right;
            right.y = 0f;
            right.Normalize();

            // Create a movement vector relative to the camera's orientation
            Vector3 move = (forward * moveZ + right * moveX).normalized * currentSpeed * Time.deltaTime;

            // Apply the movement to the car
            transform.Translate(move, Space.World);

            // Rotate the car towards the movement direction if there is any input
            if (move != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                // Rotate the wheels based on moveSpeed
                float wheelRotationSpeed = currentSpeed * 10 * Time.deltaTime; // Adjust rotation speed if necessary
                RotateWheels(wheelRotationSpeed);
            }
        }
    }

    // Rotates the wheels around the X axis for a forward/backward motion effect
    private void RotateWheels(float rotationAmount)
    {
        frontLeftWheel.Rotate(0, 0, rotationAmount);
        frontRightWheel.Rotate(0, 0, rotationAmount);
        rearLeftWheel.Rotate(0, 0, rotationAmount);
        rearRightWheel.Rotate(0, 0, rotationAmount);
    }
}
