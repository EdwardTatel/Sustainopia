using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraRotation : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {

        mainCamera = GameObject.Find("GameCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = FaceCameraRotations();
    }
    private Quaternion FaceCameraRotations()
    {
        // Get the direction from the object to the camera
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;

        // Adjust the rotation to face the camera
        Quaternion rotation = Quaternion.LookRotation(directionToCamera);

        // Set the Y-axis rotation to 0
        rotation = Quaternion.Euler(rotation.eulerAngles.x, 0, rotation.eulerAngles.z);

        return rotation;
    }
}
