using DentedPixel.LTExamples;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    private Animation animationComponent;

    void Start()
    {
        // Get the Animator component
        animationComponent = GetComponent<Animation>();

        // Set the initial position to ensure the object starts at the correct Z position
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
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z); // Restrict movement to X and Y only
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Convert the mouse position to a world position with the orthographic camera
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    
}
