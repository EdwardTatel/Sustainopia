using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinScript : MonoBehaviour
{
    public Camera mainCamera;

    public Vector3 cameraOffset = new Vector3(0f, 0f, 0f); // Editable offset for the linecast endpoint
    private float constantZ = -7f; // Set a constant Z value if required for position adjustments

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PerformLinecast();
        }

        // Always draw the line from the object to the camera (with offset) to visualize the linecast
        Vector3 objectPosition = transform.position;
        Vector3 cameraPositionWithOffset = mainCamera.transform.position + cameraOffset; // Apply the offset to the camera position

        Debug.DrawLine(objectPosition, cameraPositionWithOffset, Color.red); // Visualize the line in Scene View
    }

    void PerformLinecast()
    {
        Vector3 objectPosition = transform.position;
        Vector3 cameraPositionWithOffset = mainCamera.transform.position + cameraOffset; // Apply the offset to the camera position

        // Perform a linecast from the object to the camera with the offset
        RaycastHit hit;
        if (Physics.Linecast(objectPosition, cameraPositionWithOffset, out hit))
        {
            Debug.Log(hit);

            // Move the object to the hit trash can
            LeanTween.move(hit.transform.gameObject, new Vector3(transform.position.x, 1.207f, transform.position.z), .5f)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() => dropTrash(hit.transform.gameObject));
        }
    }

    private void dropTrash(GameObject trash)
    {
        // Complete the trash drop to its final position
        LeanTween.moveLocal(trash.gameObject, new Vector3(trash.transform.position.x, 0.5f, trash.transform.position.z), .5f)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() => deleteThis(trash));
    }

    private void deleteThis(GameObject trash)
    {
        Destroy(trash);
    }
}
