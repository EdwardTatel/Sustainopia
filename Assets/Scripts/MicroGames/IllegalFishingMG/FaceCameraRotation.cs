using DG.Tweening;
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
    private void LateUpdate()
    {
        // Make the object face the camera
        transform.LookAt(mainCamera.transform);

        // Optionally, flip the object to face the camera correctly
        // because LookAt might make the object rotate backwards.
        transform.Rotate(0, 180, 0); // Rotate 180 degrees on the Y-axis
    }
}
