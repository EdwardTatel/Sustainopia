using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Reference to the main camera
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;

        // Make the object look at the camera while keeping the X rotation at 0
        LookAtTheCamera();
    }

    void LookAtTheCamera()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.x = 0f;
        rotation.y = 180f;
        // Apply the adjusted rotation back to the object
        transform.eulerAngles = rotation;
    }
}