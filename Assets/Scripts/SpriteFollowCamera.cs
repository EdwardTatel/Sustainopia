using UnityEngine;

public class SpriteFollowCamera : MonoBehaviour
{
    void LateUpdate()
    {
        // Get the main camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;

        // Set the sprite's rotation to face the camera while keeping it parallel to the camera's view
        transform.rotation = Quaternion.LookRotation(cameraForward, Vector3.up);
    }
}
