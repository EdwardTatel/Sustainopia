using UnityEngine;

public class RotateAroundPivot : MonoBehaviour
{
    public Transform pivot; // The pivot point around which the lever will rotate
    private float rotationSpeed = 30f; // Speed at which the lever rotates
    public float minRotationAngle = -45f; // Minimum rotation angle in degrees
    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees

    private float currentRotationAngle = 0f; // Current rotation angle

    private void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SimulateMouseClick();
    }

    private void Update()
    {
        // Get the mouse delta along the Y axis (up-down movement), and invert it
        float mouseDeltaY = -Input.GetAxis("Mouse Y");

        // Rotate the lever based on mouse movement
        RotateLever(mouseDeltaY);
    }
    public void OnMouseClick()
    {
        Debug.Log("Mouse click simulated!");
        // Put your click-related code here
    }

    // Simulate a mouse click
    public void SimulateMouseClick()
    {
        OnMouseClick();
    }
    private void RotateLever(float mouseDeltaY)
    {
        // Convert mouse delta to rotation amount
        float rotationAmount = mouseDeltaY * rotationSpeed * Time.deltaTime;

        // Calculate the new rotation angle and clamp it
        currentRotationAngle = Mathf.Clamp(currentRotationAngle + rotationAmount, minRotationAngle, maxRotationAngle);

        // Apply the clamped rotation around the pivot
        transform.RotateAround(pivot.position, Vector3.forward, currentRotationAngle - transform.eulerAngles.z);
    }

    private void OnDisable()
    {
        // Unlock the cursor and make it visible again when script is disabled
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
