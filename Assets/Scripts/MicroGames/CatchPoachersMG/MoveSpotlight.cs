using UnityEngine;

public class MoveSpotlight : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera (parent)
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the mouse position in world space and convert it to the camera's local space
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 localMousePosition = mainCamera.transform.InverseTransformPoint(mousePosition);

        // Update the object's local position based on the mouse's local position
        transform.localPosition = new Vector3(localMousePosition.x, localMousePosition.y, transform.localPosition.z);
    }

    // Helper function to convert the mouse position to world space
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); // Set Z to distance from camera

        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }
}
