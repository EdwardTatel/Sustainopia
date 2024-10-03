using System.Collections;
using UnityEngine;

public class TrashBinScript : MonoBehaviour
{
    public Camera mainCamera;

    public Vector3 cameraOffset = new Vector3(0f, 0f, 0f); // Editable offset for the linecast endpoint
    public Vector3 originOffset = new Vector3(0f, 0f, 0f);  // Offset for the linecast origin (the object)

    private float constantZ = -7f; // Set a constant Z value if required for position adjustments
    public GameObject trashLid;
    // Rotate the lid to open (e.g., 90 degrees) while dropping the trash
    float targetOpenRotation = -170f; // Desired open rotation angle
    float targetCloseRotation = -83.121f; // Desired close rotation angle


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PerformLinecast();
        }

        // Always draw the line from the object (with origin offset) to the camera (with offset) to visualize the linecast
        Vector3 objectPositionWithOffset = transform.position + originOffset;  // Apply the origin offset to the object position
        Vector3 cameraPositionWithOffset = mainCamera.transform.position + cameraOffset;  // Apply the offset to the camera position

        Debug.DrawLine(objectPositionWithOffset, cameraPositionWithOffset, Color.red);  // Visualize the line in Scene View
    }

    void PerformLinecast()
    {
        Vector3 objectPositionWithOffset = transform.position + originOffset;  // Apply the origin offset to the object position
        Vector3 cameraPositionWithOffset = mainCamera.transform.position + cameraOffset;  // Apply the offset to the camera position

        // Perform a linecast from the object (with origin offset) to the camera (with offset)
        RaycastHit hit;
        if (Physics.Linecast(objectPositionWithOffset, cameraPositionWithOffset, out hit))
        {
            Debug.Log(hit);
            StartCoroutine(RotateLid(trashLid, -190f, 0.5f));
            // Move the object to the hit trash can
            LeanTween.move(hit.transform.gameObject, new Vector3(transform.position.x, 1.207f, transform.position.z), .5f)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() => dropTrash(hit.transform.gameObject, trashLid));
        }
    }

    private void dropTrash(GameObject trash, GameObject trashLid)
    {

        // Complete the trash drop to its final position
        LeanTween.moveLocal(trash.gameObject, new Vector3(trash.transform.position.x, 0.5f, trash.transform.position.z), .5f)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() =>
            {
                // Close the lid after the trash is dropped
                StartCoroutine(RotateLid(trashLid, -83.121f, 0.5f));
                if((trash.tag != gameObject.tag))
                {
                    MicroGameVariables.deductTries();
                }
                deleteThis(trash);
            });
    }

    private void deleteThis(GameObject trash)
    {
        Destroy(trash);
    }

    private IEnumerator RotateLid(GameObject trashLid, float targetXRotation, float duration)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = trashLid.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetXRotation, 0f , 0f); // Only rotating around the Y axis

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            // Smoothly interpolate the rotation
            trashLid.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        // Ensure the final rotation is set
        trashLid.transform.rotation = targetRotation;
    }

}