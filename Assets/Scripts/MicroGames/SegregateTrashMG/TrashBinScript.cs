using System.Collections;
using UnityEngine;

public class TrashBinScript : MonoBehaviour
{
    public Camera mainCamera;

    public Vector3 cameraOffset = new Vector3(0f, 0f, 0f); // Editable offset for the linecast endpoint
    public Vector3 originOffset = new Vector3(0f, 0f, 0f);  // Offset for the linecast origin (the object)

    public GameObject trashLid;
    // Rotate the lid to open (e.g., 90 degrees) while dropping the trash


    void Update()
    {
        // Draw the line between the object and the camera in the Scene view continuously
        Vector3 objectPositionWithOffset = transform.position + originOffset;
        Vector3 cameraPositionWithOffset = mainCamera.transform.position + cameraOffset;
        Debug.DrawLine(objectPositionWithOffset, cameraPositionWithOffset, Color.red);

        if (Input.GetMouseButtonUp(0))
        {
            PerformBoxcast();
        }
    }

    void PerformBoxcast()
    {
        Vector3 objectPositionWithOffset = transform.position + originOffset;
        Vector3 cameraPositionWithOffset = mainCamera.transform.position + cameraOffset;
        Vector3 direction = (cameraPositionWithOffset - objectPositionWithOffset).normalized;

        float distance = Vector3.Distance(objectPositionWithOffset, cameraPositionWithOffset);
        Vector3 boxHalfExtents = new Vector3(0.5f, 0.5f, 0.5f);

        RaycastHit hit;
        if (Physics.BoxCast(objectPositionWithOffset, boxHalfExtents, direction, out hit, Quaternion.identity, distance))
        {

            hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            RotateLid(trashLid, -44.305f, 0.3f);
            LeanTween.move(hit.transform.gameObject, new Vector3(transform.position.x, 1.747f, transform.position.z), .7f)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() => dropTrash(hit.transform.gameObject, trashLid));

            // Visualize the BoxCast hit position and direction in Scene view
            Vector3 hitPoint = objectPositionWithOffset + direction * hit.distance;
            Debug.DrawRay(hitPoint, Vector3.up * boxHalfExtents.y, Color.green);
            Debug.DrawRay(hitPoint, Vector3.down * boxHalfExtents.y, Color.green);
            Debug.DrawRay(hitPoint, Vector3.left * boxHalfExtents.x, Color.green);
            Debug.DrawRay(hitPoint, Vector3.right * boxHalfExtents.x, Color.green);
            Debug.DrawRay(hitPoint, Vector3.forward * boxHalfExtents.z, Color.green);
            Debug.DrawRay(hitPoint, Vector3.back * boxHalfExtents.z, Color.green);
        }
    }





    private void dropTrash(GameObject trash, GameObject trashLid)
    {
        // Complete the trash drop to its final position
        LeanTween.move(trash.gameObject, new Vector3(trash.transform.position.x, 0.5f, trash.transform.position.z), 1f)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() =>
            {
                // Close the lid after the trash is dropped
                RotateLid(trashLid, 0f, 0.7f);
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

    public void RotateLid(GameObject trashLid, float targetYRotation, float duration)
    {
        // Rotate around the Y-axis
        LeanTween.rotateLocal(trashLid, new Vector3(0f, 0f, targetYRotation), duration).setEase(LeanTweenType.easeInOutSine);
    }

}   