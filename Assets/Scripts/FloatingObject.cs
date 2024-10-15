using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    // Parameters to control the range and speed of the floating movement
    private float minHeight = 0f;  // Minimum height for the object to move
    private float maxHeight = 3f;  // Maximum height for the object to move
    private float speed = .1f;      // Speed of the floating motion

    // Internal variables for the motion
    private float randomOffset;     // Random offset to make the movement unique
    private float baseHeight;       // The initial height of the object

    void Start()
    {
        // Store the initial height of the object
        baseHeight = transform.position.y;

        // Generate a random offset for the motion so different objects can move independently
        randomOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Calculate the new height using sine wave for smooth motion
        float newY = baseHeight + Mathf.Lerp(minHeight, maxHeight, Mathf.PerlinNoise(Time.time * speed, randomOffset));

        // Update the object's position, keeping X and Z unchanged
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
