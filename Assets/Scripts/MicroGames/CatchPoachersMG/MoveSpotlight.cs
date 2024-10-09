using UnityEngine;

public class MoveSpotlight : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the mouse position in screen space and clamp it to stay on the screen
        Vector3 clampedScreenPosition = GetClampedMouseScreenPosition();

        // Convert the clamped screen position to world space
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(clampedScreenPosition);

        // Set the object's position based on the clamped world position
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }

    // Helper function to get and clamp the mouse position in screen space
    private Vector3 GetClampedMouseScreenPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Clamp the mouse's X and Y position to stay within the screen boundaries
        mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0, Screen.width);
        mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0, Screen.height);

        // Set Z to the distance from the camera to the object
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        return mouseScreenPosition;
    }
}
