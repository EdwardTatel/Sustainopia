using UnityEngine;

public class MouseFollowX : MonoBehaviour
{
    public float speed = 10f; // Adjust the speed of movement
    public float xOffset = 0f; // Offset the position if needed

    void Update()
    {
        MoveObjectWithMouse();
    }

    void MoveObjectWithMouse()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world position on the x-axis
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));

        // Only update the x position of the object and add the offset if necessary
        transform.position = new Vector3(worldPosition.x + xOffset, transform.position.y, transform.position.z);
    }
}