using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpotlightCheck : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    private Coroutine destructionCoroutine; // To store the destruction coroutine
    public Sprite caughtSprite;
    public GameObject cloud;
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
                destructionCoroutine = StartCoroutine(ChangeSpriteAfterTime());

            }
        }
    }

    // Coroutine to destroy the object after the specified time
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(.5f); // Wait for the specified time
        GameObject Cloud = Instantiate(cloud, transform.position, Quaternion.identity);

        Cloud.transform.Find("Text").GetComponent<TextMeshPro>().text = "CAUGHT";
        gameObject.SetActive(false);

    }

    IEnumerator ChangeSpriteAfterTime()
    {
        yield return new WaitForSeconds(1f);

        gameObject.GetComponentInChildren<SpriteRenderer>().sprite = caughtSprite;
        StartCoroutine(DestroyAfterTime());
        // Destroy the object
    }
}