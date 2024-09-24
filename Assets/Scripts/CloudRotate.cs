using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotate : MonoBehaviour
{
    public Transform targetPoint;       // The point (e.g., mountain) the object floats around
    public float radius = 5f;           // The radius of the orbit
    public float speed = 1f;            // The speed of floating
    public Vector3 axis = Vector3.up;   // The axis around which the object orbits (default Y axis for horizontal movement)
    public float startAngle = 0f;       // The initial angle for this cloud
    public float yOffset = 2f;          // Offset for the Y axis (height)
    public bool clockwise = true;       // Controls the direction of rotation (clockwise/counterclockwise)

    private float angle = 0f;           // Angle used for calculating position

    void Start()
    {
        // Initialize the angle to the startAngle value
        angle = startAngle;
    }

    void Update()
    {
        // Update the angle based on speed and direction
        float directionMultiplier = clockwise ? 1f : -1f;
        angle += speed * directionMultiplier * Time.deltaTime;

        // Calculate the new position of the object around the target point
        Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius;
        Vector3 newPosition = targetPoint.position + offset;

        // Apply Y axis offset to allow vertical movement
        newPosition.y += yOffset;

        // Set the object's position
        transform.position = newPosition;

        // Rotate the object to face the target point, but keep X rotation fixed at -90
        Vector3 direction = targetPoint.position - transform.position;
        direction.y = 0;  // Keep the rotation horizontal

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(-90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
    }
}