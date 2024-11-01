using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform; // Assign the main camera in the Inspector
    private void Start()
    {
        if (GameVariables.city1Finished || GameVariables.city2Finished || GameVariables.city3Finished)
        {
            GameVariables.Load(transform);
        }
            GameVariables.EnableAllTexts();
    }
    void Update()
    {
        if(!GameVariables.stopControls){ 
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
        Vector3 move = (forward * moveZ + right * moveX).normalized * moveSpeed * Time.deltaTime;

        // Apply the movement to the car
        transform.Translate(move, Space.World);

        // Rotate the car towards the movement direction if there is any input
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    }
}