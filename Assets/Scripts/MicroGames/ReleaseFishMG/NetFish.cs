using System.Collections;
using UnityEngine;

public class NetFish : MonoBehaviour
{
    private Vector3 offset;
    private float jumpForce = 23f;
    private Rigidbody rb;
    public GameObject splash;

    public float objectHeight = 4f; // The fixed height you want the object to follow

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ocean")
        {
            Instantiate(splash, transform.position, Quaternion.Euler(90, 0, 0));
            StartCoroutine(DestroyObject());
        }
        else if (collision.contacts[0].normal.y > 0.5f)
        {
            Vector3 jumpDirection = Vector3.up + collision.contacts[0].normal;
            if (rb != null) rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnMouseDown()
    {
        // Get the initial offset in world space
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        rb.velocity = Vector3.zero; // Stop movement
        rb.angularVelocity = Vector3.zero;

        // Get the current mouse world position and apply the offset
        Vector3 newPosition = GetMouseWorldPosition() + offset;

        // Keep the object at the fixed height (Y position)
        newPosition.y = objectHeight;

        // Update the object's position
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Define a plane at a fixed Y position (objectHeight) to project the ray onto
        Plane plane = new Plane(Vector3.up, new Vector3(0, objectHeight, 0));

        // Perform the raycast to the plane
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            // Get the point where the ray hits the plane
            return ray.GetPoint(distance);
        }

        return Vector3.zero; // Fallback, should not happen unless the ray misses
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(this.gameObject);
    }
}