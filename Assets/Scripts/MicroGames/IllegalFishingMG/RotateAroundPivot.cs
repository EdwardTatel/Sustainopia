using UnityEngine;

public class RotateAroundPivot : MonoBehaviour
{
    public Transform pivot; // The pivot point around which the lever will rotate
    public float rotationSpeed = 10f; // Speed at which the lever rotates
    public float minRotationAngle = -45f; // Minimum rotation angle in degrees
    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees

    private Vector3 lastMousePosition;
    private float currentRotationAngle = 0f; // Current rotation angle

    private void Start()
    {
        Cursor.visible = false;
        // Initialize lastMousePosition to the current mouse position
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        // Get the current mouse position
        Vector3 currentMousePosition = Input.mousePosition;

        // Calculate the change in mouse position
        Vector3 mouseDelta = currentMousePosition - lastMousePosition;

        // Rotate the lever based on mouse movement
        RotateLever(mouseDelta);

        // Update the last mouse position
        lastMousePosition = currentMousePosition;
    }

    private void RotateLever(Vector3 mouseDelta)
    {
        // Convert mouse delta to rotation amount
        float rotationAmount = mouseDelta.x * rotationSpeed * Time.deltaTime;

        // Calculate the new rotation angle and clamp it
        currentRotationAngle = Mathf.Clamp(currentRotationAngle + rotationAmount, minRotationAngle, maxRotationAngle);

        // Apply the clamped rotation around the pivot
        transform.RotateAround(pivot.position, Vector3.forward, currentRotationAngle - transform.eulerAngles.z);
    }
}
