using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class SnapPiece : MonoBehaviour
{
    private SolarPanelScript solarPanelScript;
    private CheckPlace childObjectScript;
    public GameObject initialPosition;
    private Vector3 stayPosition;

    private void Start()
    {
        stayPosition = transform.position;
        childObjectScript = transform.Find("SolarPanel").GetComponent<CheckPlace>();
    }

    public bool IsPlaceable()
    {
        
        foreach (Transform obj in transform)
        {
            
            if (!obj.GetComponent<CheckPlace>().placeable) {
                return false;
            }
        }
        return true;
    }

    public void SnapToPlace()
    {
        if (IsPlaceable())
        {
            transform.position = childObjectScript.collidedObject.transform.position;
            foreach(Transform obj in transform)
            {
                obj.GetComponent<CheckPlace>().collidedObject.GetComponent<PanelChecker>().enablePlace = false;
            }
        }
        else
        {
            LeanTween.move(gameObject, stayPosition, 0.5f);
        }
    }
}
    