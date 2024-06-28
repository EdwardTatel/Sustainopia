using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaterial : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(curPosition.x, curPosition.y, 171.9f);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPoint.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
