using UnityEngine;

public class TrashScript : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    private Animation animationComponent;

    // Set the minimum Y level to prevent clipping (you can adjust this value as needed)
    public float minY = 0.4253356f;

    void Start()
    {
        // Get the Animator component
        animationComponent = GetComponent<Animation>();
    }

    private void OnMouseDown()
    {
        // Record the object's Z coordinate for the orthographic camera
        zCoord = transform.position.z;

        // Calculate the offset between the object's position and the mouse position
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        // Update the object's position based on the mouse position and the offset
        Vector3 newPosition = GetMouseWorldPosition() + offset;

        // Ensure the Y position doesn't go below the specified minimum Y level
        float clampedY = Mathf.Max(newPosition.y, minY);

        // Apply the clamped position (restricted to X and clamped Y only, Z remains unchanged)
        transform.position = new Vector3(newPosition.x, clampedY, transform.position.z);
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert the mouse position to a world position with the orthographic camera
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}