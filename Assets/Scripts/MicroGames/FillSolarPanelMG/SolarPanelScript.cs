using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelScript : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    private SnapPiece snapPieceScript;
    private CheckPlace checkPlaceScript;

    void Start()
    {
        mainCamera = Camera.main;
        snapPieceScript = transform.parent.GetComponent<SnapPiece>();
    }

    void OnMouseDown()
    {
        Cursor.visible = false;
        // Calculate the offset between the parent's position and the mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.transform.position.y - transform.parent.position.y;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        offset = transform.parent.position - new Vector3(mousePosition.x, transform.parent.position.y, mousePosition.z);
    }
        
    void OnMouseDrag()
    {
        // Update the parent's position based on the mouse position and the offset
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.transform.position.y - transform.parent.position.y;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.parent.position = new Vector3(mousePosition.x + offset.x, transform.parent.position.y, mousePosition.z + offset.z);
    }
    private void OnMouseUp()
    {
        snapPieceScript.SnapToPlace();
        Cursor.visible = true;
    }
}       
