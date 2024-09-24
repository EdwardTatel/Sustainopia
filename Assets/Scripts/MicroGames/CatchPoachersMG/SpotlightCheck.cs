using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCheck : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    private Coroutine destructionCoroutine; // To store the destruction coroutine

    private void Start()
    {
        mainCamera = GameObject.Find("GameCamera").GetComponent<Camera>();
    }
    void Update()
    {
        // Perform the linecast from the object to the camera
        PerformLinecast();

        // Always draw the line from the object to the camera to visualize the linecast
        Vector3 objectPosition = transform.position;
        Vector3 cameraPosition = mainCamera.transform.position;

        Debug.DrawLine(objectPosition, cameraPosition, Color.red); // Visualize the line in Scene View
    }

    void PerformLinecast()
    {
        Vector3 objectPosition = transform.position;
        Vector3 cameraPosition = mainCamera.transform.position;

        // Perform a linecast from the object to the camera
        RaycastHit hit;
        if (Physics.Linecast(objectPosition, cameraPosition, out hit))
        {
            // Log the hit information
            Debug.Log("Hit object: " + hit.transform.name);
            Debug.Log("Hit point: " + hit.point);
            Debug.Log("Hit distance: " + hit.distance);

            // If a hit is detected, cancel the destruction coroutine if it's running
            if (destructionCoroutine != null)
            {
                StopCoroutine(destructionCoroutine);
                destructionCoroutine = null;
            }
        }
        else
        {
            // If nothing is hit, start the destruction coroutine if it's not already running
            if (destructionCoroutine == null)
            {
                destructionCoroutine = StartCoroutine(DestroyAfterTime(4f));
            }
        }
    }

    // Coroutine to destroy the object after the specified time
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Wait for the specified time
        Debug.Log("No hit detected for " + time + " seconds. Destroying object.");
        Destroy(gameObject); // Destroy the object
    }
}